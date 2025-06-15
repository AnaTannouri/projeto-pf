using ProjetoPF.Dao;
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
    public partial class FrmConsultaProduto : ProjetoPF.FormConsultas.FrmConsultaPai
    {
        private FrmCadastroProduto frmCadastroProduto = new FrmCadastroProduto();
        private BaseServicos<Produtos> produtoServices = new BaseServicos<Produtos>(new BaseDao<Produtos>("Produtos"));
        public FrmConsultaProduto()
        {
            InitializeComponent();
        }

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {
            frmCadastroProduto = new FrmCadastroProduto();
            frmCadastroProduto.LimparFormulario();
            frmCadastroProduto.DesbloquearCampos();
            frmCadastroProduto.FormClosed += FrmCadastroProduto_FormClosed;
            frmCadastroProduto.ShowDialog(); 
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
            if (!string.IsNullOrWhiteSpace(pesquisa) && pesquisa.All(char.IsDigit))
            {
                MessageBox.Show("A pesquisa só pode ser feita pelo nome do produto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            List<Produtos> produtos = produtoServices.BuscarTodos(pesquisa);

            if (produtos != null && produtos.Count > 0)
            {
                foreach (var produto in produtos)
                {
                    ListViewItem item = new ListViewItem(produto.Id.ToString())
                    {
                        SubItems =
                        {
                            produto.Nome,
                            produto.PrecoCusto.ToString("C2"),
                            produto.PrecoVenda.ToString("C2"),
                            produto.Ativo ? "SIM" : "NÃO",
                            produto.DataCriacao.ToString("dd/MM/yyyy"),
                            produto.DataAtualizacao.ToString("dd/MM/yyyy")
                        }
                    };

                    if (!produto.Ativo)
                        item.ForeColor = System.Drawing.Color.Red;

                    item.Tag = produto;
                    listViewFormaPagamento.Items.Add(item);
                }
            }
            else if (!string.IsNullOrEmpty(pesquisa))
            {
                MessageBox.Show("Nenhum resultado encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void FrmCadastroProduto_FormClosed(object sender, FormClosedEventArgs e)
        {
            PopularListView(string.Empty);
            frmCadastroProduto.FormClosed -= FrmCadastroProduto_FormClosed;
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

                Produtos produtoSelecionado = produtoServices.BuscarPorId(id);

                if (produtoSelecionado == null)
                {
                    MessageBox.Show("Produto não encontrado.", "Erro");
                    return;
                }

                frmCadastroProduto = new FrmCadastroProduto();
                frmCadastroProduto.CarregarDados(produtoSelecionado, true, false);
                frmCadastroProduto.FormClosed += FrmCadastroProduto_FormClosed;
                frmCadastroProduto.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione um produto para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                Produtos produtoSelecionado = produtoServices.BuscarPorId(id);

                if (produtoSelecionado == null)
                {
                    MessageBox.Show("Produto não encontrado.", "Erro");
                    return;
                }

                frmCadastroProduto = new FrmCadastroProduto();
                frmCadastroProduto.CarregarDados(produtoSelecionado, false, true);
                frmCadastroProduto.FormClosed += FrmCadastroProduto_FormClosed;
                frmCadastroProduto.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione um produto para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmConsultaProduto_Load(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                listViewFormaPagamento.Columns.Add("Código", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Nome", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Preço Custo", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Preço Venda", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Ativo", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Criação", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Atualização", -2, HorizontalAlignment.Left);
            }

            //listViewFormaPagamento.MouseDoubleClick += listViewFormaPagamento_MouseDoubleClick;
            AjustarLarguraColunas();
            PopularListView(string.Empty);
        }
        //private void listViewFormaPagamento_MouseDoubleClick(object sender, MouseEventArgs e)
        //{
        //    if (listViewFormaPagamento.SelectedItems.Count > 0)
        //    {
        //        var itemSelecionado = listViewFormaPagamento.SelectedItems[0];
        //        Produtos produtoSelecionado = (Produtos)itemSelecionado.Tag;

        //        if (!produtoSelecionado.Ativo)
        //        {
        //            MessageBox.Show("Este produto está inativo e não pode ser selecionado.",
        //                            "Produto inativo",
        //                            MessageBoxButtons.OK,
        //                            MessageBoxIcon.Warning);
        //            return;
        //        }

        //        if (this.Owner is FrmCadastroProduto frmCadastroProduto)
        //        {
        //            frmCadastroProduto.txtProduto.Text = produtoSelecionado.Nome;
        //            frmCadastroProduto.txtCodigo.Text = produtoSelecionado.Id.ToString();
        //            this.Close();
        //        }
        //    }
        //}
    }
}
