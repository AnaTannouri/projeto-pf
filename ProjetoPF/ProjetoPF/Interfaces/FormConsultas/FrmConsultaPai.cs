using System;
using System.Windows.Forms;

namespace ProjetoPF.FormConsultas
{
    public partial class FrmConsultaPai : Form
    {
        public FrmConsultaPai()
        {
            InitializeComponent();
        }

        public virtual void PopularListView(string pesquisa)
        {

        }

        private void btnVoltar_Click(object sender, EventArgs e)
        {
            Sair();
            PopularListView(string.Empty);
        }

        public virtual void Sair()
        {
            this.Close();
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string pesquisa = txtPesquisa.Text.Trim();
            PopularListView(string.IsNullOrEmpty(pesquisa) ? string.Empty : pesquisa);
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {

            txtPesquisa.Text = string.Empty;

            PopularListView(string.Empty); 
        }

        public void btnEditar_Click(object sender, EventArgs e)
        {
        }

        public void btnAdicionar_Click(object sender, EventArgs e)
        {
        }

        private void FrmConsultaPai_Load(object sender, EventArgs e)
        {

        }
    }
}
