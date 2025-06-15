using ProjetoPF.Dao;
using ProjetoPF.FormCadastros;
using ProjetoPF.Interfaces.FormCadastros;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ProjetoPF.FormConsultas
{
    public partial class FrmConsultaFormaPagamento : FrmConsultaPai
    {
        private FrmCadastroFormaPagamento frmCadastroFormaPagamento;
        private BaseServicos<FormaPagamento> formaPagamentoServices = new BaseServicos<FormaPagamento>(new BaseDao<FormaPagamento>("FormaPagamentos"));
        public FormaPagamento FormaSelecionada { get; private set; }

        public FrmConsultaFormaPagamento()
        {
            InitializeComponent();
            frmCadastroFormaPagamento = new FrmCadastroFormaPagamento();
            this.listViewFormaPagamento.MouseDoubleClick += new MouseEventHandler(this.listViewFormaPagamento_MouseDoubleClick);
        }

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {
            frmCadastroFormaPagamento.DesbloquearCampos();
            frmCadastroFormaPagamento.LimparCampos();
            frmCadastroFormaPagamento.FormClosed += FrmCadastroFormaPagamento_FormClosed;
            DialogResult result = frmCadastroFormaPagamento.ShowDialog();

            if (result == DialogResult.OK)
            {       
                FormaPagamento novaFormaPagamento = frmCadastroFormaPagamento.FormaPagamentoAtual;    
                MessageBox.Show("Forma de pagamento adicionada com sucesso!");
                formaPagamentoServices.Criar(novaFormaPagamento);
                PopularListView(string.Empty);
            }
        }

        private void FrmConsultaFormaPagamento_Load_1(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                listViewFormaPagamento.Columns.Add("Código", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Forma de Pagamento", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Ativo", -2, HorizontalAlignment.Left); 
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
            List<FormaPagamento> formaPagamentos = formaPagamentoServices.BuscarTodos(pesquisa);

            if (formaPagamentos != null && formaPagamentos.Count > 0)
            {
                foreach (var formaPagamento in formaPagamentos)
                {
                    ListViewItem item = new ListViewItem(formaPagamento.Id.ToString())
                    {
                        Tag = formaPagamento  
                    };
                    item.SubItems.Add(formaPagamento.Descricao);
                    item.SubItems.Add(formaPagamento.Ativo ? "SIM" : "NÃO");
                    item.SubItems.Add(formaPagamento.DataCriacao.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(formaPagamento.DataAtualizacao.ToString("dd/MM/yyyy"));
                    listViewFormaPagamento.Items.Add(item);
                    
                    if (!formaPagamento.Ativo)
                    {
                        item.ForeColor = Color.Red;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(pesquisa))
            {
                MessageBox.Show("Nenhum resultado encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

                FormaPagamento formaPagamentoSelecionada = formaPagamentoServices.BuscarPorId(id);

                if (formaPagamentoSelecionada == null)
                {
                    MessageBox.Show("Forma de pagamento não encontrada.", "Erro");
                    return;
                }

                frmCadastroFormaPagamento.CarregarDados(formaPagamentoSelecionada, true, false);
                frmCadastroFormaPagamento.DesbloquearCampos();
                frmCadastroFormaPagamento.FormClosed += FrmCadastroFormaPagamento_FormClosed;

                DialogResult result = frmCadastroFormaPagamento.ShowDialog();

                if (result == DialogResult.OK)
                {
                    FormaPagamento formaPagamentoAtualizada = frmCadastroFormaPagamento.FormaPagamentoAtual;

                    if (formaPagamentoAtualizada.Id != formaPagamentoSelecionada.Id)
                    {
                        MessageBox.Show("Erro: ID foi alterado durante a edição.");
                        return;
                    }

                    formaPagamentoServices.Atualizar(formaPagamentoAtualizada);
                    MessageBox.Show("Forma de pagamento atualizada com sucesso!");
                    PopularListView(string.Empty);
                }
            }
            else
            {
                MessageBox.Show("Selecione um item para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmCadastroFormaPagamento_FormClosed(object sender, FormClosedEventArgs e)
        {
            PopularListView(string.Empty);
            frmCadastroFormaPagamento.FormClosed -= FrmCadastroFormaPagamento_FormClosed;
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

                FormaPagamento formaPagamentoSelecionada = formaPagamentoServices.BuscarPorId(id);

                if (formaPagamentoSelecionada == null)
                {
                    MessageBox.Show("Forma de pagamento não encontrada.", "Erro");
                    return;
                }

                var daoParcelas = new BaseDao<CondicaoPagamentoParcelas>("CondicaoPagamentoParcelas");
                var parcelasVinculadas = daoParcelas.BuscarTodos()
                    .Where(p => p.IdFormaPagamento == formaPagamentoSelecionada.Id)
                    .ToList();

                if (parcelasVinculadas.Any())
                {
                    MessageBox.Show("Não é possível excluir esta forma de pagamento pois ela está sendo usada em condições de pagamento.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmCadastroFormaPagamento.CarregarDados(formaPagamentoSelecionada, false, true);
                frmCadastroFormaPagamento.BloquearCampos();
                frmCadastroFormaPagamento.FormClosed += FrmCadastroFormaPagamento_FormClosed;
                frmCadastroFormaPagamento.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione um item para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void listViewFormaPagamento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                var itemSelecionado = listViewFormaPagamento.SelectedItems[0];
                FormaPagamento formaSelecionada = (FormaPagamento)itemSelecionado.Tag;

                if (!formaSelecionada.Ativo)
                {
                    MessageBox.Show("Esta forma de pagamento está inativa e não pode ser selecionada.",
                                    "Forma inativa",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                if (this.Owner is FrmCadastroCondPagamento frmCadastroCondPagamento)
                {
                    frmCadastroCondPagamento.txtForma.Text = formaSelecionada.Descricao;
                    frmCadastroCondPagamento.txtCod.Text = formaSelecionada.Id.ToString();
                    this.Close();
                }
            }
        }
    }
}
