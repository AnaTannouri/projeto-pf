using ProjetoPF.Interfaces.FormCadastros;
using System;

namespace ProjetoPF.Interfaces.FormConsultas
{

    public partial class FrmConsultaCondPagamento : ProjetoPF.FormConsultas.FrmConsultaPai
    {

        FrmCadastroCondPagamento frmCadastroCondPagamento = new FrmCadastroCondPagamento();
        public FrmConsultaCondPagamento()
        {
            InitializeComponent();
        }

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {
            frmCadastroCondPagamento.ShowDialog();
        }

        private void FrmConsultaCondPagamento_Load(object sender, EventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

        }
    }
}
