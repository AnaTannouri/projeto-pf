using ProjetoPF.FormConsultas;
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

        public FrmPrincipal()
        {
            InitializeComponent();
        }

        private void formaDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaFormaPagamento.ShowDialog();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}
