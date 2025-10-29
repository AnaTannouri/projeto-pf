using ProjetoPF.Dao.Compra;
using ProjetoPF.Model;
using ProjetoPF.Modelos;
using ProjetoPF.Modelos.Compra;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using IsolationLevel = System.Transactions.IsolationLevel;

namespace ProjetoPF.Dao.Compras
{
    public class CompraDao : BaseDao<ProjetoPF.Modelos.Compra.Compra>
    {
        public CompraDao() : base("Compras") { }
        public List<CompraCabecalho> BuscarTodosCabecalho(string filtro = "")
        {
            var dao = new BaseDao<CompraCabecalho>("Compras");
            var compras = dao.BuscarTodosWithoutId<CompraCabecalho>(filtro: filtro, orderBy: "DataCriacao");
            return compras;
        }
        public void SalvarCompraComItensParcelas(
    ProjetoPF.Modelos.Compra.Compra compra,
    List<ProjetoPF.Modelos.Compra.ItemCompra> itens,
    List<ProjetoPF.Modelos.Compra.ContasAPagar> parcelas)
        {
            if (compra == null) throw new Exception("Compra inválida.");
            if (itens == null || itens.Count == 0) throw new Exception("Inclua ao menos um item.");

            if (string.IsNullOrWhiteSpace(compra.Modelo)) throw new InvalidOperationException("Modelo inválido.");
            if (string.IsNullOrWhiteSpace(compra.Serie)) throw new InvalidOperationException("Série obrigatória.");
            if (string.IsNullOrWhiteSpace(compra.NumeroNota)) throw new InvalidOperationException("Número da nota obrigatória.");
            if (compra.IdFornecedor <= 0) throw new InvalidOperationException("Fornecedor inválido.");

            var agora = DateTime.Now;

            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        // 1) Inserir COMPRA (PK composta; sem SCOPE_IDENTITY)
                        const string sqlCompra = @"
INSERT INTO dbo.Compras
  (Modelo, Serie, NumeroNota, DataEmissao, DataEntrega, IdFornecedor,
   IdCondicaoPagamento, ValorFrete, ValorSeguro, OutrasDespesas, ValorTotal,
   Observacao, DataCriacao, DataAtualizacao, Ativo)
VALUES
  (@Modelo, @Serie, @NumeroNota, @DataEmissao, @DataEntrega, @IdFornecedor,
   @IdCondicaoPagamento, @ValorFrete, @ValorSeguro, @OutrasDespesas, @ValorTotal,
   @Observacao, @DataCriacao, @DataAtualizacao, @Ativo);";

                        using (var cmd = new SqlCommand(sqlCompra, conn, tx))
                        {
                            cmd.Parameters.Add("@Modelo", SqlDbType.TinyInt).Value = compra.Modelo;
                            cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 5).Value = compra.Serie.Trim();
                            cmd.Parameters.Add("@NumeroNota", SqlDbType.VarChar, 20).Value = compra.NumeroNota.Trim();
                            cmd.Parameters.Add("@DataEmissao", SqlDbType.DateTime).Value = compra.DataEmissao;
                            cmd.Parameters.Add("@DataEntrega", SqlDbType.DateTime).Value = compra.DataEntrega;
                            cmd.Parameters.Add("@IdFornecedor", SqlDbType.Int).Value = compra.IdFornecedor;
                            cmd.Parameters.Add("@IdCondicaoPagamento", SqlDbType.Int).Value = compra.IdCondicaoPagamento;

                            var pFrete = cmd.Parameters.Add("@ValorFrete", SqlDbType.Decimal); pFrete.Precision = 18; pFrete.Scale = 2; pFrete.Value = compra.ValorFrete;
                            var pSeg = cmd.Parameters.Add("@ValorSeguro", SqlDbType.Decimal); pSeg.Precision = 18; pSeg.Scale = 2; pSeg.Value = compra.ValorSeguro;
                            var pDesp = cmd.Parameters.Add("@OutrasDespesas", SqlDbType.Decimal); pDesp.Precision = 18; pDesp.Scale = 2; pDesp.Value = compra.OutrasDespesas;
                            var pTot = cmd.Parameters.Add("@ValorTotal", SqlDbType.Decimal); pTot.Precision = 18; pTot.Scale = 2; pTot.Value = compra.ValorTotal;

                            cmd.Parameters.Add("@Observacao", SqlDbType.VarChar).Value = (object)compra.Observacao ?? DBNull.Value;
                            cmd.Parameters.Add("@DataCriacao", SqlDbType.DateTime).Value = agora;
                            cmd.Parameters.Add("@DataAtualizacao", SqlDbType.DateTime).Value = agora;
                            cmd.Parameters.Add("@Ativo", SqlDbType.Bit).Value = compra.Ativo;

                            if (cmd.ExecuteNonQuery() != 1)
                                throw new InvalidOperationException("Falha ao inserir a compra.");
                        }

