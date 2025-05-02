using ProjetoPF.Dao;
using ProjetoPF.Interfaces.FormCadastros;
using ProjetoPF.Modelos.Pessoa;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormConsultas
{
    public partial class FrmConsultaFornecedor : ProjetoPF.FormConsultas.FrmConsultaPai
    {
        private FrmCadastroFornecedor frmCadastroFornecedor = new FrmCadastroFornecedor();
        private BaseServicos<Fornecedor> fornecedorServices = new BaseServicos<Fornecedor>(new BaseDao<Fornecedor>("Fornecedores"));

        public FrmConsultaFornecedor()
        {
            InitializeComponent();
        }

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {
            frmCadastroFornecedor = new FrmCadastroFornecedor();
            frmCadastroFornecedor.LimparFormulario();
            frmCadastroFornecedor.DesbloquearCampos();
            frmCadastroFornecedor.FormClosed += FrmCadastroFornecedor_FormClosed;
            frmCadastroFornecedor.ShowDialog();
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
            List<Fornecedor> fornecedores = fornecedorServices.BuscarTodos(pesquisa);

            if (fornecedores != null && fornecedores.Count > 0)
            {
                foreach (var fornecedor in fornecedores)
                {
                    ListViewItem item = new ListViewItem(fornecedor.Id.ToString())
                    {
                        SubItems =
                        {
                            fornecedor.NomeRazaoSocial,
                            fornecedor.Telefone,
                            fornecedor.ValorMinimoPedido?.ToString("C2") ?? "R$ 0,00",
                            fornecedor.Ativo ? "Sim" : "Não",
                            fornecedor.DataCriacao.ToString("dd/MM/yyyy"),
                            fornecedor.DataAtualizacao.ToString("dd/MM/yyyy")
                        }
                    };

                    if (!fornecedor.Ativo)
                        item.ForeColor = System.Drawing.Color.Red;

                    item.Tag = fornecedor;
                    listViewFormaPagamento.Items.Add(item);
                }
            }
            else if (!string.IsNullOrEmpty(pesquisa))
            {
                MessageBox.Show("Nenhum resultado encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmCadastroFornecedor_FormClosed(object sender, FormClosedEventArgs e)
        {
            PopularListView(string.Empty);
            frmCadastroFornecedor.FormClosed -= FrmCadastroFornecedor_FormClosed;
        }

        private void btnEditar_Click_2(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewFormaPagamento.SelectedItems[0];

                if (!int.TryParse(selectedItem.Text, out int id))
                {
                    MessageBox.Show("ID inválido selecionado.", "Erro");
                    return;
                }

                Fornecedor fornecedorSelecionado = fornecedorServices.BuscarPorId(id);

                if (fornecedorSelecionado == null)
                {
                    MessageBox.Show("Fornecedor não encontrado.", "Erro");
                    return;
                }

                frmCadastroFornecedor = new FrmCadastroFornecedor();
                frmCadastroFornecedor.CarregarDados(fornecedorSelecionado, true, false);
                frmCadastroFornecedor.FormClosed += FrmCadastroFornecedor_FormClosed;
                frmCadastroFornecedor.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione um fornecedor para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewFormaPagamento.SelectedItems[0];

                if (!int.TryParse(selectedItem.Text, out int id))
                {
                    MessageBox.Show("ID inválido selecionado.", "Erro");
                    return;
                }

                Fornecedor fornecedorSelecionado = fornecedorServices.BuscarPorId(id);

                if (fornecedorSelecionado == null)
                {
                    MessageBox.Show("Fornecedor não encontrado.", "Erro");
                    return;
                }

                FrmCadastroFornecedor frmCadastroFornecedor = new FrmCadastroFornecedor();
                frmCadastroFornecedor.CarregarDados(fornecedorSelecionado, false, true); 
                frmCadastroFornecedor.FormClosed += FrmCadastroFornecedor_FormClosed;
                frmCadastroFornecedor.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione um fornecedor para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmConsultaFornecedor_Load_1(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                listViewFormaPagamento.Columns.Add("Código", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Nome/Razão Social", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Telefone", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Valor Mínimo", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Ativo", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Criação", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Atualização", -2, HorizontalAlignment.Left);
            }

            AjustarLarguraColunas();
            PopularListView(string.Empty);
        }
    }
}
