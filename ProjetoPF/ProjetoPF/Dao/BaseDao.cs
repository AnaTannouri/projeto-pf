using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace ProjetoPF.Dao
{
    public class BaseDao<T> where T : class, new()
    {
        protected readonly string _connectionString = "data source=Ana\\SQLEXPRESS;initial catalog=projeto-pf;user id=sa;password=123";
        protected readonly string _tabela;

        public BaseDao(string tabela)
        {
            _tabela = tabela;
        }

        private SqlConnection CriarConexao()
        {
            return new SqlConnection(_connectionString);
        }

        public bool VerificarDuplicidade(string campo, string valor)
        {
            using (SqlConnection conn = CriarConexao())
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand($"SELECT COUNT(1) FROM {_tabela} WHERE LTRIM(RTRIM({campo})) = LTRIM(RTRIM(@Valor))", conn);
                cmd.Parameters.AddWithValue("@Valor", valor);

                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public void Criar(T entidade)
        {
            var descricao = typeof(T).GetProperty("Descricao")?.GetValue(entidade)?.ToString();

            if (descricao != null && VerificarDuplicidade("Descricao", descricao))
            {
                throw new Exception("Já existe uma forma de pagamento com a mesma descrição.");
            }

            using (var conn = CriarConexao())
            {
                conn.Open();

                var propriedades = typeof(T).GetProperties().Where(p => p.Name != "Id").ToList();

                var colunas = string.Join(", ", propriedades.Select(p => p.Name));
                var valores = string.Join(", ", propriedades.Select(p => $"@{p.Name}"));

                var query = $"INSERT INTO {_tabela} ({colunas}) VALUES ({valores})";

                using (var cmd = new SqlCommand(query, conn))
                {
                    foreach (var prop in propriedades)
                    {
                        var valor = prop.GetValue(entidade) ?? DBNull.Value;
                        cmd.Parameters.AddWithValue($"@{prop.Name}", valor);
                    }

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<T> BuscarTodos(string filtro = null)
        {
            List<T> lista = new List<T>();
            try
            {
                using (SqlConnection conn = CriarConexao())
                {
                    conn.Open();
                    string query = $"SELECT * FROM {_tabela}";

                    if (!string.IsNullOrEmpty(filtro))
                    {
                        query += $" WHERE Id LIKE @filtro OR Descricao LIKE @filtro";
                    }

                    SqlCommand cmd = new SqlCommand(query, conn);

                    if (!string.IsNullOrEmpty(filtro))
                    {
                        cmd.Parameters.AddWithValue("@filtro", $"%{filtro}%");
                    }

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        T obj = Activator.CreateInstance<T>();
                        foreach (var prop in typeof(T).GetProperties())
                        {
                            try
                            {
                                if (reader[prop.Name] != DBNull.Value)
                                {
                                    var value = reader[prop.Name];
                                    if (Nullable.GetUnderlyingType(prop.PropertyType) != null)
                                    {
                                        prop.SetValue(obj, Convert.ChangeType(value, Nullable.GetUnderlyingType(prop.PropertyType)));
                                    }
                                    else
                                    {
                                        prop.SetValue(obj, Convert.ChangeType(value, prop.PropertyType));
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Erro ao mapear a coluna {prop.Name}: {ex.Message}");
                            }
                        }
                        lista.Add(obj);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar dados: {ex.Message}");
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

        public void Excluir(int id)
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
            var id = typeof(T).GetProperty("Id")?.GetValue(entidade);
            if (id == null)
                throw new Exception("A entidade não possui a propriedade 'Id'.");

            using (var conn = CriarConexao())
            {
                conn.Open();

                var propriedades = typeof(T).GetProperties().Where(p => p.Name != "Id").ToList();
                var setClauses = string.Join(", ", propriedades.Select(p => $"{p.Name} = @{p.Name}"));

                var query = $"UPDATE {_tabela} SET {setClauses} WHERE Id = @Id";

                using (var cmd = new SqlCommand(query, conn))
                {
                    foreach (var prop in propriedades)
                    {
                        var valor = prop.GetValue(entidade) ?? DBNull.Value;
                        cmd.Parameters.AddWithValue($"@{prop.Name}", valor);
                    }

                    cmd.Parameters.AddWithValue("@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
