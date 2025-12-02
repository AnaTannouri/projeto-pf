using ProjetoPF.Dao.Vendas;
using ProjetoPF.Modelos.Venda;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroContaAReceber : ProjetoPF.FormCadastros.FrmCadastroPai
    {
        private bool valorEditadoManualmente = false;
        public bool ModoVisualizacao { get; set; } = false;
        public FrmCadastroContaAReceber()
        {
            InitializeComponent();
        }

        private void FrmCadastroContaAReceber_Load(object sender, EventArgs e)
        {

            datePagamento.Format = DateTimePickerFormat.Custom;
            datePagamento.CustomFormat = "dd/MM/yyyy";
            datePagamento.Value = DateTime.Today;
            datePagamento.MaxDate = DateTime.Today;

            dateEmissao.Format = DateTimePickerFormat.Custom;
            dateEmissao.CustomFormat = "dd/MM/yyyy";

            dateVencimento.Format = DateTimePickerFormat.Custom;
            dateVencimento.CustomFormat = "dd/MM/yyyy";

            BloquearCampos();
            LiberarCamposBaixa();

            txtValorPago.KeyPress += ApenasNumerosDecimais;
            txtMultaValor.KeyPress += ApenasNumerosDecimais;
            txtJurosValor.KeyPress += ApenasNumerosDecimais;
            txtDesc.KeyPress += ApenasNumerosDecimais;

            txtValorPago.Leave += FormatarMoedaAoSair;
            txtMultaValor.Leave += FormatarMoedaAoSair;
            txtJurosValor.Leave += FormatarMoedaAoSair;
            txtDesc.Leave += FormatarMoedaAoSair;

            txtValorPago.TextChanged += (s, ev) => valorEditadoManualmente = true;
            txtValorPago.TextChanged += ValorPagoEditado;

            checkAtivo.Visible = false;
        }
        private void BloquearCampos()
        {
            foreach (Control ctrl in this.Controls)
                BloquearCtrl(ctrl);
        }

        private void BloquearCtrl(Control ctrl)
        {
            if (ctrl is TextBox txt)
                txt.ReadOnly = true;

            if (ctrl is ComboBox cb)
                cb.Enabled = false;

            if (ctrl is DateTimePicker dt)
                dt.Enabled = false;

            if (ctrl is Button btn && btn != btnSalvar && btn != btnVoltar)
                btn.Enabled = false;

            if (ctrl.HasChildren)
            {
                foreach (Control child in ctrl.Controls)
                    BloquearCtrl(child);
            }
        }

        private void LiberarCamposBaixa()
        {
            txtMultaValor.ReadOnly = false;
            txtJurosValor.ReadOnly = false;
            txtDesc.ReadOnly = false;
            txtValorPago.ReadOnly = false;
            txtObservacao.ReadOnly = false;

            datePagamento.Enabled = true;
        }
        public void BloquearCamposVisualizacao()
        {
            BloquearCamposVisualizacao(this);
        }

        private void BloquearCamposVisualizacao(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is TextBox txt)
                    txt.ReadOnly = true;

                else if (ctrl is DateTimePicker dt)
                    dt.Enabled = false;

                else if (ctrl is ComboBox cb)
                    cb.Enabled = false;

                else if (ctrl is Button btn && btn != btnVoltar)
                    btn.Enabled = false;

                if (ctrl.HasChildren)
                    BloquearCamposVisualizacao(ctrl);
            }
        }

        public void CarregarParcela(ContasAReceber conta)
        {

            if (ModoVisualizacao)
                valorEditadoManualmente = true;

            txtCodigo.Text = conta.Modelo;
            txtSerie.Text = conta.Serie;
            txtNumNota.Text = conta.NumeroNota;
            txtNumParcela.Text = conta.NumeroParcela.ToString();
            txtCodCliente.Text = conta.IdCliente.ToString();
            txtCliente.Text = conta.NomeCliente;

            txtValorParcela.Text = conta.ValorParcela.ToString("C2");

            dateEmissao.Value = conta.DataEmissao.Year < 1900 ? DateTime.Today : conta.DataEmissao;
            dateVencimento.Value = conta.DataVencimento.Year < 1900 ? DateTime.Today : conta.DataVencimento;

            CodForma.Text = conta.IdFormaPagamento.ToString();
            txtForma.Text = conta.FormaPagamentoDescricao ?? "";

            string situacao = conta.Situacao?.Trim() ?? "Em Aberto";

            if (ModoVisualizacao)
            {
                btnSalvar.Visible = false;
                datePagamento.Enabled = false;

                txtMultaPorc.Text = conta.Multa.ToString("N2");
                txtJurosPorc.Text = conta.Juros.ToString("N2");
                txtDescPorc.Text = conta.Desconto.ToString("N2");

                if (situacao.Equals("Recebida", StringComparison.OrdinalIgnoreCase))
                {
                    if (conta.DataPagamento.Year < 1900)
                    {
                        datePagamento.CustomFormat = " ";
                    }
                    else
                    {
                        datePagamento.CustomFormat = "dd/MM/yyyy";
                        datePagamento.Value = conta.DataPagamento;
                    }

                    txtMultaValor.Text = conta.MultaValor.ToString("C2");
                    txtJurosValor.Text = conta.JurosValor.ToString("C2");
                    txtDesc.Text = conta.DescontoValor.ToString("C2");
                    txtValorPago.Text = conta.ValorFinalParcela.ToString("C2");
                }
                else
                {
                    datePagamento.CustomFormat = " ";
                    txtMultaValor.Text = "R$ 0,00";
                    txtJurosValor.Text = "R$ 0,00";
                    txtDesc.Text = "R$ 0,00";
                    txtValorPago.Text = "R$ 0,00";
                }

                labelCriacao.Text = conta.DataCriacao.ToString("dd/MM/yyyy HH:mm");
                lblAtualizacao.Text = conta.DataAtualizacao.ToString("dd/MM/yyyy HH:mm");

                return; 
            }
            datePagamento.CustomFormat = "dd/MM/yyyy";
            datePagamento.Value = DateTime.Today;
            datePagamento.Enabled = true;
            btnSalvar.Visible = true;

            txtMultaPorc.Text = conta.Multa.ToString("N2");
            txtJurosPorc.Text = conta.Juros.ToString("N2");
            txtDescPorc.Text = conta.Desconto.ToString("N2");

            RecalcularValores(conta);

            int diasAtraso2 = (datePagamento.Value - conta.DataVencimento).Days;
            if (diasAtraso2 > 0)
            {
                decimal jurosCalc = conta.ValorParcela * ((conta.Juros / 100) / 30m) * diasAtraso2;
                txtJurosValor.Text = jurosCalc.ToString("C2");
            }

            labelCriacao.Text = conta.DataCriacao.ToString("dd/MM/yyyy HH:mm");
            lblAtualizacao.Text = conta.DataAtualizacao.ToString("dd/MM/yyyy HH:mm");

            AtualizarValorRecebido();
        }
        private void RecalcularValores(ContasAReceber conta)
        {
            decimal baseValor = conta.ValorParcela;
            int diasAtraso = (datePagamento.Value - conta.DataVencimento).Days;

            decimal multaR = 0, jurosR = 0, descontoR = 0;

            if (diasAtraso > 0)
            {
                multaR = Math.Round(baseValor * (conta.Multa / 100), 2);
                jurosR = Math.Round(baseValor * ((conta.Juros / 100) / 30m) * diasAtraso, 2);
            }
            else
            {
                descontoR = Math.Round(baseValor * (conta.Desconto / 100), 2);
            }

            txtMultaValor.Text = multaR.ToString("C2");
            txtJurosValor.Text = jurosR.ToString("C2");
            txtDesc.Text = descontoR.ToString("C2");

            AtualizarValorRecebido();
        }

        private void AtualizarValorRecebido()
        {
            if (valorEditadoManualmente) return;

            decimal baseVal = Converter(txtValorParcela.Text);
            decimal multa = Converter(txtMultaValor.Text);
            decimal juros = Converter(txtJurosValor.Text);
            decimal desc = Converter(txtDesc.Text);

            decimal total = baseVal + multa + juros - desc;
            if (total < 0) total = 0;

            txtValorPago.TextChanged -= ValorPagoEditado;
            txtValorPago.Text = total.ToString("C2");
            txtValorPago.TextChanged += ValorPagoEditado;
        }

        private void ValorPagoEditado(object sender, EventArgs e)
        {
            valorEditadoManualmente = true;
        }

        private decimal Converter(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return 0;

            texto = texto.Replace("R$", "").Trim();
            texto = texto.Replace(".", "").Replace(",", ".");

            decimal.TryParse(texto, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor);
            return valor;
        }

        private void ApenasNumerosDecimais(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) &&
                e.KeyChar != ',' && e.KeyChar != '.')
                e.Handled = true;
        }

        private void FormatarMoedaAoSair(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            string t = txt.Text.Replace("R$", "").Trim();

            if (decimal.TryParse(t, NumberStyles.Any, CultureInfo.GetCultureInfo("pt-BR"), out decimal v))
                txt.Text = v.ToString("C2");
            else
                txt.Text = "R$ 0,00";
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (ModoVisualizacao)
                return;
            try
            {
                var conta = new ContasAReceber
                {
                    Modelo = txtCodigo.Text,
                    Serie = txtSerie.Text,
                    NumeroNota = txtNumNota.Text,
                    IdCliente = int.Parse(txtCodCliente.Text),
                    NumeroParcela = int.Parse(txtNumParcela.Text),

                    ValorParcela = Converter(txtValorParcela.Text),
                    ValorFinalParcela = Converter(txtValorPago.Text),

                    Multa = Converter(txtMultaPorc.Text),
                    Juros = Converter(txtJurosPorc.Text),
                    Desconto = Converter(txtDescPorc.Text),

                    MultaValor = Converter(txtMultaValor.Text),
                    JurosValor = Converter(txtJurosValor.Text),
                    DescontoValor = Converter(txtDesc.Text),

                    DataPagamento = datePagamento.Value,
                    Situacao = "Recebida",
                    Observacao = txtObservacao.Text
                };

                var dao = new ContasAReceberDao();
                dao.SalvarOuAtualizar(conta);

                MessageBox.Show("Baixa registrada com sucesso!",
                                "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao registrar baixa:\n" + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
