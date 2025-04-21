using ProjetoPF.Modelos.Pagamento;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;

namespace ProjetoPF.Dao
{
    public class CondicaoPagamentoDAO : BaseDao<CondicaoPagamento>
    {
        public CondicaoPagamentoDAO() : base("CondicaoPagamentos") { }

        public void SalvarCondicaoComParcelas(CondicaoPagamento condicao, List<CondicaoPagamentoParcelas> parcelas)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                if (parcelas == null || parcelas.Count == 0)
                    MessageBox.Show("Cadastre ao menos uma parcela!");

                bool duplicado = VerificarDuplicidade("Descricao", condicao.Descricao, condicao);
                if (duplicado)
                    MessageBox.Show("Já existe uma condição de pagamento com esta descrição.");

                this.Criar(condicao);

                var parcelaDao = new BaseDao<CondicaoPagamentoParcelas>("CondicaoPagamentoParcelas");

                foreach (var parcela in parcelas)
                {
                    parcela.IdCondicaoPagamento = condicao.Id;
                    parcela.DataCriacao = DateTime.Now;
                    parcela.DataAtualizacao = DateTime.Now;
                    parcelaDao.Criar(parcela);
                }

                scope.Complete();
            }
        }

        public void RemoverComParcelas(int idCondicaoPagamento)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Deleta as parcelas
                    using (SqlCommand deleteParcelasCmd = new SqlCommand(
                        "DELETE FROM CondicaoPagamentoParcelas WHERE IdCondicaoPagamento = @Id", conn, transaction))
                    {
                        deleteParcelasCmd.Parameters.AddWithValue("@Id", idCondicaoPagamento);
                        deleteParcelasCmd.ExecuteNonQuery();
                    }

                    // Deleta a condição de pagamento
                    using (SqlCommand deleteCondicaoCmd = new SqlCommand(
                        "DELETE FROM CondicaoPagamentos WHERE Id = @Id", conn, transaction))
                    {
                        deleteCondicaoCmd.Parameters.AddWithValue("@Id", idCondicaoPagamento);
                        deleteCondicaoCmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw; 
                }
            }
        }

        public void AtualizarCondicaoComParcelas(CondicaoPagamento condicao, List<CondicaoPagamentoParcelas> parcelas)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                Atualizar(condicao); 

                var parcelaDao = new BaseDao<CondicaoPagamentoParcelas>("CondicaoPagamentoParcelas");

                var antigas = parcelaDao.BuscarTodos().Where(p => p.IdCondicaoPagamento == condicao.Id).ToList();
                foreach (var antiga in antigas)
                {
                    parcelaDao.Remover(antiga.Id);
                }

                foreach (var parcela in parcelas)
                {
                    parcela.IdCondicaoPagamento = condicao.Id;
                    parcela.DataCriacao = DateTime.Now;
                    parcela.DataAtualizacao = DateTime.Now;
                    parcelaDao.Criar(parcela);
                }

                scope.Complete();
            }
        }
    }
}
