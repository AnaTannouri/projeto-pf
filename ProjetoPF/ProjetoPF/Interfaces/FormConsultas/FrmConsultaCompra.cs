using ProjetoPF.Interfaces.FormCadastros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormConsultas
{
    public partial class FrmConsultaCompra : ProjetoPF.FormConsultas.FrmConsultaPai
    {
        private FrmCadastroCompra frmCadastroCompra = new FrmCadastroCompra();
        public FrmConsultaCompra()
        {
            InitializeComponent();
        }

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {
            frmCadastroCompra = new FrmCadastroCompra();
            frmCadastroCompra.ShowDialog();
        }
    }
}
