using ProjetoPF.Modelos.Venda;
using ProjetoPF.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Dao.Vendas
{
    public class ItemVendaDao : BaseDao<ItemVenda>
    {
        public ItemVendaDao() : base("ItensVenda") { }

        public List<ItemVenda> BuscarPorChaveVenda(string modelo, string serie, string numeroNota, int idCliente)
        {
            var itens = new List<ItemVenda>();

            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
                SELECT *
                FROM ItensVenda
                WHERE Modelo = @Modelo
                  AND Serie = @Serie
                  AND NumeroNota = @NumeroNota
                  AND IdCliente = @IdCliente;", cn))
            {
                cmd.Parameters.AddWithValue("@Modelo", modelo);
                cmd.Parameters.AddWithValue("@Serie", serie);
                cmd.Parameters.AddWithValue("@NumeroNota", numeroNota);
                cmd.Parameters.AddWithValue("@IdCliente", idCliente);

                cn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        itens.Add(new ItemVenda
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

        public List<ItemVenda> BuscarPorChave(VendaKey key)
        {
            if (string.IsNullOrWhiteSpace(key.Modelo))
                throw new ArgumentException("Modelo não pode ser nulo ou vazio.", nameof(key.Modelo));
            if (string.IsNullOrWhiteSpace(key.Serie))
                throw new ArgumentException("Série não pode ser nula ou vazia.", nameof(key.Serie));
            if (string.IsNullOrWhiteSpace(key.NumeroNota))
                throw new ArgumentException("Número da nota não pode ser nulo ou vazio.", nameof(key.NumeroNota));

            return BuscarPorChaveVenda(key.Modelo, key.Serie, key.NumeroNota, key.IdCliente);
        }
    }
}
