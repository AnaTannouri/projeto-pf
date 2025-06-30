using ProjetoPF.Dao;
using ProjetoPF.Interfaces.FormCadastros;
using ProjetoPF.Modelos.Produto;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace ProjetoPF.Interfaces.FormConsultas
{
    public partial class FrmConsultaUnidadeMedida : ProjetoPF.FormConsultas.FrmConsultaPai
    {
        private FrmCadastroUnidadeMedida frmCadastroUnidade;
        private BaseServicos<UnidadeMedida> unidadeServices = new BaseServicos<UnidadeMedida>(new BaseDao<UnidadeMedida>("UnidadeMedidas"));
        public UnidadeMedida UnidadeSelecionada { get; private set; }
        public FrmConsultaUnidadeMedida()
        {
            InitializeComponent();
            frmCadastroUnidade = new FrmCadastroUnidadeMedida();
        }

        private void FrmConsultaUnidadeMedida_Load(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                listViewFormaPagamento.Columns.Add("Código", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Unidade de Medida", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Sigla", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Ativo", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Data de Criação", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Data de Atualização", -2, HorizontalAlignment.Left);
            }
            listViewFormaPagamento.MouseDoubleClick += listViewFormaPagamento_MouseDoubleClick;

            AjustarLarguraColunas();
            PopularListView(string.Empty);
        }

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {
            frmCadastroUnidade.DesbloquearCampos();
            frmCadastroUnidade.LimparCampos();
            frmCadastroUnidade.FormClosed += FrmCadastroUnidade_FormClosed;
            DialogResult result = frmCadastroUnidade.ShowDialog();

            if (result == DialogResult.OK)
            {
                UnidadeMedida novaUnidade = frmCadastroUnidade.UnidadeAtual;
                MessageBox.Show("Unidade de medida adicionada com sucesso!");
                unidadeServices.Criar(novaUnidade);
                PopularListView(string.Empty);
            }
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
            List<UnidadeMedida> unidades = unidadeServices.BuscarTodos(pesquisa);

            if (unidades != null && unidades.Count > 0)
            {
                foreach (var unidade in unidades)
                {
                    ListViewItem item = new ListViewItem(unidade.Id.ToString())
                    {
                        Tag = unidade
                    };
                    item.SubItems.Add(unidade.Descricao);
                    item.SubItems.Add(unidade.Sigla);
                    item.SubItems.Add(unidade.Ativo ? "SIM" : "NÃO");
                    item.SubItems.Add(unidade.DataCriacao.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(unidade.DataAtualizacao.ToString("dd/MM/yyyy"));
                    listViewFormaPagamento.Items.Add(item);

                    if (!unidade.Ativo)
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

                UnidadeMedida unidadeSelecionada = unidadeServices.BuscarPorId(id);

                if (unidadeSelecionada == null)
                {
                    MessageBox.Show("Unidade de medida não encontrada.", "Erro");
                    return;
                }

                frmCadastroUnidade.CarregarDados(unidadeSelecionada, true, false);
                frmCadastroUnidade.DesbloquearCampos();
                frmCadastroUnidade.FormClosed += FrmCadastroUnidade_FormClosed;

                DialogResult result = frmCadastroUnidade.ShowDialog();

                if (result == DialogResult.OK)
                {
                    UnidadeMedida unidadeAtualizada = frmCadastroUnidade.UnidadeAtual;

                    if (unidadeAtualizada.Id != unidadeSelecionada.Id)
                    {
                        MessageBox.Show("Erro: ID foi alterado durante a edição.");
                        return;
                    }

                    unidadeServices.Atualizar(unidadeAtualizada);
                    MessageBox.Show("Unidade de medida atualizada com sucesso!");
                    PopularListView(string.Empty);
                }
            }
            else
            {
                MessageBox.Show("Selecione um item para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void FrmCadastroUnidade_FormClosed(object sender, FormClosedEventArgs e)
        {
            PopularListView(string.Empty);
            frmCadastroUnidade.FormClosed -= FrmCadastroUnidade_FormClosed;
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

                UnidadeMedida unidadeSelecionada = unidadeServices.BuscarPorId(id);

                if (unidadeSelecionada == null)
                {
                    MessageBox.Show("Unidade de medida não encontrada.", "Erro");
                    return;
                }

                var daoProdutos = new BaseDao<Produtos>("Produtos");
                var produtosVinculados = daoProdutos.BuscarTodos()
                    .Where(p => p.IdUnidadeMedida == unidadeSelecionada.Id)
                    .ToList();

                if (produtosVinculados.Any())
                {
                    MessageBox.Show("Não é possível excluir esta unidade de medida pois ela está sendo usada em produtos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmCadastroUnidade.CarregarDados(unidadeSelecionada, false, true);
                frmCadastroUnidade.BloquearCampos();
                frmCadastroUnidade.FormClosed += FrmCadastroUnidade_FormClosed;
                frmCadastroUnidade.ShowDialog();
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
                UnidadeMedida unidadeSelecionada = (UnidadeMedida)itemSelecionado.Tag;

                if (!unidadeSelecionada.Ativo)
                {
                    MessageBox.Show("Esta unidade de medida está inativa e não pode ser selecionada.",
                                    "Unidade inativa",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                if (this.Owner is FrmCadastroProduto frmCadastroProduto)
                {
                    frmCadastroProduto.txtUnidadeMedida.Text = unidadeSelecionada.Descricao;
                    frmCadastroProduto.txtCodUnidadeMedida.Text = unidadeSelecionada.Id.ToString();
                    this.Close();
                }
            }
        }
    }
}

