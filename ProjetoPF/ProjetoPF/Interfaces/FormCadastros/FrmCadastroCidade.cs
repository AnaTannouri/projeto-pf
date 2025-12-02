using ProjetoPF.Dao;
using ProjetoPF.Interfaces.FormConsultas;
using ProjetoPF.Modelos.Localizacao;
using ProjetoPF.Servicos;
using ProjetoPF.Servicos.Localizacao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroCidade : ProjetoPF.FormCadastros.FrmCadastroPai
    {

        private Cidade cidade = new Cidade();
        private CidadeServicos cidadeServices = new CidadeServicos();
        private BaseServicos<Estado> estadoServices = new BaseServicos<Estado>(new BaseDao<Estado>("Estados"));

        private bool isEditando = false;
        private bool isExcluindo = false;

        private bool carregandoCombo = false;

        public FrmCadastroCidade()
        {
            InitializeComponent();
        }
        private bool ValidarEntrada()
        {
            string Nomecidade = txtCidade.Text.Trim();

            if (string.IsNullOrWhiteSpace(txtCidade.Text))
            {
                MessageBox.Show("Informe o nome da cidade.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDDD.Text))
            {
                MessageBox.Show("Informe o DDD.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCodEstado.Text))
            {
                MessageBox.Show("Selecione um estado.");
                return false;
            }
            if (cidadeServices.VerificarDuplicidade("Nome", Nomecidade, cidade))
            {
                MessageBox.Show("Esta cidade já está cadastrado.");
                return false;
            }

            return true;
        }
        private void AtualizarObjeto()
        {

            cidade.Nome = txtCidade.Text.Trim();
            cidade.DDD = txtDDD.Text.Trim();

            if (!int.TryParse(txtCodEstado.Text, out int idEstado))
                throw new Exception("Código do estado inválido.");

            Estado estadoSelecionado = estadoServices.BuscarPorId(idEstado);

            if (estadoSelecionado == null)
                throw new Exception("Estado selecionado não foi encontrado.");

            if (!estadoSelecionado.Ativo)
                throw new Exception("O estado selecionado está inativo. Selecione um estado ativo.");

            cidade.IdEstado = idEstado;

            if (isEditando || isExcluindo)
                cidade.Id = int.Parse(txtCodigo.Text);
            else
                cidade.Id = 0;

            cidade.Id = isEditando || isExcluindo ? int.Parse(txtCodigo.Text) : 0;
            cidade.DataCriacao = cidade.DataCriacao == DateTime.MinValue ? DateTime.Now : cidade.DataCriacao;
            cidade.DataAtualizacao = DateTime.Now;
            cidade.Ativo = checkAtivo.Checked;
        }
        public void CarregarDados(Cidade cidadeSelecionada, bool isEditandoForm, bool isExcluindoForm)
        {
            cidade = cidadeSelecionada;
            isEditando = isEditandoForm;
            isExcluindo = isExcluindoForm;

            txtCodigo.Text = cidade.Id.ToString();
            txtCidade.Text = cidade.Nome;
            txtDDD.Text = cidade.DDD;

            if (cidadeSelecionada.Id > 0)
            {
                Estado estado = estadoServices.BuscarPorId(cidadeSelecionada.IdEstado);
                if (estado != null)
                {
                    txtCodEstado.Text = estado.Id.ToString();
                    txtEstado.Text = estado.Nome;
                }
                else
                {
                    txtCodEstado.Text = "";
                    txtEstado.Text = "Desconhecido";
                }
            }
            else
            {
                txtCodEstado.Text = "";
                txtEstado.Text = "";
            }

            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";

            if (isExcluindo)
                BloquearCampos();
            else
                DesbloquearCampos();

            checkAtivo.Checked = cidadeSelecionada.Ativo;
            checkAtivo.Enabled = isEditandoForm;
        }
        public void LimparCampos()
        {
            txtCodigo.Clear();
            txtCidade.Clear();
            txtDDD.Clear();
            txtCodEstado.Clear();

            cidade = new Cidade();
            isEditando = false;
            isExcluindo = false;

            checkAtivo.Checked = true;
            checkAtivo.Enabled = false;
        }

        public void BloquearCampos()
        {
            txtCidade.Enabled = false;
            txtDDD.Enabled = false;
            txtEstado.Enabled = false;
            btnSalvar.Enabled = true;
            btnCadastrar.Enabled = false;
        }
        public void DesbloquearCampos()
        {
            txtCidade.Enabled = true;
            txtDDD.Enabled = true;
            btnSalvar.Enabled = true;
            txtCodigo.Enabled = false;
            txtEstado.Enabled = false;
        }

        private void btnCadastrar_Click_1(object sender, EventArgs e)
        {
            FrmConsultaEstado frmConsultaEstado = new FrmConsultaEstado();
            frmConsultaEstado.Owner = this;
            frmConsultaEstado.ShowDialog();
        }
  
        private void FrmCadastroCidade_Load_1(object sender, EventArgs e)
        {
            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";
            labelCriacao.Text = cidade.DataCriacao > DateTime.MinValue ? cidade.DataCriacao.ToShortDateString() : "";
            lblAtualizacao.Text = cidade.DataAtualizacao > DateTime.MinValue ? cidade.DataAtualizacao.ToShortDateString() : "";
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {

            try
            {
                if (isExcluindo)
                {
                    if (cidade != null && cidade.Id != 0)
                    {
                        if (MessageBox.Show("Deseja realmente remover esta cidade?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            cidadeServices.Remover(cidade.Id);
                            MessageBox.Show("Cidade removida com sucesso!");
                            LimparCampos();
                            Sair();
                        }
                    }
                    return;
                }

                if (!ValidarEntrada()) return;

                AtualizarObjeto();

                if (isEditando)
                {
                    cidadeServices.Atualizar(cidade);
                    MessageBox.Show("Cidade atualizada com sucesso!");
                }
                else
                {
                    cidadeServices.Criar(cidade);
                    MessageBox.Show("Cidade cadastrada com sucesso!");
                }

                LimparCampos();
                Sair();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                string mensagem;

                if (ex.Message.Contains("FK_CidadeCliente"))
                    mensagem = "Não é possível remover a cidade: está associada a um ou mais clientes.";
                else if (ex.Message.Contains("FK_CidadeFornecedor"))
                    mensagem = "Não é possível remover a cidade: está associada a um ou mais fornecedores.";
                else
                    mensagem = "Não é possível remover a cidade, pois ela está em uso.";

                MessageBox.Show(mensagem, "Erro de integridade referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void txtDDD_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; 
            }
        }
    }
}
