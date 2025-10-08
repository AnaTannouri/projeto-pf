using ProjetoPF.Modelos.Compra;
using ProjetoPF.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using ProjetoPF.Dao.Compra;
using IsolationLevel = System.Transactions.IsolationLevel;
using System.Security.Policy;
using ProjetoPF.Modelos.Pessoa;

namespace ProjetoPF.Dao.Compras
{
    public class CompraCabecalho : BaseModelos
    {
        public string Modelo { get; set; }  
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
        public string Observacao { get; set; }
    }

    public class CompraDao : BaseDao<ProjetoPF.Modelos.Compra.Compra>
    {
        public CompraDao() : base("Compras") { }
        public List<CompraCabecalho> BuscarTodosCabecalho(string filtro = "")
        {
            var dao = new BaseDao<CompraCabecalho>("Compras");
            return dao.BuscarTodos(filtro);
        }
        public int SalvarCompraComItensParcelas(
            ProjetoPF.Modelos.Compra.Compra compra,
            List<ProjetoPF.Modelos.Compra.ItemCompra> itens,
            List<ProjetoPF.Modelos.Compra.ContasAPagar> parcelas)
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
            {
                if (compra == null)
                    throw new Exception("Compra inválida.");
                if (itens == null || itens.Count == 0)
                    throw new Exception("Inclua ao menos um item na compra.");

                var agora = DateTime.Now;

                int idCompraGerado;
                using (var conn = new SqlConnection(_connectionString))
                using (var cmd = new SqlCommand(@"
                    INSERT INTO Compras
                        (Modelo, Serie, NumeroNota, DataEmissao, DataEntrega, IdFornecedor,
                         IdCondicaoPagamento, ValorFrete, ValorSeguro, OutrasDespesas, ValorTotal,
                         Observacao, DataCriacao, DataAtualizacao, Ativo)
                    VALUES
                        (@Modelo, @Serie, @NumeroNota, @DataEmissao, @DataEntrega, @IdFornecedor,
                         @IdCondicaoPagamento, @ValorFrete, @ValorSeguro, @OutrasDespesas, @ValorTotal,
                         @Observacao, @DataCriacao, @DataAtualizacao, @Ativo);
                    SELECT SCOPE_IDENTITY();", conn))
                {
                    cmd.Parameters.AddWithValue("@Modelo",
    compra.Modelo == 0 ? (object)DBNull.Value : compra.Modelo);
                    cmd.Parameters.AddWithValue("@Serie", compra.Serie ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@NumeroNota", compra.NumeroNota ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@DataEmissao", compra.DataEmissao);
                    cmd.Parameters.AddWithValue("@DataEntrega", compra.DataEntrega);
                    cmd.Parameters.AddWithValue("@IdFornecedor", compra.IdFornecedor);
                    cmd.Parameters.AddWithValue("@IdCondicaoPagamento", compra.IdCondicaoPagamento);
                    cmd.Parameters.AddWithValue("@ValorFrete", compra.ValorFrete);
                    cmd.Parameters.AddWithValue("@ValorSeguro", compra.ValorSeguro);
                    cmd.Parameters.AddWithValue("@OutrasDespesas", compra.OutrasDespesas);
                    cmd.Parameters.AddWithValue("@ValorTotal", compra.ValorTotal);
                    cmd.Parameters.AddWithValue("@Observacao", (object)compra.Observacao ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DataCriacao", agora);
                    cmd.Parameters.AddWithValue("@DataAtualizacao", agora);
                    cmd.Parameters.AddWithValue("@Ativo", compra.Ativo);

                    conn.Open();
                    idCompraGerado = Convert.ToInt32(cmd.ExecuteScalar());
                }

                var itemDao = new ItemCompraDao();
                foreach (var it in itens)
                {
                    it.IdCompra = idCompraGerado;
                    it.DataCriacao = agora;
                    it.DataAtualizacao = agora;
                    it.Ativo = true;
                    itemDao.Criar(it);
                }

                if (parcelas != null && parcelas.Count > 0)
                {
                    var parcelaDao = new ContasAPagarDao();
                    foreach (var p in parcelas)
                    {
                        p.IdCompra = idCompraGerado;
                        p.DataCriacao = agora;
                        p.DataAtualizacao = agora;
                        p.Ativo = true;
                        parcelaDao.Criar(p);
                    }
                }

                scope.Complete();
                return idCompraGerado;
            }
        }

        public void AtualizarCompraComItensParcelas(
                   ProjetoPF.Modelos.Compra.Compra compra,
                   List<ProjetoPF.Modelos.Compra.ItemCompra> itens,
                   List<ProjetoPF.Modelos.Compra.ContasAPagar> parcelas)
        {
            using (var scope = new TransactionScope(
                TransactionScopeOption.Required,
                new TransactionOptions { IsolationLevel = IsolationLevel.ReadCommitted }))
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
                    Modelo = compra.Modelo.ToString(),
                    Serie = compra.Serie?.Trim(),
                    NumeroNota = compra.NumeroNota?.Trim(),
                    DataEmissao = compra.DataEmissao,
                    DataEntrega = compra.DataEntrega,
                    IdFornecedor = compra.IdFornecedor,
                    IdCondicaoPagamento = compra.IdCondicaoPagamento,
                    ValorFrete = compra.ValorFrete,
                    ValorSeguro = compra.ValorSeguro,
                    OutrasDespesas = compra.OutrasDespesas,
                    ValorTotal = compra.ValorTotal,
                    Observacao = compra.Observacao
                };

                var cabDao = new BaseDao<CompraCabecalho>("Compras");
                cabDao.Atualizar(cab);

                var itemDao = new ItemCompraDao();
                var parcelaDao = new ContasAPagarDao();

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
        public decimal BuscarUltimoCustoProduto(int idProduto, int idCompraCancelada)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
        SELECT TOP 1 i.ValorUnitario
        FROM ItensCompra i
        INNER JOIN Compras c ON c.Id = i.IdCompra
        WHERE i.IdProduto = @IdProduto
          AND c.Ativo = 1
          AND c.Id <> @IdCancelada
        ORDER BY c.DataEmissao DESC", conn))
            {
                cmd.Parameters.AddWithValue("@IdProduto", idProduto);
                cmd.Parameters.AddWithValue("@IdCancelada", idCompraCancelada);

                conn.Open();
                var result = cmd.ExecuteScalar();
                return result != null ? Convert.ToDecimal(result) : 0;
            }
        }

        public void CancelarCompra(int idCompra, string motivo)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var tx = conn.BeginTransaction();

                try
                {
                    int idFornecedor = 0;
                    using (var cmdBusca = new SqlCommand("SELECT IdFornecedor FROM Compras WHERE Id = @Id", conn, tx))
                    {
                        cmdBusca.Parameters.AddWithValue("@Id", idCompra);
                        var result = cmdBusca.ExecuteScalar();
                        if (result != null)
                            idFornecedor = Convert.ToInt32(result);
                    }

                    using (var cmdUpdateParc = new SqlCommand(@"
                UPDATE ContasAPagar
                SET Ativo = 0, DataAtualizacao = @DataAtualizacao
                WHERE IdCompra = @Id", conn, tx))
                    {
                        cmdUpdateParc.Parameters.AddWithValue("@Id", idCompra);
                        cmdUpdateParc.Parameters.AddWithValue("@DataAtualizacao", DateTime.Now);
                        cmdUpdateParc.ExecuteNonQuery();
                    }

                    using (var cmdUpdateItens = new SqlCommand(@"
                UPDATE ItensCompra
                SET Ativo = 0, DataAtualizacao = @DataAtualizacao
                WHERE IdCompra = @Id", conn, tx))
                    {
                        cmdUpdateItens.Parameters.AddWithValue("@Id", idCompra);
                        cmdUpdateItens.Parameters.AddWithValue("@DataAtualizacao", DateTime.Now);
                        cmdUpdateItens.ExecuteNonQuery();
                    }

                    using (var cmdUpdateCompra = new SqlCommand(@"
                UPDATE Compras
                SET Ativo = 0,
                    MotivoCancelamento = @Motivo,
                    DataAtualizacao = @DataAtualizacao
                WHERE Id = @Id", conn, tx))
                    {
                        cmdUpdateCompra.Parameters.AddWithValue("@Id", idCompra);
                        cmdUpdateCompra.Parameters.AddWithValue("@Motivo",
                            string.IsNullOrWhiteSpace(motivo) ? "Cancelamento não informado" : motivo);
                        cmdUpdateCompra.Parameters.AddWithValue("@DataAtualizacao", DateTime.Now);
                        cmdUpdateCompra.ExecuteNonQuery();
                    }

                    var produtoFornecedorDao = new ProdutoFornecedorDAO();
                    var itemDao = new ItemCompraDao();
                    var itens = itemDao.BuscarPorCompraId(idCompra);

                    foreach (var item in itens)
                    {
                        decimal ultimoCusto = BuscarUltimoCustoProduto(item.IdProduto, idCompra);

                        produtoFornecedorDao.AtualizarPrecoUltimaCompra(item.IdProduto, idFornecedor, ultimoCusto);

                        using (var cmdUpdateProduto = new SqlCommand(@"
                    UPDATE Produtos
                    SET CustoUltimaCompra = @Custo, DataAtualizacao = @Data
                    WHERE Id = @IdProduto", conn, tx))
                        {
                            cmdUpdateProduto.Parameters.AddWithValue("@Custo", ultimoCusto);
                            cmdUpdateProduto.Parameters.AddWithValue("@Data", DateTime.Now);
                            cmdUpdateProduto.Parameters.AddWithValue("@IdProduto", item.IdProduto);
                            cmdUpdateProduto.ExecuteNonQuery();
                        }
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

        public void AtualizarCompraBasica(ProjetoPF.Modelos.Compra.Compra compra)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
        UPDATE Compras
        SET Modelo = @Modelo,
            Serie = @Serie,
            NumeroNota = @NumeroNota,
            DataEmissao = @DataEmissao,
            DataEntrega = @DataEntrega,
            IdFornecedor = @IdFornecedor,
            IdCondicaoPagamento = @IdCondicaoPagamento,
            ValorFrete = @ValorFrete,
            ValorSeguro = @ValorSeguro,
            OutrasDespesas = @OutrasDespesas,
            ValorTotal = @ValorTotal,
            Observacao = @Observacao,
            Ativo = @Ativo,
            MotivoCancelamento = @MotivoCancelamento,
            DataAtualizacao = @DataAtualizacao
        WHERE Id = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Modelo", compra.Modelo);
                cmd.Parameters.AddWithValue("@Serie", (object)compra.Serie ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NumeroNota", (object)compra.NumeroNota ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DataEmissao", compra.DataEmissao);
                cmd.Parameters.AddWithValue("@DataEntrega", compra.DataEntrega);
                cmd.Parameters.AddWithValue("@IdFornecedor", compra.IdFornecedor);
                cmd.Parameters.AddWithValue("@IdCondicaoPagamento", compra.IdCondicaoPagamento);
                cmd.Parameters.AddWithValue("@ValorFrete", compra.ValorFrete);
                cmd.Parameters.AddWithValue("@ValorSeguro", compra.ValorSeguro);
                cmd.Parameters.AddWithValue("@OutrasDespesas", compra.OutrasDespesas);
                cmd.Parameters.AddWithValue("@ValorTotal", compra.ValorTotal);
                cmd.Parameters.AddWithValue("@Observacao", (object)compra.Observacao ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Ativo", compra.Ativo);
                cmd.Parameters.AddWithValue("@MotivoCancelamento", (object)compra.MotivoCancelamento ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@DataAtualizacao", compra.DataAtualizacao);
                cmd.Parameters.AddWithValue("@Id", compra.Id);

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

                    using (var cmd = new SqlCommand("DELETE FROM ContasAPagar WHERE IdCompra = @Id", conn, tx))
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

        public bool NotaJaExiste(int modelo, string serie, string numero, int idFornecedor, int? idIgnorar = null)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = @"
            SELECT TOP 1 1
            FROM Compras
            WHERE Modelo = @Modelo
              AND Serie = @Serie
              AND NumeroNota = @Numero
              AND IdFornecedor = @Fornecedor
              AND Ativo = 1";

                if (idIgnorar.HasValue)
                    sql += " AND Id <> @IdIgnorar";  

                using (var cmd = new SqlCommand(sql, conn))
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

                    if (idIgnorar.HasValue)
                        cmd.Parameters.AddWithValue("@IdIgnorar", idIgnorar.Value);

                    conn.Open();
                    return cmd.ExecuteScalar() != null;
                }
            }
        }

        public ProjetoPF.Modelos.Compra.Compra BuscarPorNota(int modelo, string serie, string numero, int idFornecedor)

        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(@"
            SELECT TOP 1 *
            FROM Compras
            WHERE Modelo = @Modelo
              AND Serie = @Serie
              AND NumeroNota = @Numero
              AND IdFornecedor = @Fornecedor
              AND Ativo = 1", conn))
                {
                    cmd.Parameters.AddWithValue("@Modelo", modelo);
                    cmd.Parameters.AddWithValue("@Serie", serie.Trim());
                    cmd.Parameters.AddWithValue("@Numero", numero.Trim());
                    cmd.Parameters.AddWithValue("@Fornecedor", idFornecedor);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ProjetoPF.Modelos.Compra.Compra
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Modelo = Convert.ToInt32(reader["Modelo"]),
                                Serie = reader["Serie"].ToString(),
                                NumeroNota = reader["NumeroNota"].ToString(),
                                IdFornecedor = Convert.ToInt32(reader["IdFornecedor"]),
                                IdCondicaoPagamento = Convert.ToInt32(reader["IdCondicaoPagamento"]),
                                DataEmissao = Convert.ToDateTime(reader["DataEmissao"]),
                                DataEntrega = Convert.ToDateTime(reader["DataEntrega"]),
                                ValorFrete = Convert.ToDecimal(reader["ValorFrete"]),
                                ValorSeguro = Convert.ToDecimal(reader["ValorSeguro"]),
                                OutrasDespesas = Convert.ToDecimal(reader["OutrasDespesas"]),
                                ValorTotal = Convert.ToDecimal(reader["ValorTotal"]),
                                Observacao = reader["Observacao"]?.ToString(),
                                DataCriacao = Convert.ToDateTime(reader["DataCriacao"]),
                                DataAtualizacao = Convert.ToDateTime(reader["DataAtualizacao"]),
                                Ativo = Convert.ToBoolean(reader["Ativo"])
                            };
                        }
                    }
                }
            }
            return null;
        }
        public void AtualizarStatusCancelamento(int idCompra, string motivo, DateTime dataAtualizacao)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
        UPDATE Compras
        SET Ativo = 0,
            MotivoCancelamento = @Motivo,
            DataAtualizacao = @DataAtualizacao
        WHERE Id = @Id", conn))
            {
                cmd.Parameters.AddWithValue("@Id", idCompra);
                cmd.Parameters.AddWithValue("@Motivo", string.IsNullOrWhiteSpace(motivo) ? "Cancelamento não informado" : motivo);
                cmd.Parameters.AddWithValue("@DataAtualizacao", dataAtualizacao);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public decimal CalcularCustoMedioProduto(int idProduto, int idCompraCancelada)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
                SELECT 
                    SUM(i.Quantidade * i.ValorUnitario) AS TotalValor,
                    SUM(i.Quantidade) AS TotalQtd
                FROM ItensCompra i
                INNER JOIN Compras c ON c.Id = i.IdCompra
                WHERE i.IdProduto = @IdProduto
                  AND c.Ativo = 1
                  AND c.Id <> @IdCancelada", conn))
            {
                cmd.Parameters.AddWithValue("@IdProduto", idProduto);
                cmd.Parameters.AddWithValue("@IdCancelada", idCompraCancelada);

                conn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read() && reader["TotalQtd"] != DBNull.Value)
                    {
                        decimal totalValor = Convert.ToDecimal(reader["TotalValor"]);
                        decimal totalQtd = Convert.ToDecimal(reader["TotalQtd"]);
                        if (totalQtd > 0)
                            return Math.Round(totalValor / totalQtd, 2);
                    }
                }
            }
            return 0;
        }
        public ProjetoPF.Modelos.Compra.Compra BuscarPorId(int idCompra)
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
                        return new ProjetoPF.Modelos.Compra.Compra
                        {
                            Id = (int)reader["Id"],
                            Modelo = reader["Modelo"] != DBNull.Value
    ? Convert.ToInt32(reader["Modelo"])
    : 0,
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
                                : reader["MotivoCancelamento"].ToString(),
                            Observacao = reader["Observacao"] == DBNull.Value
                                ? null
                                : reader["Observacao"].ToString()
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

    public class ContasAPagarDao : BaseDao<ContasAPagar>
    {
        public ContasAPagarDao() : base("ContasAPagar") { }

        public List<ContasAPagar> BuscarPorCompraId(int idCompra)
        {
            var parcelas = new List<ContasAPagar>();

            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("SELECT * FROM ContasAPagar WHERE IdCompra = @Id", cn))
            {
                cmd.Parameters.AddWithValue("@Id", idCompra);
                cn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        parcelas.Add(new ContasAPagar
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            IdCompra = Convert.ToInt32(reader["IdCompra"]),
                            NumeroParcela = Convert.ToInt32(reader["NumeroParcela"]),
                            DataVencimento = reader["DataVencimento"] != DBNull.Value
                                ? Convert.ToDateTime(reader["DataVencimento"])
                                : DateTime.MinValue,
                            ValorParcela = reader["ValorParcela"] != DBNull.Value
                                ? Convert.ToDecimal(reader["ValorParcela"])
                                : 0m,
                            IdFormaPagamento = reader["IdFormaPagamento"] != DBNull.Value
                                ? Convert.ToInt32(reader["IdFormaPagamento"])
                                : 0,
                            DataCriacao = reader["DataCriacao"] != DBNull.Value
                                ? Convert.ToDateTime(reader["DataCriacao"])
                                : DateTime.MinValue,
                            DataAtualizacao = reader["DataAtualizacao"] != DBNull.Value
                                ? Convert.ToDateTime(reader["DataAtualizacao"])
                                : DateTime.MinValue,
                            Ativo = reader["Ativo"] != DBNull.Value &&
                                    (reader["Ativo"] is bool b ? b : Convert.ToInt32(reader["Ativo"]) == 1)
                        });
                    }
                }
            }
            return parcelas;
        }

        public void InativarPorCompraId(int idCompra)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
        UPDATE ContasAPagar
        SET Ativo = 0,
            DataAtualizacao = @DataAtualizacao
        WHERE IdCompra = @IdCompra", conn))
            {
                cmd.Parameters.AddWithValue("@IdCompra", idCompra);
                cmd.Parameters.AddWithValue("@DataAtualizacao", DateTime.Now);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void RemoverPorCompraId(int idCompra)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = "DELETE FROM ContasAPagar WHERE IdCompra = @IdCompra";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@IdCompra", idCompra);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public List<ContasAPagar> BuscarParcelasCanceladas(int idCompra)
        {
            var parcelas = new List<ContasAPagar>();

            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
        SELECT 
            IdCompra,
            NumeroParcela,
            DataVencimento,
            ValorParcela,
            IdFormaPagamento
        FROM ContasAPagar
        WHERE IdCompra = @Id
          AND Ativo = 0
        ORDER BY NumeroParcela", conn))
            {
                cmd.Parameters.AddWithValue("@Id", idCompra);
                conn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        parcelas.Add(new ContasAPagar
                        {
                            IdCompra = (int)reader["IdCompra"],
                            NumeroParcela = (int)reader["NumeroParcela"],
                            DataVencimento = reader["DataVencimento"] != DBNull.Value
                                ? Convert.ToDateTime(reader["DataVencimento"])
                                : DateTime.MinValue,
                            ValorParcela = reader["ValorParcela"] != DBNull.Value
                                ? Convert.ToDecimal(reader["ValorParcela"])
                                : 0m,
                            IdFormaPagamento = reader["IdFormaPagamento"] != DBNull.Value
                                ? Convert.ToInt32(reader["IdFormaPagamento"])
                                : 0,
                            FormaPagamentoDescricao = ""
                        });
                    }
                }
            }

            return parcelas;


        }
    }
}