                        // 2) Inserir ITENS
                        const string sqlItem = @"
INSERT INTO dbo.ItensCompra
  (Modelo, Serie, NumeroNota, IdFornecedor,
   Total, IdProduto, Quantidade, ValorUnitario,
   DataCriacao, DataAtualizacao, Ativo)
VALUES
  (@Modelo, @Serie, @NumeroNota, @IdFornecedor,
   @Total, @IdProduto, @Quantidade, @ValorUnitario,
   @DataCriacao, @DataAtualizacao, @Ativo);";

                        using (var cmdItem = new SqlCommand(sqlItem, conn, tx))
                        {
                            cmdItem.Parameters.Add("@Modelo", SqlDbType.TinyInt);
                            cmdItem.Parameters.Add("@Serie", SqlDbType.VarChar, 5);
                            cmdItem.Parameters.Add("@NumeroNota", SqlDbType.VarChar, 20);
                            cmdItem.Parameters.Add("@IdFornecedor", SqlDbType.Int);
                            cmdItem.Parameters.Add("@IdProduto", SqlDbType.Int);

                            var pQtd = cmdItem.Parameters.Add("@Quantidade", SqlDbType.Decimal); pQtd.Precision = 18; pQtd.Scale = 3;
                            var pVlr = cmdItem.Parameters.Add("@ValorUnitario", SqlDbType.Decimal); pVlr.Precision = 18; pVlr.Scale = 2;
                            var pVlrTotal = cmdItem.Parameters.Add("@Total", SqlDbType.Decimal); pVlrTotal.Precision = 18; pVlrTotal.Scale = 2;

                            cmdItem.Parameters.Add("@DataCriacao", SqlDbType.DateTime);
                            cmdItem.Parameters.Add("@DataAtualizacao", SqlDbType.DateTime);
                            cmdItem.Parameters.Add("@Ativo", SqlDbType.Bit);

                            int seq = 1;
                            foreach (var it in itens)
                            {
                                cmdItem.Parameters["@Modelo"].Value = compra.Modelo;
                                cmdItem.Parameters["@Serie"].Value = compra.Serie.Trim();
                                cmdItem.Parameters["@NumeroNota"].Value = compra.NumeroNota.Trim();
                                cmdItem.Parameters["@IdFornecedor"].Value = compra.IdFornecedor;

                                cmdItem.Parameters["@IdProduto"].Value = it.IdProduto;
                                cmdItem.Parameters["@Quantidade"].Value = it.Quantidade;
                                cmdItem.Parameters["@ValorUnitario"].Value = it.ValorUnitario;
                                cmdItem.Parameters["@Total"].Value = it.Total;

                                cmdItem.Parameters["@DataCriacao"].Value = agora;
                                cmdItem.Parameters["@DataAtualizacao"].Value = agora;
                                cmdItem.Parameters["@Ativo"].Value = true;

                                cmdItem.ExecuteNonQuery();
                            }
                        }

