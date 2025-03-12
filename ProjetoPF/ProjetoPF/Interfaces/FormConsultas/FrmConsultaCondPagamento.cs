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
    }
}
