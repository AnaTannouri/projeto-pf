using ProjetoPF.Dao;
using ProjetoPF.Modelos.Produto;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroUnidadeMedida : ProjetoPF.FormCadastros.FrmCadastroPai
    {
        private UnidadeMedida unidade = new UnidadeMedida();
        private BaseServicos<UnidadeMedida> unidadeServices = new BaseServicos<UnidadeMedida>(new BaseDao<UnidadeMedida>("UnidadeMedidas"));
        private bool isEditando = false;
        private bool isExcluindo = false;

        public UnidadeMedida UnidadeAtual => unidade;
        public FrmCadastroUnidadeMedida()
        {
            InitializeComponent();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            try
            {
                if (!ValidarEntrada()) return;

                AtualizarObjeto();

                if (isEditando)
                {
                    if (unidadeServices.BuscarPorId(unidade.Id) == null)
                    {
                        MessageBox.Show($"O registro com ID {unidade.Id} não existe.");
                        return;
                    }

                    unidadeServices.Atualizar(unidade);
                    isEditando = false;
                    this.Close();
                    MessageBox.Show("Unidade de medida atualizada com sucesso!");
                }
                else if (isExcluindo)
                {
                    DialogResult result = MessageBox.Show(
                                          "Você tem certeza que deseja remover este item?",
                                          "Confirmar remoção",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        unidadeServices.Remover(unidade.Id);
                        isExcluindo = false;
                        this.Close();
                    }
                }
                else
                {
                    unidadeServices.Criar(unidade);
                    MessageBox.Show("Unidade de medida salva com sucesso!");
                    this.Close();
                }

                LimparCampos();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                string mensagem;

                if (ex.Message.Contains("FK_ProdutoUnidadeMedida"))
                    mensagem = "Não é possível remover: está associada a um ou mais produtos.";
                else
                    mensagem = "Não é possível remover a unidade de medida, pois ela está em uso.";

                MessageBox.Show(mensagem, "Erro de integridade referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar ou atualizar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool ValidarEntrada()
        {
            string descricao = txtDescricao.Text.Trim();
            if (string.IsNullOrEmpty(descricao))
            {
                MessageBox.Show("Informe a unidade de medida.");
                return false;
            }

            if (unidadeServices.VerificarDuplicidade("Descricao", descricao, unidade))
            {
                MessageBox.Show("Já existe uma unidade de medida com essa descrição.");
                return false;
            }

            return true;
        }
        private void AtualizarObjeto()
        {
            unidade.Descricao = txtDescricao.Text.Trim();
            unidade.Ativo = checkAtivo.Checked;

            if (isEditando || isExcluindo)
                unidade.Id = int.Parse(txtCodigo.Text);
            else
                unidade.Id = 0;

            unidade.DataCriacao = unidade.DataCriacao == DateTime.MinValue ? DateTime.Now : unidade.DataCriacao;
            unidade.DataAtualizacao = DateTime.Now;
        }
        public void CarregarDados(UnidadeMedida unidadeMedida, bool isEditandoForm, bool isExcluindoForm)
        {
            txtCodigo.Text = unidadeMedida.Id.ToString();
            txtDescricao.Text = unidadeMedida.Descricao;
            checkAtivo.Checked = unidadeMedida.Ativo;

            unidade = unidadeMedida;
            isEditando = isEditandoForm;
            isExcluindo = isExcluindoForm;
        }

        public void LimparCampos()
        {
            txtCodigo.Clear();
            txtDescricao.Clear();
            unidade = new UnidadeMedida();
            isEditando = false;
            checkAtivo.Checked = true;
        }

        public void BloquearCampos()
        {
            txtCodigo.Enabled = false;
            txtDescricao.Enabled = false;
        }

        public void DesbloquearCampos()
        {
            txtCodigo.Enabled = false;
            txtDescricao.Enabled = true;
            btnSalvar.Enabled = true;
        }

        private void FrmCadastroUnidadeMedida_Load(object sender, EventArgs e)
        {
            checkAtivo.Enabled = isEditando;
            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";
            labelCriacao.Text = unidade.DataCriacao.ToShortDateString();
            lblAtualizacao.Text = unidade.DataAtualizacao.ToShortDateString();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            isExcluindo = false;
            isEditando = false;
        }
    }
}
