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
    public partial class FrmCadastroMarca : ProjetoPF.FormCadastros.FrmCadastroPai
    {
        private Marca marca = new Marca();
        private BaseServicos<Marca> marcaServices = new BaseServicos<Marca>(new BaseDao<Marca>("Marcas"));
        private bool isEditando = false;
        private bool isExcluindo = false;
        public Marca MarcaAtual => marca;

        public FrmCadastroMarca()
        {
            InitializeComponent();
        }

        private void FrmCadastroMarca_Load(object sender, EventArgs e)
        {
            checkAtivo.Enabled = isEditando;
            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";
            labelCriacao.Text = marca.DataCriacao.ToShortDateString();
            lblAtualizacao.Text = marca.DataAtualizacao.ToShortDateString();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarEntrada()) return;

                AtualizarObjeto();

                if (isEditando)
                {
                    if (marcaServices.BuscarPorId(marca.Id) == null)
                    {
                        MessageBox.Show($"O registro com ID {marca.Id} não existe.");
                        return;
                    }

                    marcaServices.Atualizar(marca);
                    isEditando = false;
                    this.Close();
                    MessageBox.Show("Marca atualizada com sucesso!");
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
                        marcaServices.Remover(marca.Id);
                        isExcluindo = false;
                        this.Close();
                    }
                }
                else
                {
                    marcaServices.Criar(marca);
                    MessageBox.Show("Marca salva com sucesso!");
                    this.Close();
                }

                LimparCampos();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                string mensagem;

                if (ex.Message.Contains("FK_ProdutoMarca"))
                    mensagem = "Não é possível remover: está associada a um ou mais produtos.";
                else
                    mensagem = "Não é possível remover a marca, pois ela está em uso.";

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
                MessageBox.Show("Informe a marca.");
                return false;
            }

            if (marcaServices.VerificarDuplicidade("Descricao", descricao, marca))
            {
                MessageBox.Show("Já existe uma marca com essa descrição.");
                return false;
            }

            return true;
        }

        private void AtualizarObjeto()
        {
            marca.Descricao = txtDescricao.Text.Trim();
            marca.Ativo = checkAtivo.Checked;

            if (isEditando || isExcluindo)
                marca.Id = int.Parse(txtCodigo.Text);
            else
                marca.Id = 0;

            marca.DataCriacao = marca.DataCriacao == DateTime.MinValue ? DateTime.Now : marca.DataCriacao;
            marca.DataAtualizacao = DateTime.Now;
        }

        public void CarregarDados(Marca marcaSelecionada, bool isEditandoForm, bool isExcluindoForm)
        {
            txtCodigo.Text = marcaSelecionada.Id.ToString();
            txtDescricao.Text = marcaSelecionada.Descricao;
            checkAtivo.Checked = marcaSelecionada.Ativo;

            marca = marcaSelecionada;
            isEditando = isEditandoForm;
            isExcluindo = isExcluindoForm;
        }

        public void LimparCampos()
        {
            txtCodigo.Clear();
            txtDescricao.Clear();
            marca = new Marca();
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

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            isExcluindo = false;
            isEditando = false;
        }
    }
}
