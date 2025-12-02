using ProjetoPF.Dao;
using ProjetoPF.Dao.Compras;
using ProjetoPF.Modelos.Compra;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmContasAPagarBaixa : ProjetoPF.Interfaces.FormCadastros.FrmCadastroContaAPagar
    {
        private bool valorPagoEditadoManualmente = false;

        public FrmContasAPagarBaixa()
        {
            InitializeComponent();
        }
        private void FrmContasAPagarBaixa_Load(object sender, EventArgs e)
        {
            var eventClickField = typeof(Control)
       .GetField("EventClick", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            if (eventClickField != null)
            {
                object eventClick = eventClickField.GetValue(null);
                var eventsProperty = typeof(Component)
                    .GetProperty("Events", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
                var eventList = (EventHandlerList)eventsProperty.GetValue(btnSalvar, null);
                eventList.RemoveHandler(eventClick, eventList[eventClick]);
            }

            btnSalvar.Click += new EventHandler(this.btnSalvar_Click);

            this.ModoBaixa = true;

            btnSalvar.Text = "Dar Baixa";
            checkAtivo.Visible = false;

            datePagamento.Format = DateTimePickerFormat.Short;
            datePagamento.Value = DateTime.Today;
            datePagamento.MaxDate = DateTime.Today.AddDays(1);

            txtValorPago.KeyPress += ApenasNumerosDecimais;
            txtValorPago.TextChanged += (s, ev) => valorPagoEditadoManualmente = true;
            txtValorPago.Leave += FormatarMoedaAoSair;

            datePagamento.ValueChanged += (s, ev) => { valorPagoEditadoManualmente = false; AtualizarValorPago(); };
            txtMulta.Leave += (s, ev) => { valorPagoEditadoManualmente = false; AtualizarValorPago(); };
            txtJuros.Leave += (s, ev) => { valorPagoEditadoManualmente = false; AtualizarValorPago(); };
            txtDesconto.Leave += (s, ev) => { valorPagoEditadoManualmente = false; AtualizarValorPago(); };

            txtMultaReais.Leave += (s, ev) => { valorPagoEditadoManualmente = false; AtualizarValorPago(); };
            txtJurosReais.Leave += (s, ev) => { valorPagoEditadoManualmente = false; AtualizarValorPago(); };
            txtDescontoReais.Leave += (s, ev) => { valorPagoEditadoManualmente = false; AtualizarValorPago(); };

            txtMultaReais.KeyPress += ApenasNumerosDecimais;
            txtJurosReais.KeyPress += ApenasNumerosDecimais;
            txtDescontoReais.KeyPress += ApenasNumerosDecimais;

            txtMultaReais.Leave += FormatarMoedaAoSair;
            txtJurosReais.Leave += FormatarMoedaAoSair;
            txtDescontoReais.Leave += FormatarMoedaAoSair;
            txtValorPago.Leave += FormatarMoedaAoSair;
            datePagamento.ValueChanged += (s, ev) =>
            {
                valorPagoEditadoManualmente = false;
                if (!string.IsNullOrEmpty(txtNumNota.Text))
                {
                    var parcela = new ContasAPagar
                    {
                        ValorParcela = ConverterTextoParaDecimal(txtValorParcela.Text),
                        Multa = ConverterTextoParaDecimal(txtMulta.Text),
                        Juros = ConverterTextoParaDecimal(txtJuros.Text),
                        Desconto = ConverterTextoParaDecimal(txtDesconto.Text),
                        DataVencimento = dateVencimento.Value
                    };
                    RecalcularValoresReais(parcela);
                }
            };


            BloquearCampos();

        }
        public void BloquearCamposVisualizar()
        {
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is TextBox textBox) textBox.ReadOnly = true;
                if (ctrl is ComboBox combo) combo.Enabled = false;
                if (ctrl is DateTimePicker dt) dt.Enabled = false;
                if (ctrl is Button btn && btn != btnVoltar) btn.Enabled = false;
                if (ctrl is CheckBox check) check.Enabled = false;
                btnSalvar.Visible = false;
            }
        }

        private void BloquearCampos()
        {
            txtCodigo.Enabled = false;
            txtSerie.Enabled = false;
            txtNumNota.Enabled = false;
            txtCodFornecedor.Enabled = false;
            txtFornecedor.Enabled = false;
            dateEmissao.Enabled = false;
            dateVencimento.Enabled = false;
            txtNumParcela.Enabled = false;
            txtValorParcela.Enabled = false;
            btnPesquisarFornecedor.Enabled = false;
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
            string texto = txt.Text.Replace("R$", "").Trim();

            if (string.IsNullOrWhiteSpace(texto))
            {
                txt.Text = "R$ 0,00";
                return;
            }
            texto = texto.Replace(".", ",");

            if (decimal.TryParse(texto, NumberStyles.Any, new CultureInfo("pt-BR"), out decimal valor))
                txt.Text = valor.ToString("C2", CultureInfo.GetCultureInfo("pt-BR"));
            else
                txt.Text = "R$ 0,00";
        }
        private void SetarDataSeguro(DateTimePicker picker, DateTime data)
        {
            if (data < picker.MinDate)
                picker.Value = picker.MinDate;
            else if (data > picker.MaxDate)
                picker.Value = picker.MaxDate;
            else
                picker.Value = data;
        }

        public void CarregarParcela(ContasAPagar parcela)
        {
            if (parcela == null) return;

            txtCodigo.Text = parcela.Modelo.ToString();
            txtSerie.Text = parcela.Serie;
            txtNumNota.Text = parcela.NumeroNota;
            txtCodFornecedor.Text = parcela.IdFornecedor.ToString();
            txtFornecedor.Text = parcela.NomeFornecedor ?? "";
            txtNumParcela.Text = parcela.NumeroParcela.ToString();
            txtValorParcela.Text = parcela.ValorParcela.ToString("C2");

            SetarDataSeguro(dateEmissao,
                parcela.DataEmissao == DateTime.MinValue ? DateTime.Today : parcela.DataEmissao);
            SetarDataSeguro(dateVencimento,
                parcela.DataVencimento == DateTime.MinValue ? DateTime.Today.AddDays(1) : parcela.DataVencimento);

            var formaDao = new FormaPagamentoDAO();
            var forma = formaDao.BuscarPorId(parcela.IdFormaPagamento);
            if (forma != null)
            {
                txtCodForma.Text = forma.Id.ToString();
                txtForma.Text = forma.Descricao;
            }
            else
            {
                txtCodForma.Text = parcela.IdFormaPagamento.ToString();
                txtForma.Text = parcela.FormaPagamentoDescricao ?? "-";
            }

            txtMulta.Text = parcela.Multa.ToString("N2");
            txtJuros.Text = parcela.Juros.ToString("N2");
            txtDesconto.Text = parcela.Desconto.ToString("N2");

            txtMulta.ReadOnly = true;
            txtJuros.ReadOnly = true;
            txtDesconto.ReadOnly = true;

            decimal valorBase = parcela.ValorParcela;
            DateTime dataVenc = parcela.DataVencimento;
            int diasAtraso = (DateTime.Today - dataVenc).Days;

            RecalcularValoresReais(parcela);

            datePagamento.Value = DateTime.Now;

            dateEmissao.Enabled = false;
            dateVencimento.Enabled = false;

            valorPagoEditadoManualmente = false;
            AtualizarValorPago();

            decimal valorPagoDecimal = ConverterTextoParaDecimal(txtValorPago.Text);
            txtValorPago.Text = valorPagoDecimal.ToString("C2", CultureInfo.GetCultureInfo("pt-BR"));
            valorPagoEditadoManualmente = false;
        }

        private void AtualizarValorPago()
        {

            if (valorPagoEditadoManualmente)
                return;

            decimal valorBase = ConverterTextoParaDecimal(txtValorParcela.Text);
            decimal valorMulta = ConverterTextoParaDecimal(txtMultaReais.Text);
            decimal valorJuros = ConverterTextoParaDecimal(txtJurosReais.Text);
            decimal valorDesconto = ConverterTextoParaDecimal(txtDescontoReais.Text);

            decimal valorFinal = valorBase + valorMulta + valorJuros - valorDesconto;

            if (valorFinal < 0)
                valorFinal = 0;

            txtValorPago.TextChanged -= (s, e) => valorPagoEditadoManualmente = true;
            txtValorPago.Text = valorFinal.ToString("C2", CultureInfo.GetCultureInfo("pt-BR"));
            txtValorPago.TextChanged += (s, e) => valorPagoEditadoManualmente = true;
        }

        protected override void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {

                decimal valorBase = ConverterTextoParaDecimal(txtValorParcela.Text);
                decimal multaPerc = ConverterTextoParaDecimal(txtMulta.Text);
                decimal jurosPerc = ConverterTextoParaDecimal(txtJuros.Text);
                decimal descontoPerc = ConverterTextoParaDecimal(txtDesconto.Text);

                decimal multaValor = ConverterTextoParaDecimal(txtMultaReais.Text);
                decimal jurosValor = ConverterTextoParaDecimal(txtJurosReais.Text);
                decimal descontoValor = ConverterTextoParaDecimal(txtDescontoReais.Text);
                decimal valorPago = ConverterTextoParaDecimal(txtValorPago.Text);

                if (datePagamento.Value == DateTime.MinValue)
                {
                    MessageBox.Show("A data de pagamento é obrigatória.",
                                    "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var conta = new ContasAPagar
                {
                    Modelo = string.IsNullOrWhiteSpace(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text),
                    Serie = txtSerie.Text.Trim(),
                    NumeroNota = txtNumNota.Text.Trim(),
                    IdFornecedor = string.IsNullOrWhiteSpace(txtCodFornecedor.Text) ? 0 : int.Parse(txtCodFornecedor.Text),
                    NumeroParcela = string.IsNullOrWhiteSpace(txtNumParcela.Text) ? 0 : int.Parse(txtNumParcela.Text),

                    ValorParcela = valorBase,
                    ValorFinalParcela = valorPago,

 
                    Multa = multaPerc,
                    Juros = jurosPerc,
                    Desconto = descontoPerc,

                    MultaValor = multaValor,
                    JurosValor = jurosValor,
                    DescontoValor = descontoValor,

                    DataPagamento = datePagamento.Value,
                    Situacao = "Paga",
                    Observacao = txtObservacao.Text
                };

                var dao = new ContasAPagarDao();
                dao.SalvarOuAtualizar(conta);

                if (this.Owner is ProjetoPF.Interfaces.FormConsultas.FrmConsultaContasAPagar consultaForm)
                {
                    consultaForm.PopularListView(string.Empty);
                    consultaForm.BringToFront();
                }

                MessageBox.Show("Baixa registrada com sucesso!",
                                "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (FormatException ex)
            {
                MessageBox.Show($"Erro de formato numérico: {ex.Message}",
                                "Erro de Conversão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao registrar baixa: {ex.Message}",
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private decimal ConverterTextoParaDecimal(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return 0m;
            texto = texto.Replace("\u00A0", "");  
            texto = texto.Replace("\u200B", ""); 

            texto = texto.Replace("R$", "");
            texto = texto.Replace("r$", "");
            texto = texto.Replace("R", "");
            texto = texto.Replace("$", "");
            texto = texto.Trim();

            texto = texto.Replace(".", "").Replace(",", ".");

            if (decimal.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
                return valor;

            if (decimal.TryParse(texto, NumberStyles.Any, new CultureInfo("pt-BR"), out valor))
                return valor;

            return 0m;
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {

            if (ModoVisualizacao)
            {
                this.Close();
                return;
            }

            var result = MessageBox.Show("Deseja realmente voltar? As informações não salvas serão perdidas.",
                                         "Confirmação",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
                this.Close();
        }
        private void RecalcularValoresReais(ContasAPagar parcela)
        {
            if (parcela == null) return;

            decimal valorBase = parcela.ValorParcela;
            DateTime dataVenc = parcela.DataVencimento;
            DateTime dataPag = datePagamento.Value;
            int diasAtraso = (dataPag - dataVenc).Days;

            decimal valorMultaR = 0m;
            decimal valorJurosR = 0m;
            decimal valorDescontoR = 0m;

            if (diasAtraso > 0)
            {
                valorMultaR = Math.Round(valorBase * (parcela.Multa / 100), 2, MidpointRounding.AwayFromZero);
                valorJurosR = Math.Round(valorBase * ((parcela.Juros / 100) / 30m) * diasAtraso, 2, MidpointRounding.AwayFromZero);
            }
            else
            {
                valorDescontoR = Math.Round(valorBase * (parcela.Desconto / 100), 2, MidpointRounding.AwayFromZero);
            }

            txtMultaReais.Text = valorMultaR.ToString("C2");
            txtJurosReais.Text = valorJurosR.ToString("C2");
            txtDescontoReais.Text = valorDescontoR.ToString("C2");

            AtualizarValorPago();
        }

    }
}
