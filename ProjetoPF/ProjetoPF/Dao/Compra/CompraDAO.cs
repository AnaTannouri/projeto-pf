using ProjetoPF.Modelos.Compra;
using ProjetoPF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using ProjetoPF.Modelos.Localizacao;
using static System.Windows.Forms.LinkLabel;
using System.Security.Cryptography;

namespace ProjetoPF.Dao.Compras
{
    public class CompraCabecalho : BaseModelos
    {
        public int Modelo { get; set; }
        public string Serie { get; set; }
        public string NumeroNota { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataEntrega { get; set; }
        public int IdFornecedor { get; set; }
        public int IdCondicaoPagamento { get; set; }
        public decimal ValorFrete { get; set; }
        public decimal ValorSeguro { get; set; }
        public decimal OutrasDespesas { get; set; }
        public decimal ValorTotal { get; set; }
    }

    public class CompraDao : BaseDao<Compra>
    {
        public CompraDao() : base("Compras") { }
        public List<CompraCabecalho> BuscarTodosCabecalho(string filtro = "")
        {
            var dao = new BaseDao<CompraCabecalho>("Compras");
            return dao.BuscarTodos(filtro);
        }
        public void SalvarCompraComItensParcelas(Compra compra, List<ItemCompra> itens, List<ParcelaCompra> parcelas)
        {
            using (var scope = new TransactionScope(
           TransactionScopeOption.Required,
           new TransactionOptions
           {
               IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
           }))
            {
                if (itens == null || itens.Count == 0)
                    throw new Exception("Inclua ao menos um item na compra.");

                var agora = DateTime.Now;

                var cab = new CompraCabecalho
                {
                    DataCriacao = agora,
                    DataAtualizacao = agora,
                    Ativo = true,
                    Modelo = compra.Modelo,
                    Serie = compra.Serie?.Trim(),
                    NumeroNota = compra.NumeroNota?.Trim(),
                    DataEmissao = compra.DataEmissao,
                    DataEntrega = compra.DataEntrega,
                    IdFornecedor = compra.IdFornecedor,
                    IdCondicaoPagamento = compra.IdCondicaoPagamento,
                    ValorFrete = compra.ValorFrete,
                    ValorSeguro = compra.ValorSeguro,
                    OutrasDespesas = compra.OutrasDespesas,
                    ValorTotal = compra.ValorTotal
                };

                var cabDao = new BaseDao<CompraCabecalho>("Compras");
                cabDao.Criar(cab);
                compra.Id = cab.Id;

                var itemDao = new ItemCompraDao();
                foreach (var it in itens)
                {
                    it.IdCompra = compra.Id;
                    it.DataCriacao = agora;
                    it.DataAtualizacao = agora;
                    it.Ativo = true;
                    itemDao.Criar(it);
                }

                if (parcelas != null && parcelas.Count > 0)
                {
                    var parcelaDao = new ParcelaCompraDao();
                    foreach (var p in parcelas)
                    {
                        p.IdCompra = compra.Id;
                        p.DataCriacao = agora;
                        p.DataAtualizacao = agora;
                        p.Ativo = true;
                        parcelaDao.Criar(p);
                    }
                }

                scope.Complete();
            }
        }

