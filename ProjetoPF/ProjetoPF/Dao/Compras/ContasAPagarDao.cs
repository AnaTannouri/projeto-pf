using ProjetoPF.Modelos;
using ProjetoPF.Modelos.Compra;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace ProjetoPF.Dao.Compras
{
    public class ContasAPagarDao : BaseDao<ContasAPagar>
    {
        public ContasAPagarDao() : base("ContasAPagar") { }
        public List<ContasAPagar> BuscarPorChaveCompra(string modelo, string serie, string numeroNota, int idFornecedor)
        {
            var parcelas = new List<ContasAPagar>();

            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
                SELECT 
                    c.NumeroParcela,
                    c.DataVencimento,
                    c.ValorParcela,
                    c.IdFormaPagamento,
                    f.Descricao AS FormaPagamentoDescricao,
                    c.DataCriacao,
                    c.DataAtualizacao,
                    c.Ativo
                FROM ContasAPagar c
                LEFT JOIN FormaPagamentos f ON f.Id = c.IdFormaPagamento
                WHERE c.Modelo = @Modelo 
                  AND c.Serie = @Serie 
                  AND c.NumeroNota = @NumeroNota 
                  AND c.IdFornecedor = @IdFornecedor
                ORDER BY c.NumeroParcela", cn))
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
                        parcelas.Add(new ContasAPagar
                        {
                            NumeroParcela = reader["NumeroParcela"] != DBNull.Value
                                ? Convert.ToInt32(reader["NumeroParcela"])
                                : 0,
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
                                    (reader["Ativo"] is bool b ? b : Convert.ToInt32(reader["Ativo"]) == 1),
                            FormaPagamentoDescricao = reader["FormaPagamentoDescricao"] != DBNull.Value
                                ? reader["FormaPagamentoDescricao"].ToString()
                                : "-"
                        });
                    }
                }
            }
            return parcelas;
        }
        public bool ExisteContaPagaAssociada(string modelo, string serie, string numeroNota, int idFornecedor)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = @"
            SELECT COUNT(*) 
            FROM ContasAPagar
            WHERE Modelo = @Modelo
              AND Serie = @Serie
              AND NumeroNota = @NumeroNota
              AND IdFornecedor = @IdFornecedor
              AND Situacao = 'Paga';";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Modelo", modelo);
                    cmd.Parameters.AddWithValue("@Serie", serie);
                    cmd.Parameters.AddWithValue("@NumeroNota", numeroNota);
                    cmd.Parameters.AddWithValue("@IdFornecedor", idFornecedor);

                    int qtd = Convert.ToInt32(cmd.ExecuteScalar());
                    return qtd > 0;
                }
            }
        }

        public void SalvarOuAtualizar(ContasAPagar conta)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sqlVerifica = @"
