using ProjetoPF.Modelos.Produto;
using System;
using System.Data.SqlClient;

namespace ProjetoPF.Dao.Item
{
    public class ProdutoDAO : BaseDao<Produtos>
    {
        public int ObterEstoqueAtual(int idProduto)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT Estoque FROM Produtos WHERE Id = @Id AND Ativo = 1";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", idProduto);

                object result = cmd.ExecuteScalar();

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }

                return 0; // Retorna 0 se não encontrar ou estoque for nulo
            }
        }

        public string ObterNomeProduto(int idProduto)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT Nome FROM Produtos WHERE Id = @Id AND Ativo = 1";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@Id", idProduto);

                object result = cmd.ExecuteScalar();

                return result != null && result != DBNull.Value ? result.ToString() : "Produto não encontrado";
            }
        }
        public ProdutoDAO() : base("Produtos")
        {

        }
    }
}
