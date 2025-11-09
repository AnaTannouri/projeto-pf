using ProjetoPF.Dao;
using ProjetoPF.Dao.Compra;
using ProjetoPF.Interfaces.FormCadastros;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Modelos.Pessoa;
using ProjetoPF.Modelos.Produto;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormConsultas
{
    public partial class FrmConsultaFornecedor : ProjetoPF.FormConsultas.FrmConsultaPai
    {
        private FrmCadastroFornecedor frmCadastroFornecedor = new FrmCadastroFornecedor();
        private BaseServicos<Fornecedor> fornecedorServices = new BaseServicos<Fornecedor>(new BaseDao<Fornecedor>("Fornecedores"));
        private BaseServicos<CondicaoPagamento> condicaoPagamentoServices = new BaseServicos<CondicaoPagamento>(new BaseDao<CondicaoPagamento>("CondicaoPagamentos"));

        public Fornecedor FornecedorSelecionado { get; private set; }

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
                            fornecedor.Ativo ? "SIM" : "NÃO",
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

                var daoProdutoFornecedor = new ProdutoFornecedorDAO();
                var produtosVinculados = daoProdutoFornecedor.BuscarPorFornecedor(fornecedorSelecionado.Id);

                if (produtosVinculados.Any())
                {
                    MessageBox.Show("Não é possível excluir este fornecedor, pois ele está vinculado a um ou mais produtos.",
                                    "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmCadastroFornecedor = new FrmCadastroFornecedor();
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
                listViewFormaPagamento.Columns.Add("Código", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Nome/Razão Social", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Telefone", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Valor Mínimo", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Ativo", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Criação", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Atualização", -2, HorizontalAlignment.Right);
            }
            listViewFormaPagamento.MouseDoubleClick += listViewFormaPagamento_MouseDoubleClick;

            AjustarLarguraColunas();
            PopularListView(string.Empty);
        }
        private void listViewFormaPagamento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                var itemSelecionado = listViewFormaPagamento.SelectedItems[0];
                Fornecedor fornecedorSelecionado = (Fornecedor)itemSelecionado.Tag;

                if (!fornecedorSelecionado.Ativo)
                {
                    MessageBox.Show("Este fornecedor está inativo e não pode ser selecionado.",
                                    "Fornecedor inativo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }
                FornecedorSelecionado = fornecedorSelecionado;

                if (this.Owner is FrmCadastroProduto frmCadastroProduto)
                {
                    frmCadastroProduto.txtFornecedor.Text = fornecedorSelecionado.NomeRazaoSocial;
                    frmCadastroProduto.txtCodFornecedor.Text = fornecedorSelecionado.Id.ToString();
                    this.Close();
                }
                else if (this.Owner is FrmCadastroCompra frmCadastroCompra)
                {
                    frmCadastroCompra.txtFornecedor.Text = fornecedorSelecionado.NomeRazaoSocial;
                    frmCadastroCompra.txtCodFornecedor.Text = fornecedorSelecionado.Id.ToString();
                    this.Close();
                }
                else if (this.Owner is FrmCadastroContaAPagar frmCadastroContaAPagar)
                {
                    frmCadastroContaAPagar.txtFornecedor.Text = fornecedorSelecionado.NomeRazaoSocial;
                    frmCadastroContaAPagar.txtCodFornecedor.Text = fornecedorSelecionado.Id.ToString();
                    this.Close();
                }
                else
                {
                    this.Close();
                }
            }
        }
    }
}
