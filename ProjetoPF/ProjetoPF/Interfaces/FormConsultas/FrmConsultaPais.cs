using ProjetoPF.Dao;
using ProjetoPF.Interfaces.FormCadastros;
using ProjetoPF.Modelos.Localizacao;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormConsultas
{
    public partial class FrmConsultaPais : ProjetoPF.FormConsultas.FrmConsultaPai
    {
        private FrmCadastroPais frmCadastroPais;
        private BaseServicos<Pais> paisServices = new BaseServicos<Pais>(new BaseDao<Pais>("Paises"));
        public FrmConsultaPais()
        {
            InitializeComponent();
            frmCadastroPais = new FrmCadastroPais();
        }

        private void AjustarLarguraColunas()
        {
            int columnWidth = listViewFormaPagamento.Width / listViewFormaPagamento.Columns.Count;
            foreach (ColumnHeader col in listViewFormaPagamento.Columns)
            {
                col.Width = columnWidth;
            }
        }

        public override void PopularListView(string pesquisa)
        {
            listViewFormaPagamento.Items.Clear();
            List<Pais> paises = paisServices.BuscarTodos(pesquisa);

            if (paises != null && paises.Count > 0)
            {
                foreach (var pais in paises)
                {
                    ListViewItem item = new ListViewItem(pais.Id.ToString())
                    {
                        SubItems =
                {
                    pais.Nome,
                    pais.Sigla,
                    pais.DDI,
                    pais.DataCriacao.ToString("dd/MM/yyyy"),
                    pais.DataAtualizacao.ToString("dd/MM/yyyy")
                }
                    };
                    listViewFormaPagamento.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Nenhum resultado encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmCadastroPais_FormClosed(object sender, FormClosedEventArgs e)
        {
            PopularListView(string.Empty);
            frmCadastroPais.FormClosed -= FrmCadastroPais_FormClosed;
        }

        private void btnAdicionar_Click_2(object sender, EventArgs e)
        {
            frmCadastroPais = new FrmCadastroPais();
            frmCadastroPais.LimparCampos();
            frmCadastroPais.DesbloquearCampos();
            frmCadastroPais.FormClosed += FrmCadastroPais_FormClosed;
            frmCadastroPais.ShowDialog();
        }

        private void btnEditar_Click_2(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewFormaPagamento.SelectedItems[0];

                if (!int.TryParse(selectedItem.Text, out int id))
                {
                    MessageBox.Show("ID inválido selecionado.", "Erro");
                    return;
                }

                Pais paisSelecionado = paisServices.BuscarPorId(id);

                if (paisSelecionado == null)
                {
                    MessageBox.Show("País não encontrado.", "Erro");
                    return;
                }

                frmCadastroPais = new FrmCadastroPais();
                frmCadastroPais.CarregarDados(paisSelecionado, true, false);
                frmCadastroPais.DesbloquearCampos();
                frmCadastroPais.FormClosed += FrmCadastroPais_FormClosed;
                frmCadastroPais.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione um país para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewFormaPagamento.SelectedItems[0];

                if (!int.TryParse(selectedItem.Text, out int id))
                {
                    MessageBox.Show("ID inválido selecionado.", "Erro");
                    return;
                }

                Pais paisSelecionado = paisServices.BuscarPorId(id);

                if (paisSelecionado == null)
                {
                    MessageBox.Show("País não encontrado.", "Erro");
                    return;
                }

                frmCadastroPais = new FrmCadastroPais();
                frmCadastroPais.CarregarDados(paisSelecionado, false, true);
                frmCadastroPais.BloquearCampos();
                frmCadastroPais.FormClosed += FrmCadastroPais_FormClosed;
                frmCadastroPais.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione um país para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmConsultaPais_Load(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                listViewFormaPagamento.Columns.Add("Código", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Nome", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Sigla", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("DDI", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Data de Criação", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Data de Atualização", -2, HorizontalAlignment.Left);
            }

            AjustarLarguraColunas();
            PopularListView(string.Empty);
        }
    }
}
