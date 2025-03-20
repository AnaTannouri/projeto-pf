using System;
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                MessageBox.Show("A condição está ativa.");
            }
            else
            {
                MessageBox.Show("A condição está inativa.");
            }
        }
        public virtual void CarregarFormasDePagamento()
        {

        }
    }
}
