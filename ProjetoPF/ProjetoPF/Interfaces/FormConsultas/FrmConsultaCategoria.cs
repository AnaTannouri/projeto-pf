using ProjetoPF.Dao;
using ProjetoPF.FormCadastros;
using ProjetoPF.Interfaces.FormCadastros;
using ProjetoPF.Modelos.Produto;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormConsultas
{
    public partial class FrmConsultaCategoria : ProjetoPF.FormConsultas.FrmConsultaPai
    {
        private FrmCadastroCategoria frmCadastroCategoria;
        private BaseServicos<Categoria> categoriaServices = new BaseServicos<Categoria>(new BaseDao<Categoria>("Categorias"));
        public Categoria CategoriaSelecionada { get; private set; }

        public FrmConsultaCategoria()
        {
            InitializeComponent();
            frmCadastroCategoria = new FrmCadastroCategoria();
            this.listViewFormaPagamento.MouseDoubleClick += new MouseEventHandler(this.listViewFormaPagamento_MouseDoubleClick);
        }

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {
            frmCadastroCategoria.DesbloquearCampos();
            frmCadastroCategoria.LimparCampos();
            frmCadastroCategoria.FormClosed += FrmCadastroCategoria_FormClosed;
            DialogResult result = frmCadastroCategoria.ShowDialog();

            if (result == DialogResult.OK)
            {
                Categoria novaCategoria = frmCadastroCategoria.CategoriaAtual;
                MessageBox.Show("Categoria adicionada com sucesso!");
                categoriaServices.Criar(novaCategoria);
                PopularListView(string.Empty);
            }
        }

        private void FrmConsultaCategoria_Load(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                listViewFormaPagamento.Columns.Add("Código", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Categoria", -2, HorizontalAlignment.Left);
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
            List<Categoria> categorias = categoriaServices.BuscarTodos(pesquisa);

            if (categorias != null && categorias.Count > 0)
            {
                foreach (var categoria in categorias)
                {
                    ListViewItem item = new ListViewItem(categoria.Id.ToString())
                    {
                        Tag = categoria
                    };
                    item.SubItems.Add(categoria.Descricao);
                    item.SubItems.Add(categoria.Ativo ? "SIM" : "NÃO");
                    item.SubItems.Add(categoria.DataCriacao.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(categoria.DataAtualizacao.ToString("dd/MM/yyyy"));
                    listViewFormaPagamento.Items.Add(item);

                    if (!categoria.Ativo)
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

                Categoria categoriaSelecionada = categoriaServices.BuscarPorId(id);

                if (categoriaSelecionada == null)
                {
                    MessageBox.Show("Categoria não encontrada.", "Erro");
                    return;
                }

                frmCadastroCategoria.CarregarDados(categoriaSelecionada, true, false);
                frmCadastroCategoria.DesbloquearCampos();
                frmCadastroCategoria.FormClosed += FrmCadastroCategoria_FormClosed;

                DialogResult result = frmCadastroCategoria.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Categoria categoriaAtualizada = frmCadastroCategoria.CategoriaAtual;

                    if (categoriaAtualizada.Id != categoriaSelecionada.Id)
                    {
                        MessageBox.Show("Erro: ID foi alterado durante a edição.");
                        return;
                    }

                    categoriaServices.Atualizar(categoriaAtualizada);
                    MessageBox.Show("Categoria atualizada com sucesso!");
                    PopularListView(string.Empty);
                }
            }
            else
            {
                MessageBox.Show("Selecione um item para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void FrmCadastroCategoria_FormClosed(object sender, FormClosedEventArgs e)
        {
            PopularListView(string.Empty);
            frmCadastroCategoria.FormClosed -= FrmCadastroCategoria_FormClosed;
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

                Categoria categoriaSelecionada = categoriaServices.BuscarPorId(id);

                if (categoriaSelecionada == null)
                {
                    MessageBox.Show("Categoria não encontrada.", "Erro");
                    return;
                }

                var daoProdutos = new BaseDao<Produtos>("Produtos");
                var produtosVinculados = daoProdutos.BuscarTodos()
                    .Where(p => p.IdCategoria == categoriaSelecionada.Id)
                    .ToList();

                if (produtosVinculados.Any())
                {
                    MessageBox.Show("Não é possível excluir esta categoria pois ela está sendo usada em produtos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmCadastroCategoria.CarregarDados(categoriaSelecionada, false, true);
                frmCadastroCategoria.BloquearCampos();
                frmCadastroCategoria.FormClosed += FrmCadastroCategoria_FormClosed;
                frmCadastroCategoria.ShowDialog();
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
                Categoria categoriaSelecionada = (Categoria)itemSelecionado.Tag;

                if (!categoriaSelecionada.Ativo)
                {
                    MessageBox.Show("Esta categoria está inativa e não pode ser selecionada.",
                                    "Categoria inativa",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                if (this.Owner is FrmCadastroProduto frmCadastroProduto)
                {
                    frmCadastroProduto.txtCategoria.Text = categoriaSelecionada.Descricao;
                    frmCadastroProduto.txtCodCategoria.Text = categoriaSelecionada.Id.ToString();
                    this.Close();
                }
            }
        }
    }
}

