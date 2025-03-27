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
    public partial class FrmConsultaCliente : ProjetoPF.FormConsultas.FrmConsultaPai
    {
        private FrmCadastroCliente frmCadastroCliente = new FrmCadastroCliente();
        public FrmConsultaCliente()
        {
            InitializeComponent();
        }

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {
            frmCadastroCliente.ShowDialog();
        }

        private void FrmConsultaCliente_Load(object sender, EventArgs e)
        {

        }
    }
}
