using ProjetoPF.Dao;
using System;
using System.Windows.Forms;

namespace ProjetoPF.FormCadastros
{
    public partial class FrmCadastroFormaPagamento : ProjetoPF.FormCadastros.FrmCadastroPai
    {
        private FormaPagamento formaPagamento;
        private FormaPagamentoServicos formaPagamentoServicos;
        private FormaPagamentoDAO formaPagamentoDao;

        public FrmCadastroFormaPagamento()
        {
            InitializeComponent();
            formaPagamentoServicos = new FormaPagamentoServicos();
            formaPagamento = new FormaPagamento();
            formaPagamentoDao = new FormaPagamentoDAO();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            string descricaoLimpa = txtDescricao.Text.Trim().ToLower();

            Console.WriteLine("Valor a ser verificado: '" + descricaoLimpa + "'");

            if (string.IsNullOrEmpty(descricaoLimpa))
            {
                MessageBox.Show("Informe uma descrição.");
                return;
            }

            if (formaPagamentoDao.VerificarDuplicidade("Descricao", descricaoLimpa) && formaPagamento.Id == 0)
            {
                MessageBox.Show("Esta forma de pagamento já está cadastrada.");
                return;
            }

            formaPagamento.Descricao = descricaoLimpa;

            if (formaPagamento.Id == 0)
            {
                formaPagamentoServicos.CadastrarFormaPagamento(formaPagamento);
                MessageBox.Show("Forma de pagamento salva com sucesso!");
            }
            else
            {
                MessageBox.Show("Forma de pagamento atualizada com sucesso!");
            }
            LimparCampos();
        }

        public void CarregarDados(FormaPagamento formaPagamento)
        {
            txtCodigo.Text = formaPagamento.Id.ToString();
            txtDescricao.Text = formaPagamento.Descricao;
        }

        public void LimparCampos()
        {
            txtDescricao.Clear();
            txtCodigo.Clear();
        }
    }
}
