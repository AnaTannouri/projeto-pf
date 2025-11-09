using ProjetoPF.Dao.Vendas;
using ProjetoPF.Model;
using ProjetoPF.Modelos;
using ProjetoPF.Modelos.Venda;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using IsolationLevel = System.Transactions.IsolationLevel;


namespace ProjetoPF.Dao.Vendas
{
    public class VendaDao : BaseDao<ProjetoPF.Modelos.Venda.Venda>
    {
        public VendaDao() : base("Vendas") { }

        public List<VendaCabecalho> BuscarTodosCabecalho(string filtro = "")
        {
            var dao = new BaseDao<VendaCabecalho>("Vendas");
            var vendas = dao.BuscarTodosWithoutId<VendaCabecalho>(filtro: filtro, orderBy: "DataCriacao");
            return vendas;
        }
        public void SalvarVendaComItensParcelas(
      ProjetoPF.Modelos.Venda.Venda venda,
      List<ProjetoPF.Modelos.Venda.ItemVenda> itens,
      List<ProjetoPF.Modelos.Venda.ContasAReceber> parcelas)
        {
            if (venda == null)
                throw new Exception("Venda inválida.");
            if (itens == null || itens.Count == 0)
                throw new Exception("Inclua ao menos um item na venda.");

            if (string.IsNullOrWhiteSpace(venda.Modelo))
                throw new InvalidOperationException("Modelo inválido.");
            if (string.IsNullOrWhiteSpace(venda.Serie))
                throw new InvalidOperationException("Série obrigatória.");
            if (string.IsNullOrWhiteSpace(venda.NumeroNota))
                throw new InvalidOperationException("Número da nota obrigatória.");
            if (venda.IdCliente <= 0)
                throw new InvalidOperationException("Cliente inválido.");
            if (venda.IdFuncionario <= 0)
                throw new InvalidOperationException("Funcionário inválido.");

            var agora = DateTime.Now;

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        // 1) Inserir VENDA
                        const string sqlVenda = @"
INSERT INTO dbo.Vendas
  (Modelo, Serie, NumeroNota, DataEmissao,
   IdCliente, IdFuncionario, IdCondicaoPagamento,
   ValorTotal, Observacao,
   DataCriacao, DataAtualizacao, Ativo)
VALUES
  (@Modelo, @Serie, @NumeroNota, @DataEmissao,
   @IdCliente, @IdFuncionario, @IdCondicaoPagamento,
   @ValorTotal, @Observacao,
   @DataCriacao, @DataAtualizacao, @Ativo);";

                        using (var cmd = new SqlCommand(sqlVenda, conn, tx))
                        {
                            cmd.Parameters.Add("@Modelo", SqlDbType.TinyInt).Value = venda.Modelo;
                            cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 5).Value = venda.Serie.Trim();
                            cmd.Parameters.Add("@NumeroNota", SqlDbType.VarChar, 20).Value = venda.NumeroNota.Trim();
                            cmd.Parameters.Add("@DataEmissao", SqlDbType.DateTime).Value = venda.DataEmissao;
                            cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = venda.IdCliente;
                            cmd.Parameters.Add("@IdFuncionario", SqlDbType.Int).Value = venda.IdFuncionario;
                            cmd.Parameters.Add("@IdCondicaoPagamento", SqlDbType.Int).Value = venda.IdCondicaoPagamento;

                            var pTot = cmd.Parameters.Add("@ValorTotal", SqlDbType.Decimal);
                            pTot.Precision = 18;
                            pTot.Scale = 2;
                            pTot.Value = venda.ValorTotal;

                            cmd.Parameters.Add("@Observacao", SqlDbType.VarChar).Value =
                                (object)venda.Observacao ?? DBNull.Value;

                            cmd.Parameters.Add("@DataCriacao", SqlDbType.DateTime).Value = agora;
                            cmd.Parameters.Add("@DataAtualizacao", SqlDbType.DateTime).Value = agora;
                            cmd.Parameters.Add("@Ativo", SqlDbType.Bit).Value = venda.Ativo;

                            if (cmd.ExecuteNonQuery() != 1)
                                throw new InvalidOperationException("Falha ao inserir a venda.");
                        }

                        // 2) Inserir ITENS
                        const string sqlItem = @"
INSERT INTO dbo.ItensVenda
  (Modelo, Serie, NumeroNota, IdCliente,
   IdProduto, Quantidade, ValorUnitario, Total,
   DataCriacao, DataAtualizacao, Ativo)
VALUES
  (@Modelo, @Serie, @NumeroNota, @IdCliente,
   @IdProduto, @Quantidade, @ValorUnitario, @Total,
   @DataCriacao, @DataAtualizacao, @Ativo);";

