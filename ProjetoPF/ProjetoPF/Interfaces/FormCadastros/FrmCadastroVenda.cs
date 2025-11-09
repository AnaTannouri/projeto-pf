using ProjetoPF.Dao;
using ProjetoPF.Dao.Vendas;
using ProjetoPF.Interfaces.FormConsultas;
using ProjetoPF.Modelos;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Modelos.Pessoa;
using ProjetoPF.Modelos.Produto;
using ProjetoPF.Modelos.Venda;
using ProjetoPF.Servicos;
using ProjetoPF.Servicos.Venda;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroVenda : ProjetoPF.FormCadastros.FrmCadastroPai
    {
        private BaseServicos<Cliente> clienteServices = new BaseServicos<Cliente>(new BaseDao<Cliente>("Clientes"));
        private BaseServicos<CondicaoPagamento> condicaoPagamentoServices = new BaseServicos<CondicaoPagamento>(new BaseDao<CondicaoPagamento>("CondicaoPagamentos"));

        private List<ItemVenda> itensVenda = new List<ItemVenda>();
        private List<ContasAReceber> parcelas = new List<ContasAReceber>();

        private readonly VendaKey? _key;
        private bool carregandoDuplicada = false;
        private bool notaDuplicada = false;

        public bool ModoSomenteLeitura { get; set; } = false;
        public FrmCadastroVenda()
        {
            InitializeComponent();
            txtCodigo.Text = "55";
            txtSerie.Text = "001";
            txtCodigo.ReadOnly = true;
            txtSerie.ReadOnly = true;

            label1.Text = "Modelo:";
            checkAtivo.Visible = false;

            DateEmissão.Format = DateTimePickerFormat.Custom;
            DateEmissão.CustomFormat = " ";
            DateEmissão.Checked = false;

            DateEmissão.ValueChanged += (s, ev) =>
            {
                DateEmissão.CustomFormat = "dd/MM/yyyy";
                TentarLiberarItens();
            };
            label18.Visible = false;
        }

        private void FrmCadastroVenda_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNumNota.Text))
            {
                var dao = new VendaDao();
                txtNumNota.Text = dao.ObterProximoNumeroNota(55, "001").ToString("D6");
            }
            txtValorUnitario.Leave += FormatarMoedaAoSair;

            checkAtivo.Visible = false;
            txtObservacao.Enabled = true; 

            txtNumNota.ReadOnly = true;
            txtNumNota.BackColor = SystemColors.ControlLight;

            if (ModoSomenteLeitura)
            {
                btnPesquisarCliente.Enabled = false;
            }

            listViewProdutos.View = View.Details;
            listViewProdutos.Columns.Clear();
            listViewProdutos.Columns.Add("Cód. Produto", 100, HorizontalAlignment.Right);
            listViewProdutos.Columns.Add("Descrição", 250, HorizontalAlignment.Left);
            listViewProdutos.Columns.Add("Quantidade", 120, HorizontalAlignment.Right);
            listViewProdutos.Columns.Add("Valor Unitário", 150, HorizontalAlignment.Right);
            listViewProdutos.Columns.Add("Total (R$)", 150, HorizontalAlignment.Right);
            listViewProdutos.FullRowSelect = true;
            listViewProdutos.MultiSelect = false;
            listViewProdutos.HideSelection = false;
            listViewProdutos.GridLines = true;

            listViewParcelas.View = View.Details;
            listViewParcelas.Columns.Clear();
            listViewParcelas.Columns.Add("Nº Parcela", 80, HorizontalAlignment.Right);
            listViewParcelas.Columns.Add("Vencimento", 150, HorizontalAlignment.Left);
            listViewParcelas.Columns.Add("Valor Parcela", 150, HorizontalAlignment.Right);
            listViewParcelas.Columns.Add("Forma Pagamento", 150, HorizontalAlignment.Left);


            txtQuantidade.TextChanged += txtQuantidade_TextChanged;
            txtValorUnitario.TextChanged += txtValorUnitario_TextChanged;

            SetCabecalhoEnabled(true);
            SetItensEnabled(false);
            SetCondicaoEnabled(false);
            SetSalvarEnabled(false);
            listViewProdutos.Enabled = false;
            listViewParcelas.Enabled = false;


            listViewProdutos.SelectedIndexChanged += (s, ev) =>
            {
                bool selecionado = listViewProdutos.SelectedItems.Count > 0;
                btnEditar.Enabled = selecionado;
                btnRemover.Enabled = selecionado;
            };

            txtCodigo.Text = "55"; 
            txtSerie.Text = "1"; 
            txtCodigo.Enabled = false;
            txtSerie.Enabled = false;

            if (ModoSomenteLeitura)
            {
                AplicarSomenteLeitura();

                if (btnVoltar != null)
                    btnVoltar.Text = "Cancelar";
            }

            txtQuantidade.KeyPress += ApenasNumerosDecimais;
            txtValorUnitario.KeyPress += ApenasNumerosDecimais;
        }

        public FrmCadastroVenda(VendaKey key)
        {
            InitializeComponent();
            label1.Text = "Modelo:";
            _key = key;

            try
            {
                var dao = new VendaDao();
                var venda = dao.BuscarPorChave(key);

                if (venda == null)
                {
                    MessageBox.Show("Venda não encontrada.", "Aviso",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                txtCodigo.Text = venda.Modelo;
                txtSerie.Text = venda.Serie;
                txtNumNota.Text = venda.NumeroNota;
                txtCodCliente.Text = venda.IdCliente.ToString();
                txtCodCondicao.Text = venda.IdCondicaoPagamento.ToString();

                DateEmissão.Format = DateTimePickerFormat.Custom;
                DateEmissão.CustomFormat = "dd/MM/yyyy";
                DateEmissão.Value = venda.DataEmissao;
                DateEmissão.Enabled = false;

                var cliente = new BaseServicos<Cliente>(new BaseDao<Cliente>("Clientes"))
                                  .BuscarPorId(venda.IdCliente);
                txtCliente.Text = cliente?.NomeRazaoSocial ?? "Desconhecido";

                var funcionario = new BaseServicos<Funcionario>(
                                      new BaseDao<Funcionario>("Funcionarios"))
                                      .BuscarPorId(venda.IdFuncionario);
                if (funcionario != null)
                {
                    txtCodFuncionario.Text = funcionario.Id.ToString();
                    txtFuncionario.Text = funcionario.NomeRazaoSocial;
                }
                else
                {
                    txtCodFuncionario.Text = "";
                    txtFuncionario.Text = "(não encontrado)";
                }

                var condicao = new BaseServicos<CondicaoPagamento>(
                                   new BaseDao<CondicaoPagamento>("CondicaoPagamentos"))
                                   .BuscarPorId(venda.IdCondicaoPagamento);
                txtCondicao.Text = condicao?.Descricao ?? "Desconhecida";

                listViewProdutos.Items.Clear();
                var itens = new ItemVendaDao().BuscarPorChave(key);
                itensVenda = itens;

                foreach (var it in itens)
                {
                    var produto = new BaseServicos<Produtos>(
                        new BaseDao<Produtos>("Produtos")).BuscarPorId(it.IdProduto);

                    var lvi = new ListViewItem(it.IdProduto.ToString());
                    lvi.SubItems.Add(produto?.Nome ?? "Desconhecido");
                    lvi.SubItems.Add(it.Quantidade.ToString("N2"));
                    lvi.SubItems.Add(it.ValorUnitario.ToString("C2"));
                    lvi.SubItems.Add(it.Total.ToString("C2"));
                    listViewProdutos.Items.Add(lvi);
                }

                labelCriacao.Text = venda.DataCriacao != DateTime.MinValue
    ? venda.DataCriacao.ToString("dd/MM/yyyy HH:mm")
    : "-";

                lblAtualizacao.Text = venda.DataAtualizacao != DateTime.MinValue
                    ? venda.DataAtualizacao.ToString("dd/MM/yyyy HH:mm")
                    : "-";

                AtualizarTotalProdutos();

                listViewParcelas.Items.Clear();
                var parcelasLista = new ContasAReceberDao().BuscarPorChave(key);
                parcelas = parcelasLista;

                foreach (var parcela in parcelasLista)
                {
                    string formaDescricao;

                    if (parcela.IdFormaPagamento > 0)
                    {
                        var formaPg = new BaseServicos<FormaPagamento>(
                            new BaseDao<FormaPagamento>("FormaPagamentos"))
                            .BuscarPorId(parcela.IdFormaPagamento);

                        formaDescricao = formaPg?.Descricao ?? "(não encontrada)";
                    }
                    else
                    {
                        formaDescricao = "Sem forma de pagamento";
                    }

                    var item = new ListViewItem(parcela.NumeroParcela.ToString());
                    item.SubItems.Add(parcela.DataVencimento.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(parcela.ValorParcela.ToString("C2"));
                    item.SubItems.Add(formaDescricao);

                    listViewParcelas.Items.Add(item);
                }

                AtualizarTotalParcelas();

                AplicarSomenteLeitura();

                if (!venda.Ativo && !string.IsNullOrWhiteSpace(venda.MotivoCancelamento))
                {
                    label18.Text = "Motivo do Cancelamento: " + venda.MotivoCancelamento;
                    label18.Visible = true;  
                }
                else
                {
                    label18.Visible = false;
                }

                txtCodigo.ReadOnly = true;
                txtSerie.ReadOnly = true;
                btnLimpar.Enabled = false;
                txtNumNota.ReadOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar venda: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private void btnGerarParcelas_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtCodCliente.Text, out int idCliente))
            {
                MessageBox.Show("Selecione primeiro o cliente.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var cliente = clienteServices.BuscarPorId(idCliente);
            if (cliente == null)
            {
                MessageBox.Show("Cliente não encontrado.", "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var condicaoCliente = condicaoPagamentoServices.BuscarPorId(cliente.CondicaoPagamentoId);
            if (condicaoCliente == null)
            {
                MessageBox.Show("O cliente não possui condição de pagamento cadastrada.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var condicaoAVista = condicaoPagamentoServices
                .BuscarTodos("")
                .FirstOrDefault(c =>
                    c.Descricao.Equals("A VISTA", StringComparison.OrdinalIgnoreCase) && c.Ativo);

            if (condicaoAVista == null)
            {
                MessageBox.Show("Nenhuma condição 'A VISTA' cadastrada.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult resp = MessageBox.Show(
       $"Deseja manter a condição padrão do cliente ({condicaoCliente.Descricao})?\n\n" +
       $"Clique em 'Não' para trocá-la por '{condicaoAVista.Descricao}'.",
       "Condição de Pagamento",
       MessageBoxButtons.YesNoCancel,
       MessageBoxIcon.Question,
       MessageBoxDefaultButton.Button1 
   );

            if (resp == DialogResult.Cancel)
                return;

            if (resp == DialogResult.No)
            {
                txtCodCondicao.Text = condicaoAVista.Id.ToString();
                txtCondicao.Text = condicaoAVista.Descricao;
            }
            else
            {
                txtCodCondicao.Text = condicaoCliente.Id.ToString();
                txtCondicao.Text = condicaoCliente.Descricao;
            }

            GerarParcelasCondicaoPagamento();

            if (listViewParcelas.Items.Count > 0)
            {
                btnGerarParcelas.Enabled = false;
                SetTotaisEnabled(false);
                SetItensEnabled(false);
                SetCondicaoEnabled(true);
                SetSalvarEnabled(true);
                txtObservacao.Enabled = true;
            }
        }

        private void btnPesquisarProduto_Click(object sender, EventArgs e)
        {
            using (var formConsulta = new FrmConsultaProduto())
            {
                formConsulta.ModoSelecao = true;

                if (formConsulta.ShowDialog() == DialogResult.OK && formConsulta.ProdutoSelecionado != null)
                {
                    var produto = formConsulta.ProdutoSelecionado;
                    txtProduto.Text = produto.Nome;
                    txtCodProduto.Text = produto.Id.ToString();
                }
            }
        }
        private void PreencherCondicaoPadraoDoCliente(int idCliente)
        {
            var cliente = clienteServices.BuscarPorId(idCliente);
            if (cliente == null)
            {
                txtCondicao.Text = string.Empty;
                txtCodCondicao.Text = string.Empty;
                return;
            }

            var cond = condicaoPagamentoServices.BuscarPorId(cliente.CondicaoPagamentoId);
            if (cond != null && cond.Ativo)
            {
                txtCondicao.Text = cond.Descricao;
                txtCodCondicao.Text = cond.Id.ToString();
            }
            else
            {
                var aVista = condicaoPagamentoServices.BuscarTodos("")
                    .FirstOrDefault(c => c.Descricao.Equals("A VISTA", StringComparison.OrdinalIgnoreCase) && c.Ativo);

                if (aVista != null)
                {
                    txtCondicao.Text = aVista.Descricao;
                    txtCodCondicao.Text = aVista.Id.ToString();
                }
                else
                {
                    txtCondicao.Text = string.Empty;
                    txtCodCondicao.Text = string.Empty;
                }
            }
            txtCondicao.Enabled = false;
            txtCodCondicao.Enabled = false;
        }

        private void btnPesquisarFuncionario_Click(object sender, EventArgs e)
        {
            using (var formConsulta = new FrmConsultaFuncionario())
            {
                formConsulta.ModoSelecao = true;

                if (formConsulta.ShowDialog() == DialogResult.OK && formConsulta.FuncionarioSelecionado != null)
                {
                    var funcionario = formConsulta.FuncionarioSelecionado;
              
                    txtCodFuncionario.Text = funcionario.Id.ToString();
                    txtFuncionario.Text = funcionario.NomeRazaoSocial;

                    TentarLiberarItens();
                }
            }
        }

        private void btnPesquisarCliente_Click_1(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCodCliente.Text))
            {
                txtNumNota.Text = string.Empty;
                txtCodCliente.Text = string.Empty;
                txtCliente.Text = string.Empty;
                txtCondicao.Text = string.Empty;
                txtCodCondicao.Text = string.Empty;

                txtCodProduto.Text = string.Empty;
                txtProduto.Text = string.Empty;
                txtQuantidade.Text = string.Empty;
                txtValorUnitario.Text = string.Empty;
                txtTotal.Text = "R$ 0,00";

                txtValorTotal.Text = "R$ 0,00";

                DateEmissão.Format = DateTimePickerFormat.Custom;
                DateEmissão.CustomFormat = " ";

                itensVenda.Clear();
                AtualizarListViewItens();
            }

            using (var frmConsulta = new FrmConsultaCliente())
            {
                frmConsulta.ModoSelecao = true;
                frmConsulta.Owner = this;

                if (frmConsulta.ShowDialog() == DialogResult.OK && frmConsulta.ClienteSelecionado != null)
                {
                    var cliente = frmConsulta.ClienteSelecionado;

                    txtCodCliente.Text = cliente.Id.ToString();
                    txtCliente.Text = cliente.NomeRazaoSocial;

                    PreencherCondicaoPadraoDoCliente(cliente.Id);

                    TentarLiberarItens();
                }
            }
        }
        private void BloquearTudoExcetoCabecalho()
        {
            txtCodigo.Enabled = false;
            txtSerie.Enabled = false;
            txtNumNota.Enabled = false;
            txtCodCliente.Enabled = false;

            DateEmissão.Enabled = false;

            SetItensEnabled(false);
            SetCondicaoEnabled(false);
            SetSalvarEnabled(false);

            listViewProdutos.Enabled = false;
            listViewParcelas.Enabled = false;

            btnGerarParcelas.Enabled = false;
            btnLimparParcelas.Enabled = false;
            btnPesquisarCliente.Enabled = false;
        }
        private void AtualizarListViewItens()
        {
            listViewProdutos.Items.Clear();

            foreach (var item in itensVenda)
            {
                var produto = new BaseServicos<Produtos>(new BaseDao<Produtos>("Produtos"))
                    .BuscarPorId(item.IdProduto);

                ListViewItem lvi = new ListViewItem(item.IdProduto.ToString());
                lvi.SubItems.Add(produto?.Nome ?? "");
                lvi.SubItems.Add(item.Quantidade.ToString("F4"));
                lvi.SubItems.Add(item.ValorUnitario.ToString("C2"));
                lvi.SubItems.Add(item.Total.ToString("C2"));
                lvi.Tag = item;

                listViewProdutos.Items.Add(lvi);
            }

            AtualizarTotalProdutos();

            btnEditar.Enabled = listViewProdutos.Items.Count > 0;
            btnRemover.Enabled = listViewProdutos.Items.Count > 0;
        }
        private decimal ConverterMoeda(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return 0;

            texto = texto.Replace("R$", "")
                         .Replace(" ", "")
                         .Replace(".", "")
                         .Replace(",", ".");

            if (decimal.TryParse(texto, System.Globalization.NumberStyles.Any,
                                 System.Globalization.CultureInfo.InvariantCulture,
                                 out decimal valor))
            {
                return Math.Round(valor, 4);
            }

            return 0;
        }

        private decimal ConverterNumero(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return 0;

            texto = texto.Replace(",", ".");
            if (decimal.TryParse(texto, System.Globalization.NumberStyles.AllowDecimalPoint,
                                 System.Globalization.CultureInfo.InvariantCulture,
                                 out decimal valor))
            {
                return Math.Round(valor, 4);
            }

            return 0;
        }

        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            decimal quantidade = ConverterNumero(txtQuantidade.Text);
            decimal valorUnitario = ConverterMoeda(txtValorUnitario.Text);

            if (quantidade <= 0 || valorUnitario <= 0)
            {
                MessageBox.Show("Informe uma quantidade e valor unitário válidos.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(txtCodProduto.Text, out int idProduto))
            {
                MessageBox.Show("Selecione um produto válido.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existente = itensVenda.FirstOrDefault(i => i.IdProduto == idProduto);
            if (existente != null)
                itensVenda.Remove(existente);

            var item = new ItemVenda
            {
                IdProduto = idProduto,
                Quantidade = quantidade,
                ValorUnitario = valorUnitario,
                Total = quantidade * valorUnitario
            };

            itensVenda.Add(item);
            AtualizarListViewItens();

            txtCodProduto.Text = string.Empty;
            txtProduto.Text = string.Empty;
            txtQuantidade.Text = string.Empty;
            txtValorUnitario.Text = string.Empty;
            txtTotal.Text = "R$ 0,00";

            AtualizarValorTotalVenda();

            if (itensVenda.Count == 1)
            {
                SetCabecalhoEnabled(false);
                SetItensEnabled(true);
                SetCondicaoEnabled(true);
                SetSalvarEnabled(false);
            }
            if (listViewParcelas.Items.Count > 0)
            {
                parcelas.Clear();
                listViewParcelas.Items.Clear();
                label21.Text = "R$ 0,00";
            }
        }
        private void AtualizarTotalParcelas()
        {
            decimal total = parcelas.Sum(p => p.ValorParcela);
            label21.Text = total.ToString("C2");
            AtualizarValorTotalVenda();
        }
        private bool DataEstaVazia(DateTimePicker dtp)
        {
            return dtp.Format == DateTimePickerFormat.Custom && dtp.CustomFormat == " ";
        }

        private void ApenasNumerosDecimais(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        private void FormatarMoedaAoSair(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            if (decimal.TryParse(txt.Text, out decimal valor))
                txt.Text = valor.ToString("C2");
            else
                txt.Text = "R$ 0,00";
        }

        private void AtualizarTotalProdutos()
        {
            decimal total = itensVenda.Sum(i => i.Total);
            txtValorTotal.Text = total.ToString("C2");

            AtualizarValorTotalVenda();
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
            decimal quantidade = ConverterNumero(txtQuantidade.Text);
            decimal valorUnitario = ConverterMoeda(txtValorUnitario.Text);

            if (quantidade > 0 && valorUnitario > 0)
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

            var condicao = new BaseServicos<CondicaoPagamento>(
                new BaseDao<CondicaoPagamento>("CondicaoPagamentos"))
                .BuscarPorId(idCondicao);

            var parcelasCondicao = new BaseServicos<CondicaoPagamentoParcelas>(
                new BaseDao<CondicaoPagamentoParcelas>("CondicaoPagamentoParcelas"))
                .BuscarTodos()
                .Where(p => p.IdCondicaoPagamento == idCondicao)
                .OrderBy(p => p.NumParcela)
                .ToList();

            decimal totalVenda = itensVenda.Sum(i => i.Total);
            DateTime dataBase = DateEmissão.Value;

            parcelas.Clear();

            foreach (var p in parcelasCondicao)
            {
                decimal porcentagem = p.Porcentagem / 100m;

                parcelas.Add(new ContasAReceber
                {
                    NumeroParcela = p.NumParcela,
                    DataVencimento = dataBase.AddDays(p.Prazo),
                    ValorParcela = Math.Round(porcentagem * totalVenda, 2),
                    IdFormaPagamento = p.IdFormaPagamento,
                    FormaPagamentoDescricao = BuscarDescricaoFormaPagamento(p.IdFormaPagamento)
                });
            }

            AtualizarListViewParcelas(parcelas);
        }
        private string BuscarDescricaoFormaPagamento(int idForma)
        {
            var servicoForma = new BaseServicos<FormaPagamento>(
                new BaseDao<FormaPagamento>("FormaPagamentos"));
            var forma = servicoForma.BuscarPorId(idForma);
            return forma?.Descricao ?? "";
        }

        private void AtualizarListViewParcelas(List<ContasAReceber> parcelas)
        {
            listViewParcelas.Items.Clear();
            decimal totalParcelas = 0;

            foreach (var parcela in parcelas)
            {
                ListViewItem item = new ListViewItem(parcela.NumeroParcela.ToString());
                item.SubItems.Add(parcela.DataVencimento.ToString("dd/MM/yyyy"));
                item.SubItems.Add(parcela.ValorParcela.ToString("C2"));
                item.SubItems.Add(parcela.FormaPagamentoDescricao ?? "");
                listViewParcelas.Items.Add(item);

                totalParcelas += parcela.ValorParcela;
            }

            label21.Text = totalParcelas.ToString("C2");
        }
        private void AtualizarValorTotalVenda()
        {
            decimal totalProdutos = itensVenda.Sum(i => i.Total);

            txtValorTotal.Text = totalProdutos.ToString("C2");

            bool habilitarCondicao = itensVenda.Count > 0 && totalProdutos > 0;
            SetCondicaoEnabled(habilitarCondicao);
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            LimparVenda();
            LimparParcelasUiOnly();

            if (int.TryParse(txtCodCliente.Text, out int idCliente))
                PreencherCondicaoPadraoDoCliente(idCliente);

            SetCondicaoEnabled(false);
            btnLimparParcelas.Enabled = false;
            SetSalvarEnabled(false);
            SetCabecalhoEnabled(true);
            SetItensEnabled(true);

            SetTotaisEnabled(false);
        }
        private void LimparVenda(bool confirmarSeTemDados = true)
        {
            txtCodProduto.Text = string.Empty;
            txtProduto.Text = string.Empty;
            txtQuantidade.Text = string.Empty;
            txtValorUnitario.Text = string.Empty;
            txtTotal.Text = "R$ 0,00";
            txtObservacao.Text = string.Empty;

            itensVenda.Clear();
            listViewProdutos.Items.Clear();
            txtValorTotal.Text = "R$ 0,00";

            parcelas.Clear();
            listViewParcelas.Items.Clear();
            label21.Text = "R$ 0,00";

            txtValorTotal.Text = "R$ 0,00";

            SetTotaisEnabled(false);
            SetCondicaoEnabled(false);
            btnLimparParcelas.Enabled = false;
            SetSalvarEnabled(false);
            SetCabecalhoEnabled(true);
            SetItensEnabled(false);

            AtualizarValorTotalVenda();

            txtCodigo.Text = "55";
            txtSerie.Text = "001";
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listViewProdutos.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione um item para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var itemSelecionado = listViewProdutos.SelectedItems[0];
            var itemVenda = (ItemVenda)itemSelecionado.Tag;

            txtCodProduto.Text = itemVenda.IdProduto.ToString();
            var produto = new BaseServicos<Produtos>(new BaseDao<Produtos>("Produtos")).BuscarPorId(itemVenda.IdProduto);
            txtProduto.Text = produto?.Nome ?? "";
            txtQuantidade.Text = itemVenda.Quantidade.ToString("F4");
            txtValorUnitario.Text = itemVenda.ValorUnitario.ToString("F2");
            txtTotal.Text = itemVenda.Total.ToString("C2");

            itensVenda.Remove(itemVenda);
            AtualizarListViewItens();

            parcelas.Clear();
            listViewParcelas.Items.Clear();
            label21.Text = "R$ 0,00";

            AtualizarValorTotalVenda();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (listViewProdutos.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione um item para remover.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Remover item(ns) selecionado(s)?", "Confirmar",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            foreach (ListViewItem lvi in listViewProdutos.SelectedItems)
                if (lvi.Tag is ItemVenda item)
                    itensVenda.Remove(item);

            AtualizarListViewItens();
            AtualizarValorTotalVenda();

            if (itensVenda.Count == 0)
            {
                SetTotaisEnabled(false);
                SetCondicaoEnabled(false);
                btnLimparParcelas.Enabled = false;
                SetSalvarEnabled(false);
                SetCabecalhoEnabled(true);
                SetItensEnabled(true);
            }

            AtualizarValorTotalVenda();
        }

        private void btnLimparParcelas_Click(object sender, EventArgs e)
        {
            parcelas.Clear();
            listViewParcelas.Items.Clear();
            label21.Text = "R$ 0,00";

            btnGerarParcelas.Enabled = itensVenda.Count > 0 && !string.IsNullOrWhiteSpace(txtCodCondicao.Text);

            txtObservacao.Text = string.Empty;
            txtObservacao.Enabled = false;

            SetTotaisEnabled(true);
            SetSalvarEnabled(false);
            SetCondicaoEnabled(true);
            SetItensEnabled(true);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                int modelo = 55;
                string serie = "001";

                var vendaDao = new VendaDao();

                string numeroGerado = vendaDao.ObterProximoNumeroNota(modelo, serie).ToString("D6");
                txtNumNota.Text = numeroGerado;

                if (!int.TryParse(txtCodCliente.Text, out int idCliente))
                {
                    MessageBox.Show("Selecione o cliente.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (vendaDao.ExisteContaComMesmoTitulo(modelo, serie, txtNumNota.Text, idCliente))
                {
                    MessageBox.Show(
                        "Já existe uma conta a receber registrada com esse Modelo, Série, Número e Cliente.\n" +
                        "Não é possível cadastrar uma venda com esses mesmos dados.",
                        "Duplicidade detectada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                if (NotaJaExiste(modelo, serie, txtNumNota.Text, idCliente))
                {
                    MessageBox.Show("Já existe uma venda cadastrada com esse Modelo, Série, Número e Cliente.",
                                    "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (DateEmissão.CustomFormat == " ")
                {
                    MessageBox.Show("Informe a data de emissão.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    DateEmissão.Focus();
                    return;
                }

                if (itensVenda == null || itensVenda.Count == 0)
                {
                    MessageBox.Show("Inclua pelo menos um produto.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!int.TryParse(txtCodCondicao.Text, out int idCond))
                {
                    MessageBox.Show("Selecione a condição de pagamento.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (parcelas == null || parcelas.Count == 0)
                {
                    MessageBox.Show("Gere ao menos uma parcela.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal TryMoeda(string s)
                {
                    if (string.IsNullOrWhiteSpace(s)) return 0m;
                    s = s.Replace("R$", "").Trim();
                    decimal.TryParse(s, out var v);
                    return v;
                }

                var venda = new Venda
                {
                    Modelo = modelo.ToString(),
                    Serie = serie,
                    NumeroNota = numeroGerado,
                    DataEmissao = DateEmissão.Value.Date,
                    IdCliente = idCliente,
                    IdCondicaoPagamento = idCond,
                    ValorTotal = TryMoeda(txtValorTotal.Text),
                    Itens = itensVenda,
                    Parcelas = parcelas,
                    DataCriacao = DateTime.Now,
                    DataAtualizacao = DateTime.Now,
                    Ativo = true,
                    Observacao = txtObservacao.Text?.Trim()
                };

                if (int.TryParse(txtCodFuncionario.Text, out int idFunc))
                    venda.IdFuncionario = idFunc;
                else
                    throw new InvalidOperationException("Funcionário não selecionado.");

                vendaDao.SalvarVendaComItensParcelas(venda, itensVenda, parcelas);

                MessageBox.Show("Venda salva com sucesso!",
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimparVenda(false);

                if (int.TryParse(txtCodCliente.Text, out int idCli))
                    PreencherCondicaoPadraoDoCliente(idCli);

                AbrirConsultaVendas();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar a venda: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void AbrirConsultaVendas()
        {
            var frm = new FrmConsultaVenda();

            if (this.MdiParent != null)
            {
                frm.MdiParent = this.MdiParent;
                frm.Show();
            }
            else
            {
                frm.Show();
            }
        }
        private void SetCabecalhoEnabled(bool enabled)
        {
            if (ModoSomenteLeitura || notaDuplicada) enabled = false;

            txtCodigo.Enabled = false;
            txtSerie.Enabled = false;
            DateEmissão.Enabled = enabled;
        }

        private void SetItensEnabled(bool enabled)
        {
            btnPesquisarProduto.Enabled = enabled;
            txtQuantidade.Enabled = enabled;
            txtValorUnitario.Enabled = enabled;
            btnAdicionar.Enabled = enabled;
            btnLimpar.Enabled = enabled;
            txtObservacao.Enabled = enabled;    
            btnEditar.Enabled = enabled && listViewProdutos.Items.Count > 0;
            btnRemover.Enabled = enabled && listViewProdutos.Items.Count > 0;
            listViewProdutos.Enabled = enabled;
        }

        private void SetCondicaoEnabled(bool enabled)
        {
            txtCondicao.Enabled = false;
            txtCodCondicao.Enabled = false;

            bool podeGerar = enabled && itensVenda.Count > 0 && !string.IsNullOrWhiteSpace(txtCodCondicao.Text);

            listViewParcelas.Enabled = podeGerar;
            btnGerarParcelas.Enabled = podeGerar;
            btnLimparParcelas.Enabled = enabled && listViewParcelas.Items.Count > 0;
        }

        private void SetSalvarEnabled(bool enabled)
        {
            btnSalvar.Enabled = enabled;
        }
        private void TentarLiberarItens()
        {
            if (notaDuplicada)
            {
                TravarCamposDuplicata();
                return;
            }

            if (ModoSomenteLeitura || notaDuplicada)
            {
                BloquearTudoExcetoCabecalho();
                return;
            }

            bool cabecalhoPreenchido =
                !string.IsNullOrWhiteSpace(txtCodCliente.Text) &&
                !string.IsNullOrWhiteSpace(txtCodFuncionario.Text) &&
                !DataEstaVazia(DateEmissão);

            if (cabecalhoPreenchido)
            {
                SetCabecalhoEnabled(false);
                SetItensEnabled(true);

                SetCondicaoEnabled(false);
                listViewParcelas.Enabled = false;
                txtObservacao.Enabled = false;

                btnLimpar.Enabled = true;
            }
            else
            {
                SetItensEnabled(false);
                SetCondicaoEnabled(false);
                listViewParcelas.Enabled = false;
                txtObservacao.Enabled = false;
                btnGerarParcelas.Enabled = false;
                btnLimparParcelas.Enabled = false;
                btnLimpar.Enabled = false;
            }
        }

        private bool NotaJaExiste(int modelo, string serie, string numero, int idCliente)
        {
            var dao = new VendaDao();
            return dao.NotaJaExiste(modelo, serie, numero, idCliente);
        }

        private void LimparParcelasUiOnly()
        {
            parcelas.Clear();
            listViewParcelas.Items.Clear();
            label21.Text = "R$ 0,00";
        }

        private void SetTotaisEnabled(bool enabled)
        {
            txtValorTotal.Enabled = false; 
        }
        private void AplicarSomenteLeitura()
        {
            SetCabecalhoEnabled(false);
            btnPesquisarCliente.Enabled = false;

            SetItensEnabled(false);
            listViewProdutos.Enabled = false;

            SetTotaisEnabled(false);
            txtValorTotal.Enabled = false;

            SetCondicaoEnabled(false);
            btnGerarParcelas.Enabled = false;
            btnLimparParcelas.Enabled = false;
            listViewParcelas.Enabled = false;

            txtObservacao.Enabled = false;
            btnSalvar.Enabled = false;

            txtCodigo.Enabled = false;
            txtSerie.Enabled = false;
            txtNumNota.Enabled = false;

            btnPesquisarFuncionario.Enabled = false;

        }
        private void TravarCamposDuplicata()
        {
            txtCodigo.Enabled = false;
            txtSerie.Enabled = false;
            txtNumNota.Enabled = false;
            txtCodCliente.Enabled = false;
            btnPesquisarCliente.Enabled = false;

            DateEmissão.Enabled = false;

            SetItensEnabled(false);
            listViewProdutos.Enabled = false;
            btnLimpar.Enabled = false;

            SetTotaisEnabled(false);
            txtValorTotal.Enabled = false;

            SetCondicaoEnabled(false);
            btnGerarParcelas.Enabled = false;
            btnLimparParcelas.Enabled = false;
            listViewParcelas.Enabled = false;

            txtObservacao.Enabled = false;

            btnSalvar.Enabled = false;
        }
    }
}
