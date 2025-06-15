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

        FrmConsultaCategoria frmConsultaCategoria = new FrmConsultaCategoria();
        FrmConsultaUnidadeMedida frmConsultaUnidadeMedida = new FrmConsultaUnidadeMedida();
        FrmConsultaMarca frmConsultaMarca = new FrmConsultaMarca();
        FrmConsultaProduto frmConsultaProduto = new FrmConsultaProduto();  
        public FrmPrincipal()
        {
            InitializeComponent();
        }
        private void formaDePagamentoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmConsultaFormaPagamento.ShowDialog();
        }

        private void condiçãoDePagamentoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmConsultaCondPagamento.ShowDialog();
        }

        private void paísToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmConsultaPais.ShowDialog();
        }

        private void estadoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmConsultaEstado.ShowDialog();
        }

        private void cidadeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmConsultaCidade.ShowDialog();
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaCliente.ShowDialog();
        }

        private void funcionárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaFuncionario.ShowDialog();
        }

        private void fornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaFornecedor.ShowDialog();
        }

        private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaCategoria.ShowDialog();
        }

        private void unidadeDeMedidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaUnidadeMedida.ShowDialog();
        }

        private void marcaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaMarca.ShowDialog();
        }
        private void produtoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaProduto.ShowDialog();
        }
    }
}