                        // 3) Inserir PARCELAS
                        if (parcelas != null && parcelas.Count > 0)
                        {
                            const string sqlParc = @"
INSERT INTO dbo.ContasAPagar
  (Modelo, Serie, NumeroNota, IdFornecedor,
   NumeroParcela, DataEmissao, DataVencimento, ValorParcela, IdFormaPagamento,
   DataCriacao, DataAtualizacao, Ativo, Situacao)
VALUES
  (@Modelo, @Serie, @NumeroNota, @IdFornecedor,
   @NumeroParcela, @DataEmissao, @DataVencimento, @ValorParcela, @IdFormaPagamento,
   @DataCriacao, @DataAtualizacao, @Ativo, @Situacao);";

                            using (var cmdParc = new SqlCommand(sqlParc, conn, tx))
                            {
                                cmdParc.Parameters.Add("@Modelo", SqlDbType.TinyInt);
                                cmdParc.Parameters.Add("@Serie", SqlDbType.VarChar, 5);
                                cmdParc.Parameters.Add("@NumeroNota", SqlDbType.VarChar, 20);
                                cmdParc.Parameters.Add("@IdFornecedor", SqlDbType.Int);
                                cmdParc.Parameters.Add("@NumeroParcela", SqlDbType.Int);
                                cmdParc.Parameters.Add("@DataEmissao", SqlDbType.DateTime); // ✅ novo
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
                                    cmdParc.Parameters["@Modelo"].Value = compra.Modelo;
                                    cmdParc.Parameters["@Serie"].Value = compra.Serie.Trim();
                                    cmdParc.Parameters["@NumeroNota"].Value = compra.NumeroNota.Trim();
                                    cmdParc.Parameters["@IdFornecedor"].Value = compra.IdFornecedor;

                                    cmdParc.Parameters["@NumeroParcela"].Value = p.NumeroParcela > 0 ? p.NumeroParcela : n++;
                                    cmdParc.Parameters["@DataEmissao"].Value = compra.DataEmissao; // ✅ adicionado
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
        public void CancelarCompra(CompraKey compraKey, string motivo)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (var tx = conn.BeginTransaction())
                {
                    try
                    {
                        // Normaliza motivo
                        string motivoEfetivo = string.IsNullOrWhiteSpace(motivo)
                            ? "Cancelamento não informado"
                            : motivo.Trim();

                        // ------------------------------------------------------------------
                        // 1) Cancelar a COMPRA pela PK composta (nada de Id)
                        //    - Se já estava cancelada ou não existe, o número de linhas será 0
                        // ------------------------------------------------------------------
                        int linhasCompra;
                        using (var cmd = new SqlCommand(@"
                        UPDATE Compras
                           SET Ativo = 0,
                               MotivoCancelamento = @Motivo,
                               DataAtualizacao = SYSUTCDATETIME()
                         WHERE Modelo       = @Modelo
                           AND Serie        = @Serie
                           AND NumeroNota   = @NumeroNota
                           AND IdFornecedor = @IdFornecedor;", conn, tx))
                        {
                            cmd.Parameters.Add("@Modelo", SqlDbType.VarChar, 10).Value = compraKey.Modelo;
                            cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 10).Value = compraKey.Serie;
                            cmd.Parameters.Add("@NumeroNota", SqlDbType.VarChar, 20).Value = compraKey.NumeroNota;
                            cmd.Parameters.Add("@IdFornecedor", SqlDbType.Int).Value = compraKey.IdFornecedor;
                            cmd.Parameters.Add("@Motivo", SqlDbType.NVarChar, 4000).Value = (object)motivoEfetivo ?? DBNull.Value;

                            linhasCompra = cmd.ExecuteNonQuery();
                        }

                        if (linhasCompra == 0)
                            throw new InvalidOperationException("Compra não encontrada ou já cancelada.");

                        // ------------------------------------------------------------------
                        // 2) Inativar PARCELAS vinculadas à compra (mesma PK composta)
                        // ------------------------------------------------------------------
                        using (var cmd = new SqlCommand(@"
UPDATE ContasAPagar
   SET Ativo = 0,
       Situacao = 'Cancelada',
       DataAtualizacao = SYSUTCDATETIME()
 WHERE Modelo       = @Modelo
   AND Serie        = @Serie
   AND NumeroNota   = @NumeroNota
   AND IdFornecedor = @IdFornecedor;", conn, tx))
                        {
                            cmd.Parameters.Add("@Modelo", SqlDbType.VarChar, 10).Value = compraKey.Modelo;
                            cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 10).Value = compraKey.Serie;
                            cmd.Parameters.Add("@NumeroNota", SqlDbType.VarChar, 20).Value = compraKey.NumeroNota;
                            cmd.Parameters.Add("@IdFornecedor", SqlDbType.Int).Value = compraKey.IdFornecedor;

                            cmd.ExecuteNonQuery();
                        }

                        // ------------------------------------------------------------------
                        // 3) Inativar ITENS vinculados à compra (mesma PK composta)
                        // ------------------------------------------------------------------
                        using (var cmd = new SqlCommand(@"
                        UPDATE ItensCompra
                           SET Ativo = 0,
                               DataAtualizacao = SYSUTCDATETIME()
                         WHERE Modelo       = @Modelo
                           AND Serie        = @Serie
                           AND NumeroNota   = @NumeroNota
                           AND IdFornecedor = @IdFornecedor;", conn, tx))
                        {
                            cmd.Parameters.Add("@Modelo", SqlDbType.VarChar, 10).Value = compraKey.Modelo;
                            cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 10).Value = compraKey.Serie;
                            cmd.Parameters.Add("@NumeroNota", SqlDbType.VarChar, 20).Value = compraKey.NumeroNota;
                            cmd.Parameters.Add("@IdFornecedor", SqlDbType.Int).Value = compraKey.IdFornecedor;

                            cmd.ExecuteNonQuery();
                        }

                        // ------------------------------------------------------------------
                        // 4) Recarregar ITENS da compra (para recalcular custos por produto)
                        //    Observação: usa a mesma conexão/transação.
                        // ------------------------------------------------------------------
                        var itens = new List<ItemCompra>();
                        using (var cmd = new SqlCommand(@"
                        SELECT i.IdProduto, i.Quantidade, i.ValorUnitario
                          FROM ItensCompra i
                         WHERE i.Modelo       = @Modelo
                           AND i.Serie        = @Serie
                           AND i.NumeroNota   = @NumeroNota
                           AND i.IdFornecedor = @IdFornecedor;", conn, tx))
                        {
                            cmd.Parameters.Add("@Modelo", SqlDbType.VarChar, 10).Value = compraKey.Modelo;
                            cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 10).Value = compraKey.Serie;
                            cmd.Parameters.Add("@NumeroNota", SqlDbType.VarChar, 20).Value = compraKey.NumeroNota;
                            cmd.Parameters.Add("@IdFornecedor", SqlDbType.Int).Value = compraKey.IdFornecedor;

                            using (var r = cmd.ExecuteReader())
                            {
                                while (r.Read())
                                {
                                    itens.Add(new ItemCompra
                                    {
                                        IdProduto = r.GetInt32(0),
                                        Quantidade = Convert.ToDecimal(r["Quantidade"]),
                                        ValorUnitario = Convert.ToDecimal(r["ValorUnitario"])
                                    });
                                }
                            }
                        }

                        // ------------------------------------------------------------------
                        // 5) Para cada produto afetado:
                        //    - Recalcular "último custo" (última compra ATIVA diferente da cancelada)
                        //    - Recalcular "custo médio" (somente compras ATIVAS ≠ cancelada)
                        //    - Atualizar tabelas de apoio e Produtos
                        // ------------------------------------------------------------------
                        foreach (var item in itens.GroupBy(x => x.IdProduto)
                                                   .Select(g => g.First())) // ou g.OrderByDescending(i => i.AlgumCampo).First()
                        {
                            // 5.1) Último custo (TOP 1 pela data de emissão, excluindo a compra cancelada e só Ativo=1)
                            decimal ultimoCusto = 0m;
                            using (var cmd = new SqlCommand(@"
                            SELECT TOP(1) CAST(i.ValorUnitario AS DECIMAL(18,6)) AS UltimoCusto
                              FROM ItensCompra i
                              JOIN Compras c
                                ON c.Modelo       = i.Modelo
                               AND c.Serie        = i.Serie
                               AND c.NumeroNota   = i.NumeroNota
                               AND c.IdFornecedor = i.IdFornecedor
                             WHERE i.IdProduto = @IdProduto
                               AND c.Ativo = 1
                               AND NOT (
                                     c.Modelo       = @ModeloEx
                                 AND c.Serie        = @SerieEx
                                 AND c.NumeroNota   = @NumeroEx
                                 AND c.IdFornecedor = @FornecedorEx
                               )
                             ORDER BY c.DataEmissao DESC, c.DataCriacao DESC;", conn, tx))
                            {
                                cmd.Parameters.Add("@IdProduto", SqlDbType.Int).Value = item.IdProduto;
                                cmd.Parameters.Add("@ModeloEx", SqlDbType.VarChar, 10).Value = compraKey.Modelo;
                                cmd.Parameters.Add("@SerieEx", SqlDbType.VarChar, 10).Value = compraKey.Serie;
                                cmd.Parameters.Add("@NumeroEx", SqlDbType.VarChar, 20).Value = compraKey.NumeroNota;
                                cmd.Parameters.Add("@FornecedorEx", SqlDbType.Int).Value = compraKey.IdFornecedor;

                                var escalar = cmd.ExecuteScalar();
                                if (escalar != null && escalar != DBNull.Value)
                                    ultimoCusto = Convert.ToDecimal(escalar);
                            }

                            // 5.2) Custo médio (somente compras ATIVAS, excluindo a cancelada)
                            //      Reutilize seu método existente que já exclui a compraKey:
                            decimal custoMedio = CalcularCustoMedioProduto(item.IdProduto, compraKey, conn, tx);

                            // 5.3) Atualizar Produtos (último custo + custo médio)
                            using (var cmd = new SqlCommand(@"
UPDATE Produtos
   SET CustoUltimaCompra = @CustoUltima,
       DataAtualizacao   = SYSUTCDATETIME()
 WHERE Id = @IdProduto;", conn, tx))
                            {
                                cmd.Parameters.Add("@CustoUltima", SqlDbType.Decimal).Value = ultimoCusto;
                                cmd.Parameters["@CustoUltima"].Precision = 18;
                                cmd.Parameters["@CustoUltima"].Scale = 6;

                                cmd.Parameters.Add("@IdProduto", SqlDbType.Int).Value = item.IdProduto;
                                cmd.ExecuteNonQuery();
                            }

                            // 5.4) (Opcional) Atualizar preço na tabela ProdutoFornecedor, se você usa essa relação.
                            //      Ideal: criar uma sobrecarga que receba conn/tx para manter atomicidade.
                            // new ProdutoFornecedorDAO().AtualizarPrecoUltimaCompra(item.IdProduto, compraKey.IdFornecedor, ultimoCusto);
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

        public bool NotaJaExiste(int modelo, string serie, string numero, int idFornecedor)
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
                                Modelo = reader["Modelo"].ToString(),
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

        public decimal CalcularCustoMedioProduto(int idProduto, CompraKey key)
        {
            // Wrapper: abre a própria conexão quando você está fora de uma transação
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                // Sem transação aqui (tx = null)
                return CalcularCustoMedioProduto(idProduto, key, conn, null);
            }
        }

        // Core: usa a MESMA conexão/tx do chamador (evita bloqueio/timeout)
        public decimal CalcularCustoMedioProduto(int idProduto, CompraKey key, SqlConnection conn, SqlTransaction tx)
        {
            const string sql = @"
SELECT CustoMedio =
    CAST(
        SUM(CAST(i.Quantidade AS DECIMAL(18,6)) * CAST(i.ValorUnitario AS DECIMAL(18,6)))
        / NULLIF(SUM(CAST(i.Quantidade AS DECIMAL(18,6))), 0)
    AS DECIMAL(18,2))
FROM ItensCompra i
JOIN Compras c
  ON c.Modelo       = i.Modelo
 AND c.Serie        = i.Serie
 And c.NumeroNota   = i.NumeroNota
 And c.IdFornecedor = i.IdFornecedor
WHERE i.IdProduto = @IdProduto
  AND c.Ativo = 1
  AND NOT (
        c.Modelo       = @Modelo
    AND c.Serie        = @Serie
    AND c.NumeroNota   = @NumeroNota
    AND c.IdFornecedor = @IdFornecedor
);";

            using (var cmd = new SqlCommand(sql, conn, tx))
            {
                cmd.Parameters.Add("@IdProduto", SqlDbType.Int).Value = idProduto;

                // ⚠️ Ajuste os tipos/tamanhos para os do seu schema:
                cmd.Parameters.Add("@Modelo", SqlDbType.VarChar, 10).Value = key.Modelo;
                cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 10).Value = key.Serie;
                cmd.Parameters.Add("@NumeroNota", SqlDbType.VarChar, 20).Value = key.NumeroNota;
                cmd.Parameters.Add("@IdFornecedor", SqlDbType.Int).Value = key.IdFornecedor;

                var v = cmd.ExecuteScalar();
                return (v == null || v == DBNull.Value) ? 0m : (decimal)v;
            }
        }

        public ProjetoPF.Modelos.Compra.Compra BuscarPorChave(CompraKey key)
        {
            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
SELECT TOP 1 *
FROM dbo.Compras
WHERE Modelo = @Modelo 
  AND Serie = @Serie 
  AND NumeroNota = @NumeroNota 
  AND IdFornecedor = @IdFornecedor;", cn))
            {
                cmd.Parameters.Add("@Modelo", SqlDbType.VarChar, 2).Value = key.Modelo.Trim();
                cmd.Parameters.Add("@Serie", SqlDbType.VarChar, 5).Value = key.Serie.Trim();
                cmd.Parameters.Add("@NumeroNota", SqlDbType.VarChar, 20).Value = key.NumeroNota.Trim();
                cmd.Parameters.Add("@IdFornecedor", SqlDbType.Int).Value = key.IdFornecedor;

                cn.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new ProjetoPF.Modelos.Compra.Compra
                        {
                            Modelo = reader["Modelo"]?.ToString(),
                            Serie = reader["Serie"]?.ToString(),
                            NumeroNota = reader["NumeroNota"]?.ToString(),
                            DataEmissao = reader["DataEmissao"] != DBNull.Value ? Convert.ToDateTime(reader["DataEmissao"]) : DateTime.MinValue,
                            DataEntrega = reader["DataEntrega"] != DBNull.Value ? Convert.ToDateTime(reader["DataEntrega"]) : DateTime.MinValue,
                            IdFornecedor = reader["IdFornecedor"] != DBNull.Value ? Convert.ToInt32(reader["IdFornecedor"]) : 0,
                            IdCondicaoPagamento = reader["IdCondicaoPagamento"] != DBNull.Value ? Convert.ToInt32(reader["IdCondicaoPagamento"]) : 0,
                            ValorFrete = reader["ValorFrete"] != DBNull.Value ? Convert.ToDecimal(reader["ValorFrete"]) : 0,
                            ValorSeguro = reader["ValorSeguro"] != DBNull.Value ? Convert.ToDecimal(reader["ValorSeguro"]) : 0,
                            OutrasDespesas = reader["OutrasDespesas"] != DBNull.Value ? Convert.ToDecimal(reader["OutrasDespesas"]) : 0,
                            ValorTotal = reader["ValorTotal"] != DBNull.Value ? Convert.ToDecimal(reader["ValorTotal"]) : 0,
                            DataCriacao = reader["DataCriacao"] != DBNull.Value ? Convert.ToDateTime(reader["DataCriacao"]) : DateTime.MinValue,
                            DataAtualizacao = reader["DataAtualizacao"] != DBNull.Value ? Convert.ToDateTime(reader["DataAtualizacao"]) : DateTime.MinValue,
                            Ativo = reader["Ativo"] != DBNull.Value && Convert.ToBoolean(reader["Ativo"]),
                            MotivoCancelamento = reader["MotivoCancelamento"] == DBNull.Value ? null : reader["MotivoCancelamento"].ToString(),
                            Observacao = reader["Observacao"] == DBNull.Value ? null : reader["Observacao"].ToString()
                        };
                    }
                }
            }

            return null;
        }
        public bool ExisteContaComMesmoTitulo(int modelo, string serie, string numero, int idFornecedor)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                string sql = @"
            SELECT TOP 1 1
            FROM ContasAPagar
            WHERE Modelo = @Modelo
              AND Serie = @Serie
              AND NumeroNota = @Numero
              AND IdFornecedor = @Fornecedor
              AND Ativo = 1";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Modelo", modelo);
                    cmd.Parameters.AddWithValue("@Serie", serie.Trim());
                    cmd.Parameters.AddWithValue("@Numero", numero.Trim());
                    cmd.Parameters.AddWithValue("@Fornecedor", idFornecedor);

                    conn.Open();
                    return cmd.ExecuteScalar() != null;
                }
            }
        }

    }
}