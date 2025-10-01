using ProjetoPF.Modelos.Compra;
using System.Data.SqlClient;
using System;
using System.Linq;

namespace ProjetoPF.Dao.Compra
{
    public class ProdutoFornecedorDAO : BaseDao<ProdutoFornecedor>
    {
        public ProdutoFornecedorDAO() : base("ProdutoFornecedor")
        {
        }
        public void CriarOuAtualizarProdutoFornecedor(ProdutoFornecedor associacao)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
        IF EXISTS (
            SELECT 1 FROM ProdutoFornecedor
            WHERE IdProduto = @IdProduto AND IdFornecedor = @IdFornecedor
        )
        BEGIN
            UPDATE ProdutoFornecedor
            SET PrecoUltimaCompra = @PrecoUltimaCompra,
                DataUltimaCompra = @DataUltimaCompra,
                Observacao = @Observacao,
                DataAtualizacao = GETDATE()
            WHERE IdProduto = @IdProduto AND IdFornecedor = @IdFornecedor
        END
        ELSE
        BEGIN
            INSERT INTO ProdutoFornecedor (IdProduto, IdFornecedor, PrecoUltimaCompra, DataUltimaCompra, Observacao, DataCriacao, DataAtualizacao)
            VALUES (@IdProduto, @IdFornecedor, @PrecoUltimaCompra, @DataUltimaCompra, @Observacao, GETDATE(), GETDATE())
        END
    ", conn))
            {
                cmd.Parameters.AddWithValue("@IdProduto", associacao.IdProduto);
                cmd.Parameters.AddWithValue("@IdFornecedor", associacao.IdFornecedor);
                cmd.Parameters.AddWithValue("@PrecoUltimaCompra", associacao.PrecoUltimaCompra);
                if (associacao.DataUltimaCompra.HasValue && associacao.DataUltimaCompra.Value >= new DateTime(1753, 1, 1))
                    cmd.Parameters.AddWithValue("@DataUltimaCompra", associacao.DataUltimaCompra.Value);
                else
                    cmd.Parameters.AddWithValue("@DataUltimaCompra", DBNull.Value);
                if (string.IsNullOrWhiteSpace(associacao.Observacao))
                    cmd.Parameters.AddWithValue("@Observacao", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Observacao", associacao.Observacao);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public ProdutoFornecedor BuscarPorProdutoFornecedor(int idProduto, int idFornecedor)
        {
            var resultado = BuscarTodos($"IdProduto = {idProduto} AND IdFornecedor = {idFornecedor}");
            return resultado.FirstOrDefault();
        }
        public bool ExisteVinculo(int idProduto, int idFornecedor)
        {
            string filtro = $"IdProduto = {idProduto} AND IdFornecedor = {idFornecedor}";
            var resultados = BuscarTodos(filtro);
            return resultados != null && resultados.Count > 0;
        }
    }
}
