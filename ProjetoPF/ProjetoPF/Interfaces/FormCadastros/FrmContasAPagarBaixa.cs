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

            // 🔹 Agora liga apenas o evento desta tela
            btnSalvar.Click += new EventHandler(this.btnSalvar_Click);

            // 🔹 Configurações específicas da tela de baixa
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
            if (decimal.TryParse(txt.Text.Replace("R$", "").Trim(),
                                 out decimal valor))
            {
                txt.Text = valor.ToString("C2");
            }
            else
            {
                txt.Text = "R$ 0,00";
            }
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

            decimal valorBase = parcela.ValorParcela;
            decimal valorFinal;

            if (DateTime.Today > parcela.DataVencimento)
            {
                valorFinal = valorBase + (valorBase * (parcela.Multa / 100)) + (valorBase * (parcela.Juros / 100));
            }
            else
            {
                valorFinal = valorBase - (valorBase * (parcela.Desconto / 100));
            }


            datePagamento.Value = DateTime.Now;

            dateEmissao.Enabled = false;
            dateVencimento.Enabled = false;
            valorPagoEditadoManualmente = false; // garante que o cálculo ocorra
            AtualizarValorPago();
            // Formata corretamente o valor pago
            decimal valorPagoDecimal = ConverterTextoParaDecimal(txtValorPago.Text);
            txtValorPago.Text = valorPagoDecimal.ToString("C2", CultureInfo.GetCultureInfo("pt-BR"));
            valorPagoEditadoManualmente = false; // mantém falso para permitir o próximo cálculo
        }
        private void AtualizarValorPago()
        {
            // Se o valor foi digitado manualmente, não recalcula automaticamente
            if (valorPagoEditadoManualmente)
                return;

            decimal valorBase = ConverterTextoParaDecimal(txtValorParcela.Text);
            decimal multa = ConverterTextoParaDecimal(txtMulta.Text);
            decimal juros = ConverterTextoParaDecimal(txtJuros.Text);
            decimal desconto = ConverterTextoParaDecimal(txtDesconto.Text);

            DateTime dataVenc = dateVencimento.Value;
            DateTime dataPag = datePagamento.Value;

            int diasAtraso = (dataPag - dataVenc).Days;
            decimal valorFinal = valorBase;

            if (diasAtraso > 0)
            {
                decimal valorMulta = valorBase * (multa / 100);
                decimal valorJurosProporcional = valorBase * ((juros / 100) / 30m) * diasAtraso;
                valorFinal = valorBase + valorMulta + valorJurosProporcional;
            }
            else if (diasAtraso <= 0)
            {
                valorFinal = valorBase - (valorBase * (desconto / 100));
            }

            // Atualiza o campo sem reentrar no evento
            txtValorPago.TextChanged -= (s, e) => valorPagoEditadoManualmente = true;
            txtValorPago.Text = valorFinal.ToString("C2", CultureInfo.GetCultureInfo("pt-BR"));
            txtValorPago.TextChanged += (s, e) => valorPagoEditadoManualmente = true;
        }
        private decimal CalcularValorFinal()
        {
            decimal valorBase = ConverterTextoParaDecimal(txtValorParcela.Text);
            decimal multa = ConverterTextoParaDecimal(txtMulta.Text);
            decimal juros = ConverterTextoParaDecimal(txtJuros.Text);
            decimal desconto = ConverterTextoParaDecimal(txtDesconto.Text);

            DateTime dataVenc = dateVencimento.Value;
            DateTime dataPag = datePagamento.Value;

            int diasAtraso = (dataPag - dataVenc).Days;
            decimal valorFinal = valorBase;

            if (diasAtraso > 0)
            {
                decimal valorMulta = valorBase * (multa / 100);
                decimal valorJurosProporcional = valorBase * ((juros / 100) / 30m) * diasAtraso;
                valorFinal = valorBase + valorMulta + valorJurosProporcional;
            }
            else
            {
                valorFinal = valorBase - (valorBase * (desconto / 100));
            }

            return valorFinal;
        }

        protected override void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // ✅ Calcula diretamente o valor final, sem depender de texto formatado
                decimal valorBase = ConverterTextoParaDecimal(txtValorParcela.Text);
                decimal multa = ConverterTextoParaDecimal(txtMulta.Text);
                decimal juros = ConverterTextoParaDecimal(txtJuros.Text);
                decimal desconto = ConverterTextoParaDecimal(txtDesconto.Text);

                DateTime dataVenc = dateVencimento.Value;
                DateTime dataPag = datePagamento.Value;

                int diasAtraso = (dataPag - dataVenc).Days;
                decimal valorFinal = valorBase;

                // ✅ Cálculo correto do valor com multa/juros/desconto
                if (diasAtraso > 0)
                {
                    decimal valorMulta = valorBase * (multa / 100);
                    decimal valorJurosProporcional = valorBase * ((juros / 100) / 30m) * diasAtraso;
                    valorFinal = valorBase + valorMulta + valorJurosProporcional;
                }
                else if (diasAtraso <= 0)
                {
                    valorFinal = valorBase - (valorBase * (desconto / 100));
                }


                // ✅ Validação da Data de Pagamento
                if (datePagamento.Value == DateTime.MinValue)
                {
                    MessageBox.Show("A data de pagamento é obrigatória.",
                                    "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ✅ Monta o objeto com o valor já calculado
                var conta = new ContasAPagar
                {
                    Modelo = string.IsNullOrWhiteSpace(txtCodigo.Text) ? 0 : int.Parse(txtCodigo.Text),
                    Serie = txtSerie.Text.Trim(),
                    NumeroNota = txtNumNota.Text.Trim(),
                    IdFornecedor = string.IsNullOrWhiteSpace(txtCodFornecedor.Text) ? 0 : int.Parse(txtCodFornecedor.Text),
                    NumeroParcela = string.IsNullOrWhiteSpace(txtNumParcela.Text) ? 0 : int.Parse(txtNumParcela.Text),

                    ValorParcela = valorBase,
                    ValorFinalParcela = valorFinal,
                    Multa = multa,
                    Juros = juros,
                    Desconto = desconto,

                    DataPagamento = datePagamento.Value,
                    Situacao = "Paga",
                    Observacao = txtObservacao.Text
                };

                // ✅ Salva no banco
                var dao = new ContasAPagarDao();
                dao.SalvarOuAtualizar(conta);
                // ✅ Atualiza lista da tela de consulta ANTES de fechar

                if (this.Owner is ProjetoPF.Interfaces.FormConsultas.FrmConsultaContasAPagar consultaForm)
                {
                    consultaForm.PopularListView(string.Empty);
                    consultaForm.BringToFront();  // traz a tela de consulta pro foco
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

            // Remove espaços não quebráveis e caracteres invisíveis
            texto = texto.Replace("\u00A0", "");  // Non-breaking space
            texto = texto.Replace("\u200B", "");  // Zero-width space

            // Remove o símbolo da moeda em qualquer forma
            texto = texto.Replace("R$", "");
            texto = texto.Replace("r$", "");
            texto = texto.Replace("R", "");
            texto = texto.Replace("$", "");
            texto = texto.Trim();

            // Remove pontos de milhar e padroniza vírgula/ponto
            texto = texto.Replace(".", "").Replace(",", ".");

            // Tenta converter com cultura invariante
            if (decimal.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
                return valor;

            // Tenta com cultura brasileira se ainda falhar
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
    }
}
