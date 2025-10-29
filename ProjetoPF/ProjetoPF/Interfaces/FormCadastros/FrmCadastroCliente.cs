using ProjetoPF.Dao;
using ProjetoPF.FormCadastros;
using ProjetoPF.FormConsultas;
using ProjetoPF.Interfaces.FormConsultas;
using ProjetoPF.Modelos.Localizacao;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Modelos.Pessoa;
using ProjetoPF.Servicos;
using ProjetoPF.Servicos.Localizacao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroCliente : FrmCadastroPessoa
    {
        private Cliente cliente = new Cliente();


        private FrmCadastroFormaPagamento frmCadastroFormaPagamento = new FrmCadastroFormaPagamento();
        private BaseServicos<FormaPagamento> formaServices = new BaseServicos<FormaPagamento>(new BaseDao<FormaPagamento>("FormaPagamentos"));

        private FrmCadastroCondPagamento frmCadastroCondPagamento = new FrmCadastroCondPagamento();
        private BaseServicos<CondicaoPagamento> condicacaoServices = new BaseServicos<CondicaoPagamento>(new BaseDao<CondicaoPagamento>("CondicaoPagamentos"));
        private BaseServicos<Cidade> cidadeServices = new BaseServicos<Cidade>(new BaseDao<Cidade>("Cidades"));
        private BaseServicos<Pais> paisServices = new BaseServicos<Pais>(new BaseDao<Pais>("Paises"));
        private ClienteServicos clienteServicos = new ClienteServicos();

        private bool carregandoFormas = false;
        private bool carregandoCondicoes = false;
        private bool carregandoCidades = false;
        private bool dataSelecionada = false;

        private bool isEditando = false;
        private bool isExcluindo = false;

        public FrmCadastroCliente()
        {
            InitializeComponent();
            CarregarTipoPessoa();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = " ";

            txtCpf_Cnpj.KeyPress += SomenteNumerosPontuacao_KeyPress; 
            txtCep.KeyPress += SomenteNumerosPontuacao_KeyPress;       
            txtNumero.KeyPress += SomenteNumeros_KeyPress;            
            txtTelefone.KeyPress += Telefone_KeyPress;
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
                MessageBox.Show("Selecione um tipo.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNome_RazaoSocial.Text))
            {
                MessageBox.Show("Informe o nome ou razão social do cliente.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtRua.Text))
            {
                MessageBox.Show("Informe o endereço do cliente.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show("Informe o número da residência do cliente.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBairro.Text))
            {
                MessageBox.Show("Informe o bairro do cliente.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCidade.Text))
            {
                MessageBox.Show("Selecione uma cidade para o cliente.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Informe o e-mail do cliente.");
                return false;
            }
            else if (!ValidarEmail(txtEmail.Text))
            {
                MessageBox.Show("E-mail inválido.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                MessageBox.Show("Informe o telefone do cliente.");
                return false;
            }

            string tipoPessoa = comboPessoa.SelectedItem?.ToString().ToUpper() ?? "";
            string nomeCidade = txtCidade.Text.Trim();

            var cidadeSelecionada = cidadeServices.BuscarTodos()
                .FirstOrDefault(c => c.Nome.Equals(nomeCidade, StringComparison.OrdinalIgnoreCase));

            if (cidadeSelecionada != null)
            {
                var estado = new EstadoServicos().BuscarPorId(cidadeSelecionada.IdEstado);
                if (estado != null)
                {
                    var pais = paisServices.BuscarPorId(estado.IdPais);
                    if (pais != null)
                    {
                        if (tipoPessoa == "FÍSICA")
                        {
                            if (pais.Nome.Equals("Brasil", StringComparison.OrdinalIgnoreCase))
                            {
                                if (string.IsNullOrWhiteSpace(txtCpf_Cnpj.Text))
                                {
                                    MessageBox.Show("Informe o CPF do cliente (obrigatório para pessoa física no Brasil).");
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
                                    MessageBox.Show("Informe o RG do cliente (obrigatório para pessoa física estrangeira).");
                                    return false;
                                }
                            }
                        }
                        else if (tipoPessoa == "JURÍDICA")
                        {
                            if (string.IsNullOrWhiteSpace(txtCpf_Cnpj.Text))
                            {
                                MessageBox.Show("Informe o CNPJ do cliente (obrigatório para pessoa jurídica).");
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
            if (string.IsNullOrWhiteSpace(txtCondicao.Text))
            {
                MessageBox.Show("Selecione uma condição para o cliente.");
                return false;
            }

            if (clienteServicos.DocumentoDuplicado(cliente))
            {
                MessageBox.Show("Já existe um cliente com os mesmos dados de documento (CPF/CNPJ/RG).", "Duplicidade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            txtComplemento.Clear();

            checkAtivo.Checked = true;
            checkAtivo.Enabled = false;

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = " ";

            comboPessoa.SelectedIndex = -1;
            comboClassificacao.SelectedIndex = -1;

            isEditando = false;
            isExcluindo = false;
        }

        public void CarregarDados(Cliente clienteSelecionado, bool isEditandoForm, bool isExcluindoForm)
        {

            cliente = clienteSelecionado;
            isEditando = isEditandoForm;
            isExcluindo = isExcluindoForm;

            txtCodigo.Text = cliente.Id.ToString();
            txtNome_RazaoSocial.Text = cliente.NomeRazaoSocial;
            txtApelido_NomeFantasia.Text = cliente.ApelidoNomeFantasia;
            txtCpf_Cnpj.Text = cliente.CpfCnpj;
            txtRg_InscricaoEstadual.Text = cliente.RgInscricaoEstadual;
            txtEmail.Text = cliente.Email;
            txtTelefone.Text = cliente.Telefone;
            txtRua.Text = cliente.Rua;
            txtNumero.Text = cliente.Numero;
            txtBairro.Text = cliente.Bairro;
            txtCep.Text = cliente.Cep;
            txtComplemento.Text = cliente.Complemento;
            checkAtivo.Checked = clienteSelecionado.Ativo;
            checkAtivo.Enabled = isEditandoForm;

            if (cliente.TipoPessoa != null)
            {
                string tipo = cliente.TipoPessoa.ToUpper();

                if (tipo == "FÍSICA" || tipo == "FISICA")
                    comboPessoa.SelectedItem = "FÍSICA";
                else if (tipo == "JURÍDICA" || tipo == "JURIDICA")
                    comboPessoa.SelectedItem = "JURÍDICA";
                else
                    comboPessoa.SelectedItem = null;
            }
            else
            {
                comboPessoa.SelectedItem = null;
            }

            comboClassificacao.SelectedItem = cliente.Classificacao;

            if (cliente.DataNascimentoCriacao.HasValue)
            {
                dateTimePicker1.Value = cliente.DataNascimentoCriacao.Value;
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            }
            else
            {
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = " ";
            }

            txtCodigoCondicao.Text = cliente.CondicaoPagamentoId.ToString();
            var condicao = condicacaoServices.BuscarPorId(cliente.CondicaoPagamentoId);
            txtCondicao.Text = condicao?.Descricao ?? "Desconhecida";

            var cidade = cidadeServices.BuscarPorId(cliente.IdCidade);
            if (cidade != null)
            {
                txtCodigoCidade.Text = cidade.Id.ToString();
                txtCidade.Text = cidade.Nome;

                var estadoServices = new EstadoServicos();
                var estado = estadoServices.BuscarPorId(cidade.IdEstado);
                txtUF.Text = estado?.UF?? "";
            }
            else
            {
                txtCodigoCidade.Clear();
                txtCidade.Text = "Desconhecida";
                txtUF.Text = "";
            }

            comboPessoa.Enabled = !isEditandoForm;

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
            txtComplemento.Enabled = false;
            txtCodigoCidade.Enabled = false;
            txtCodigoCondicao.Enabled = false;

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
        }

        private void FrmCadastroCliente_Load_1(object sender, EventArgs e)
        {
            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";
            dateTimePicker1.MaxDate = DateTime.Today;
            labelCriacao.Text = cliente.DataCriacao > DateTime.MinValue ? cliente.DataCriacao.ToShortDateString() : "";
            lblAtualizacao.Text = cliente.DataAtualizacao > DateTime.MinValue ? cliente.DataAtualizacao.ToShortDateString() : "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FrmConsultaCondPagamento frmConsultaCondPagamento = new FrmConsultaCondPagamento();
            frmConsultaCondPagamento.Owner = this;
            frmConsultaCondPagamento.ShowDialog();
        }
        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (isExcluindo)
                {
                    if (cliente != null && cliente.Id != 0)
                    {
                        if (MessageBox.Show("Deseja realmente remover este cliente?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            clienteServicos.Remover(cliente.Id);
                            MessageBox.Show("Cliente removido com sucesso!");
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
                    clienteServicos.Atualizar(cliente);
                    MessageBox.Show("Cliente atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    clienteServicos.Criar(cliente);
                    MessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimparFormulario();
                Sair();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE"))
                {
                    MessageBox.Show("Não é possível remover o cliente, pois ele está vinculado a outro(s) registro(s).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Erro ao salvar ou atualizar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void AtualizarObjeto()
        {
            cliente.TipoPessoa = comboPessoa.Text;
            cliente.NomeRazaoSocial = txtNome_RazaoSocial.Text.Trim();
            cliente.ApelidoNomeFantasia = txtApelido_NomeFantasia.Text.Trim();
            cliente.RgInscricaoEstadual = txtRg_InscricaoEstadual.Text.Trim();
            cliente.Email = txtEmail.Text.Trim();
            cliente.Telefone = txtTelefone.Text.Trim();
            cliente.Rua = txtRua.Text.Trim();
            cliente.Numero = txtNumero.Text.Trim();
            cliente.Bairro = txtBairro.Text.Trim();
            cliente.Cep = txtCep.Text.Trim();
            cliente.Complemento = txtComplemento.Text.Trim();
            cliente.CpfCnpj = txtCpf_Cnpj.Text.Trim();
            cliente.Classificacao = comboClassificacao.SelectedItem?.ToString();
            cliente.Ativo = checkAtivo.Checked;
            cliente.DataNascimentoCriacao = dateTimePicker1.CustomFormat == " " ? (DateTime?)null : dateTimePicker1.Value;


            if (!int.TryParse(txtCodigoCondicao.Text, out int idCondicao))
                throw new Exception("Código da condição de pagamento inválido.");
            cliente.CondicaoPagamentoId = idCondicao;

            var cidadeSelecionada = cidadeServices.BuscarTodos()
                .FirstOrDefault(c => c.Nome.Equals(txtCidade.Text.Trim(), StringComparison.OrdinalIgnoreCase));

            if (cidadeSelecionada == null)
                throw new Exception("Cidade selecionada não foi encontrada.");

            cliente.IdCidade = cidadeSelecionada.Id;

            cliente.Id = (isEditando || isExcluindo) ? int.Parse(txtCodigo.Text) : 0;

            cliente.DataCriacao = cliente.DataCriacao == DateTime.MinValue ? DateTime.Now : cliente.DataCriacao;
            cliente.DataAtualizacao = DateTime.Now;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmConsultaCidade frmConsultaCidade = new FrmConsultaCidade();
            frmConsultaCidade.Owner = this;
            frmConsultaCidade.ShowDialog();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
        }

        private void comboPessoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboPessoa.SelectedItem?.ToString() == "FÍSICA")
            {
                label4.Text = "Cliente:";
            }

            if (comboPessoa.SelectedItem?.ToString() == "JURÍDICA")
            {
                label4.Text = "Razão Social Cliente:";
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
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                e.KeyChar != '-' &&
                e.KeyChar != '/')
            {
                e.Handled = true;
            }
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