        public void AtualizarCompraComItensParcelas(Compra compra, List<ItemCompra> itens, List<ParcelaCompra> parcelas)
        {
            using (var scope = new TransactionScope(
           TransactionScopeOption.Required,
           new TransactionOptions
           {
               IsolationLevel = System.Transactions.IsolationLevel.ReadCommitted
           }))
            {
                if (compra.Id <= 0) throw new Exception("Id da compra inválido.");
                if (itens == null || itens.Count == 0)
                    throw new Exception("Inclua ao menos um item na compra.");

                var agora = DateTime.Now;

                var cab = new CompraCabecalho
                {
                    Id = compra.Id,
                    DataCriacao = compra.DataCriacao == default ? agora : compra.DataCriacao,
                    DataAtualizacao = agora,
                    Ativo = compra.Ativo,
                    Modelo = compra.Modelo,
                    Serie = compra.Serie?.Trim(),
                    NumeroNota = compra.NumeroNota?.Trim(),
                    DataEmissao = compra.DataEmissao,
                    DataEntrega = compra.DataEntrega,
                    IdFornecedor = compra.IdFornecedor,
                    IdCondicaoPagamento = compra.IdCondicaoPagamento,
                    ValorFrete = compra.ValorFrete,
                    ValorSeguro = compra.ValorSeguro,
                    OutrasDespesas = compra.OutrasDespesas,
                    ValorTotal = compra.ValorTotal
                };

                var cabDao = new BaseDao<CompraCabecalho>("Compras");
                cabDao.Atualizar(cab);

                var itemDao = new ItemCompraDao();
                var parcelaDao = new ParcelaCompraDao();

                itemDao.RemoverPorCompraId(compra.Id);
                parcelaDao.RemoverPorCompraId(compra.Id);

                foreach (var it in itens)
                {
                    it.IdCompra = compra.Id;
                    it.DataCriacao = agora;
                    it.DataAtualizacao = agora;
                    it.Ativo = true;
                    itemDao.Criar(it);
                }

                if (parcelas != null && parcelas.Count > 0)
                {
                    foreach (var p in parcelas)
                    {
                        p.IdCompra = compra.Id;
                        p.DataCriacao = agora;
                        p.DataAtualizacao = agora;
                        p.Ativo = true;
                        parcelaDao.Criar(p);
                    }
                }

                scope.Complete();
            }
        }
        public void CancelarCompra(int idCompra, string motivo = null)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
        UPDATE Compras
        SET Ativo = 0,
            DataAtualizacao = @DataAtualizacao,
            MotivoCancelamento = @Motivo
        WHERE Id = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", idCompra);
                cmd.Parameters.AddWithValue("@DataAtualizacao", DateTime.Now);
                cmd.Parameters.AddWithValue("@Motivo", string.IsNullOrWhiteSpace(motivo) ? (object)DBNull.Value : motivo);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void RemoverCompraComFilhos(int idCompra)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var tx = conn.BeginTransaction();

                try
                {
                    using (var cmd = new SqlCommand("DELETE FROM ItensCompra WHERE IdCompra = @Id", conn, tx))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = idCompra;
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand("DELETE FROM ParcelasCompra WHERE IdCompra = @Id", conn, tx))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = idCompra;
                        cmd.ExecuteNonQuery();
                    }

