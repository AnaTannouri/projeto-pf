using ProjetoPF.Modelos.Compra;
using ProjetoPF.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ProjetoPF.Dao
{
    public class BaseDao<T> where T : ProjetoPF.Model.BaseModelos, new()
    {
        protected readonly string _connectionString = "data source=ANA\\SQLEXPRESS;initial catalog=projeto-pf;user id=sa;password=123";
        protected readonly string _tabela;

        public BaseDao(string tabela)
        {
            _tabela = tabela;
        }

        private SqlConnection CriarConexao()
        {
            return new SqlConnection(_connectionString);
        }

        public bool VerificarDuplicidade(string campo, string valor, T entidade)
        {
            using (SqlConnection conn = CriarConexao())
            {
                conn.Open();
                string query = $"SELECT COUNT(1) FROM {_tabela} WHERE LTRIM(RTRIM({campo})) = LTRIM(RTRIM(@Valor))";
                var idProp = typeof(T).GetProperty("Id");
                if (idProp != null && (int)idProp.GetValue(entidade) != 0)
                {
                    query += " AND Id != @Id";
                }
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Valor", valor);
                if (idProp != null && (int)idProp.GetValue(entidade) != 0)
                {
                    cmd.Parameters.AddWithValue("@Id", idProp.GetValue(entidade));
                }
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public void Criar(T entidade)
        {
            using (var conn = CriarConexao())
            {
                conn.Open();
                var propriedades = typeof(T).GetProperties().Where(p => p.Name != "Id").ToList();
                var colunas = string.Join(", ", propriedades.Select(p => p.Name));
                var valores = string.Join(", ", propriedades.Select(p => $"@{p.Name}"));

                var query = $@"
            INSERT INTO {_tabela} ({colunas}) 
            VALUES ({valores});
            SELECT CAST(SCOPE_IDENTITY() AS INT);";

                using (var cmd = new SqlCommand(query, conn))
                {
                    foreach (var prop in propriedades)
                    {
                        var valor = prop.GetValue(entidade) ?? DBNull.Value;
                        cmd.Parameters.AddWithValue($"@{prop.Name}", valor);
                    }

                    var id = (int)cmd.ExecuteScalar();

                    var propId = typeof(T).GetProperty("Id");
                    if (propId != null && propId.CanWrite)
                    {
                        propId.SetValue(entidade, id);
                    }
                }
            }
        }
        public List<T> BuscarTodos(string filtro = null)
        {
            List<T> lista = new List<T>();

            using (SqlConnection conn = CriarConexao())
            {
                conn.Open();
                string query = $"SELECT * FROM {_tabela}";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;

                if (!string.IsNullOrEmpty(filtro))
                {
                    List<string> whereConditions = new List<string>();

                    if (int.TryParse(filtro, out int id))
                    {
                        whereConditions.Add("Id = @Id");
                        cmd.Parameters.AddWithValue("@Id", id);
                    }

                    int count = 0;

                    var propsTexto = typeof(T).GetProperties()
                        .Where(p => p.PropertyType == typeof(string));
                    foreach (var prop in propsTexto)
                    {
                        string paramName = $"@p{count}";
                        whereConditions.Add($"{prop.Name} LIKE {paramName}");
                        cmd.Parameters.AddWithValue(paramName, $"%{filtro}%");
                        count++;
                    }

                    if (decimal.TryParse(filtro.Replace(".", ","), out decimal valorDecimal))
                    {
                        var propsDecimal = typeof(T).GetProperties()
                            .Where(p => p.PropertyType == typeof(decimal) || p.PropertyType == typeof(decimal?));

                        foreach (var prop in propsDecimal)
                        {
                            string paramName = $"@d{count}";
                            whereConditions.Add($"{prop.Name} = {paramName}");
                            cmd.Parameters.AddWithValue(paramName, valorDecimal);
                            count++;
                        }
                    }

                    if (whereConditions.Count > 0)
                        query += " WHERE " + string.Join(" OR ", whereConditions);
                }

                cmd.CommandText = query;

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    T obj = new T();
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        if (!reader.HasColumn(prop.Name)) continue;
                        if (reader[prop.Name] != DBNull.Value)
                        {
                            var value = reader[prop.Name];
                            if (Nullable.GetUnderlyingType(prop.PropertyType) != null)
                                prop.SetValue(obj, Convert.ChangeType(value, Nullable.GetUnderlyingType(prop.PropertyType)));
                            else
                                prop.SetValue(obj, Convert.ChangeType(value, prop.PropertyType));
                        }
                    }
                    lista.Add(obj);
                }
            }

            return lista;
        }

        public T BuscarPorId(int id)
        {
            using (SqlConnection conn = CriarConexao())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT * FROM {_tabela} WHERE Id = @Id", conn);
                cmd.Parameters.AddWithValue("@Id", id);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    T obj = new T();
                    foreach (var prop in typeof(T).GetProperties())
                    {
                        if (!reader[prop.Name].Equals(DBNull.Value))
                            prop.SetValue(obj, reader[prop.Name]);
                    }
                    return obj;
                }
            }
            return null;
        }

        public void Remover(int id)
        {
            using (var connection = CriarConexao())
            {
                connection.Open();
                string query = $"DELETE FROM {_tabela} WHERE Id = @Id";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", id);
                cmd.ExecuteNonQuery();
            }
        }

        public void Atualizar(T entidade)
        {
            using (var conn = CriarConexao())
            {
                conn.Open();
                var propriedades = typeof(T).GetProperties().Where(p => p.Name != "Id").ToList();
                var setClause = string.Join(", ", propriedades.Select(p => $"{p.Name} = @{p.Name}"));
                var query = $"UPDATE {_tabela} SET {setClause} WHERE Id = @Id";
                using (var cmd = new SqlCommand(query, conn))
                {
                    foreach (var prop in propriedades)
                    {
                        var valor = prop.GetValue(entidade) ?? DBNull.Value;
                        cmd.Parameters.AddWithValue($"@{prop.Name}", valor);
                    }
                    cmd.Parameters.AddWithValue("@Id", entidade.Id);
                    try
                    {
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("Nenhum registro foi atualizado. Verifique se o ID existe.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro ao atualizar o registro: {ex.Message}");
                        throw;
                    }
                }
            }
        }

        public List<T> BuscarTodosWithoutId<T>(string filtro = null, string orderBy = null) where T : new()
        {
            var lista = new List<T>();
            using (var conn = CriarConexao())
            {
                conn.Open();

                // Base SELECT
                var query = $"SELECT * FROM [{_tabela}]";
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    var whereConditions = new List<string>();
                    int paramIndex = 0;

                    var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    var propsByName = props.ToDictionary(p => p.Name, p => p, StringComparer.OrdinalIgnoreCase);

                    // ===== 1) Filtro modo key=value (exato, ideal p/ PK composta) =====
                    if (!string.IsNullOrWhiteSpace(filtro) && filtro.Contains('='))
                    {
                        var pairs = filtro.Split(new[] { ';', ',' }, StringSplitOptions.RemoveEmptyEntries);
                        foreach (var raw in pairs)
                        {
                            var idx = raw.IndexOf('=');
                            if (idx <= 0) continue;

                            var key = raw.Substring(0, idx).Trim();
                            var valStr = raw.Substring(idx + 1).Trim();

                            if (!propsByName.TryGetValue(key, out var prop))
                                continue; // ignora chave inexistente na entidade

                            var paramName = "@k" + paramIndex++;
                            var typedValue = ConvertTo(valStr, prop.PropertyType);
                            cmd.Parameters.AddWithValue(paramName, typedValue ?? DBNull.Value);
                            whereConditions.Add($"[{prop.Name}] = {paramName}");
                        }
                    }
                    // ===== 2) Filtro amplo (sem '=') =====
                    else if (!string.IsNullOrWhiteSpace(filtro))
                    {
                        // Strings -> LIKE
                        var propsTexto = props.Where(p => p.PropertyType == typeof(string));
                        foreach (var prop in propsTexto)
                        {
                            var paramName = "@p" + paramIndex++;
                            whereConditions.Add($"[{prop.Name}] LIKE {paramName}");
                            cmd.Parameters.AddWithValue(paramName, $"%{filtro}%");
                        }

                        // Integrais -> igualdade se conseguir converter
                        if (long.TryParse(filtro, out var inteiro))
                        {
                            var propsInteiros = props.Where(p =>
                                p.PropertyType == typeof(byte) || p.PropertyType == typeof(byte?) ||
                                p.PropertyType == typeof(short) || p.PropertyType == typeof(short?) ||
                                p.PropertyType == typeof(int) || p.PropertyType == typeof(int?) ||
                                p.PropertyType == typeof(long) || p.PropertyType == typeof(long?));
                            foreach (var prop in propsInteiros)
                            {
                                var paramName = "@i" + paramIndex++;
                                cmd.Parameters.AddWithValue(paramName, ConvertTo(filtro, prop.PropertyType));
                                whereConditions.Add($"[{prop.Name}] = {paramName}");
                            }
                        }

                        // Decimais -> igualdade se conseguir converter (suporta . e ,)
                        if (TryParseDecimalFlexible(filtro, out var dec))
                        {
                            var propsDecimal = props.Where(p =>
                                p.PropertyType == typeof(decimal) || p.PropertyType == typeof(decimal?));
                            foreach (var prop in propsDecimal)
                            {
                                var paramName = "@d" + paramIndex++;
                                cmd.Parameters.AddWithValue(paramName, dec);
                                whereConditions.Add($"[{prop.Name}] = {paramName}");
                            }
                        }

                        // DateTime -> igualdade se conseguir converter
                        if (DateTime.TryParse(filtro, CultureInfo.GetCultureInfo("pt-BR"), DateTimeStyles.None, out var dt) ||
                            DateTime.TryParse(filtro, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                        {
                            var propsDt = props.Where(p =>
                                p.PropertyType == typeof(DateTime) || p.PropertyType == typeof(DateTime?));
                            foreach (var prop in propsDt)
                            {
                                var paramName = "@t" + paramIndex++
                                    ;
                                cmd.Parameters.AddWithValue(paramName, dt);
                                whereConditions.Add($"[{prop.Name}] = {paramName}");
                            }
                        }
                    }

                    if (whereConditions.Count > 0)
                        query += " WHERE " + string.Join(" AND ", whereConditions); // AND em key=value, OR no amplo? Aqui usamos AND para pairs; amplo usa várias condições: se quiser OR, mude acima.

                    if (!string.IsNullOrWhiteSpace(orderBy))
                        query += $" ORDER BY {orderBy}";

                    cmd.CommandText = query;

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Mapa de colunas existentes no resultado (evita KeyNotFound no reader)
                        var schema = reader.GetSchemaTable();
                        var colNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
                        foreach (DataRow row in schema.Rows)
                            colNames.Add(row["ColumnName"].ToString());

                        while (reader.Read())
                        {
                            var obj = new T();
                            foreach (var prop in props)
                            {
                                if (!colNames.Contains(prop.Name)) continue; // coluna não existe no SELECT
                                var val = reader[prop.Name];
                                if (val == DBNull.Value) continue;

                                var targetType = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                                // Conversão segura (enums, numéricos, datas, etc.)
                                var conv = System.Convert.ChangeType(val, targetType, CultureInfo.InvariantCulture);
                                prop.SetValue(obj, conv);
                            }
                            lista.Add(obj);
                        }
                    }
                }
            }
            return lista;
        }

        // ===== Helpers =====
        private static object ConvertTo(string input, Type targetType)
        {
            var t = Nullable.GetUnderlyingType(targetType) ?? targetType;

            if (t == typeof(string)) return input;

            if (t.IsEnum) return Enum.Parse(t, input, ignoreCase: true);

            if (t == typeof(bool))
                return bool.TryParse(input, out var b) ? b : (object)0; // aceita "0/1"?
            if (t == typeof(byte))
                return byte.Parse(input, NumberStyles.Integer, CultureInfo.InvariantCulture);
            if (t == typeof(short))
                return short.Parse(input, NumberStyles.Integer, CultureInfo.InvariantCulture);
            if (t == typeof(int))
                return int.Parse(input, NumberStyles.Integer, CultureInfo.InvariantCulture);
            if (t == typeof(long))
                return long.Parse(input, NumberStyles.Integer, CultureInfo.InvariantCulture);
            if (t == typeof(decimal))
            {
                if (TryParseDecimalFlexible(input, out var d)) return d;
                return decimal.Parse(input, CultureInfo.InvariantCulture);
            }
            if (t == typeof(DateTime))
            {
                if (DateTime.TryParse(input, CultureInfo.GetCultureInfo("pt-BR"), DateTimeStyles.None, out var dt)) return dt;
                if (DateTime.TryParse(input, CultureInfo.InvariantCulture, DateTimeStyles.None, out dt)) return dt;
                throw new FormatException($"Data inválida: {input}");
            }

            // fallback
            return System.Convert.ChangeType(input, t, CultureInfo.InvariantCulture);
        }

        private static bool TryParseDecimalFlexible(string s, out decimal value)
        {
            // tenta pt-BR e Invariant
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.GetCultureInfo("pt-BR"), out value)) return true;
            if (decimal.TryParse(s, NumberStyles.Number, CultureInfo.InvariantCulture, out value)) return true;
            // tenta trocar vírgula/ponto
            var swap = s.Contains(',') ? s.Replace(",", ".") : s.Replace(".", ",");
            return decimal.TryParse(swap, NumberStyles.Number, CultureInfo.GetCultureInfo("pt-BR"), out value)
                || decimal.TryParse(swap, NumberStyles.Number, CultureInfo.InvariantCulture, out value);
        }
    }
}
