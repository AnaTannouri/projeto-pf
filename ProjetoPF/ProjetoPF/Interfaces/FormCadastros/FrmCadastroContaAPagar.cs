using ProjetoPF.Dao;
using ProjetoPF.Dao.Compras;
using ProjetoPF.Dao.Pessoa;
using ProjetoPF.FormConsultas;
using ProjetoPF.Interfaces.FormConsultas;
using ProjetoPF.Modelos.Compra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroContaAPagar : ProjetoPF.FormCadastros.FrmCadastroPai
    {
        FrmConsultaFornecedor frmConsultaFornecedor = new FrmConsultaFornecedor();
        FrmConsultaFormaPagamento frmConsultaFormaPagamento = new FrmConsultaFormaPagamento();
        public bool ModoVisualizacao { get; set; } = false;
        public bool ModoBaixa { get; set; } = false;

        public FrmCadastroContaAPagar()
        {
            InitializeComponent();
        }
        private bool ValidarCamposObrigatorios()
        {
            if (string.IsNullOrWhiteSpace(txtCodigo.Text))
            {
                MessageBox.Show("O campo Modelo é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCodigo.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtSerie.Text))
            {
                MessageBox.Show("O campo Série é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSerie.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumNota.Text))
            {
                MessageBox.Show("O campo Número da Nota é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumNota.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCodFornecedor.Text))
            {
                MessageBox.Show("Selecione um fornecedor antes de salvar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnPesquisarFornecedor.Focus();
                return false;
            }
            if (dateEmissao.CustomFormat == " ")
            {
                MessageBox.Show("A Data de Emissão é obrigatória.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateEmissao.Focus();
                return false;
            }

            if (dateVencimento.CustomFormat == " ")
            {
                MessageBox.Show("A Data de Vencimento é obrigatória.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateVencimento.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNumParcela.Text))
            {
                MessageBox.Show("O campo Número da Parcela é obrigatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumParcela.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtValorParcela.Text) || txtValorParcela.Text == "R$ 0,00")
            {
                MessageBox.Show("O campo Valor da Parcela é obrigatório e deve ser maior que zero.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtValorParcela.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCodForma.Text))
            {
                MessageBox.Show("Selecione uma Forma de Pagamento.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnPesquisarForma.Focus();
                return false;
            }

            return true;
        }
        private void FrmCadastroContaAPagar_Load(object sender, EventArgs e)
        {

            if (ModoVisualizacao)
            {
                // 🔹 Garante formato fixo de data (dd/MM/yyyy)
                dateEmissao.Format = DateTimePickerFormat.Custom;
                dateEmissao.CustomFormat = "dd/MM/yyyy";

                dateVencimento.Format = DateTimePickerFormat.Custom;
                dateVencimento.CustomFormat = "dd/MM/yyyy";

            }

            label1.Text = "Modelo:";
            checkAtivo.Enabled = false;

            dateEmissao.Format = DateTimePickerFormat.Custom;
            dateEmissao.CustomFormat = " ";

            dateVencimento.Format = DateTimePickerFormat.Custom;
            dateVencimento.MinDate = DateTimePicker.MinimumDateTime;
            dateVencimento.CustomFormat = " ";

            txtCodigo.KeyPress += ApenasNumeros_KeyPress;
            txtSerie.KeyPress += ApenasNumeros_KeyPress;
            txtNumNota.KeyPress += ApenasNumeros_KeyPress;
            txtNumParcela.KeyPress += ApenasNumeros_KeyPress;

            txtCodigo.TextChanged += CamposCompra_TextChanged;
            txtSerie.TextChanged += CamposCompra_TextChanged;
            txtNumNota.TextChanged += CamposCompra_TextChanged;
            txtCodFornecedor.TextChanged += CamposCompra_TextChanged;

            txtValorParcela.KeyPress += ApenasNumerosDecimais_KeyPress;
            txtValorParcela.Leave += FormatarComoReal_Leave;
            txtValorParcela.Enter += RemoverSimboloReal_Enter;

            txtMulta.KeyPress += ApenasNumerosDecimais_KeyPress;
            txtJuros.KeyPress += ApenasNumerosDecimais_KeyPress;
            txtDesconto.KeyPress += ApenasNumerosDecimais_KeyPress;

            txtMulta.Leave += FormatarPercentual_Leave;
            txtJuros.Leave += FormatarPercentual_Leave;
            txtDesconto.Leave += FormatarPercentual_Leave;
        }
        private void CamposCompra_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCodigo.Text) &&
                !string.IsNullOrWhiteSpace(txtSerie.Text) &&
                !string.IsNullOrWhiteSpace(txtNumNota.Text) &&
                !string.IsNullOrWhiteSpace(txtCodFornecedor.Text))
            {
                VerificarCompraExistente();
            }
        }
        private void btnPesquisarFornecedor_Click(object sender, EventArgs e)
        {
            frmConsultaFornecedor.Owner = this;
            frmConsultaFornecedor.ShowDialog();
        }

        private void btnPesquisarForma_Click(object sender, EventArgs e)
        {
            frmConsultaFormaPagamento.Owner = this;
            frmConsultaFormaPagamento.ShowDialog();
        }

        private void dateEmissao_ValueChanged(object sender, EventArgs e)
        {
            dateEmissao.CustomFormat = "dd/MM/yyyy";
            dateVencimento.MinDate = dateEmissao.Value;
            if (dateVencimento.Value < dateEmissao.Value)
            {
                dateVencimento.Value = dateEmissao.Value;
            }
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
            }

            btnSalvar.Visible = false;
        }
        public void CarregarConta(ContasAPagar conta)
        {

            if (conta == null) return;

            txtCodigo.Text = conta.Modelo.ToString();
            txtSerie.Text = conta.Serie;
            txtNumNota.Text = conta.NumeroNota;
            txtCodFornecedor.Text = conta.IdFornecedor.ToString();

            var fornecedorDao = new FornecedorDAO();
            var fornecedor = fornecedorDao.BuscarPorId(conta.IdFornecedor);
            txtFornecedor.Text = fornecedor?.NomeRazaoSocial ?? "-";

            txtNumParcela.Text = conta.NumeroParcela.ToString();
            txtValorParcela.Text = conta.ValorParcela.ToString("C2");

            txtMulta.Text = conta.Multa.ToString("N2");
            txtJuros.Text = conta.Juros.ToString("N2");
            txtDesconto.Text = conta.Desconto.ToString("N2");

            dateEmissao.Value = conta.DataEmissao;
            dateVencimento.Value = conta.DataVencimento;

            // 🔹 Busca e preenche a forma de pagamento
            var formaDao = new FormaPagamentoDAO();
            var forma = formaDao.BuscarPorId(conta.IdFormaPagamento);
            if (forma != null)
            {
                txtCodForma.Text = forma.Id.ToString();
                txtForma.Text = forma.Descricao;
            }
            else
            {
                txtCodForma.Text = "";
                txtForma.Text = "-";
            }

            if (conta.DataCriacao != DateTime.MinValue)
                labelCriacao.Text = conta.DataCriacao.ToString("dd/MM/yyyy HH:mm:ss");
            else
                labelCriacao.Text = "-";

            if (conta.DataAtualizacao != DateTime.MinValue)
                lblAtualizacao.Text = conta.DataAtualizacao.ToString("dd/MM/yyyy HH:mm:ss");
            else
                lblAtualizacao.Text = "-";
        }
        private void dateVencimento_ValueChanged(object sender, EventArgs e)
        {
            dateVencimento.CustomFormat = "dd/MM/yyyy";
        }

        private void ApenasNumeros_KeyPress(object sender, KeyPressEventArgs e)
        { 
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void ApenasNumerosDecimais_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            TextBox txt = sender as TextBox;

            if ((e.KeyChar == ',' || e.KeyChar == '.') && (txt.Text.Contains(",") || txt.Text.Contains(".")))
            {
                e.Handled = true;
            }

            if (e.KeyChar == '.')
            {
                e.KeyChar = ',';
            }
        }
        private void FormatarComoReal_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            string valor = txt.Text.Replace("R$", "").Trim();

            if (string.IsNullOrEmpty(valor))
            {
                txt.Text = "R$ 0,00";
                return;
            }

            if (decimal.TryParse(valor, out decimal valorDecimal))
            {
                txt.Text = string.Format(System.Globalization.CultureInfo.GetCultureInfo("pt-BR"), "R$ {0:N2}", valorDecimal);
            }
            else
            {
                txt.Text = "R$ 0,00";
            }
        }
        private void RemoverSimboloReal_Enter(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            txt.Text = txt.Text.Replace("R$", "").Trim();
        }
        private void FormatarPercentual_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            string valor = txt.Text.Trim().Replace("%", "");

            if (string.IsNullOrEmpty(valor))
            {
                txt.Text = "0,00%";
                return;
            }

            if (!valor.Contains(","))
            {
                valor += ",00";
            }

            txt.Text = valor + "%";
        }
        private void LimparCampos()
        {
            txtCodigo.Clear();
            txtSerie.Clear();
            txtNumNota.Clear();
            txtCodFornecedor.Clear();
            txtFornecedor.Clear();
            txtNumParcela.Clear();
            txtValorParcela.Clear();
            txtMulta.Clear();
            txtJuros.Clear();
            txtDesconto.Clear();
            txtCodForma.Clear();
            txtForma.Clear();

            dateEmissao.Format = DateTimePickerFormat.Custom;
            dateEmissao.CustomFormat = " ";
            dateVencimento.Format = DateTimePickerFormat.Custom;
            dateVencimento.CustomFormat = " ";

            checkAtivo.Checked = false;
        }
        protected virtual void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarCamposObrigatorios())
                    return; 

                decimal.TryParse(txtMulta.Text.Replace("%", "").Trim(), out decimal multa);
                decimal.TryParse(txtJuros.Text.Replace("%", "").Trim(), out decimal juros);
                decimal.TryParse(txtDesconto.Text.Replace("%", "").Trim(), out decimal desconto);

                var conta = new ContasAPagar
                {
                    Modelo = int.Parse(txtCodigo.Text.Trim()),
                    Serie = txtSerie.Text.Trim(),
                    NumeroNota = txtNumNota.Text.Trim(),
                    IdFornecedor = int.Parse(txtCodFornecedor.Text),
                    NumeroParcela = int.Parse(txtNumParcela.Text),
                    ValorParcela = decimal.Parse(txtValorParcela.Text.Replace("R$", "").Trim()),
                    IdFormaPagamento = int.Parse(txtCodForma.Text),
                    DataEmissao = dateEmissao.Value,
                    DataVencimento = dateVencimento.Value,
                    Multa = multa,       
                    Juros = juros,        
                    Desconto = desconto,  
                    Ativo = true
                };

                var dao = new ContasAPagarDao();
                dao.SalvarOuAtualizar(conta);

                MessageBox.Show("Conta a pagar salva com sucesso!",
                     "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (this.Owner is FrmConsultaContasAPagar frmConsulta)
                {
                    frmConsulta.PopularListView(string.Empty);
                }

                LimparCampos();
                this.Close();   
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar conta: {ex.Message}",
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
        private void VerificarCompraExistente()
        {
            if (ModoBaixa)
                return;
            if (string.IsNullOrWhiteSpace(txtCodigo.Text) ||
       string.IsNullOrWhiteSpace(txtSerie.Text) ||
       string.IsNullOrWhiteSpace(txtNumNota.Text) ||
       string.IsNullOrWhiteSpace(txtCodFornecedor.Text))
                return;

            try
            {
                var dao = new ContasAPagarDao();
                bool existe = dao.CompraJaExiste(
                    txtCodigo.Text.Trim(),
                    txtSerie.Text.Trim(),
                    txtNumNota.Text.Trim(),
                    int.Parse(txtCodFornecedor.Text)
                );

                if (existe)
                {
                    MessageBox.Show(
                        "Já existe uma conta a pagar com este modelo, série, número da nota e fornecedor.\n" +
                        "Não é permitido adicionar uma nova parcela para a mesma compra.",
                        "Duplicidade detectada",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    BloquearCampos();
                }
                else
                {
                    DesbloquearCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao verificar duplicidade: {ex.Message}",
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void BloquearCampos()
        {
            txtNumParcela.Enabled = false;
            txtValorParcela.Enabled = false;
            txtMulta.Enabled = false;
            txtJuros.Enabled = false;
            txtDesconto.Enabled = false;
            txtCodForma.Enabled = false;
            txtForma.Enabled = false;
            btnPesquisarForma.Enabled = false;
            dateEmissao.Enabled = false;
            dateVencimento.Enabled = false;
            btnSalvar.Enabled = false;
        }

        private void DesbloquearCampos()
        {
            txtNumParcela.Enabled = true;
            txtValorParcela.Enabled = true;
            txtMulta.Enabled = true;
            txtJuros.Enabled = true;
            txtDesconto.Enabled = true;
            btnPesquisarForma.Enabled = true;
            dateEmissao.Enabled = true;
            dateVencimento.Enabled = true;
            btnSalvar.Enabled = true;
        }
    }
}
