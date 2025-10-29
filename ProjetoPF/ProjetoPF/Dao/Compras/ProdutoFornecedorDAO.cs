using ProjetoPF.Modelos.Compra;
using System.Data.SqlClient;
using System;
using System.Linq;
using System.Collections.Generic;

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
                Ativo = 1
            WHERE IdProduto = @IdProduto AND IdFornecedor = @IdFornecedor;
        END
        ELSE
        BEGIN
            INSERT INTO ProdutoFornecedor (IdProduto, IdFornecedor, PrecoUltimaCompra, DataUltimaCompra, Ativo)
            VALUES (@IdProduto, @IdFornecedor, @PrecoUltimaCompra, @DataUltimaCompra, 1);
        END", conn))
            {
                cmd.Parameters.AddWithValue("@IdProduto", associacao.IdProduto);
                cmd.Parameters.AddWithValue("@IdFornecedor", associacao.IdFornecedor);
                cmd.Parameters.AddWithValue("@PrecoUltimaCompra", associacao.PrecoUltimaCompra);
                cmd.Parameters.AddWithValue("@DataUltimaCompra", associacao.DataUltimaCompra ?? (object)DBNull.Value);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void InativarPorCompra(int idProduto, int idFornecedor)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
        UPDATE ProdutoFornecedor
        SET Ativo = 0
        WHERE IdProduto = @IdProduto AND IdFornecedor = @IdFornecedor", conn))
            {
                cmd.Parameters.AddWithValue("@IdProduto", idProduto);
                cmd.Parameters.AddWithValue("@IdFornecedor", idFornecedor);
                conn.Open();
                cmd.ExecuteNonQuery();

            }
        }
        public List<ProdutoFornecedor> ListarPorProduto(int idProduto)
        {
            var lista = new List<ProdutoFornecedor>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
                SELECT pf.*, f.NomeRazaoSocial AS NomeFornecedor
                FROM ProdutoFornecedor pf
                INNER JOIN Fornecedores f ON f.Id = pf.IdFornecedor
                WHERE pf.IdProduto = @IdProduto
                ORDER BY pf.DataAtualizacao DESC", conn))
            {
                cmd.Parameters.AddWithValue("@IdProduto", idProduto);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var pf = new ProdutoFornecedor
                        {
                            Id = (int)reader["Id"],
                            IdProduto = (int)reader["IdProduto"],
                            IdFornecedor = (int)reader["IdFornecedor"],
                            PrecoUltimaCompra = reader["PrecoUltimaCompra"] != DBNull.Value ? Convert.ToDecimal(reader["PrecoUltimaCompra"]) : 0,
                            DataUltimaCompra = reader["DataUltimaCompra"] != DBNull.Value ? Convert.ToDateTime(reader["DataUltimaCompra"]) : (DateTime?)null,
                            DataCriacao = reader["DataCriacao"] != DBNull.Value ? Convert.ToDateTime(reader["DataCriacao"]) : DateTime.MinValue,
                            DataAtualizacao = reader["DataAtualizacao"] != DBNull.Value ? Convert.ToDateTime(reader["DataAtualizacao"]) : DateTime.MinValue
                        };

                        lista.Add(pf);
                    }
                }
            }

            return lista;
        }
        public bool ExisteCompraVinculada(int idProduto, int idFornecedor)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
        SELECT COUNT(*) 
        FROM ItensCompra ic
        INNER JOIN Compras c ON c.Id = ic.IdCompra
        WHERE ic.IdProduto = @IdProduto 
          AND c.IdFornecedor = @IdFornecedor", conn))
            {
                cmd.Parameters.AddWithValue("@IdProduto", idProduto);
                cmd.Parameters.AddWithValue("@IdFornecedor", idFornecedor);

                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }
        public void RemoverVinculo(int idProduto, int idFornecedor)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
                DELETE FROM ProdutoFornecedor 
                WHERE IdProduto = @IdProduto AND IdFornecedor = @IdFornecedor", conn))
            {
                cmd.Parameters.AddWithValue("@IdProduto", idProduto);
                cmd.Parameters.AddWithValue("@IdFornecedor", idFornecedor);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public List<ProdutoFornecedor> BuscarPorFornecedor(int idFornecedor)
        {
            var lista = new List<ProdutoFornecedor>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT * FROM ProdutoFornecedor WHERE IdFornecedor = @id", conn))
            {
                cmd.Parameters.AddWithValue("@id", idFornecedor);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        lista.Add(new ProdutoFornecedor
                        {
                            Id = (int)reader["Id"],
                            IdProduto = (int)reader["IdProduto"],
                            IdFornecedor = (int)reader["IdFornecedor"],
                            PrecoUltimaCompra = reader["PrecoUltimaCompra"] != DBNull.Value ? (decimal)reader["PrecoUltimaCompra"] : 0,
                            DataUltimaCompra = reader["DataUltimaCompra"] != DBNull.Value ? (DateTime?)reader["DataUltimaCompra"] : null
                        });
                    }
                }
            }
            return lista;
        }
    }
}

