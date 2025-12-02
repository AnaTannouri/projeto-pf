using ProjetoPF.Dao;
using ProjetoPF.FormCadastros;
using ProjetoPF.FormConsultas;
using ProjetoPF.Interfaces.FormConsultas;
using ProjetoPF.Modelos.Localizacao;
using ProjetoPF.Modelos.Pessoa;
using ProjetoPF.Servicos;
using ProjetoPF.Servicos.Localizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroEstado : FrmCadastroPai
    {
        private Estado estado = new Estado();
        private EstadoServicos estadoServices = new EstadoServicos();
        private BaseServicos<Pais> paisServices = new BaseServicos<Pais>(new BaseDao<Pais>("Paises"));

        private bool isEditando = false;
        private bool isExcluindo = false;

        private bool carregandoCombo = false;
        public Estado EstadoAtual => estado;

        public FrmCadastroEstado()
        {
            InitializeComponent();
        }

        private bool ValidarEntrada()
        {
            if (string.IsNullOrWhiteSpace(txtEstado.Text))
            {
                MessageBox.Show("Informe o nome do estado.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUf.Text))
            {
                MessageBox.Show("Informe a UF do estado.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCodPais.Text))
            {
                MessageBox.Show("Selecione um país.");
                return false;
            }

            return true;
        }

        private void AtualizarObjeto()
        {
            estado.Nome = txtEstado.Text.Trim();
            estado.UF = txtUf.Text.Trim();

            estado.Ativo = checkAtivo.Checked;

            if (!int.TryParse(txtCodPais.Text, out int idPais))
                throw new Exception("Código do país inválido.");

            Pais paisSelecionado = paisServices.BuscarPorId(idPais);

            if (paisSelecionado == null)
                throw new Exception("País selecionado não foi encontrado.");

            estado.IdPais = idPais; 

            if (isEditando || isExcluindo)
                estado.Id = int.Parse(txtCodigo.Text);
            else
                estado.Id = 0;

            estado.DataCriacao = estado.DataCriacao == DateTime.MinValue ? DateTime.Now : estado.DataCriacao;
            estado.DataAtualizacao = DateTime.Now;
        }

        public void CarregarDados(Estado estadoSelecionado, bool isEditandoForm, bool isExcluindoForm)
        {
            estado = estadoSelecionado;
            isEditando = isEditandoForm;
            isExcluindo = isExcluindoForm;

            txtCodigo.Text = estadoSelecionado.Id.ToString();
            txtEstado.Text = estadoSelecionado.Nome;
            txtUf.Text = estadoSelecionado.UF;

            checkAtivo.Checked = estadoSelecionado.Ativo;
            checkAtivo.Enabled = isEditandoForm;

            if (estadoSelecionado.IdPais > 0)
            {
                Pais pais = paisServices.BuscarPorId(estadoSelecionado.IdPais);
                if (pais != null)
                {
                    txtCodPais.Text = pais.Id.ToString();
                    txtPais.Text = pais.Nome;
                }
                else
                {
                    txtCodPais.Text = "";
                    txtPais.Text = "Desconhecido";
                }
            }
            else
            {
                txtCodPais.Text = "";
                txtPais.Text = "";
            }

            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";

            if (isExcluindo)
                BloquearCampos();
            else
                DesbloquearCampos();

        }

         public void LimparCampos()
        {
            txtCodigo.Clear();
            txtEstado.Clear();
            txtUf.Clear();

            txtCodPais.Clear();

            estado = new Estado();
            isEditando = false;
            isExcluindo = false;

            checkAtivo.Checked = true;
            checkAtivo.Enabled = false;
        }

        public void BloquearCampos()
        {
            txtEstado.Enabled = false;
            txtUf.Enabled = false;
            txtPais.Enabled = false;
            btnSalvar.Enabled = true;
            btnPaís.Enabled = false;
        }

        public void DesbloquearCampos()
        {
            txtEstado.Enabled = true;
            txtUf.Enabled = true;
            btnSalvar.Enabled = true;
            txtCodigo.Enabled = false;
            checkAtivo.Enabled = isEditando;
        }
        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (isExcluindo)
                {
                    if (estado != null && estado.Id != 0)
                    {
                        if (MessageBox.Show("Deseja realmente remover este estado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            estadoServices.Remover(estado.Id);
                            MessageBox.Show("Estado removido com sucesso!");
                            LimparCampos();
                            Sair();
                        }
                    }
                    return;
                }

                if (!ValidarEntrada()) return;

                Pais pais = paisServices.BuscarPorId(estado.IdPais);
                if (pais != null && !pais.Ativo)
                {
                    MessageBox.Show("O país selecionado está inativo. Selecione um país ativo para continuar.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AtualizarObjeto();

                if (isEditando)
                {
                    estadoServices.AtualizarEstado(estado);
                    MessageBox.Show("Estado atualizado com sucesso!");
                }
                else
                {
                    estadoServices.CadastrarEstado(estado);
                    MessageBox.Show("Estado salvo com sucesso!");
                }

                LimparCampos();
                Sair();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE") && ex.InnerException.Message.Contains("Cidades"))
                {
                    MessageBox.Show("Não é possível remover o estado, pois ele está vinculado a uma ou mais cidades.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex.Message.Contains("REFERENCE") && ex.Message.Contains("Cidades"))
                {
                    MessageBox.Show("Não é possível remover o estado, pois ele está vinculado a uma ou mais cidades.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Erro ao salvar ou atualizar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            FrmConsultaPais frmConsultaPais = new FrmConsultaPais();
            frmConsultaPais.Owner = this;
            frmConsultaPais.ShowDialog();
        }

        private void FrmCadastroEstado_Load(object sender, EventArgs e)
        {
            labelCriacao.Text = estado.DataCriacao > DateTime.MinValue ? estado.DataCriacao.ToShortDateString() : "";
            lblAtualizacao.Text = estado.DataAtualizacao > DateTime.MinValue ? estado.DataAtualizacao.ToShortDateString() : "";
        }
    }
}
