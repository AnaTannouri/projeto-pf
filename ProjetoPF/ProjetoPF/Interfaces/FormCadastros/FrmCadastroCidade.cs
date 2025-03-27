using ProjetoPF.Dao;
using ProjetoPF.Modelos.Localizacao;
using ProjetoPF.Servicos;
using ProjetoPF.Servicos.Localizacao;
using System;
using System.Collections.Generic;
using System.Data;
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
        public Cidade CidadeAtual => cidade;

        public FrmCadastroCidade()
        {
            InitializeComponent();
        }
        public void CarregarEstados(int? idSelecionado = null)
        {
            try
            {
                carregandoCombo = true;

                var listaEstados = estadoServices.BuscarTodos();

                comboEstados.DataSource = null;
                comboEstados.Items.Clear();

                if (listaEstados != null && listaEstados.Any())
                {
                    comboEstados.DataSource = listaEstados;
                    comboEstados.DisplayMember = "Nome";
                    comboEstados.ValueMember = "Id";

                    if (idSelecionado.HasValue)
                    {
                        comboEstados.SelectedValue = idSelecionado.Value;
                        comboEstados.Text = idSelecionado.Value.ToString();
                    }
                    else
                    {
                        comboEstados.SelectedIndex = -1;
                        txtCodEstado.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar estados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                carregandoCombo = false;
            }
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

            if (comboEstados.SelectedItem == null)
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

            if (comboEstados.SelectedValue != null)
            {
                cidade.IdEstado = (int)comboEstados.SelectedValue;
            }
            else
            {
                throw new Exception("Nenhum estado foi selecionado.");
            }

            cidade.Id = isEditando || isExcluindo ? int.Parse(txtCodigo.Text) : 0;
            cidade.DataCriacao = cidade.DataCriacao == DateTime.MinValue ? DateTime.Now : cidade.DataCriacao;
            cidade.DataAtualizacao = DateTime.Now;
        }

        public void CarregarDados(Cidade cidadeSelecionada, bool isEditandoForm, bool isExcluindoForm)
        {
            cidade = cidadeSelecionada;
            isEditando = isEditandoForm;
            isExcluindo = isExcluindoForm;

            txtCodigo.Text = cidade.Id.ToString();
            txtCidade.Text = cidade.Nome;
            txtDDD.Text = cidade.DDD;

            CarregarEstados(cidade.IdEstado);
            comboEstados.SelectedValue = cidade.IdEstado;
            txtCodEstado.Text = cidade.IdEstado.ToString();

            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";

            if (isExcluindo)
                BloquearCampos();
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
        }

        public void BloquearCampos()
        {
            txtCidade.Enabled = false;
            txtDDD.Enabled = false;
            comboEstados.Enabled = false;
            btnSalvar.Enabled = true;
            btnCadastrar.Enabled = false;
        }

        public void DesbloquearCampos()
        {
            txtCidade.Enabled = true;
            txtDDD.Enabled = true;
            comboEstados.Enabled = true;
            btnSalvar.Enabled = true;
            txtCodigo.Enabled = false;
        }

        private void btnCadastrar_Click_1(object sender, EventArgs e)
        {
            FrmCadastroEstado frmCadastroEstado = new FrmCadastroEstado();

            frmCadastroEstado.FormClosed += (s, args) =>
            {
                CarregarEstados(null);

                var listaAtualizada = (List<Estado>)comboEstados.DataSource;

                if (listaAtualizada != null && listaAtualizada.Any())
                {
                    var ultimoEstado = listaAtualizada.OrderByDescending(p => p.Id).FirstOrDefault();

                    if (ultimoEstado != null)
                    {
                        comboEstados.SelectedValue = ultimoEstado.Id;
                        txtCodEstado.Text = ultimoEstado.Id.ToString();
                    }
                }
            };

            frmCadastroEstado.ShowDialog();
        }

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {

            try
            {
                if (isExcluindo)
                {
                    if (cidade != null && cidade.Id != 0)
                    {
                        if (MessageBox.Show("Deseja realmente excluir esta cidade?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
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
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
            }
        }

        private void FrmCadastroCidade_Load_1(object sender, EventArgs e)
        {
            if (comboEstados.Items.Count == 0)
                CarregarEstados();

            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";
        }

        private void comboEstados_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (carregandoCombo) return;

            if (comboEstados.SelectedItem is Estado estado)
            {
                txtCodEstado.Text = estado.Id.ToString();
            }
            else
            {
                txtCodEstado.Clear();
            }
        }
    }
}