                        using (var cmdItem = new SqlCommand(sqlItem, conn, tx))
                        {
                            cmdItem.Parameters.Add("@Modelo", SqlDbType.TinyInt);
                            cmdItem.Parameters.Add("@Serie", SqlDbType.VarChar, 5);
                            cmdItem.Parameters.Add("@NumeroNota", SqlDbType.VarChar, 20);
                            cmdItem.Parameters.Add("@IdCliente", SqlDbType.Int);
                            cmdItem.Parameters.Add("@IdProduto", SqlDbType.Int);

                            var pQtd = cmdItem.Parameters.Add("@Quantidade", SqlDbType.Decimal);
                            pQtd.Precision = 18; pQtd.Scale = 3;

                            var pVlr = cmdItem.Parameters.Add("@ValorUnitario", SqlDbType.Decimal);
                            pVlr.Precision = 18; pVlr.Scale = 2;

                            var pTotItem = cmdItem.Parameters.Add("@Total", SqlDbType.Decimal);
                            pTotItem.Precision = 18; pTotItem.Scale = 2;

                            cmdItem.Parameters.Add("@DataCriacao", SqlDbType.DateTime);
                            cmdItem.Parameters.Add("@DataAtualizacao", SqlDbType.DateTime);
                            cmdItem.Parameters.Add("@Ativo", SqlDbType.Bit);

                            foreach (var it in itens)
                            {
                                cmdItem.Parameters["@Modelo"].Value = venda.Modelo;
                                cmdItem.Parameters["@Serie"].Value = venda.Serie.Trim();
                                cmdItem.Parameters["@NumeroNota"].Value = venda.NumeroNota.Trim();
                                cmdItem.Parameters["@IdCliente"].Value = venda.IdCliente;
                                cmdItem.Parameters["@IdProduto"].Value = it.IdProduto;
                                cmdItem.Parameters["@Quantidade"].Value = it.Quantidade;
                                cmdItem.Parameters["@ValorUnitario"].Value = it.ValorUnitario;
                                cmdItem.Parameters["@Total"].Value = it.Total;
                                cmdItem.Parameters["@DataCriacao"].Value = agora;
                                cmdItem.Parameters["@DataAtualizacao"].Value = agora;
                                cmdItem.Parameters["@Ativo"].Value = true;
                                cmdItem.ExecuteNonQuery();

                                // Atualiza estoque
                                using (var cmdEstoque = new SqlCommand(@"
UPDATE Produtos
   SET Estoque = Estoque - @Qtd,
       DataAtualizacao = @DataAtualizacao
 WHERE Id = @IdProduto;", conn, tx))
                                {
                                    cmdEstoque.Parameters.AddWithValue("@Qtd", it.Quantidade);
                                    cmdEstoque.Parameters.AddWithValue("@IdProduto", it.IdProduto);
                                    cmdEstoque.Parameters.AddWithValue("@DataAtualizacao", agora);
                                    cmdEstoque.ExecuteNonQuery();
                                }
                            }
                        }

                        // 3) Inserir PARCELAS
                        if (parcelas != null && parcelas.Count > 0)
                        {
                            const string sqlParc = @"
INSERT INTO dbo.ContasAReceber
  (Modelo, Serie, NumeroNota, IdCliente,
   NumeroParcela, DataEmissao, DataVencimento,
   ValorParcela, IdFormaPagamento,
   DataCriacao, DataAtualizacao, Ativo, Situacao)
VALUES
  (@Modelo, @Serie, @NumeroNota, @IdCliente,
   @NumeroParcela, @DataEmissao, @DataVencimento,
   @ValorParcela, @IdFormaPagamento,
   @DataCriacao, @DataAtualizacao, @Ativo, @Situacao);";

                            using (var cmdParc = new SqlCommand(sqlParc, conn, tx))
                            {
                                cmdParc.Parameters.Add("@Modelo", SqlDbType.TinyInt);
                                cmdParc.Parameters.Add("@Serie", SqlDbType.VarChar, 5);
                                cmdParc.Parameters.Add("@NumeroNota", SqlDbType.VarChar, 20);
                                cmdParc.Parameters.Add("@IdCliente", SqlDbType.Int);
                                cmdParc.Parameters.Add("@NumeroParcela", SqlDbType.Int);
                                cmdParc.Parameters.Add("@DataEmissao", SqlDbType.DateTime);
                                cmdParc.Parameters.Add("@DataVencimento", SqlDbType.DateTime);

                                var pVal = cmdParc.Parameters.Add("@ValorParcela", SqlDbType.Decimal);
                                pVal.Precision = 18; pVal.Scale = 2;

                                cmdParc.Parameters.Add("@IdFormaPagamento", SqlDbType.Int);
                                cmdParc.Parameters.Add("@DataCriacao", SqlDbType.DateTime);
                                cmdParc.Parameters.Add("@DataAtualizacao", SqlDbType.DateTime);
                                cmdParc.Parameters.Add("@Ativo", SqlDbType.Bit);
                                cmdParc.Parameters.Add("@Situacao", SqlDbType.VarChar, 20);

                                int n = 1;
                                foreach (var p in parcelas)
                                {
                                    cmdParc.Parameters["@Modelo"].Value = venda.Modelo;
                                    cmdParc.Parameters["@Serie"].Value = venda.Serie.Trim();
                                    cmdParc.Parameters["@NumeroNota"].Value = venda.NumeroNota.Trim();
                                    cmdParc.Parameters["@IdCliente"].Value = venda.IdCliente;

                                    cmdParc.Parameters["@NumeroParcela"].Value = p.NumeroParcela > 0 ? p.NumeroParcela : n++;
                                    cmdParc.Parameters["@DataEmissao"].Value = venda.DataEmissao;
                                    cmdParc.Parameters["@DataVencimento"].Value = p.DataVencimento;
                                    cmdParc.Parameters["@ValorParcela"].Value = p.ValorParcela;
                                    cmdParc.Parameters["@IdFormaPagamento"].Value = p.IdFormaPagamento;
                                    cmdParc.Parameters["@DataCriacao"].Value = agora;
                                    cmdParc.Parameters["@DataAtualizacao"].Value = agora;
                                    cmdParc.Parameters["@Ativo"].Value = true;
                                    cmdParc.Parameters["@Situacao"].Value = "Em Aberto";
                                    cmdParc.ExecuteNonQuery();
                                }
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
        }

        public void CancelarVenda(VendaKey vendaKey, string motivo)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        string motivoEfetivo = string.IsNullOrWhiteSpace(motivo)
                            ? "Cancelamento não informado"
                            : motivo.Trim();

                        // ------------------------------------------------------------------
                        // 1) Cancelar a VENDA (cabeçalho) pela PK composta
                        // ------------------------------------------------------------------
                        int linhasVenda;
                        using (var cmd = new SqlCommand(@"
                UPDATE Vendas
                   SET Ativo = 0,
                       MotivoCancelamento = @Motivo,
                       DataAtualizacao = SYSUTCDATETIME()
                 WHERE Modelo       = @Modelo
                   AND Serie        = @Serie
                   AND NumeroNota   = @NumeroNota
                   AND IdCliente    = @IdCliente;", conn, tx))
                        {
                            cmd.Parameters.Add("@Modelo", SqlDbType.VarChar, 10).Value = vendaKey.Modelo;
                            cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 10).Value = vendaKey.Serie;
                            cmd.Parameters.Add("@NumeroNota", SqlDbType.VarChar, 20).Value = vendaKey.NumeroNota;
                            cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = vendaKey.IdCliente;
                            cmd.Parameters.Add("@Motivo", SqlDbType.NVarChar, 4000).Value =
                                (object)motivoEfetivo ?? DBNull.Value;

                            linhasVenda = cmd.ExecuteNonQuery();
                        }

                        if (linhasVenda == 0)
                            throw new InvalidOperationException("Venda não encontrada ou já cancelada.");

                        // ------------------------------------------------------------------
                        // 2) Cancelar PARCELAS vinculadas (ContasAReceber)
                        // ------------------------------------------------------------------
                        using (var cmd = new SqlCommand(@"
UPDATE ContasAReceber
   SET Ativo = 0,
       Situacao = 'Cancelada',
       DataAtualizacao = SYSUTCDATETIME()
 WHERE Modelo       = @Modelo
   AND Serie        = @Serie
   AND NumeroNota   = @NumeroNota
   AND IdCliente    = @IdCliente;", conn, tx))
                        {
                            cmd.Parameters.Add("@Modelo", SqlDbType.VarChar, 10).Value = vendaKey.Modelo;
                            cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 10).Value = vendaKey.Serie;
                            cmd.Parameters.Add("@NumeroNota", SqlDbType.VarChar, 20).Value = vendaKey.NumeroNota;
                            cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = vendaKey.IdCliente;

                            cmd.ExecuteNonQuery();
                        }

                        // ------------------------------------------------------------------
                        // 3) Inativar ITENS vinculados à venda
                        // ------------------------------------------------------------------
                        using (var cmd = new SqlCommand(@"
UPDATE ItensVenda
   SET Ativo = 0,
       DataAtualizacao = SYSUTCDATETIME()
 WHERE Modelo       = @Modelo
   AND Serie        = @Serie
   AND NumeroNota   = @NumeroNota
   AND IdCliente    = @IdCliente;", conn, tx))
                        {
                            cmd.Parameters.Add("@Modelo", SqlDbType.VarChar, 10).Value = vendaKey.Modelo;
                            cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 10).Value = vendaKey.Serie;
                            cmd.Parameters.Add("@NumeroNota", SqlDbType.VarChar, 20).Value = vendaKey.NumeroNota;
                            cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = vendaKey.IdCliente;

                            cmd.ExecuteNonQuery();
                        }

                        // ------------------------------------------------------------------
                        // 4) Recarregar ITENS da venda para ajustar estoque
                        // ------------------------------------------------------------------
                        var itens = new List<ItemVenda>();
                        using (var cmd = new SqlCommand(@"
                SELECT i.IdProduto, i.Quantidade, i.ValorUnitario
                  FROM ItensVenda i
                 WHERE i.Modelo       = @Modelo
                   AND i.Serie        = @Serie
                   AND i.NumeroNota   = @NumeroNota
                   AND i.IdCliente    = @IdCliente;", conn, tx))
                        {
                            cmd.Parameters.Add("@Modelo", SqlDbType.VarChar, 10).Value = vendaKey.Modelo;
                            cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 10).Value = vendaKey.Serie;
                            cmd.Parameters.Add("@NumeroNota", SqlDbType.VarChar, 20).Value = vendaKey.NumeroNota;
                            cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = vendaKey.IdCliente;

                            using (var r = cmd.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    itens.Add(new ItemVenda
                                    {
                                        IdProduto = r.GetInt32(0),
                                        Quantidade = Convert.ToDecimal(r["Quantidade"]),
                                        ValorUnitario = Convert.ToDecimal(r["ValorUnitario"])
                                    });
                                }
                            }
                        }

                        // ------------------------------------------------------------------
                        // 5) Para cada produto, DEVOLVER ao estoque
                        // ------------------------------------------------------------------
                        foreach (var item in itens.GroupBy(x => x.IdProduto)
                                                  .Select(g => g.First()))
                        {
                            using (var cmd = new SqlCommand(@"
UPDATE Produtos
   SET Estoque = Estoque + @Qtd,
       DataAtualizacao = SYSUTCDATETIME()
 WHERE Id = @IdProduto;", conn, tx))
                            {
                                cmd.Parameters.Add("@Qtd", SqlDbType.Decimal).Value = item.Quantidade;
                                cmd.Parameters["@Qtd"].Precision = 18;
                                cmd.Parameters["@Qtd"].Scale = 3;
                                cmd.Parameters.Add("@IdProduto", SqlDbType.Int).Value = item.IdProduto;

                                cmd.ExecuteNonQuery();
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
        }
        public bool NotaJaExiste(int modelo, string serie, string numero, int idCliente)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = @"
        SELECT TOP 1 1
        FROM Vendas
        WHERE Modelo = @Modelo
          AND Serie = @Serie
          AND NumeroNota = @Numero
          AND IdCliente = @Cliente
          AND Ativo = 1;";

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

                    cmd.Parameters.AddWithValue("@Cliente", idCliente);

                    conn.Open();
                    return cmd.ExecuteScalar() != null;
                }
            }
        }
        public ProjetoPF.Modelos.Venda.Venda BuscarPorNota(int modelo, string serie, string numero, int idCliente)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(@"
        SELECT TOP 1 *
        FROM Vendas
        WHERE Modelo = @Modelo
          AND Serie = @Serie
          AND NumeroNota = @Numero
          AND IdCliente = @Cliente
          AND Ativo = 1;", conn))
                {
                    cmd.Parameters.AddWithValue("@Modelo", modelo);
                    cmd.Parameters.AddWithValue("@Serie", serie.Trim());
                    cmd.Parameters.AddWithValue("@Numero", numero.Trim());
                    cmd.Parameters.AddWithValue("@Cliente", idCliente);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new ProjetoPF.Modelos.Venda.Venda
                            {
                                Modelo = reader["Modelo"].ToString(),
                                Serie = reader["Serie"].ToString(),
                                NumeroNota = reader["NumeroNota"].ToString(),
                                IdCliente = Convert.ToInt32(reader["IdCliente"]),
                                IdCondicaoPagamento = Convert.ToInt32(reader["IdCondicaoPagamento"]),
                                DataEmissao = Convert.ToDateTime(reader["DataEmissao"]),
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
        public ProjetoPF.Modelos.Venda.Venda BuscarPorChave(VendaKey key)
        {
            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
SELECT TOP 1 
    Modelo,
    Serie,
    NumeroNota,
    DataEmissao,
    IdCliente,
    IdFuncionario,          -- ✅ adicionado
    IdCondicaoPagamento,
    ValorTotal,
    Observacao,
    DataCriacao,
    DataAtualizacao,
    Ativo,
    MotivoCancelamento
FROM dbo.Vendas
WHERE Modelo = @Modelo 
  AND Serie = @Serie 
  AND NumeroNota = @NumeroNota 
  AND IdCliente = @IdCliente;", cn))
            {
                cmd.Parameters.Add("@Modelo", SqlDbType.TinyInt).Value = Convert.ToInt32(key.Modelo);
                cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 5).Value = key.Serie.Trim();
                cmd.Parameters.Add("@NumeroNota", SqlDbType.VarChar, 20).Value = key.NumeroNota.Trim();
                cmd.Parameters.Add("@IdCliente", SqlDbType.Int).Value = key.IdCliente;

                cn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ProjetoPF.Modelos.Venda.Venda
                        {
                            Modelo = reader["Modelo"]?.ToString(),
                            Serie = reader["Serie"]?.ToString(),
                            NumeroNota = reader["NumeroNota"]?.ToString(),

                            DataEmissao = reader["DataEmissao"] != DBNull.Value
                                ? Convert.ToDateTime(reader["DataEmissao"])
                                : DateTime.MinValue,

                            IdCliente = reader["IdCliente"] != DBNull.Value
                                ? Convert.ToInt32(reader["IdCliente"])
                                : 0,

                            IdFuncionario = reader["IdFuncionario"] != DBNull.Value
                                ? Convert.ToInt32(reader["IdFuncionario"])
                                : 0,

                            IdCondicaoPagamento = reader["IdCondicaoPagamento"] != DBNull.Value
                                ? Convert.ToInt32(reader["IdCondicaoPagamento"])
                                : 0,

                            ValorTotal = reader["ValorTotal"] != DBNull.Value
                                ? Convert.ToDecimal(reader["ValorTotal"])
                                : 0,

                            DataCriacao = reader["DataCriacao"] != DBNull.Value
                                ? Convert.ToDateTime(reader["DataCriacao"])
                                : DateTime.MinValue,

                            DataAtualizacao = reader["DataAtualizacao"] != DBNull.Value
                                ? Convert.ToDateTime(reader["DataAtualizacao"])
                                : DateTime.MinValue,

                            Ativo = reader["Ativo"] != DBNull.Value && Convert.ToBoolean(reader["Ativo"]),

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

        public bool ExisteContaComMesmoTitulo(int modelo, string serie, string numero, int idCliente)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = @"
        SELECT TOP 1 1
        FROM ContasAReceber
        WHERE Modelo = @Modelo
          AND Serie = @Serie
          AND NumeroNota = @Numero
          AND IdCliente = @Cliente
          AND Ativo = 1;";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Modelo", modelo);
                    cmd.Parameters.AddWithValue("@Serie", serie.Trim());
                    cmd.Parameters.AddWithValue("@Numero", numero.Trim());
                    cmd.Parameters.AddWithValue("@Cliente", idCliente);

                    conn.Open();
                    return cmd.ExecuteScalar() != null;
                }
            }
        }
        public int ObterProximoNumeroNota(int modelo, string serie)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand(
                    "SELECT ISNULL(MAX(CAST(NumeroNota AS INT)), 0) + 1 FROM Vendas WHERE Modelo = @modelo AND Serie = @serie", conn))
                {
                    cmd.Parameters.AddWithValue("@modelo", modelo);
                    cmd.Parameters.AddWithValue("@serie", serie);
                    var result = cmd.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
        }

    }
}
