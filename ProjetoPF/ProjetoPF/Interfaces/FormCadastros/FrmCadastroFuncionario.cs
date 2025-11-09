using ProjetoPF.Dao;
using ProjetoPF.Interfaces.FormConsultas;
using ProjetoPF.Modelos.Localizacao;
using ProjetoPF.Modelos.Pessoa;
using ProjetoPF.Servicos;
using ProjetoPF.Servicos.Localizacao;
using ProjetoPF.Servicos.Pessoa;
using System;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroFuncionario : ProjetoPF.Interfaces.FormCadastros.FrmCadastroPessoa
    {
        private Funcionario funcionario = new Funcionario();
        private BaseServicos<Funcionario> funcionarioServicos = new BaseServicos<Funcionario>(new BaseDao<Funcionario>("Funcionarios"));

        private BaseServicos<Cidade> cidadeServices = new BaseServicos<Cidade>(new BaseDao<Cidade>("Cidades"));
        private BaseServicos<Pais> paisServices = new BaseServicos<Pais>(new BaseDao<Pais>("Paises"));

        private bool carregandoCidades = false;

        private bool isEditando = false;
        private bool isExcluindo = false;

        public FrmCadastroFuncionario()
        {
            InitializeComponent();

            label4.Text = "Funcionário:";
            label5.Text = "Apelido:";
            label16.Text = "Data de Nascimento:";
            label3.Text = "CPF:";
            label6.Text = "RG:";
            lblClassificacao.Text = "Gênero:";

            comboPessoa.Items.Clear();
            comboPessoa.Items.Add("FÍSICA");
            comboPessoa.SelectedItem = "FÍSICA";
            comboPessoa.Enabled = false;
            comboPessoa.Visible = false;
            label2.Visible = false;

            txtCidadeFunc.Enabled = false;
            DataDem.Enabled = false;

            DataDem.Format = DateTimePickerFormat.Custom;
            DataDem.CustomFormat = " ";

            txtCpf_Cnpj.KeyPress += SomenteNumerosPontuacao_KeyPress;
            txtCep.KeyPress += SomenteNumerosPontuacao_KeyPress;
            txtTelefone.KeyPress += Telefone_KeyPress;
            txtNumero.KeyPress += SomenteNumeros_KeyPress;
            txtSalario.KeyPress += SomenteNumerosPontuacao_KeyPress;
            txtCargaHoraria.KeyPress += SomenteNumerosPontuacao_KeyPress;
        }
        private bool ValidarEntrada()
        {
            if (string.IsNullOrWhiteSpace(txtNome_RazaoSocial.Text))
            {
                MessageBox.Show("Informe o nome do funcionário.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtRua.Text))
            {
                MessageBox.Show("Informe o endereço do funcionário.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show("Informe o número da residência do funcionário.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtBairro.Text))
            {
                MessageBox.Show("Informe o bairro do funcionário.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCep.Text))
            {
                MessageBox.Show("Informe o cep do funcionário.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCidadeFunc.Text))
            {
                MessageBox.Show("Selecione a cidade do funcionário.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Informe o e-mail do funcionário.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                MessageBox.Show("Informe o telefone do funcionário.");
                return false;
            }
            else
            {
                string nomeCidade = txtCidadeFunc.Text.Trim();
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

                            if (pais != null && tipoPessoa == "FÍSICA")
                            {
                                if (pais.Nome.Equals("Brasil", StringComparison.OrdinalIgnoreCase))
                                {
                                    string cpf = new string(txtCpf_Cnpj.Text.Where(char.IsDigit).ToArray());

                                    if (string.IsNullOrWhiteSpace(cpf))
                                    {
                                        MessageBox.Show("Informe o CPF do funcionário (obrigatório para pessoa física no Brasil).");
                                        return false;
                                    }
                                    else if (!ValidarCpf(cpf))
                                    {
                                        MessageBox.Show("CPF inválido.");
                                        return false;
                                    }
                                }
                                else
                                {
                                    if (string.IsNullOrWhiteSpace(txtRg_InscricaoEstadual.Text))
                                    {
                                        MessageBox.Show("Informe o RG do funcionário (obrigatório para pessoa física estrangeira).");
                                        return false;
                                    }
                                }
                            }
                        }
                    }
                }
            }
                if (string.IsNullOrWhiteSpace(txtCargo.Text))
            {
                MessageBox.Show("Informe o cargo do funcionário.");
                return false;
            }
            string salarioTexto = txtSalario.Text.Replace("R$", "").Trim()
                                      .Replace(".", "")
                                      .Replace(",", ".");

            if (!decimal.TryParse(salarioTexto, System.Globalization.NumberStyles.Any,
                                  System.Globalization.CultureInfo.InvariantCulture, out _))
            {
                MessageBox.Show("Informe um salário válido.");
                return false;
            }
            if (Dataadm.CustomFormat == " ")
            {
                MessageBox.Show("Selecione a data de admissão do funcionário.");
                return false;
            }

            if (Dataadm.Value.Date > DateTime.Today)
            {
                MessageBox.Show("A data de admissão não pode ser posterior à data de hoje.",
                                "Data inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (comboTurno.SelectedItem == null)
            {
                MessageBox.Show("Selecione o turno do funcionário.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCargaHoraria.Text))
            {
                MessageBox.Show("Informe a carga horária do funcionário.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtMatricula.Text))
            {
                MessageBox.Show("Informe a matrícula do funcionário.");
                return false;
            }
            FuncionarioServicos servicoFuncionario = new FuncionarioServicos();

            if (servicoFuncionario.DocumentoDuplicado(funcionario))
            {
                MessageBox.Show("Já existe um funcionário com o mesmo CPF ou RG.", "Duplicidade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }
        public void LimparFormulario()
        {
            txtCodigo.Clear();
            txtMatricula.Clear();
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
            txtCodigoCidadeFunc.Clear();
            txtCargo.Clear();
            txtSalario.Clear();
            txtCargaHoraria.Clear();
            txtCidadeFunc.Clear();

            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = " ";
            Dataadm.Format = DateTimePickerFormat.Custom;
            Dataadm.CustomFormat = " ";
            DataDem.Format = DateTimePickerFormat.Custom;
            DataDem.CustomFormat = " ";

            comboPessoa.SelectedIndex = -1;
            comboClassificacao.SelectedIndex = -1;
            comboTurno.SelectedIndex = -1;

            checkAtivo.Checked = true;
            checkAtivo.Enabled = false;

            isEditando = false;
            isExcluindo = false;
        }

        public void CarregarDados(Funcionario funcionarioSelecionado, bool isEditandoForm, bool isExcluindoForm)
        {
            CarregarCombosFixos();

            funcionario = funcionarioSelecionado;
            isEditando = isEditandoForm;
            isExcluindo = isExcluindoForm;

            txtCodigo.Text = funcionario.Id.ToString();
            txtMatricula.Text = funcionario.Matricula;
            txtNome_RazaoSocial.Text = funcionario.NomeRazaoSocial;
            txtApelido_NomeFantasia.Text = funcionario.ApelidoNomeFantasia;
            txtCpf_Cnpj.Text = funcionario.CpfCnpj;
            txtRg_InscricaoEstadual.Text = funcionario.RgInscricaoEstadual;
            txtEmail.Text = funcionario.Email;
            txtTelefone.Text = funcionario.Telefone;
            txtRua.Text = funcionario.Rua;
            txtNumero.Text = funcionario.Numero;
            txtBairro.Text = funcionario.Bairro;
            txtCep.Text = funcionario.Cep;
            txtCargo.Text = funcionario.Cargo;
            txtSalario.Text = string.Format(System.Globalization.CultureInfo.GetCultureInfo("pt-BR"), "R$ {0:N2}", funcionario.Salario);
            txtCargaHoraria.Text = funcionario.CargaHoraria;
            txtComplemento.Text = funcionario.Complemento;

            checkAtivo.Checked = funcionarioSelecionado.Ativo;
            checkAtivo.Enabled = isEditandoForm;

            if (funcionario.DataNascimentoCriacao.HasValue)
            {
                dateTimePicker1.Value = funcionario.DataNascimentoCriacao.Value;
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            }
            else
            {
                dateTimePicker1.Value = DateTime.Now;
                dateTimePicker1.Format = DateTimePickerFormat.Custom;
                dateTimePicker1.CustomFormat = " ";
            }

            if (funcionario.DataAdmissao.HasValue)
            {
                Dataadm.Value = funcionario.DataAdmissao.Value;
                Dataadm.Format = DateTimePickerFormat.Custom;
                Dataadm.CustomFormat = "dd/MM/yyyy";
            }
            else
            {
                Dataadm.Value = DateTime.Now;
                Dataadm.Format = DateTimePickerFormat.Custom;
                Dataadm.CustomFormat = " ";
            }

            if (funcionario.DataDemissao.HasValue)
            {
                DataDem.Value = funcionario.DataDemissao.Value;
                DataDem.Format = DateTimePickerFormat.Custom;
                DataDem.CustomFormat = "dd/MM/yyyy";
            }
            else
            {
                DataDem.Value = DateTime.Now;
                DataDem.Format = DateTimePickerFormat.Custom;
                DataDem.CustomFormat = " ";
            }

            comboPessoa.SelectedItem = "FÍSICA";
            comboPessoa.Enabled = false;

            comboClassificacao.SelectedIndex = -1;
            if (!string.IsNullOrWhiteSpace(funcionario.Classificacao))
            {
                string valorFuncionario = funcionario.Classificacao.Trim().ToUpperInvariant();

                for (int i = 0; i < comboClassificacao.Items.Count; i++)
                {
                    string valorCombo = comboClassificacao.Items[i]?.ToString().Trim().ToUpperInvariant();

                    if (valorFuncionario == valorCombo)
                    {
                        comboClassificacao.SelectedIndex = i;
                        break;
                    }
                }
            }

            comboTurno.SelectedIndex = -1;
            if (!string.IsNullOrWhiteSpace(funcionario.Turno))
            {
                foreach (var item in comboTurno.Items)
                {
                    if (string.Equals(item.ToString().Trim(), funcionario.Turno.Trim(), StringComparison.OrdinalIgnoreCase))
                    {
                        comboTurno.SelectedItem = item;
                        break;
                    }
                }
            }

            txtCodigoCidadeFunc.Text = funcionario.IdCidade.ToString();
            var cidade = cidadeServices.BuscarPorId(funcionario.IdCidade);
            if (cidade != null)
            {
                txtCidadeFunc.Text = cidade.Nome;

                var estadoServices = new EstadoServicos();
                var estado = estadoServices.BuscarPorId(cidade.IdEstado);
                txtUF.Text = estado?.UF ?? "";
            }
            else
            {
                txtCidadeFunc.Text = "";
                txtUF.Text = "";
            }

            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";

            if (isExcluindo)
                BloquearCampos();
            else
                DesbloquearCampos();
        }
            public void BloquearCampos()
        {
            txtCodigo.Enabled = false;
            txtMatricula.Enabled = false;
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
            txtCodigoCidadeFunc.Enabled = false;
            txtComplemento.Enabled = false;
            txtCargo.Enabled = false;
            txtSalario.Enabled = false;
            txtCargaHoraria.Enabled = false;
            txtCidadeFunc.Enabled = false;

            comboPessoa.Enabled = false;
            comboClassificacao.Enabled = false;
            comboTurno.Enabled = false;

            dateTimePicker1.Enabled = false;
            Dataadm.Enabled = false;
            DataDem.Enabled = false;

            btnCadastrarCidade.Enabled = false;
        }

        public void DesbloquearCampos()
        {
            txtCodigo.Enabled = false;
            txtMatricula.Enabled = true;
            txtNome_RazaoSocial.Enabled = true;
            txtApelido_NomeFantasia.Enabled = true;
            txtCpf_Cnpj.Enabled = true;
            txtRg_InscricaoEstadual.Enabled = true;
            txtEmail.Enabled = true;
            txtTelefone.Enabled = true;
            txtRua.Enabled = true;
            txtNumero.Enabled = true;
            txtBairro.Enabled = true;
            txtCep.Enabled = true;
            txtCodigoCidadeFunc.Enabled = false;
            txtCargo.Enabled = true;
            txtSalario.Enabled = true;
            txtCargaHoraria.Enabled = true;
            comboClassificacao.Enabled = true;
            comboTurno.Enabled = true;
            dateTimePicker1.Enabled = true;
            Dataadm.Enabled = true;
            DataDem.Enabled = isEditando; 

            btnCadastrarCidade.Enabled = true;
        }
        private void AtualizarObjeto()
        {
            funcionario.Matricula = txtMatricula.Text.Trim();
            funcionario.NomeRazaoSocial = txtNome_RazaoSocial.Text.Trim();
            funcionario.ApelidoNomeFantasia = txtApelido_NomeFantasia.Text.Trim();
            funcionario.RgInscricaoEstadual = txtRg_InscricaoEstadual.Text.Trim();
            funcionario.CpfCnpj = txtCpf_Cnpj.Text.Trim();
            funcionario.Email = txtEmail.Text.Trim();
            funcionario.Telefone = txtTelefone.Text.Trim();
            funcionario.Rua = txtRua.Text.Trim();
            funcionario.Numero = txtNumero.Text.Trim();
            funcionario.Bairro = txtBairro.Text.Trim();
            funcionario.Cep = txtCep.Text.Trim();
            funcionario.Complemento = txtComplemento.Text.Trim();
            funcionario.Classificacao = comboClassificacao.SelectedItem?.ToString().Trim() ?? "";

            funcionario.DataNascimentoCriacao = dateTimePicker1.CustomFormat == " " ? (DateTime?)null : dateTimePicker1.Value;
            funcionario.DataDemissao = DataDem.CustomFormat == " " ? (DateTime?)null : DataDem.Value;
            funcionario.DataAdmissao = Dataadm.Value;

            var cidadeSelecionada = cidadeServices.BuscarTodos()
    .FirstOrDefault(c => c.Nome.Equals(txtCidadeFunc.Text.Trim(), StringComparison.OrdinalIgnoreCase));

            if (cidadeSelecionada == null)
                throw new Exception("Cidade selecionada não foi encontrada.");

            if (!cidadeSelecionada.Ativo)
                throw new Exception("A cidade selecionada está inativa. Selecione uma cidade ativa.");

            funcionario.IdCidade = cidadeSelecionada.Id;
            funcionario.Cargo = txtCargo.Text.Trim();
            string textoSalario = txtSalario.Text.Replace("R$", "").Trim()
                                    .Replace(".", "")
                                    .Replace(",", ".");

            if (decimal.TryParse(textoSalario, System.Globalization.NumberStyles.Any,
                                 System.Globalization.CultureInfo.InvariantCulture, out var salario))
            {
                funcionario.Salario = salario;
            }
            else
            {
                funcionario.Salario = 0;
            }
            funcionario.Turno = comboTurno.SelectedItem?.ToString().Trim();
            funcionario.CargaHoraria = txtCargaHoraria.Text.Trim();
            funcionario.DataAdmissao = Dataadm.Value;

            funcionario.Id = (isEditando || isExcluindo) ? int.Parse(txtCodigo.Text) : 0;
            funcionario.DataCriacao = funcionario.DataCriacao == DateTime.MinValue ? DateTime.Now : funcionario.DataCriacao;
            funcionario.DataAtualizacao = DateTime.Now;
            funcionario.Ativo = checkAtivo.Checked;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (isExcluindo)
                {
                    if (funcionario != null && funcionario.Id != 0)
                    {
                        if (MessageBox.Show("Deseja realmente remover este funcionário?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            funcionarioServicos.Remover(funcionario.Id);
                            MessageBox.Show("Funcionário removido com sucesso!");
                            Close();
                        }
                    }
                    return;
                }

                if (!ValidarEntrada())
                    return;

                if (DataDem.Value.Date < Dataadm.Value.Date)
                {
                    MessageBox.Show("A data de demissão não pode ser anterior à data de admissão.", "Data inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AtualizarObjeto();

                if (isEditando)
                {
                    funcionarioServicos.Atualizar(funcionario);
                    MessageBox.Show("Funcionário atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    funcionarioServicos.Criar(funcionario);
                    MessageBox.Show("Funcionário cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar ou atualizar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmCadastroFuncionario_Load(object sender, EventArgs e)
        {
            label4.Text = "Funcionário";

            CarregarCombosFixos();

            if (funcionario != null && funcionario.Id > 0 && (isEditando || isExcluindo))
            {
                CarregarDados(funcionario, isEditando, isExcluindo);
            }

            dateTimePicker1.ValueChanged += dateTimePicker1_ValueChanged;
            dateTimePicker1.MaxDate = DateTime.Today;
            DataDem.ValueChanged += DataDem_ValueChanged;
            Dataadm.ValueChanged += Dataadm_ValueChanged;
            

            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";
            labelCriacao.Text = funcionario.DataCriacao > DateTime.MinValue ? funcionario.DataCriacao.ToShortDateString() : "";
            lblAtualizacao.Text = funcionario.DataAtualizacao > DateTime.MinValue ? funcionario.DataAtualizacao.ToShortDateString() : "";
            if (comboPessoa.Items.Count == 0)
            {
                comboPessoa.Items.Add("FÍSICA");
            }
            comboPessoa.SelectedItem = "FÍSICA";

            txtSalario.KeyPress += SomenteNumerosPontuacao_KeyPress;
            txtSalario.Leave += FormatarComoReal_Leave;
            txtSalario.Enter += RemoverSimboloReal_Enter;

            txtCpf_Cnpj.Leave += FormatarCpf_Leave;
            txtCep.Leave += FormatarCep_Leave;

        }
        private void FormatarCpf_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            string cpf = new string(txt.Text.Where(char.IsDigit).ToArray());

            if (cpf.Length == 11)
                txt.Text = Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
            else
                txt.Text = cpf; // mantém como digitado se incompleto
        }

        private void FormatarCep_Leave(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;
            string cep = new string(txt.Text.Where(char.IsDigit).ToArray());

            if (cep.Length == 8)
                txt.Text = Convert.ToUInt64(cep).ToString(@"00000\-000");
            else
                txt.Text = cep; // mantém se incompleto
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

        private void btnCadastrarCidade_Click(object sender, EventArgs e)
        {
            FrmConsultaCidade frmConsultaCidade = new FrmConsultaCidade();
            frmConsultaCidade.Owner = this;
            frmConsultaCidade.ShowDialog();
        }
        private void CarregarCombosFixos()
        {
            comboClassificacao.Items.Clear();
            comboClassificacao.Items.AddRange(new string[] { "MASCULINO", "FEMININO", "OUTROS" });

            comboTurno.Items.Clear();
            comboTurno.Items.AddRange(new string[] { "MANHÃ", "TARDE", "NOITE", "INTEGRAL", "PLANTÃO" });
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
        }

        private void DataDem_ValueChanged(object sender, EventArgs e)
        {
            DataDem.CustomFormat = "dd/MM/yyyy";
        }

        private void Dataadm_ValueChanged(object sender, EventArgs e)
        {
            Dataadm.CustomFormat = "dd/MM/yyyy";
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

        private bool ValidarEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
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

        private void txtCargaHoraria_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
