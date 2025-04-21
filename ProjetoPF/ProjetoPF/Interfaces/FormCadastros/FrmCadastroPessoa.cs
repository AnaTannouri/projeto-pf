using ProjetoPF.Dao;
using ProjetoPF.Modelos.Localizacao;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroPessoa : ProjetoPF.FormCadastros.FrmCadastroPai
    {
        private FrmCadastroCidade frmCadastroCidade = new FrmCadastroCidade();
        private BaseServicos<Cidade> cidadeServices = new BaseServicos<Cidade>(new BaseDao<Cidade>("Cidades"));
        private bool carregandoCidades = false;

        public FrmCadastroPessoa()
        {
            InitializeComponent();
        }
        public void CarregarTipoPessoa()
        {
            comboPessoa.Items.Clear();
            comboPessoa.BindingContext = new BindingContext();
            comboPessoa.Items.Add("FÍSICA");
            comboPessoa.Items.Add("JURÍDICA");
            comboPessoa.SelectedIndex = -1;
        }

        private void FrmCadastroPessoa_Load(object sender, EventArgs e)
        {
            CarregarTipoPessoa();
        }

        private void comboPessoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboClassificacao.DataSource = null; 
            if (comboClassificacao.Items.Count > 0)
                comboClassificacao.Items.Clear();

            comboClassificacao.Text = string.Empty;
            comboClassificacao.SelectedIndex = -1;

            if (comboPessoa.SelectedItem?.ToString() == "FÍSICA")
            {
                label4.Text = "Nome:";
                label5.Text = "Apelido:";
                label16.Text = "Data de Nascimento:";
                label3.Text = "Cpf:";
                label6.Text = "Rg:";
            }
            if (comboPessoa.SelectedItem?.ToString() == "JURÍDICA")
            {
                label4.Text = "Razão Social:";
                label5.Text = "Nome Fantasia:";
                label16.Text = "Data de Criação:";
                label3.Text = "Cnpj:";
                label6.Text = "Inscrição Estadual:";
            }
            if (comboPessoa.SelectedItem?.ToString() == "FÍSICA")
            {
                lblClassificacao.Text = "Gênero:";

            }
            if (comboPessoa.SelectedItem?.ToString() == "FÍSICA")
            {
                lblClassificacao.Text = "Gênero:";
                lblClassificacao.Visible = true;
                comboClassificacao.Visible = true;

                comboClassificacao.Items.Clear();
                comboClassificacao.Items.Add("FEMININO");
                comboClassificacao.Items.Add("MASCULINO");
                comboClassificacao.Items.Add("OUTROS");
                comboClassificacao.SelectedIndex = -1;
            }
            else if (comboPessoa.SelectedItem?.ToString() == "JURÍDICA")
            {
                lblClassificacao.Visible = false;
                comboClassificacao.Visible = false;
            }
        }
    }
}
