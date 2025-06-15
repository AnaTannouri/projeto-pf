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
using System.Linq;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormConsultas
{
    public partial class FrmConsultaMarca : ProjetoPF.FormConsultas.FrmConsultaPai
    {
        private FrmCadastroMarca frmCadastroMarca;
        private BaseServicos<Marca> marcaServices = new BaseServicos<Marca>(new BaseDao<Marca>("Marcas"));
        public Marca MarcaSelecionada { get; private set; }
        public FrmConsultaMarca()
        {
            InitializeComponent();
            frmCadastroMarca = new FrmCadastroMarca();
        }

        private void FrmConsultaMarca_Load(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                listViewFormaPagamento.Columns.Add("Código", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Marca", -2, HorizontalAlignment.Left);
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
            frmCadastroMarca.DesbloquearCampos();
            frmCadastroMarca.LimparCampos();
            frmCadastroMarca.FormClosed += FrmCadastroMarca_FormClosed;
            DialogResult result = frmCadastroMarca.ShowDialog();

            if (result == DialogResult.OK)
            {
                Marca novaMarca = frmCadastroMarca.MarcaAtual;
                MessageBox.Show("Marca adicionada com sucesso!");
                marcaServices.Criar(novaMarca);
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
            List<Marca> marcas = marcaServices.BuscarTodos(pesquisa);

            if (marcas != null && marcas.Count > 0)
            {
                foreach (var marca in marcas)
                {
                    ListViewItem item = new ListViewItem(marca.Id.ToString())
                    {
                        Tag = marca
                    };
                    item.SubItems.Add(marca.Descricao);
                    item.SubItems.Add(marca.Ativo ? "SIM" : "NÃO");
                    item.SubItems.Add(marca.DataCriacao.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(marca.DataAtualizacao.ToString("dd/MM/yyyy"));
                    listViewFormaPagamento.Items.Add(item);

                    if (!marca.Ativo)
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

                Marca marcaSelecionada = marcaServices.BuscarPorId(id);

                if (marcaSelecionada == null)
                {
                    MessageBox.Show("Marca não encontrada.", "Erro");
                    return;
                }

                frmCadastroMarca.CarregarDados(marcaSelecionada, true, false);
                frmCadastroMarca.DesbloquearCampos();
                frmCadastroMarca.FormClosed += FrmCadastroMarca_FormClosed;

                DialogResult result = frmCadastroMarca.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Marca marcaAtualizada = frmCadastroMarca.MarcaAtual;

                    if (marcaAtualizada.Id != marcaSelecionada.Id)
                    {
                        MessageBox.Show("Erro: ID foi alterado durante a edição.");
                        return;
                    }

                    marcaServices.Atualizar(marcaAtualizada);
                    MessageBox.Show("Marca atualizada com sucesso!");
                    PopularListView(string.Empty);
                }
            }
            else
            {
                MessageBox.Show("Selecione um item para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void FrmCadastroMarca_FormClosed(object sender, FormClosedEventArgs e)
        {
            PopularListView(string.Empty);
            frmCadastroMarca.FormClosed -= FrmCadastroMarca_FormClosed;
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

                Marca marcaSelecionada = marcaServices.BuscarPorId(id);

                if (marcaSelecionada == null)
                {
                    MessageBox.Show("Marca não encontrada.", "Erro");
                    return;
                }

                var daoProdutos = new BaseDao<Produtos>("Produtos");
                var produtosVinculados = daoProdutos.BuscarTodos()
                    .Where(p => p.IdMarca == marcaSelecionada.Id)
                    .ToList();

                if (produtosVinculados.Any())
                {
                    MessageBox.Show("Não é possível excluir esta marca pois ela está sendo usada em produtos.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmCadastroMarca.CarregarDados(marcaSelecionada, false, true);
                frmCadastroMarca.BloquearCampos();
                frmCadastroMarca.FormClosed += FrmCadastroMarca_FormClosed;
                frmCadastroMarca.ShowDialog();
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
                Marca marcaSelecionada = (Marca)itemSelecionado.Tag;

                if (!marcaSelecionada.Ativo)
                {
                    MessageBox.Show("Esta marca está inativa e não pode ser selecionada.",
                                    "Marca inativa",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                if (this.Owner is FrmCadastroProduto frmCadastroProduto)
                {
                    frmCadastroProduto.txtMarca.Text = marcaSelecionada.Descricao;
                    frmCadastroProduto.txtCodMarca.Text = marcaSelecionada.Id.ToString();
                    this.Close();
                }
            }
        }
    }
}
