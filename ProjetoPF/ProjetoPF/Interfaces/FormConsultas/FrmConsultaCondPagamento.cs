using ProjetoPF.Dao;
using ProjetoPF.FormCadastros;
using ProjetoPF.Interfaces.FormCadastros;
using ProjetoPF.Modelos.Localizacao;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjetoPF.FormConsultas
{
    public partial class FrmConsultaCondPagamento : FrmConsultaPai
    {
        private FrmCadastroCondPagamento frmCadastroCondPagamento;
        private BaseServicos<CondicaoPagamento> condicaoPagamentoServices = new BaseServicos<CondicaoPagamento>(new BaseDao<CondicaoPagamento>("CondicaoPagamentos"));

        public FrmConsultaCondPagamento()
        {
            InitializeComponent();
            frmCadastroCondPagamento = new FrmCadastroCondPagamento();
            this.listViewFormaPagamento.MouseDoubleClick += new MouseEventHandler(this.listViewFormaPagamento_MouseDoubleClick);
        }
        private void FrmConsultaCondPagamento_Load_1(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                listViewFormaPagamento.Columns.Add("Código", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Condição", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Juros %", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Multa %", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Desconto %", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Ativo", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Criação", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Atualização", -2, HorizontalAlignment.Left);
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

        private void FrmCadastroCondPagamento_FormClosed(object sender, FormClosedEventArgs e)
        {
            PopularListView(string.Empty);
            frmCadastroCondPagamento.FormClosed -= FrmCadastroCondPagamento_FormClosed;
        }

        public override void PopularListView(string pesquisa)
        {
            listViewFormaPagamento.Items.Clear();
            List<CondicaoPagamento> condicoes = condicaoPagamentoServices.BuscarTodos(pesquisa);

            if (condicoes != null && condicoes.Count > 0)
            {
                foreach (var condicao in condicoes)
                {
                    ListViewItem item = new ListViewItem(condicao.Id.ToString())
                    {
                        Tag = condicao
                    };

                    item.SubItems.Add(condicao.Descricao);
                    item.SubItems.Add(condicao.TaxaJuros.ToString("F2"));
                    item.SubItems.Add(condicao.Multa.ToString("F2"));
                    item.SubItems.Add(condicao.Desconto.ToString("F2"));
                    item.SubItems.Add(condicao.Ativo ? "Sim" : "Não");
                    item.SubItems.Add(condicao.DataCriacao.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(condicao.DataAtualizacao.ToString("dd/MM/yyyy"));

                    if (!condicao.Ativo)
                    {
                        item.ForeColor = System.Drawing.Color.Red;
                    }

                    listViewFormaPagamento.Items.Add(item);
                }
            }
            else if (!string.IsNullOrEmpty(pesquisa))
            {
                MessageBox.Show("Nenhum resultado encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewFormaPagamento.SelectedItems[0];

                if (!int.TryParse(selectedItem.Text, out int id))
                {
                    MessageBox.Show("ID inválido selecionado.", "Erro");
                    return;
                }

                CondicaoPagamento condicaoSelecionada = condicaoPagamentoServices.BuscarPorId(id);

                if (condicaoSelecionada == null)
                {
                    MessageBox.Show("Condição de pagamento não encontrada.", "Erro");
                    return;
                }

                frmCadastroCondPagamento = new FrmCadastroCondPagamento();
                frmCadastroCondPagamento.CarregarDados(condicaoSelecionada, false, true);
                frmCadastroCondPagamento.BloquearCampos();
                frmCadastroCondPagamento.FormClosed += FrmCadastroCondPagamento_FormClosed;
                frmCadastroCondPagamento.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione uma condição para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {
            frmCadastroCondPagamento = new FrmCadastroCondPagamento();
            frmCadastroCondPagamento.FormClosed += FrmCadastroCondPagamento_FormClosed;
            frmCadastroCondPagamento.ShowDialog();
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewFormaPagamento.SelectedItems[0];

                if (!int.TryParse(selectedItem.Text, out int id))
                {
                    MessageBox.Show("ID inválido selecionado.", "Erro");
                    return;
                }

                CondicaoPagamento condicaoSelecionada = condicaoPagamentoServices.BuscarPorId(id);

                if (condicaoSelecionada == null)
                {
                    MessageBox.Show("Condição de pagamento não encontrada.", "Erro");
                    return;
                }

                frmCadastroCondPagamento = new FrmCadastroCondPagamento();
                frmCadastroCondPagamento.CarregarDados(condicaoSelecionada, true, false);
                frmCadastroCondPagamento.FormClosed += FrmCadastroCondPagamento_FormClosed;
                frmCadastroCondPagamento.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione uma condição para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void listViewFormaPagamento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Owner is FrmCadastroCliente frmCadastroCliente)
            {
                if (listViewFormaPagamento.SelectedItems.Count > 0)
                {
                    var itemSelecionado = listViewFormaPagamento.SelectedItems[0];
                    var condicaoSelecionado = (CondicaoPagamento)itemSelecionado.Tag;

                    if (!condicaoSelecionado.Ativo)
                    {
                        MessageBox.Show("Esta condição de pagamento está inativa e não pode ser selecionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    frmCadastroCliente.txtCondicao.Text = condicaoSelecionado.Descricao;
                    frmCadastroCliente.txtCodigoCondicao.Text = condicaoSelecionado.Id.ToString();
                    frmCadastroCliente.Tag = condicaoSelecionado;
                    this.Close();
                }
            }
            else if (this.Owner is FrmCadastroFornecedor frmCadastroFornecedor)
            {
                if (listViewFormaPagamento.SelectedItems.Count > 0)
                {
                    var itemSelecionado = listViewFormaPagamento.SelectedItems[0];
                    var condicaoSelecionado = (CondicaoPagamento)itemSelecionado.Tag;

                    if (!condicaoSelecionado.Ativo)
                    {
                        MessageBox.Show("Esta condição de pagamento está inativa e não pode ser selecionada.", "Condição Inativa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    frmCadastroFornecedor.txtCondicao.Text = condicaoSelecionado.Descricao;
                    frmCadastroFornecedor.txtCodigoCondicao.Text = condicaoSelecionado.Id.ToString();
                    frmCadastroFornecedor.Tag = condicaoSelecionado;
                    this.Close();
                }
            }
        }
    }
}
