using ProjetoPF.Dao;
using ProjetoPF.Modelos.Localizacao;
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
    public partial class FrmCadastroCategoria : ProjetoPF.FormCadastros.FrmCadastroPai
    {
        private Categoria categoria = new Categoria();
        private BaseServicos<Categoria> categoriaServices = new BaseServicos<Categoria>(new BaseDao<Categoria>("Categorias"));
        private bool isEditando = false;
        private bool isExcluindo = false;
        public Categoria CategoriaAtual => categoria;
        public FrmCadastroCategoria()
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
                    if (categoriaServices.BuscarPorId(categoria.Id) == null)
                    {
                        MessageBox.Show($"O registro com ID {categoria.Id} não existe.");
                        return;
                    }
                    categoriaServices.Atualizar(categoria);
                    isEditando = false;
                    this.Close();
                    MessageBox.Show("Categoria atualizada com sucesso!");
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
                        categoriaServices.Remover(categoria.Id);
                        isExcluindo = false;
                        this.Close();
                    }
                }
                else
                {
                    categoriaServices.Criar(categoria);
                    MessageBox.Show("Categoria salva com sucesso!");
                    this.Close();
                }
                LimparCampos();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                string mensagem;

                if (ex.Message.Contains("FK_CategoriaProduto"))
                    mensagem = "Não é possível remover: está associada a um ou mais produtos.";
                else
                    mensagem = "Não é possível remover a forma de pagamento, pois ela está em uso.";

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
                MessageBox.Show("Informe uma categoria.");
                return false;
            }

            if (categoriaServices.VerificarDuplicidade("Descricao", descricao, categoria))
            {
                MessageBox.Show("Já existe uma categoria com essa descrição.");
                return false;
            }

            return true;
        }
        private void AtualizarObjeto()
        {
            categoria.Descricao = txtDescricao.Text.Trim();
            categoria.Ativo = checkAtivo.Checked;

            if (isEditando || isExcluindo)
                categoria.Id = int.Parse(txtCodigo.Text);
            else
                categoria.Id = 0;

            categoria.DataCriacao = categoria.DataCriacao == DateTime.MinValue ? DateTime.Now : categoria.DataCriacao;
            categoria.DataAtualizacao = DateTime.Now;
        }
        public void CarregarDados(Categoria cate, bool isEditandoForm, bool isExcluindoForm)
        {
            txtCodigo.Text = cate.Id.ToString();
            txtDescricao.Text = cate.Descricao;
            checkAtivo.Checked = cate.Ativo;

            categoria = cate;
            isEditando = isEditandoForm;
            isExcluindo = isExcluindoForm;
        }

        public void LimparCampos()
        {
            txtCodigo.Clear();
            txtDescricao.Clear();
            categoria = new Categoria();
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

        private void FrmCadastroCategoria_Load(object sender, EventArgs e)
        {
            checkAtivo.Enabled = isEditando;
            if (isExcluindo)
            {
                btnSalvar.Text = "Remover";
            }
            else
            {
                btnSalvar.Text = "Salvar";
            }
            labelCriacao.Text = categoria.DataCriacao > DateTime.MinValue ? categoria.DataCriacao.ToShortDateString() : "";
            lblAtualizacao.Text = categoria.DataAtualizacao > DateTime.MinValue ? categoria.DataAtualizacao.ToShortDateString() : "";
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            isExcluindo = false;
            isEditando = false;
        }
    }
}
