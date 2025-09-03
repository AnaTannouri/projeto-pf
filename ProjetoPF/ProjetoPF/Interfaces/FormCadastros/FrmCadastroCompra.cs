using ProjetoPF.Dao;
using ProjetoPF.FormConsultas;
using ProjetoPF.Interfaces.FormConsultas;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Modelos.Pessoa;
using ProjetoPF.Servicos;
using ProjetoPF.Servicos.Pessoa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using ProjetoPF.Modelos.Produto;
using ProjetoPF.Modelos.Compra;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroCompra : ProjetoPF.FormCadastros.FrmCadastroPai
    {
        private BaseServicos<Fornecedor> fornecedorServices = new BaseServicos<Fornecedor>(new BaseDao<Fornecedor>("Fornecedores"));
        private BaseServicos<CondicaoPagamento> condicaoPagamentoServices = new BaseServicos<CondicaoPagamento>(new BaseDao<CondicaoPagamento>("CondicaoPagamentos"));
        private List<ItemCompra> itensCompra = new List<ItemCompra>();
        private List<ParcelaCompra> parcelas = new List<ParcelaCompra>();

        public FrmCadastroCompra()
        {
            InitializeComponent();
            label1.Text = "Modelo:";

            dtpEmissao.Format = DateTimePickerFormat.Custom;
            dtpEmissao.CustomFormat = " ";

            dtpEntrega.Format = DateTimePickerFormat.Custom;
            dtpEntrega.CustomFormat = " "; 

            dtpEmissao.ValueChanged += (s, e) =>
            {
                dtpEmissao.CustomFormat = "dd/MM/yyyy";
            };

            dtpEntrega.ValueChanged += (s, e) =>
            {
                dtpEntrega.CustomFormat = "dd/MM/yyyy";
            };

        }

        private void btnPesquisarCondicao_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCodFornecedor.Text, out int idFornecedor))
            {
                MessageBox.Show("Selecione um fornecedor antes de escolher a condição de pagamento.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fornecedor = fornecedorServices.BuscarPorId(idFornecedor);
            if (fornecedor == null)
            {
                MessageBox.Show("Fornecedor não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var condicaoAVista = condicaoPagamentoServices.BuscarTodos("")
                .FirstOrDefault(c => c.Descricao.Equals("A VISTA", StringComparison.OrdinalIgnoreCase));

            if (condicaoAVista == null)
            {
                MessageBox.Show("Condição 'À Vista' não encontrada no banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var idsPermitidos = new List<int> { condicaoAVista.Id };

            if (fornecedor.CondicaoPagamentoId != condicaoAVista.Id)
            {
                idsPermitidos.Add(fornecedor.CondicaoPagamentoId);
            }

            var formConsulta = new FrmConsultaCondPagamentoFiltrado(idsPermitidos);
            formConsulta.ShowDialog();

            if (formConsulta.CondicaoSelecionada != null)
            {
                txtCondicao.Text = formConsulta.CondicaoSelecionada.Descricao;
                txtCodCondicao.Text = formConsulta.CondicaoSelecionada.Id.ToString();
            }
        }

        private void btnPesquisarProduto_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCodFornecedor.Text, out int idFornecedor))
            {
                MessageBox.Show("Selecione um fornecedor antes de escolher o produto.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var servicoProduto = new BaseServicos<Produtos>(new BaseDao<Produtos>("Produtos"));
            var produtosDoFornecedor = servicoProduto.BuscarTodos("")
                .Where(p => p.IdFornecedor == idFornecedor)
                .ToList();

            if (!produtosDoFornecedor.Any())
            {
                MessageBox.Show("O fornecedor selecionado não possui produtos cadastrados.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var idsPermitidos = produtosDoFornecedor.Select(p => p.Id).ToList();

            using (var formConsulta = new FrmConsultaProdutoFiltrado(idsPermitidos))
            {
                formConsulta.ShowDialog();

                if (formConsulta.ProdutoSelecionado != null)
                {
                    txtProduto.Text = formConsulta.ProdutoSelecionado.Nome;
                    txtCodProduto.Text = formConsulta.ProdutoSelecionado.Id.ToString();
                }
            }
        }

        private void btPesquisarFornecedor_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCodFornecedor.Text))
            {
                txtCodigo.Text = string.Empty;
                txtSerie.Text = string.Empty;
                txtNumeroNota.Text = string.Empty;
                txtCodFornecedor.Text = string.Empty;
                txtCondicao.Text = string.Empty;
                txtCodCondicao.Text = string.Empty;

                txtCodProduto.Text = string.Empty;
                txtProduto.Text = string.Empty;
                txtQuantidade.Text = string.Empty;
                txtValorUnitario.Text = string.Empty;
                txtTotal.Text = "R$ 0,00";

                txtValorFrete.Text = string.Empty;
                txtSeguro.Text = string.Empty;
                txtDespesas.Text = string.Empty;
                txtValorTotal.Text = string.Empty;

                dtpEmissao.Format = DateTimePickerFormat.Custom;
                dtpEmissao.CustomFormat = " ";

                dtpEntrega.Format = DateTimePickerFormat.Custom;
                dtpEntrega.CustomFormat = " ";

                itensCompra.Clear();
                AtualizarListViewItens();
            }

            FrmConsultaFornecedor frmConsultaFornecedor = new FrmConsultaFornecedor();
            frmConsultaFornecedor.Owner = this;
            frmConsultaFornecedor.ShowDialog();
        }

        private void dtpEmissao_ValueChanged(object sender, EventArgs e)
        {
            dtpEmissao.CustomFormat = "dd/MM/yyyy";
        }

        private void dtpEntrega_ValueChanged(object sender, EventArgs e)
        {
            dtpEntrega.CustomFormat = "dd/MM/yyyy";
        }

        private void AtualizarListViewItens()
        {
            listViewProduto.Items.Clear();

            foreach (var item in itensCompra)
            {
                var produto = new BaseServicos<Produtos>(new BaseDao<Produtos>("Produtos"))
                    .BuscarPorId(item.IdProduto);

                ListViewItem lvi = new ListViewItem(item.IdProduto.ToString());
                lvi.SubItems.Add(produto?.Nome ?? ""); // Descrição
                lvi.SubItems.Add(item.Quantidade.ToString("F2"));
                lvi.SubItems.Add(item.ValorUnitario.ToString("C2"));
                lvi.SubItems.Add(item.Total.ToString("C2"));
                lvi.Tag = item;

                listViewProduto.Items.Add(lvi);
            }

            AtualizarTotalProdutos();
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtQuantidade.Text, out decimal quantidade) ||
        !decimal.TryParse(txtValorUnitario.Text, out decimal valorUnitario))
            {
                MessageBox.Show("Quantidade ou valor inválido.");
                return;
            }

            if (!int.TryParse(txtCodProduto.Text, out int idProduto))
            {
                MessageBox.Show("Produto inválido.");
                return;
            }

            var existente = itensCompra.FirstOrDefault(i => i.IdProduto == idProduto);
            if (existente != null)
                itensCompra.Remove(existente);

            var item = new ItemCompra
            {
                IdProduto = idProduto,
                Quantidade = quantidade,
                ValorUnitario = valorUnitario,
                Total = quantidade * valorUnitario
            };

            itensCompra.Add(item);
            AtualizarListViewItens();

            txtCodProduto.Text = string.Empty;
            txtProduto.Text = string.Empty;
            txtQuantidade.Text = string.Empty;
            txtValorUnitario.Text = string.Empty;
            txtTotal.Text = string.Empty;

            if (listViewParcelas.Items.Count > 0)
            {
                parcelas.Clear();
                listViewParcelas.Items.Clear();
                label21.Text = "R$ 0,00";
            }

            AtualizarValorTotalCompra();
        }

        private void FrmCadastroCompra_Load(object sender, EventArgs e)
        {
            listViewProduto.View = View.Details;
            listViewProduto.FullRowSelect = true;
            listViewProduto.GridLines = true;

            listViewProduto.Columns.Clear();
            listViewProduto.Columns.Add("Cód. Produto", 100);
            listViewProduto.Columns.Add("Descrição", 250);
            listViewProduto.Columns.Add("Quantidade", 120);
            listViewProduto.Columns.Add("Valor Unitário", 150);
            listViewProduto.Columns.Add("Total (R$)", 150);


            listViewParcelas.View = View.Details;
            listViewParcelas.FullRowSelect = true;
            listViewParcelas.GridLines = true;

            listViewParcelas.Columns.Clear();
            listViewParcelas.Columns.Add("Nº Parcela", 80);
            listViewParcelas.Columns.Add("Vencimento", 150);
            listViewParcelas.Columns.Add("Valor Parcela", 150);


            txtQuantidade.TextChanged += txtQuantidade_TextChanged;
            txtValorUnitario.TextChanged += txtValorUnitario_TextChanged;
            txtValorFrete.TextChanged += (s, ev) => AtualizarValorTotalCompra();
            txtSeguro.TextChanged += (s, ev) => AtualizarValorTotalCompra();
            txtDespesas.TextChanged += (s, ev) => AtualizarValorTotalCompra();
        }
        private void AtualizarTotalProdutos()
        {
            decimal total = itensCompra.Sum(i => i.Total);
            label4.Text = total.ToString("C2"); 
        }
        private void txtQuantidade_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalUnitario();
        }

        private void txtValorUnitario_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalUnitario();
        }

        private void CalcularTotalUnitario()
        {
            if (decimal.TryParse(txtQuantidade.Text, out decimal quantidade) &&
                decimal.TryParse(txtValorUnitario.Text, out decimal valorUnitario))
            {
                decimal total = quantidade * valorUnitario;
                txtTotal.Text = total.ToString("C2");
            }
            else
            {
                txtTotal.Text = "R$ 0,00";
            }
        }
        private void GerarParcelasCondicaoPagamento()
        {
            if (!int.TryParse(txtCodCondicao.Text, out int idCondicao))
            {
                MessageBox.Show("Condição de pagamento não selecionada.");
                return;
            }

            var condicao = new BaseServicos<CondicaoPagamento>(new BaseDao<CondicaoPagamento>("CondicaoPagamentos")).BuscarPorId(idCondicao);
            var parcelasCondicao = new BaseServicos<CondicaoPagamentoParcelas>(new BaseDao<CondicaoPagamentoParcelas>("CondicaoPagamentoParcelas"))
                .BuscarTodos()
                .Where(p => p.IdCondicaoPagamento == idCondicao)
                .OrderBy(p => p.NumParcela)
                .ToList();

            decimal totalProdutos = itensCompra.Sum(i => i.Total);
            decimal frete = string.IsNullOrWhiteSpace(txtValorFrete.Text) ? 0 : decimal.Parse(txtValorFrete.Text);
            decimal seguro = string.IsNullOrWhiteSpace(txtSeguro.Text) ? 0 : decimal.Parse(txtSeguro.Text);
            decimal despesas = string.IsNullOrWhiteSpace(txtDespesas.Text) ? 0 : decimal.Parse(txtDespesas.Text);

            decimal totalCompra = totalProdutos + frete + seguro + despesas;
            DateTime dataBase = dtpEmissao.Value;

            parcelas.Clear(); 

            foreach (var p in parcelasCondicao)
            {
                decimal porcentagem = p.Porcentagem / 100m;

                parcelas.Add(new ParcelaCompra
                {
                    NumeroParcela = p.NumParcela,
                    DataVencimento = dataBase.AddDays(p.Prazo),
                    ValorParcela = Math.Round(porcentagem * totalCompra, 2)
                });
            }

            AtualizarListViewParcelas(parcelas);
        }

        private void AtualizarListViewParcelas(List<ParcelaCompra> parcelas)
        {
            listViewParcelas.Items.Clear();
            decimal totalParcelas = 0;

            foreach (var parcela in parcelas)
            {
                ListViewItem item = new ListViewItem(parcela.NumeroParcela.ToString());
                item.SubItems.Add(parcela.DataVencimento.ToString("dd/MM/yyyy"));
                item.SubItems.Add(parcela.ValorParcela.ToString("C2"));
                listViewParcelas.Items.Add(item);

                totalParcelas += parcela.ValorParcela;
            }

            label21.Text = totalParcelas.ToString("C2");
        }



        private void btnGerarParcelas_Click(object sender, EventArgs e)
        {
            GerarParcelasCondicaoPagamento();
        }
        private void AtualizarValorTotalCompra()
        {
            decimal totalProdutos = itensCompra.Sum(i => i.Total);
            decimal frete = decimal.TryParse(txtValorFrete.Text, out decimal f) ? f : 0;
            decimal seguro = decimal.TryParse(txtSeguro.Text, out decimal s) ? s : 0;
            decimal despesas = decimal.TryParse(txtDespesas.Text, out decimal d) ? d : 0;

            decimal totalCompra = totalProdutos + frete + seguro + despesas;
            txtValorTotal.Text = totalCompra.ToString("C2");
        }
        private void btnLimparParcelas_Click(object sender, EventArgs e)
        {
            parcelas.Clear(); 
            listViewParcelas.Items.Clear(); 
            label21.Text = "R$ 0,00";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listViewProduto.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione um item para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var itemSelecionado = listViewProduto.SelectedItems[0];
            var itemCompra = (ItemCompra)itemSelecionado.Tag;

            txtCodProduto.Text = itemCompra.IdProduto.ToString();
            var produto = new BaseServicos<Produtos>(new BaseDao<Produtos>("Produtos")).BuscarPorId(itemCompra.IdProduto);
            txtProduto.Text = produto?.Nome ?? "";
            txtQuantidade.Text = itemCompra.Quantidade.ToString("F2");
            txtValorUnitario.Text = itemCompra.ValorUnitario.ToString("F2");
            txtTotal.Text = itemCompra.Total.ToString("C2");

            itensCompra.Remove(itemCompra);
            AtualizarListViewItens();

            parcelas.Clear();
            listViewParcelas.Items.Clear();
            label21.Text = "R$ 0,00";

            AtualizarValorTotalCompra();
        }
    }

}
