using ProjetoPF.FormConsultas;
using ProjetoPF.Interfaces.FormConsultas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoPF
{
    public partial class FrmPrincipal : Form
    {
        FrmConsultaFormaPagamento frmConsultaFormaPagamento = new FrmConsultaFormaPagamento();
        FrmConsultaCondPagamento frmConsultaCondPagamento = new FrmConsultaCondPagamento();

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
    }
}
