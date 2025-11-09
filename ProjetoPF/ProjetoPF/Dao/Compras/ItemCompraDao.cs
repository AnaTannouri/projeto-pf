using ProjetoPF.Modelos.Compra;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using ProjetoPF.Modelos;

namespace ProjetoPF.Dao.Compras
{
    public class ItemCompraDao : BaseDao<ItemCompra>
    {
        public ItemCompraDao() : base("ItensCompra") { }

        public List<ItemCompra> BuscarPorChaveCompra(string modelo, string serie, string numeroNota, int idFornecedor)
        {
            var itens = new List<ItemCompra>();

            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
                SELECT *
                FROM ItensCompra
                WHERE Modelo = @Modelo
                  AND Serie = @Serie
                  AND NumeroNota = @NumeroNota
                  AND IdFornecedor = @IdFornecedor;", cn))
            {
                cmd.Parameters.AddWithValue("@Modelo", modelo);
                cmd.Parameters.AddWithValue("@Serie", serie);
                cmd.Parameters.AddWithValue("@NumeroNota", numeroNota);
                cmd.Parameters.AddWithValue("@IdFornecedor", idFornecedor);

                cn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        itens.Add(new ItemCompra
                        {
                            IdProduto = (int)reader["IdProduto"],
                            Quantidade = (decimal)reader["Quantidade"],
                            ValorUnitario = (decimal)reader["ValorUnitario"],
                            Total = (decimal)reader["Total"],
                            DataCriacao = (DateTime)reader["DataCriacao"],
                            DataAtualizacao = reader["DataAtualizacao"] == DBNull.Value
                                ? DateTime.MinValue
                                : (DateTime)reader["DataAtualizacao"],
                            Ativo = (bool)reader["Ativo"]
                        });
                    }
                }
            }

            return itens;
        }
        public List<ItemCompra> BuscarPorChave(CompraKey key)
        {
            if (string.IsNullOrWhiteSpace(key.Modelo))
                throw new ArgumentException("Modelo não pode ser nulo.", nameof(key.Modelo));
            if (string.IsNullOrWhiteSpace(key.Serie))
                throw new ArgumentException("Série não pode ser nula.", nameof(key.Serie));
            if (string.IsNullOrWhiteSpace(key.NumeroNota))
                throw new ArgumentException("Número da nota não pode ser nulo.", nameof(key.NumeroNota));

            return BuscarPorChaveCompra(key.Modelo, key.Serie, key.NumeroNota, key.IdFornecedor);
        }
    }
}
