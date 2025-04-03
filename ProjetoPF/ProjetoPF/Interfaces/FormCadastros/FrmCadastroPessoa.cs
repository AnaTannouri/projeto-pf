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
        private void CarregarTipoPessoa()
        {
            comboPessoa.Items.Clear();
            comboPessoa.BindingContext = new BindingContext();
            comboPessoa.Items.Add("Física");
            comboPessoa.Items.Add("Jurídica");
            comboPessoa.SelectedIndex = -1;
        }

        private void FrmCadastroPessoa_Load(object sender, EventArgs e)
        {
            CarregarTipoPessoa();
        }

        private void comboPessoa_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboClassificacao.DataSource = null; // desvincula o DataSource
            if (comboClassificacao.Items.Count > 0)
                comboClassificacao.Items.Clear();

            comboClassificacao.Text = string.Empty;
            comboClassificacao.SelectedIndex = -1;

            if (comboPessoa.SelectedItem?.ToString() == "Física")
            {
                lblClassificacao.Text = "Gênero:";
                comboClassificacao.Items.Add("Masculino");
                comboClassificacao.Items.Add("Feminino");
            }
            else if (comboPessoa.SelectedItem?.ToString() == "Jurídica")
            {
                lblClassificacao.Text = "Tipo de Empresa:";
                comboClassificacao.Items.Add("MEI");
                comboClassificacao.Items.Add("LTDA");
                comboClassificacao.Items.Add("SA");
            }
        }
    }
}
