using ProjetoPF.Dao;
using ProjetoPF.Modelos.Pagamento;
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
    public partial class FrmConsultaCondPagamentoFiltrado : ProjetoPF.FormConsultas.FrmConsultaCondPagamento
    {
        private BaseServicos<CondicaoPagamento> condicaoPagamentoServices = new BaseServicos<CondicaoPagamento>(new BaseDao<CondicaoPagamento>("CondicaoPagamentos"));

        public CondicaoPagamento CondicaoSelecionada { get; private set; }
        public FrmConsultaCondPagamentoFiltrado()
        {
            InitializeComponent();
        }
        private List<int> _idsPermitidos;

        public FrmConsultaCondPagamentoFiltrado(List<int> idsPermitidos)
        {
            InitializeComponent();
            _idsPermitidos = idsPermitidos;
            this.listViewFormaPagamento.MouseDoubleClick += listViewFormaPagamento_MouseDoubleClick;
            btnAdicionar.Visible = false;
            btnEditar.Visible = false;
            btnExcluir.Visible = false;
        }
        public override void PopularListView(string pesquisa)
        {
            listViewFormaPagamento.Items.Clear();

            var condicoes = condicaoPagamentoServices.BuscarTodos(pesquisa)
                .Where(c => _idsPermitidos.Contains(c.Id)) 
                .ToList();

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
                item.SubItems.Add(condicao.Ativo ? "SIM" : "NÃO");
                item.SubItems.Add(condicao.DataCriacao.ToString("dd/MM/yyyy"));
                item.SubItems.Add(condicao.DataAtualizacao.ToString("dd/MM/yyyy"));

                if (!condicao.Ativo)
                    item.ForeColor = System.Drawing.Color.Red;

                listViewFormaPagamento.Items.Add(item);
            }
        }
        private void listViewFormaPagamento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                var itemSelecionado = listViewFormaPagamento.SelectedItems[0];
                var condicaoSelecionada = (CondicaoPagamento)itemSelecionado.Tag;

                if (!condicaoSelecionada.Ativo)
                {
                    MessageBox.Show("Esta condição de pagamento está inativa e não pode ser selecionada.", "Condição Inativa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.CondicaoSelecionada = condicaoSelecionada;
                this.Close();
            }
        }
    }
}
