using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ProjetoPF.Dao
{
    public class BaseDao<T> where T : ProjetoPF.Model.BaseModelos, new()
    {
        protected readonly string _connectionString = "data source=ANA-TANNOURI\\SQLEXPRESS;initial catalog=projeto-pf;user id=sa;password=123";
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
    }
}
