using ProjetoPF.Dao;
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
    public partial class FrmConsultaProdutoFiltrado : ProjetoPF.Interfaces.FormConsultas.FrmConsultaProduto
    {
        private List<int> _idsPermitidos;
        private BaseServicos<Produtos> produtoServices = new BaseServicos<Produtos>(new BaseDao<Produtos>("Produtos"));

        public Produtos ProdutoSelecionado { get; private set; }

        public FrmConsultaProdutoFiltrado(List<int> idsPermitidos)
        {
            InitializeComponent();
            _idsPermitidos = idsPermitidos;
            listViewFormaPagamento.MouseDoubleClick += listViewFormaPagamento_MouseDoubleClick;

            btnAdicionar.Visible = false;
            btnEditar.Visible = false;
            btnExcluir.Visible = false;
        }

        public override void PopularListView(string pesquisa)
        {
            listViewFormaPagamento.Items.Clear();

            var produtos = produtoServices.BuscarTodos(pesquisa)
                .Where(p => _idsPermitidos.Contains(p.Id))
                .ToList();

            foreach (var produto in produtos)
            {
                ListViewItem item = new ListViewItem(produto.Id.ToString());
                item.SubItems.Add(produto.Nome);
                item.SubItems.Add(produto.PrecoCusto.ToString("C2"));
                item.SubItems.Add(produto.PrecoVenda.ToString("C2"));
                item.SubItems.Add(produto.Ativo ? "SIM" : "NÃO");
                item.SubItems.Add(produto.DataCriacao.ToString("dd/MM/yyyy"));
                item.SubItems.Add(produto.DataAtualizacao.ToString("dd/MM/yyyy"));
                item.Tag = produto;

                if (!produto.Ativo)
                    item.ForeColor = Color.Red;

                listViewFormaPagamento.Items.Add(item);
            }
        }

        private void listViewFormaPagamento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                var item = listViewFormaPagamento.SelectedItems[0];
                var produto = (Produtos)item.Tag;

                if (!produto.Ativo)
                {
                    MessageBox.Show("Este produto está inativo e não pode ser selecionado.",
                                    "Produto inativo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.ProdutoSelecionado = produto;
                this.Close();
            }
        }
    }
}
