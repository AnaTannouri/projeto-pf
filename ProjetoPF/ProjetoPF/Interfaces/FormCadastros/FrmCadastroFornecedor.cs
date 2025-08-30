using ProjetoPF.Dao;
using ProjetoPF.FormCadastros;
using ProjetoPF.FormConsultas;
using ProjetoPF.Interfaces.FormConsultas;
using ProjetoPF.Modelos.Localizacao;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Modelos.Pessoa;
using ProjetoPF.Servicos;
using ProjetoPF.Servicos.Localizacao;
using ProjetoPF.Servicos.Pessoa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroFornecedor : ProjetoPF.Interfaces.FormCadastros.FrmCadastroPessoa
    {
        private Fornecedor fornecedor = new Fornecedor();
        private FrmCadastroCondPagamento frmCadastroCondPagamento = new FrmCadastroCondPagamento();
   
        private BaseServicos<CondicaoPagamento> condicacaoServices = new BaseServicos<CondicaoPagamento>(new BaseDao<CondicaoPagamento>("CondicaoPagamentos"));
        private BaseServicos<Cidade> cidadeServices = new BaseServicos<Cidade>(new BaseDao<Cidade>("Cidades"));
        private BaseServicos<Pais> paisServices = new BaseServicos<Pais>(new BaseDao<Pais>("Paises"));
        private BaseServicos<Fornecedor> fornecedorServicos = new BaseServicos<Fornecedor>(new BaseDao<Fornecedor>("Fornecedores"));
        FornecedorServicos servicoFornecedor = new FornecedorServicos();

        private bool carregandoCondicoes = false;
        private bool carregandoCidades = false;

        private bool isEditando = false;
        private bool isExcluindo = false;
        public FrmCadastroFornecedor()
        {
            InitializeComponent();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = " ";

            txtCpf_Cnpj.KeyPress += SomenteNumerosPontuacao_KeyPress;
            txtTelefone.KeyPress += Telefone_KeyPress;
            txtCep.KeyPress += SomenteNumerosPontuacao_KeyPress;
            txtNumero.KeyPress += SomenteNumeros_KeyPress;
            txtValorMin.KeyPress += SomenteNumerosPontuacao_KeyPress;
        }
        private bool ValidarCpf(string cpf)
        {
            cpf = new string(cpf.Where(char.IsDigit).ToArray());
            if (cpf.Length != 11 || cpf.All(c => c == cpf[0])) return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito += resto.ToString();

            return cpf.EndsWith(digito);
        }
        private bool ValidarCnpj(string cnpj)
        {
            cnpj = new string(cnpj.Where(char.IsDigit).ToArray());
            if (cnpj.Length != 14 || cnpj.All(c => c == cnpj[0])) return false;

            int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            string digito = resto.ToString();
            tempCnpj += digito;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            digito += resto.ToString();

            return cnpj.EndsWith(digito);
        }

        private bool ValidarEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        private bool ValidarEntrada()
        {
            if (!isEditando && comboPessoa.SelectedItem == null)
            {
                MessageBox.Show("Selecione um tipo de pessoa.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNome_RazaoSocial.Text))
            {
                MessageBox.Show("Informe o nome ou razão social do fornecedor.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtRua.Text))
            {
                MessageBox.Show("Informe o endereço do fornecedor.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show("Informe o número da residência do fornecedor.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtBairro.Text))
            {
                MessageBox.Show("Informe o bairro do fornecedor.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCep.Text))
            {
                MessageBox.Show("Informe o cep do fornecedor.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCidade.Text))
            {
                MessageBox.Show("Informe a cidade do fornecedor.");
                return false;
            }
            if (!ValidarEmail(txtEmail.Text))
            {
                MessageBox.Show("E-mail inválido.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                MessageBox.Show("Informe o telefone do fornecedor.");
                return false;
            }
            else
            {
                string nomeCidade = txtCidade.Text.Trim();
                if (!string.IsNullOrEmpty(nomeCidade))
                {
                    var cidadeSelecionada = cidadeServices.BuscarTodos()
                        .FirstOrDefault(c => c.Nome.Equals(nomeCidade, StringComparison.OrdinalIgnoreCase));

                    if (cidadeSelecionada != null)
                    {
                        var estadoServices = new EstadoServicos();
                        var estado = estadoServices.BuscarPorId(cidadeSelecionada.IdEstado);

                        if (estado != null)
                        {
                            var pais = paisServices.BuscarPorId(estado.IdPais);
                            string tipoPessoa = comboPessoa.SelectedItem?.ToString().ToUpper() ?? "";

                            if (pais != null)
                            {
                                if (tipoPessoa == "FÍSICA")
                                {
                                    if (pais.Nome.Equals("Brasil", StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (string.IsNullOrWhiteSpace(txtCpf_Cnpj.Text))
                                        {
                                            MessageBox.Show("Informe o CPF do fornecedor (obrigatório para pessoa física no Brasil).");
                                            return false;
                                        }
                                        else if (!ValidarCpf(txtCpf_Cnpj.Text))
                                        {
                                            MessageBox.Show("CPF inválido.");
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        if (string.IsNullOrWhiteSpace(txtRg_InscricaoEstadual.Text))
                                        {
                                            MessageBox.Show("Informe o RG do fornecedor (obrigatório para pessoa física estrangeira).");
                                            return false;
                                        }
                                    }
                                }
                                else if (tipoPessoa == "JURÍDICA")
                                {
                                    if (string.IsNullOrWhiteSpace(txtCpf_Cnpj.Text))
                                    {
                                        MessageBox.Show("Informe o CNPJ do fornecedor (obrigatório para pessoa jurídica).");
                                        return false;
                                    }
                                    else if (!ValidarCnpj(txtCpf_Cnpj.Text))
                                    {
                                        MessageBox.Show("CNPJ inválido.");
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (string.IsNullOrWhiteSpace(txtCondicao.Text))
            {
                MessageBox.Show("Informe a condição de pagamento do fornecedor.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtValorMin.Text))
            {
                MessageBox.Show("Informe o valor minímo de pedido do fornecedor.");
                return false;
            }
            if (servicoFornecedor.DocumentoDuplicado(fornecedor))
            {
                MessageBox.Show("Já existe um fornecedor com o mesmo CPF, CNPJ ou RG.", "Duplicidade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public void LimparFormulario()
        {
            txtCodigo.Clear();
            txtNome_RazaoSocial.Clear();
            txtApelido_NomeFantasia.Clear();
            txtCpf_Cnpj.Clear();
            txtRg_InscricaoEstadual.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            txtRua.Clear();
            txtNumero.Clear();
            txtBairro.Clear();
            txtCep.Clear();
            txtCodigoCidade.Clear();
            txtCodigoCondicao.Clear();
            txtValorMin.Clear();
            checkAtivo.Checked = true;
            checkAtivo.Enabled = false;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = " ";

            comboPessoa.SelectedIndex = -1;
            comboClassificacao.SelectedIndex = -1;

            isEditando = false;
            isExcluindo = false;
        }

        public void CarregarDados(Fornecedor fornecedorSelecionado, bool isEditandoForm, bool isExcluindoForm)
        {
            fornecedor = fornecedorSelecionado;
            isEditando = isEditandoForm;
            isExcluindo = isExcluindoForm;

            txtCodigo.Text = fornecedor.Id.ToString();
            txtNome_RazaoSocial.Text = fornecedor.NomeRazaoSocial;
            txtApelido_NomeFantasia.Text = fornecedor.ApelidoNomeFantasia;
            txtCpf_Cnpj.Text = fornecedor.CpfCnpj;
            txtRg_InscricaoEstadual.Text = fornecedor.RgInscricaoEstadual;
            txtEmail.Text = fornecedor.Email;
            txtTelefone.Text = fornecedor.Telefone;
            txtRua.Text = fornecedor.Rua;
            txtNumero.Text = fornecedor.Numero;
            txtBairro.Text = fornecedor.Bairro;
            txtCep.Text = fornecedor.Cep;
            txtComplemento.Text = fornecedor.Complemento;
            comboPessoa.SelectedItem = fornecedor.TipoPessoa;
            comboPessoa.Enabled = !isEditandoForm;
            comboClassificacao.SelectedItem = fornecedor.Classificacao;
            checkAtivo.Checked = fornecedor.Ativo;
            checkAtivo.Enabled = isEditandoForm;


            txtCodigoCidade.Text = fornecedor.IdCidade.ToString();

            var cidade = cidadeServices.BuscarPorId(fornecedor.IdCidade);
            if (cidade != null)
            {
                txtCidade.Text = cidade.Nome;

                var estadoServices = new EstadoServicos();
                var estado = estadoServices.BuscarPorId(cidade.IdEstado);
                txtUF.Text = estado?.UF ?? ""; 
            }
            else
            {
                txtCidade.Text = "Desconhecida";
                txtUF.Text = "";
            }

            txtCodigoCondicao.Text = fornecedor.CondicaoPagamentoId.ToString();
            var condicao = condicacaoServices.BuscarPorId(fornecedor.CondicaoPagamentoId);
            txtCondicao.Text = condicao != null ? condicao.Descricao : "Desconhecida";

            if (fornecedor.DataNascimentoCriacao.HasValue)
            {
                dateTimePicker1.Value = fornecedor.DataNascimentoCriacao.Value;
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            }

            txtValorMin.Text = fornecedor.ValorMinimoPedido.HasValue
                ? fornecedor.ValorMinimoPedido.Value.ToString("N2")
                : string.Empty;

            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";

            if (isExcluindo)
                BloquearCampos();
            else
                DesbloquearCampos();
        }

        public void BloquearCampos()
        {
            txtCodigo.Enabled = false;
            txtNome_RazaoSocial.Enabled = false;
            txtApelido_NomeFantasia.Enabled = false;
            txtCpf_Cnpj.Enabled = false;
            txtRg_InscricaoEstadual.Enabled = false;
            txtEmail.Enabled = false;
            txtTelefone.Enabled = false;
            txtRua.Enabled = false;
            txtNumero.Enabled = false;
            txtBairro.Enabled = false;
            txtCep.Enabled = false;
            txtCodigoCidade.Enabled = false;
            txtCodigoCondicao.Enabled = false;
            txtValorMin.Enabled = false;
            txtComplemento.Enabled = false;

            comboPessoa.Enabled = false;
            comboClassificacao.Enabled = false;

            dateTimePicker1.Enabled = false;

            button2.Enabled = false;
            btnCadastrarCidade.Enabled = false;
        }

        public void DesbloquearCampos()
        {
            txtCodigo.Enabled = false;
            txtNome_RazaoSocial.Enabled = true;
            txtCpf_Cnpj.Enabled = true;
            txtRg_InscricaoEstadual.Enabled = true;
            txtEmail.Enabled = true;
            txtTelefone.Enabled = true;
            txtRua.Enabled = true;
            txtNumero.Enabled = true;
            txtBairro.Enabled = true;
            txtCep.Enabled = true;
            txtValorMin.Enabled = true; 
        }

        private void FrmCadastroFornecedor_Load(object sender, EventArgs e)
        {
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            dateTimePicker1.MaxDate = DateTime.Today;
            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";
            labelCriacao.Text = fornecedor.DataCriacao > DateTime.MinValue ? fornecedor.DataCriacao.ToShortDateString() : "";
            lblAtualizacao.Text = fornecedor.DataAtualizacao > DateTime.MinValue ? fornecedor.DataAtualizacao.ToShortDateString() : "";
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (isExcluindo)
                {
                    if (fornecedor != null && fornecedor.Id != 0)
                    {
                        if (MessageBox.Show("Deseja realmente remover este fornecedor?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            fornecedorServicos.Remover(fornecedor.Id);
                            MessageBox.Show("Fornecedor removido com sucesso!");
                            LimparFormulario();
                            Sair();
                        }
                    }
                    return;
                }

                AtualizarObjeto();
                if (!ValidarEntrada())
                {
                    return;
                }       

                if (isEditando)
                {
                    AtualizarObjeto();
                    fornecedorServicos.Atualizar(fornecedor);
                    MessageBox.Show("Fornecedor atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    fornecedorServicos.Criar(fornecedor);
                    MessageBox.Show("Fornecedor cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimparFormulario();
                Sair();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE"))
                {
                    MessageBox.Show("Não é possível remover o fornecedor, pois ele está vinculado a outro(s) registro(s).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Erro ao salvar ou atualizar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void AtualizarObjeto()
        {
            fornecedor.TipoPessoa = comboPessoa.Text;
            fornecedor.NomeRazaoSocial = txtNome_RazaoSocial.Text.Trim();
            fornecedor.ApelidoNomeFantasia = txtApelido_NomeFantasia.Text.Trim();
            fornecedor.RgInscricaoEstadual = txtRg_InscricaoEstadual.Text.Trim();
            fornecedor.Email = txtEmail.Text.Trim();
            fornecedor.Telefone = txtTelefone.Text.Trim();
            fornecedor.Rua = txtRua.Text.Trim();
            fornecedor.Numero = txtNumero.Text.Trim();
            fornecedor.Bairro = txtBairro.Text.Trim();
            fornecedor.Cep = txtCep.Text.Trim();
            fornecedor.Classificacao = comboClassificacao.SelectedItem?.ToString();
            fornecedor.DataNascimentoCriacao = dateTimePicker1.CustomFormat == " " ? (DateTime?)null : dateTimePicker1.Value;
            fornecedor.Complemento = txtComplemento.Text.Trim();
            fornecedor.Complemento = txtComplemento.Text.Trim();
            fornecedor.Ativo = checkAtivo.Checked;

            if (!int.TryParse(txtCodigoCidade.Text, out int idCidade))
                throw new Exception("Código da cidade inválido.");
            fornecedor.IdCidade = idCidade;

            fornecedor.CpfCnpj = txtCpf_Cnpj.Text.Trim();

            if (!int.TryParse(txtCodigoCondicao.Text, out int idCondicao))
                throw new Exception("Código da condição de pagamento inválido.");
            fornecedor.CondicaoPagamentoId = idCondicao;

            fornecedor.Id = (isEditando || isExcluindo) ? int.Parse(txtCodigo.Text) : 0;

            fornecedor.DataCriacao = fornecedor.DataCriacao == DateTime.MinValue ? DateTime.Now : fornecedor.DataCriacao;
            fornecedor.DataAtualizacao = DateTime.Now;

            if (decimal.TryParse(txtValorMin.Text, out var valor))
                fornecedor.ValorMinimoPedido = valor;
            else
                fornecedor.ValorMinimoPedido = null;
        }

        private void btnCadastrarCidade_Click(object sender, EventArgs e)
        {
            FrmConsultaCidade frmConsultaCidade = new FrmConsultaCidade();
            frmConsultaCidade.Owner = this;
            frmConsultaCidade.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmConsultaCondPagamento frmConsultaCondPagamento = new FrmConsultaCondPagamento();
            frmConsultaCondPagamento.Owner = this;
            frmConsultaCondPagamento.ShowDialog();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
        }

        private void comboPessoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboPessoa.SelectedItem?.ToString() == "FÍSICA")
            {
                label4.Text = "Fornecedor:";
            }

            if (comboPessoa.SelectedItem?.ToString() == "JURÍDICA")
            {
                label4.Text = "Fornecedor:";
            }
        }
        private void SomenteNumeros_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void SomenteNumerosPontuacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
                return;

            if ((e.KeyChar == ',' || e.KeyChar == '.') && !txt.Text.Contains(',') && !txt.Text.Contains('.'))
                return;
            e.Handled = true;
        }

        private void Telefone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '(' &&
                e.KeyChar != ')' &&
                e.KeyChar != '-' &&
                e.KeyChar != ' ')
            {
                e.Handled = true;
            }
        }
    }
}
