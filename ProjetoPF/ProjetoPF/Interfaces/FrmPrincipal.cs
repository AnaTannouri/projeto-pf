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

        FrmConsultaCompra frmConsultaCompra = new FrmConsultaCompra();
        FrmConsultaContasAPagar frmConsultaContasAPagar = new FrmConsultaContasAPagar();
        public FrmPrincipal()
        {
            InitializeComponent();
        }
        private void formaDePagamentoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaFormaPagamento.ShowDialog();
        }

        private void condiçãoDePagamentoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaCondPagamento.ShowDialog();
        }

        private void paísToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaPais.ShowDialog();
        }

        private void estadoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaEstado.ShowDialog();
        }

        private void cidadeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaCidade.ShowDialog();
        }

        private void clienteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaCliente.ShowDialog();
        }

        private void funcionárioToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaFuncionario.ShowDialog();
        }

        private void fornecedorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaFornecedor.ShowDialog();
        }

        private void categoriaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaCategoria.ShowDialog();
        }

        private void unidadeDeMedidaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaUnidadeMedida.ShowDialog();
        }

        private void marcaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConsultaMarca.ShowDialog();
        }

        private void produtoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmConsultaProduto.ShowDialog();
        }
        private void comprasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaCompra.ShowDialog();
        }

        private void contasAPagarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaContasAPagar.ShowDialog();
        }
    }
}
