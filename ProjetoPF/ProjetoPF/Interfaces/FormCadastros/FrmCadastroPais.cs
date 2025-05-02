using ProjetoPF.Dao;
using ProjetoPF.Modelos.Localizacao;
using ProjetoPF.Modelos.Pessoa;
using ProjetoPF.Servicos;
using ProjetoPF.Servicos.Localizacao;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroPais : ProjetoPF.FormCadastros.FrmCadastroPai
    {
        private Pais pais = new Pais();
        private PaisServicos PaisServices = new PaisServicos();
        private bool isEditando = false;
        private bool isExcluindo = false;
        public Pais PaisAtual => pais;
        public FrmCadastroPais()
        {
            InitializeComponent();
        }
        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                AtualizarObjeto(); 

                if (isExcluindo)
                {
                    if (MessageBox.Show("Deseja realmente remover este país?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        PaisServices.Remover(pais.Id);
                        MessageBox.Show("País removido com sucesso!");
                        LimparCampos();
                        Sair();
                    }
                    return;
                }

                if (!ValidarEntrada()) return;

                if (isEditando)
                {
                    PaisServices.AtualizarPais(pais);
                    MessageBox.Show("País atualizado com sucesso!");
                }
                else
                {
                    PaisServices.CadastrarPais(pais);
                    MessageBox.Show("País cadastrado com sucesso!");
                }

                LimparCampos();
                this.Close();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE") && ex.InnerException.Message.Contains("Estados"))
                {
                    MessageBox.Show("Não é possível remover o país, pois ele está vinculado a um ou mais estados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex.Message.Contains("REFERENCE") && ex.Message.Contains("Estados"))
                {
                    MessageBox.Show("Não é possível remover o país, pois ele está vinculado a um ou mais estados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Erro ao salvar ou atualizar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private bool ValidarEntrada()
        {
            string nomepais = txtPais.Text.Trim();
            string sigla = txtSigla.Text.Trim();
            string ddi = txtDdi.Text.Trim();

            if (string.IsNullOrEmpty(nomepais))
            {
                MessageBox.Show("Informe um país.");
                return false;
            }

            if (string.IsNullOrEmpty(sigla))
            {
                MessageBox.Show("Informe a sigla do país.");
                return false;
            }

            if (string.IsNullOrEmpty(ddi))
            {
                MessageBox.Show("Informe o DDI do país.");
                return false;
            }

            if (PaisServices.VerificarDuplicidade("Nome", nomepais, pais))
            {
                MessageBox.Show("Este país já está cadastrado.");
                return false;
            }

            return true;
        }

        private void AtualizarObjeto()
        {
            pais.Nome = txtPais.Text.Trim();
            pais.Sigla = txtSigla.Text.Trim();
            pais.DDI = txtDdi.Text.Trim();
            pais.Ativo = checkAtivo.Checked;

            if (isEditando || isExcluindo)
                pais.Id = int.Parse(txtCodigo.Text);
            else
                pais.Id = 0;

            pais.DataCriacao = pais.DataCriacao == DateTime.MinValue ? DateTime.Now : pais.DataCriacao;
            pais.DataAtualizacao = DateTime.Now;
        }
        public void CarregarDados(Pais paisSelecionado, bool isEditandoForm, bool isExcluindoForm)
        {
            txtCodigo.Text = paisSelecionado.Id.ToString();
            txtPais.Text = paisSelecionado.Nome;
            txtSigla.Text = paisSelecionado.Sigla;
            txtDdi.Text = paisSelecionado.DDI;
            checkAtivo.Checked = paisSelecionado.Ativo;
            checkAtivo.Enabled = isEditandoForm;

            pais = paisSelecionado;
            isEditando = isEditandoForm;
            isExcluindo = isExcluindoForm;

            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";
        }
        public void LimparCampos()
        {
            txtCodigo.Clear();
            txtPais.Clear();
            txtSigla.Clear();
            txtDdi.Clear();
            checkAtivo.Checked = true;
            checkAtivo.Enabled = false;
            pais = new Pais();
            isEditando = false;
            isExcluindo = false;
        }
        public void BloquearCampos()
        {
            txtCodigo.Enabled = false;
            txtPais.Enabled = false;
            txtSigla.Enabled = false;
            txtDdi.Enabled = false;
            btnSalvar.Enabled = true;
        }

        public void DesbloquearCampos()
        {
            txtPais.Enabled = true;
            txtSigla.Enabled = true;
            txtDdi.Enabled = true;
            btnSalvar.Enabled = true;
            txtCodigo.Enabled = false;
        }

        private void FrmCadastroPais_Load(object sender, EventArgs e)
        {
            labelCriacao.Text = pais.DataCriacao.ToShortDateString();
            lblAtualizacao.Text = pais.DataAtualizacao.ToShortDateString();
        }
    }
}
