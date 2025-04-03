using ProjetoPF.Dao;
using ProjetoPF.FormCadastros;
using ProjetoPF.Modelos.Localizacao;
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

        public void CarregarPaises(int? idSelecionado = null)
        {
            try
            {
                carregandoCombo = true;

                var listaPaises = paisServices.BuscarTodos();

                comboPaises.Items.Clear();

                if (listaPaises != null && listaPaises.Any())
                {
                    comboPaises.DataSource = listaPaises;
                    comboPaises.DisplayMember = "Nome";
                    comboPaises.ValueMember = "Id";

                    if (idSelecionado.HasValue)
                    {
                        comboPaises.SelectedValue = idSelecionado.Value;
                        txtCodPais.Text = idSelecionado.Value.ToString();
                    }
                    else
                    {
                        comboPaises.SelectedIndex = -1;
                        txtCodPais.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar países: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                carregandoCombo = false;
            }
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

            if (comboPaises.SelectedItem == null)
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

            if (comboPaises.SelectedValue != null)
            {
                estado.IdPais = (int)comboPaises.SelectedValue;
            }
            else
            {
                throw new Exception("Nenhum país foi selecionado.");
            }

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

            CarregarPaises(estadoSelecionado.IdPais);

            comboPaises.SelectedValue = estadoSelecionado.IdPais;
            txtCodPais.Text = estadoSelecionado.IdPais.ToString();

            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";

            if (isExcluindo)
                BloquearCampos();
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
        }

        public void BloquearCampos()
        {
            txtEstado.Enabled = false;
            txtUf.Enabled = false;
            comboPaises.Enabled = false;
            btnSalvar.Enabled = true;
            btnCadastrar.Enabled = false;
        }

        public void DesbloquearCampos()
        {
            txtEstado.Enabled = true;
            txtUf.Enabled = true;
            comboPaises.Enabled = true;
            btnSalvar.Enabled = true;
            txtCodigo.Enabled = false;
        }

        private void comboPaises_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (carregandoCombo) return;

            if (comboPaises.SelectedItem is Pais pais)
            {
                txtCodPais.Text = pais.Id.ToString();
            }
            else
            {
                txtCodPais.Clear();
            }
        }

        private void FrmCadastroEstado_Load_1(object sender, EventArgs e)
        {
            if (comboPaises.Items.Count == 0)
                CarregarPaises();

            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (isExcluindo)
                {
                    if (estado != null && estado.Id != 0)
                    {
                        if (MessageBox.Show("Deseja realmente excluir este estado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
                    MessageBox.Show("Não é possível excluir o estado, pois ele está vinculado a uma ou mais cidades.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ex.Message.Contains("REFERENCE") && ex.Message.Contains("Cidades"))
                {
                    MessageBox.Show("Não é possível excluir o estado, pois ele está vinculado a uma ou mais cidades.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Erro ao salvar ou atualizar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            FrmCadastroPais frmCadastroPais = new FrmCadastroPais();

            frmCadastroPais.FormClosed += (s, args) =>
            {
                CarregarPaises(null);

                var listaAtualizada = (List<Pais>)comboPaises.DataSource;

                if (listaAtualizada != null && listaAtualizada.Any())
                {
                    var ultimoPais = listaAtualizada.OrderByDescending(p => p.Id).FirstOrDefault();

                    if (ultimoPais != null)
                    {
                        comboPaises.SelectedValue = ultimoPais.Id;
                        txtCodPais.Text = ultimoPais.Id.ToString();
                    }
                }
            };

            frmCadastroPais.ShowDialog();
        }
    }
}
