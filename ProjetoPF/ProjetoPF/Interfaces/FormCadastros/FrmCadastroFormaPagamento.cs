using ProjetoPF.Dao;
using ProjetoPF.FormConsultas;
using ProjetoPF.Interfaces.FormCadastros;
using ProjetoPF.Servicos;
using System;
using System.Windows.Forms;

namespace ProjetoPF.FormCadastros
{
    public partial class FrmCadastroFormaPagamento : FrmCadastroPai
    {
        private FormaPagamento formaPagamento = new FormaPagamento();
        private BaseServicos<FormaPagamento> formaPagamentoServices = new BaseServicos<FormaPagamento>(new BaseDao<FormaPagamento>("FormaPagamentos"));
        private bool isEditando = false;
        private bool isExcluindo = false;
        public FormaPagamento FormaPagamentoAtual => formaPagamento;

        public FrmCadastroFormaPagamento()
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
                    if (formaPagamentoServices.BuscarPorId(formaPagamento.Id) == null)
                    {
                        MessageBox.Show($"O registro com ID {formaPagamento.Id} não existe.");
                        return;
                    }
                    formaPagamentoServices.Atualizar(formaPagamento);
                    isEditando = false;
                    this.Sair();
                    MessageBox.Show("Forma de pagamento atualizada com sucesso!");
                }
                else if (isExcluindo)
                {
                    DialogResult result = MessageBox.Show(
                                          "Você tem certeza que deseja excluir este item?",
                                          "Confirmar Exclusão",
                                          MessageBoxButtons.YesNo,
                                          MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        formaPagamentoServices.Remover(formaPagamento.Id);
                        isExcluindo = false;
                        this.Sair();
                    }
                }
                else
                {
                    formaPagamentoServices.Criar(formaPagamento);
                    MessageBox.Show("Forma de pagamento salva com sucesso!");
                }
                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar ou atualizar: {ex.Message}");
            }

        }

        private bool ValidarEntrada()
        {
            string descricao = txtDescricao.Text.Trim();
            if (string.IsNullOrEmpty(descricao))
            {
                MessageBox.Show("Informe uma descrição.");
                return false;
            }

            if (formaPagamentoServices.VerificarDuplicidade("Descricao", descricao, formaPagamento))
            {
                MessageBox.Show("Já existe uma forma de pagamento com essa descrição.");
                return false;
            }

            return true;
        }

        private void AtualizarObjeto()
        {
            formaPagamento.Descricao = txtDescricao.Text.Trim();

            if (isEditando || isExcluindo)
                formaPagamento.Id = int.Parse(txtCodigo.Text);
            else
                formaPagamento.Id = 0;

            formaPagamento.DataCriacao = formaPagamento.DataCriacao == DateTime.MinValue ? DateTime.Now : formaPagamento.DataCriacao;
            formaPagamento.DataAtualizacao = DateTime.Now;
        }

        public void CarregarDados(FormaPagamento forma, bool isEditandoForm, bool isExcluindoForm)
        {
            txtCodigo.Text = forma.Id.ToString();
            txtDescricao.Text = forma.Descricao;
            formaPagamento = forma;
            isEditando = isEditandoForm;
            isExcluindo = isExcluindoForm;
        }

        public void LimparCampos()
        {
            txtCodigo.Clear();
            txtDescricao.Clear();
            formaPagamento = new FormaPagamento();
            isEditando = false;
        }
        public void BloquearCampos()
        {
            txtCodigo.Enabled = false;
            txtDescricao.Enabled = false;
        }

        public void DesbloquearCampos()
        {
            txtCodigo.Enabled = true;
            txtDescricao.Enabled = true;
            btnSalvar.Enabled = true;
        }

        private void FrmCadastroFormaPagamento_Load(object sender, EventArgs e)
        {
            if (isExcluindo)
            {
                btnSalvar.Text = "Remover"; // Altere o texto do botão para "Remover" no modo de exclusão
            }
            else
            {
                btnSalvar.Text = "Salvar"; // Ou outro texto relevante no modo normal
            }
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            isExcluindo = false;
            isEditando = false;
        }
    }
}