                    using (var cmd = new SqlCommand("DELETE FROM Compras WHERE Id = @Id", conn, tx))
                    {
                        cmd.Parameters.Add("@Id", SqlDbType.Int).Value = idCompra;
                        cmd.ExecuteNonQuery();
                    }

                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        public bool NotaJaExiste(int modelo, string serie, string numero, int idFornecedor)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
        SELECT TOP 1 1
        FROM Compras
        WHERE Modelo = @Modelo
          AND Serie = @Serie
          AND NumeroNota = @Numero
          AND IdFornecedor = @Fornecedor
          AND Ativo = 1;", conn))
            {
                cmd.Parameters.AddWithValue("@Modelo", modelo);

                if (string.IsNullOrEmpty(serie))
                    cmd.Parameters.AddWithValue("@Serie", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Serie", serie.Trim());

                if (string.IsNullOrEmpty(numero))
                    cmd.Parameters.AddWithValue("@Numero", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@Numero", numero.Trim());

                cmd.Parameters.AddWithValue("@Fornecedor", idFornecedor);

                conn.Open();
                return cmd.ExecuteScalar() != null;
            }
        }
        public Compra BuscarPorId(int idCompra)
        {
            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT * FROM Compras WHERE Id = @Id", cn))
            {
                cmd.Parameters.AddWithValue("@Id", idCompra);
                cn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new Compra
                        {
                            Id = (int)reader["Id"],
                            Modelo = (int)reader["Modelo"],
                            Serie = reader["Serie"].ToString(),
                            NumeroNota = reader["NumeroNota"].ToString(),
                            DataEmissao = (DateTime)reader["DataEmissao"],
                            DataEntrega = (DateTime)reader["DataEntrega"],
                            IdFornecedor = (int)reader["IdFornecedor"],
                            IdCondicaoPagamento = (int)reader["IdCondicaoPagamento"],
                            ValorFrete = (decimal)reader["ValorFrete"],
                            ValorSeguro = (decimal)reader["ValorSeguro"],
                            OutrasDespesas = (decimal)reader["OutrasDespesas"],
                            ValorTotal = (decimal)reader["ValorTotal"],
                            DataCriacao = (DateTime)reader["DataCriacao"],
                            DataAtualizacao = (DateTime)reader["DataAtualizacao"],
                            Ativo = (bool)reader["Ativo"],
                            MotivoCancelamento = reader["MotivoCancelamento"] == DBNull.Value
                                         ? null
                                         : reader["MotivoCancelamento"].ToString()
                        };
                    }
                }
            }
            return null;
        }
    }

    public class ItemCompraDao : BaseDao<ItemCompra>
    {
        public ItemCompraDao() : base("ItensCompra") { }

        public List<ItemCompra> BuscarPorCompraId(int idCompra)
        {
            var itens = new List<ItemCompra>();

            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT * FROM ItensCompra WHERE IdCompra = @Id", cn))
            {
                cmd.Parameters.AddWithValue("@Id", idCompra);
                cn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        itens.Add(new ItemCompra
                        {
                            Id = (int)reader["Id"],
                            IdCompra = (int)reader["IdCompra"],
                            IdProduto = (int)reader["IdProduto"],
                            Quantidade = (decimal)reader["Quantidade"],
                            ValorUnitario = (decimal)reader["ValorUnitario"],
                            Total = (decimal)reader["Total"],
                            DataCriacao = (DateTime)reader["DataCriacao"],
                            DataAtualizacao = (DateTime)reader["DataAtualizacao"],
                            Ativo = (bool)reader["Ativo"] 
                        });
                    }
                }
            }
            return itens;
        }

        public void RemoverPorCompraId(int idCompra)
        {
            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("DELETE FROM ItensCompra WHERE IdCompra = @Id", cn))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = idCompra;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }

    public class ParcelaCompraDao : BaseDao<ParcelaCompra>
    {
        public ParcelaCompraDao() : base("ParcelasCompra") { }

        public List<ParcelaCompra> BuscarPorCompraId(int idCompra)
        {
            var parcelas = new List<ParcelaCompra>();

            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT * FROM ParcelasCompra WHERE IdCompra = @Id", cn))
            {
                cmd.Parameters.AddWithValue("@Id", idCompra);
                cn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        parcelas.Add(new ParcelaCompra
                        {
                            Id = (int)reader["Id"],
                            IdCompra = (int)reader["IdCompra"],
                            NumeroParcela = (int)reader["NumeroParcela"],
                            DataVencimento = (DateTime)reader["DataVencimento"],
                            ValorParcela = (decimal)reader["ValorParcela"],
                            IdFormaPagamento = (int)reader["IdFormaPagamento"],
                            DataCriacao = (DateTime)reader["DataCriacao"],
                            DataAtualizacao = (DateTime)reader["DataAtualizacao"],
                            Ativo = (bool)reader["Ativo"],
                        });
                    }
                }
            }
            return parcelas;
        }

        public void RemoverPorCompraId(int idCompra)
        {
            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("DELETE FROM ParcelasCompra WHERE IdCompra = @Id", cn))
            {
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = idCompra;
                cn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}