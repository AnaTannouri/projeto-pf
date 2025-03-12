using ProjetoPF.Dao;
using ProjetoPF.FormCadastros;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjetoPF.FormConsultas
{
    public partial class FrmConsultaFormaPagamento : FrmConsultaPai
    {
        private FrmCadastroFormaPagamento frmCadastroFormaPagamento;
        private BaseDao<FormaPagamento> formaPagamentoDao;

        public FrmConsultaFormaPagamento()
        {
            InitializeComponent();
            frmCadastroFormaPagamento = new FrmCadastroFormaPagamento();
            formaPagamentoDao = new BaseDao<FormaPagamento>("FormaPagamentos");
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string pesquisa = txtPesquisa.Text.Trim();
            PopularListView(string.IsNullOrEmpty(pesquisa) ? string.Empty : pesquisa);
        }

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {
            frmCadastroFormaPagamento.LimparCampos();
            frmCadastroFormaPagamento.ShowDialog();
        }

        private void FrmConsultaFormaPagamento_Load_1(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                // Adiciona as colunas na ListView
                listViewFormaPagamento.Columns.Add("Código", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Descrição", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Data de Criação", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Data de Atualização", -2, HorizontalAlignment.Left);
            }

            AjustarLarguraColunas();
            PopularListView(string.Empty);
        }

        private void AjustarLarguraColunas()
        {
            int columnWidth = listViewFormaPagamento.Width / listViewFormaPagamento.Columns.Count;
            foreach (ColumnHeader col in listViewFormaPagamento.Columns)
            {
                col.Width = columnWidth;
            }
        }

        public override void PopularListView(string pesquisa)
        {
            listViewFormaPagamento.Items.Clear();
            List<FormaPagamento> formaPagamentos = formaPagamentoDao.BuscarTodos(pesquisa);

            if (formaPagamentos != null && formaPagamentos.Count > 0)
            {
                foreach (var formaPagamento in formaPagamentos)
                {
                    ListViewItem item = new ListViewItem(formaPagamento.Id.ToString())
                    {
                        SubItems =
                        {
                            formaPagamento.Descricao,
                            formaPagamento.DataCriacao.ToString("dd/MM/yyyy"),
                            formaPagamento.DataAtualizacao.ToString("dd/MM/yyyy")
                        }
                    };
                    listViewFormaPagamento.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Nenhum resultado encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewFormaPagamento.SelectedItems[0];
                int id = int.Parse(selectedItem.Text);
                FormaPagamento formaPagamentoSelecionada = formaPagamentoDao.BuscarPorId(id);

                frmCadastroFormaPagamento.CarregarDados(formaPagamentoSelecionada);

                DialogResult result = frmCadastroFormaPagamento.ShowDialog();
                if (result == DialogResult.OK)
                {
                    formaPagamentoDao.Atualizar(formaPagamentoSelecionada);
                    PopularListView(txtPesquisa.Text.Trim());
                }

                frmCadastroFormaPagamento.LimparCampos();
            }
            else
            {
                MessageBox.Show("Selecione uma forma de pagamento para editar.", "Aviso");
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count == 0)
            {
                MessageBox.Show("Por favor, selecione um item para excluir.");
                return;
            }

            int idFormaPagamento = int.Parse(listViewFormaPagamento.SelectedItems[0].Text);
            DialogResult result = MessageBox.Show("Tem certeza de que deseja excluir esta forma de pagamento?",
                                           "Confirmação de Exclusão",
                                           MessageBoxButtons.YesNo,
                                           MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    formaPagamentoDao.Excluir(idFormaPagamento);
                    listViewFormaPagamento.Items.Remove(listViewFormaPagamento.SelectedItems[0]);
                    MessageBox.Show("Forma de pagamento excluída com sucesso!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao excluir: {ex.Message}");
                }
            }
        }
    }
}
