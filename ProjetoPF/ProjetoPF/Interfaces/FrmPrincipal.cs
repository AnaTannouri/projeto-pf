using ProjetoPF.FormConsultas;
using ProjetoPF.Interfaces.FormConsultas;
using System;
using System.Windows.Forms;

namespace ProjetoPF
{
    public partial class FrmPrincipal : Form
    {
        FrmConsultaFormaPagamento frmConsultaFormaPagamento = new FrmConsultaFormaPagamento();
        FrmConsultaCondPagamento frmConsultaCondPagamento = new FrmConsultaCondPagamento();

        FrmConsultaPais frmConsultaPais = new FrmConsultaPais();
        FrmConsultaEstado frmConsultaEstado = new FrmConsultaEstado();
        FrmConsultaCidade frmConsultaCidade = new FrmConsultaCidade();

        FrmConsultaCliente frmConsultaCliente = new FrmConsultaCliente();
        FrmConsultaFornecedor frmConsultaFornecedor = new FrmConsultaFornecedor();
        FrmConsultaFuncionario frmConsultaFuncionario = new FrmConsultaFuncionario();

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void formaDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaFormaPagamento.ShowDialog();
        }

        private void condiçãoDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaCondPagamento.ShowDialog();
        }

        private void paísToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaPais.ShowDialog();
        }
        private void estadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaEstado.ShowDialog();
        }

        private void cidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaCidade.ShowDialog();
        }

        private void clienteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaCliente.ShowDialog();
        }

        private void fornecedorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaFornecedor.ShowDialog();
        }

        private void funcionárioToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaFuncionario.ShowDialog();
        }
    }
}
