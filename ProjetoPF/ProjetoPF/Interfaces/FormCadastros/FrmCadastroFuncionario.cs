using ProjetoPF.Dao;
using ProjetoPF.Modelos.Localizacao;
using ProjetoPF.Modelos.Pessoa;
using ProjetoPF.Servicos;
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
    public partial class FrmCadastroFuncionario : ProjetoPF.Interfaces.FormCadastros.FrmCadastroPessoa
    {
        private Funcionario funcionario = new Funcionario();
        private BaseServicos<Funcionario> funcionarioServicos = new BaseServicos<Funcionario>(new BaseDao<Funcionario>("Funcionarios"));

        private BaseServicos<Cidade> cidadeServices = new BaseServicos<Cidade>(new BaseDao<Cidade>("Cidades"));

        private bool carregandoCidades = false;

        private bool isEditando = false;
        private bool isExcluindo = false;
        public FrmCadastroFuncionario()
        {
            InitializeComponent();
            label4.Text = "Nome";
            label5.Text = "Apelido";
            label16.Text = "Data de Nascimento";
            label3.Text = "CPF";
            label6.Text = "RG";
            lblClassificacao.Text = "Gênero";
            comboPessoa.Enabled = false;
            comboPessoa.SelectedItem = "F";
        }
        private bool ValidarEntrada()
        {
            if (string.IsNullOrWhiteSpace(txtMatricula.Text))
            {
                MessageBox.Show("Informe a matrícula do funcionário.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNome_RazaoSocial.Text))
            {
                MessageBox.Show("Informe o nome do funcionário.");
                return false;
            }
            if (!checkEstrangeiro.Checked && string.IsNullOrWhiteSpace(txtCpf_Cnpj.Text))
            {
                MessageBox.Show("Informe o CPF para funcionários não estrangeiros.");
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
            if (string.IsNullOrWhiteSpace(txtRua.Text))
            {
                MessageBox.Show("Informe a rua do funcionário.");
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
            if (comboCidade.SelectedItem == null)
            {
                MessageBox.Show("Selecione a cidade do funcionário.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCep.Text))
            {
                MessageBox.Show("Informe o CEP do funcionário.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtCargo.Text))
            {
                MessageBox.Show("Informe o cargo do funcionário.");
                return false;
            }
            if (!decimal.TryParse(txtSalario.Text, out _))
            {
                MessageBox.Show("Informe um salário válido.");
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
            return true;
        }
        public void CarregarCidades(int? idSelecionado = null)
        {
            try
            {
                carregandoCidades = true;

                comboCidade.DataSource = null;
                comboCidade.Items.Clear();

                var listaCidades = cidadeServices.BuscarTodos();

                if (listaCidades != null && listaCidades.Any())
                {
                    comboCidade.DataSource = listaCidades;
                    comboCidade.DisplayMember = "Nome";
                    comboCidade.ValueMember = "Id";

                    if (idSelecionado.HasValue)
                    {
                        comboCidade.SelectedValue = idSelecionado.Value;
                        txtCodigoCidade.Text = idSelecionado.Value.ToString();
                    }
                    else
                    {
                        comboCidade.SelectedIndex = -1;
                        txtCodigoCidade.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar cidades: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                carregandoCidades = false;
            }
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
            txtCodigoCidade.Clear();
            txtCargo.Clear();
            txtSalario.Clear();
            txtCargaHoraria.Clear();

            checkEstrangeiro.Checked = false;
            dateTimePicker1.Value = DateTime.Now;
            Dataadm.Value = DateTime.Now;
            DataDem.Value = DateTime.Now;
            DataDem.Checked = false;

            comboPessoa.SelectedIndex = -1;
            comboClassificacao.SelectedIndex = -1;
            comboCidade.SelectedIndex = -1;
            comboTurno.SelectedIndex = -1;

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
            txtSalario.Text = funcionario.Salario.ToString("F2");
            txtCargaHoraria.Text = funcionario.CargaHoraria;

            checkEstrangeiro.Checked = funcionario.Estrangeiro;
            dateTimePicker1.Value = funcionario.DataNascimentoCriacao;
            Dataadm.Value = funcionario.DataAdmissao;
            DataDem.Value = funcionario.DataDemissao;

            comboPessoa.SelectedItem = "F";
            comboPessoa.Enabled = false;

            comboClassificacao.SelectedIndex = comboClassificacao.FindStringExact(funcionario.Classificacao?.Trim());
            comboTurno.SelectedIndex = comboTurno.FindStringExact(funcionario.Turno?.Trim());

            CarregarCidades(funcionario.IdCidade);
            comboCidade.SelectedValue = funcionario.IdCidade;
            txtCodigoCidade.Text = funcionario.IdCidade.ToString();

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
            txtCodigoCidade.Enabled = false;
            txtCargo.Enabled = false;
            txtSalario.Enabled = false;
            txtCargaHoraria.Enabled = false;

            comboPessoa.Enabled = false;
            comboClassificacao.Enabled = false;
            comboCidade.Enabled = false;
            comboTurno.Enabled = false;

            dateTimePicker1.Enabled = false;
            Dataadm.Enabled = false;
            DataDem.Enabled = false;
            checkEstrangeiro.Enabled = false;

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
            txtCodigoCidade.Enabled = false;
            txtCargo.Enabled = true;
            txtSalario.Enabled = true;
            txtCargaHoraria.Enabled = true;

            comboClassificacao.Enabled = true;
            comboCidade.Enabled = true;
            comboTurno.Enabled = true;

            dateTimePicker1.Enabled = true;
            Dataadm.Enabled = true;
            DataDem.Enabled = true;
            checkEstrangeiro.Enabled = true;

            btnCadastrarCidade.Enabled = true;
        }
        private void AtualizarObjeto()
        {
            funcionario.Matricula = txtMatricula.Text.Trim();
            funcionario.NomeRazaoSocial = txtNome_RazaoSocial.Text.Trim();
            funcionario.ApelidoNomeFantasia = txtApelido_NomeFantasia.Text.Trim();
            funcionario.CpfCnpj = checkEstrangeiro.Checked ? null : txtCpf_Cnpj.Text.Trim();
            funcionario.RgInscricaoEstadual = txtRg_InscricaoEstadual.Text.Trim();
            funcionario.Email = txtEmail.Text.Trim();
            funcionario.Telefone = txtTelefone.Text.Trim();
            funcionario.Rua = txtRua.Text.Trim();
            funcionario.Numero = txtNumero.Text.Trim();
            funcionario.Bairro = txtBairro.Text.Trim();
            funcionario.Cep = txtCep.Text.Trim();
            funcionario.Estrangeiro = checkEstrangeiro.Checked;
            funcionario.Classificacao = comboClassificacao.Text.Trim();
            funcionario.DataNascimentoCriacao = dateTimePicker1.Value;

            funcionario.Cargo = txtCargo.Text.Trim();
            funcionario.Salario = decimal.Parse(txtSalario.Text);
            funcionario.Turno = comboTurno.Text.Trim(); 
            funcionario.CargaHoraria = txtCargaHoraria.Text.Trim();
            funcionario.DataAdmissao = Dataadm.Value;
            funcionario.DataDemissao = DataDem.Value;

            if (comboCidade.SelectedItem is Cidade cidadeSelecionada)
            {
                funcionario.IdCidade = cidadeSelecionada.Id;
            }
            else
            {
                throw new Exception("Nenhuma cidade foi selecionada.");
            }

            funcionario.Id = (isEditando || isExcluindo) ? int.Parse(txtCodigo.Text) : 0;
            funcionario.DataCriacao = funcionario.DataCriacao == DateTime.MinValue ? DateTime.Now : funcionario.DataCriacao;
            funcionario.DataAtualizacao = DateTime.Now;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (isExcluindo)
                {
                    if (funcionario != null && funcionario.Id != 0)
                    {
                        if (MessageBox.Show("Deseja realmente excluir este funcionário?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
            if (comboCidade.Items.Count == 0)
                CarregarCidades();

            CarregarCombosFixos();

            comboClassificacao.Items.Clear();
            comboClassificacao.Items.AddRange(new string[] { "Masculino", "Feminino", "Outro" });

            comboTurno.Items.Clear();
            comboTurno.Items.AddRange(new string[] { "Manhã", "Tarde", "Noite", "Integral", "Plantão" });

            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";
        }

        private void btnCadastrarCidade_Click(object sender, EventArgs e)
        {
            FrmCadastroCidade frmCadastroCidade = new FrmCadastroCidade();
            frmCadastroCidade.FormClosed += (s, args) =>
            {
                CarregarCidades(null);

                var listaAtualizada = (List<Cidade>)comboCidade.DataSource;

                if (listaAtualizada != null && listaAtualizada.Any())
                {
                    var ultimaCidade = listaAtualizada.OrderByDescending(p => p.Id).FirstOrDefault();

                    if (ultimaCidade != null)
                    {
                        comboCidade.SelectedValue = ultimaCidade.Id;
                        txtCodigoCidade.Text = ultimaCidade.Id.ToString();
                    }
                }
            };

            frmCadastroCidade.ShowDialog();
        }

        private void comboCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (carregandoCidades) return;

            if (comboCidade.SelectedItem is Cidade cidade)
            {
                txtCodigoCidade.Text = cidade.Id.ToString();
            }
            else
            {
                txtCodigoCidade.Clear();
            }
        }
        private void CarregarCombosFixos()
        {
            comboClassificacao.Items.Clear();
            comboClassificacao.Items.AddRange(new string[] { "Masculino", "Feminino", "Outro" });

            comboTurno.Items.Clear();
            comboTurno.Items.AddRange(new string[] { "Manhã", "Tarde", "Noite", "Integral", "Plantão" });
        }
    }
}