SELECT COUNT(*) FROM ContasAPagar
 WHERE Modelo = @Modelo
   AND Serie = @Serie
   AND NumeroNota = @NumeroNota
   AND IdFornecedor = @IdFornecedor
   AND NumeroParcela = @NumeroParcela;";

                using (var cmdVerifica = new SqlCommand(sqlVerifica, conn))
                {
                    cmdVerifica.Parameters.AddWithValue("@Modelo", conta.Modelo);
                    cmdVerifica.Parameters.AddWithValue("@Serie", conta.Serie);
                    cmdVerifica.Parameters.AddWithValue("@NumeroNota", conta.NumeroNota);
                    cmdVerifica.Parameters.AddWithValue("@IdFornecedor", conta.IdFornecedor);
                    cmdVerifica.Parameters.AddWithValue("@NumeroParcela", conta.NumeroParcela);

                    int existe = (int)cmdVerifica.ExecuteScalar();

                    if (existe > 0)
                    {
                       
                        string sqlUpdate = @"
UPDATE ContasAPagar
   SET 
       Multa = @Multa,
       Juros = @Juros,
       Desconto = @Desconto,
       MultaValor = @MultaValor,
       JurosValor = @JurosValor,
       DescontoValor = @DescontoValor,
       ValorFinalParcela = @ValorFinalParcela,
       DataPagamento = @DataPagamento,
       Situacao = @Situacao,
       Observacao = @Observacao,
       DataAtualizacao = @DataAtualizacao
 WHERE Modelo = @Modelo
   AND Serie = @Serie
   AND NumeroNota = @NumeroNota
   AND IdFornecedor = @IdFornecedor
   AND NumeroParcela = @NumeroParcela;";

                        using (var cmd = new SqlCommand(sqlUpdate, conn))
                        {
                            cmd.Parameters.AddWithValue("@Multa", conta.Multa);
                            cmd.Parameters.AddWithValue("@Juros", conta.Juros);
                            cmd.Parameters.AddWithValue("@Desconto", conta.Desconto);

                            cmd.Parameters.AddWithValue("@MultaValor", conta.MultaValor);
                            cmd.Parameters.AddWithValue("@JurosValor", conta.JurosValor);
                            cmd.Parameters.AddWithValue("@DescontoValor", conta.DescontoValor);

                            cmd.Parameters.AddWithValue("@ValorFinalParcela", conta.ValorFinalParcela);
                            cmd.Parameters.AddWithValue("@DataPagamento", CorrigirDataSql(conta.DataPagamento));
                            cmd.Parameters.AddWithValue("@Situacao", conta.Situacao ?? "Paga");
                            cmd.Parameters.AddWithValue("@Observacao", string.IsNullOrWhiteSpace(conta.Observacao) ? (object)DBNull.Value : conta.Observacao);
                            cmd.Parameters.AddWithValue("@DataAtualizacao", CorrigirDataSql(DateTime.Now));

                            cmd.Parameters.AddWithValue("@Modelo", conta.Modelo);
                            cmd.Parameters.AddWithValue("@Serie", conta.Serie);
                            cmd.Parameters.AddWithValue("@NumeroNota", conta.NumeroNota);
                            cmd.Parameters.AddWithValue("@IdFornecedor", conta.IdFornecedor);
                            cmd.Parameters.AddWithValue("@NumeroParcela", conta.NumeroParcela);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        string sqlInsert = @"
INSERT INTO ContasAPagar
(Modelo, Serie, NumeroNota, IdFornecedor, NumeroParcela,
 ValorParcela, IdFormaPagamento, DataEmissao, DataVencimento,
 Multa, Juros, Desconto, MultaValor, JurosValor, DescontoValor,
 DataCriacao, DataAtualizacao, Ativo, Situacao)
VALUES
(@Modelo, @Serie, @NumeroNota, @IdFornecedor, @NumeroParcela,
 @ValorParcela, @IdFormaPagamento, @DataEmissao, @DataVencimento,
 @Multa, @Juros, @Desconto, @MultaValor, @JurosValor, @DescontoValor,
 @DataCriacao, @DataAtualizacao, @Ativo, @Situacao);";

                        using (var cmd = new SqlCommand(sqlInsert, conn))
                        {
                            cmd.Parameters.AddWithValue("@Modelo", conta.Modelo);
                            cmd.Parameters.AddWithValue("@Serie", conta.Serie);
                            cmd.Parameters.AddWithValue("@NumeroNota", conta.NumeroNota);
                            cmd.Parameters.AddWithValue("@IdFornecedor", conta.IdFornecedor);
                            cmd.Parameters.AddWithValue("@NumeroParcela", conta.NumeroParcela);
                            cmd.Parameters.AddWithValue("@ValorParcela", conta.ValorParcela);
                            cmd.Parameters.AddWithValue("@IdFormaPagamento", conta.IdFormaPagamento);
                            cmd.Parameters.AddWithValue("@DataEmissao", CorrigirDataSql(conta.DataEmissao));
                            cmd.Parameters.AddWithValue("@DataVencimento", CorrigirDataSql(conta.DataVencimento));

                            cmd.Parameters.AddWithValue("@Multa", conta.Multa);
                            cmd.Parameters.AddWithValue("@Juros", conta.Juros);
                            cmd.Parameters.AddWithValue("@Desconto", conta.Desconto);

                            cmd.Parameters.AddWithValue("@MultaValor", conta.MultaValor);
                            cmd.Parameters.AddWithValue("@JurosValor", conta.JurosValor);
                            cmd.Parameters.AddWithValue("@DescontoValor", conta.DescontoValor);

                            cmd.Parameters.AddWithValue("@DataCriacao", CorrigirDataSql(DateTime.Now));
                            cmd.Parameters.AddWithValue("@DataAtualizacao", CorrigirDataSql(DateTime.Now));
                            cmd.Parameters.AddWithValue("@Ativo", true);
                            cmd.Parameters.AddWithValue("@Situacao", conta.Situacao ?? "Em Aberto");

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }


        public List<ContasAPagar> BuscarPorChave(CompraKey key)
        {
            if (key.Modelo == null)
                throw new ArgumentException("Modelo não pode ser nulo.", nameof(key.Modelo));
            if (key.Serie == null)
                throw new ArgumentException("Série não pode ser nula.", nameof(key.Serie));
            if (key.NumeroNota == null)
                throw new ArgumentException("Número da nota não pode ser nulo.", nameof(key.NumeroNota));

            return BuscarPorChaveCompra(key.Modelo, key.Serie, key.NumeroNota, key.IdFornecedor);
        }
        public bool CompraJaExiste(string modelo, string serie, string numeroNota, int idFornecedor)
        {
            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
        SELECT COUNT(*) 
        FROM ContasAPagar
        WHERE Modelo = @Modelo
          AND Serie = @Serie
          AND NumeroNota = @NumeroNota
          AND IdFornecedor = @IdFornecedor
          AND Ativo = 1;", cn))
            {
                cmd.Parameters.AddWithValue("@Modelo", modelo);
                cmd.Parameters.AddWithValue("@Serie", serie);
                cmd.Parameters.AddWithValue("@NumeroNota", numeroNota);
                cmd.Parameters.AddWithValue("@IdFornecedor", idFornecedor);

                cn.Open();
                return (int)cmd.ExecuteScalar() > 0;
            }
        }
        private DateTime CorrigirDataSql(DateTime data)
        {
            if (data < new DateTime(1753, 1, 1))
                return new DateTime(1753, 1, 1);
            if (data > new DateTime(9999, 12, 31))
                return new DateTime(9999, 12, 31);
            return data;
        }
        public ContasAPagar BuscarParcelaCompleta(int modelo, string serie, string numeroNota, int idFornecedor, int numeroParcela)
        {
            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
        SELECT 
            c.Modelo,
            c.Serie,
            c.NumeroNota,
            c.IdFornecedor,
            f.NomeRazaoSocial AS NomeFornecedor,
            c.NumeroParcela,
            c.DataEmissao,
            c.DataVencimento,
            c.ValorParcela,
            c.Multa,
            c.Juros,
            c.Desconto,
            c.IdFormaPagamento,
            fp.Descricao AS FormaPagamentoDescricao,
            c.DataCriacao,
            c.DataAtualizacao,
            c.Ativo
        FROM ContasAPagar c
        INNER JOIN Fornecedores f ON f.Id = c.IdFornecedor
        LEFT JOIN FormaPagamentos fp ON fp.Id = c.IdFormaPagamento
        WHERE c.Modelo = @Modelo
          AND c.Serie = @Serie
          AND c.NumeroNota = @NumeroNota
          AND c.IdFornecedor = @IdFornecedor
          AND c.NumeroParcela = @NumeroParcela;", cn))
            {
                cmd.Parameters.AddWithValue("@Modelo", modelo);
                cmd.Parameters.AddWithValue("@Serie", serie);
                cmd.Parameters.AddWithValue("@NumeroNota", numeroNota);
                cmd.Parameters.AddWithValue("@IdFornecedor", idFornecedor);
                cmd.Parameters.AddWithValue("@NumeroParcela", numeroParcela);

                cn.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var conta = new ContasAPagar
                        {
                            Modelo = Convert.ToInt32(reader["Modelo"]),
                            Serie = reader["Serie"].ToString(),
                            NumeroNota = reader["NumeroNota"].ToString(),
                            IdFornecedor = Convert.ToInt32(reader["IdFornecedor"]),
                            NomeFornecedor = reader["NomeFornecedor"].ToString(),
                            NumeroParcela = Convert.ToInt32(reader["NumeroParcela"]),
                            DataEmissao = reader["DataEmissao"] != DBNull.Value ? Convert.ToDateTime(reader["DataEmissao"]) : DateTime.MinValue,
                            DataVencimento = reader["DataVencimento"] != DBNull.Value ? Convert.ToDateTime(reader["DataVencimento"]) : DateTime.MinValue,
                            ValorParcela = reader["ValorParcela"] != DBNull.Value ? Convert.ToDecimal(reader["ValorParcela"]) : 0m,
                            Multa = reader["Multa"] != DBNull.Value ? Convert.ToDecimal(reader["Multa"]) : 0m,
                            Juros = reader["Juros"] != DBNull.Value ? Convert.ToDecimal(reader["Juros"]) : 0m,
                            Desconto = reader["Desconto"] != DBNull.Value ? Convert.ToDecimal(reader["Desconto"]) : 0m,
                            IdFormaPagamento = reader["IdFormaPagamento"] != DBNull.Value ? Convert.ToInt32(reader["IdFormaPagamento"]) : 0,
                            FormaPagamentoDescricao = reader["FormaPagamentoDescricao"] != DBNull.Value ? reader["FormaPagamentoDescricao"].ToString() : "-",
                            DataCriacao = reader["DataCriacao"] != DBNull.Value ? Convert.ToDateTime(reader["DataCriacao"]) : DateTime.MinValue,
                            DataAtualizacao = reader["DataAtualizacao"] != DBNull.Value ? Convert.ToDateTime(reader["DataAtualizacao"]) : DateTime.MinValue,
                            Ativo = reader["Ativo"] != DBNull.Value && (reader["Ativo"] is bool b ? b : Convert.ToInt32(reader["Ativo"]) == 1)
                        };

                        if (conta.Multa == 0 && conta.Juros == 0 && conta.Desconto == 0)
                        {
                            var condDao = new CondicaoPagamentoDAO();
                            var condicao = condDao.BuscarPorCompra(conta.Modelo, conta.Serie, conta.NumeroNota, conta.IdFornecedor);

                            if (condicao != null)
                            {
                                conta.Multa = condicao.Multa;
                                conta.Juros = condicao.TaxaJuros;
                                conta.Desconto = condicao.Desconto;
                            }
                        }

                        return conta;
                    }
                }
            }

            return null; 
        }
        public void AtualizarSituacao(ContasAPagar conta)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = @"
UPDATE ContasAPagar
   SET Situacao = @Situacao,
       DataAtualizacao = @DataAtualizacao
 WHERE Modelo = @Modelo
   AND Serie = @Serie
   AND NumeroNota = @NumeroNota
   AND IdFornecedor = @IdFornecedor
   AND NumeroParcela = @NumeroParcela;";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Situacao", conta.Situacao);
                    cmd.Parameters.AddWithValue("@DataAtualizacao", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Modelo", conta.Modelo);
                    cmd.Parameters.AddWithValue("@Serie", conta.Serie);
                    cmd.Parameters.AddWithValue("@NumeroNota", conta.NumeroNota);
                    cmd.Parameters.AddWithValue("@IdFornecedor", conta.IdFornecedor);
                    cmd.Parameters.AddWithValue("@NumeroParcela", conta.NumeroParcela);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public bool CancelarConta(int modelo, string serie, string numeroNota, int idFornecedor, int numeroParcela, string motivo)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = @"
            UPDATE ContasAPagar
               SET Situacao = 'Cancelada',
                   MotivoCancelamento = @Motivo,
                   DataAtualizacao = @DataAtualizacao
             WHERE Modelo = @Modelo
               AND Serie = @Serie
               AND NumeroNota = @NumeroNota
               AND IdFornecedor = @IdFornecedor
               AND NumeroParcela = @NumeroParcela
               AND Situacao NOT IN ('Paga', 'Cancelada');";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Motivo", string.IsNullOrWhiteSpace(motivo) ? (object)DBNull.Value : motivo);
                    cmd.Parameters.AddWithValue("@DataAtualizacao", DateTime.Now);
                    cmd.Parameters.AddWithValue("@Modelo", modelo);
                    cmd.Parameters.AddWithValue("@Serie", serie);
                    cmd.Parameters.AddWithValue("@NumeroNota", numeroNota);
                    cmd.Parameters.AddWithValue("@IdFornecedor", idFornecedor);
                    cmd.Parameters.AddWithValue("@NumeroParcela", numeroParcela);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
        public bool ContaVemDeCompra(string modelo, string serie, string numeroNota, int idFornecedor)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = @"
            SELECT COUNT(*) 
              FROM Compras
             WHERE Modelo = @Modelo
               AND Serie = @Serie
               AND NumeroNota = @NumeroNota
               AND IdFornecedor = @IdFornecedor;";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Modelo", modelo);
                    cmd.Parameters.AddWithValue("@Serie", serie);
                    cmd.Parameters.AddWithValue("@NumeroNota", numeroNota);
                    cmd.Parameters.AddWithValue("@IdFornecedor", idFornecedor);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
            }
        }
        public bool ExisteParcelaAnteriorEmAberto(string modelo, string serie, string numeroNota, int idFornecedor, int numeroParcelaAtual)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string sql = @"
            SELECT COUNT(*) 
              FROM ContasAPagar
             WHERE Modelo = @Modelo
               AND Serie = @Serie
               AND NumeroNota = @NumeroNota
               AND IdFornecedor = @IdFornecedor
               AND NumeroParcela < @NumeroParcelaAtual
               AND (Situacao NOT IN ('Paga', 'Cancelada'));";

                using (var cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Modelo", modelo);
                    cmd.Parameters.AddWithValue("@Serie", serie);
                    cmd.Parameters.AddWithValue("@NumeroNota", numeroNota);
                    cmd.Parameters.AddWithValue("@IdFornecedor", idFornecedor);
                    cmd.Parameters.AddWithValue("@NumeroParcelaAtual", numeroParcelaAtual);

                    int resultado = Convert.ToInt32(cmd.ExecuteScalar());
                    return resultado > 0;
                }
            }
        }
    }
}

