using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjetoPF.FormCadastros
{
    public partial class FrmCadastroPai : Form
    {
        public FrmCadastroPai()
        {
            InitializeComponent();
        }

        public virtual void Salvar()
        {
            Sair();
        }

        public virtual void Sair()
        {
            this.Close();
        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Sair();
        }
        public virtual void ConhecaObjeto(object obj)
        {
        }

        private void FrmCadastroPai_Load(object sender, EventArgs e)
        {

        }
    }
}
