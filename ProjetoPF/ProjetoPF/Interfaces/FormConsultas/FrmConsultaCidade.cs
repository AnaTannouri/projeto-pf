using ProjetoPF.Dao;
using ProjetoPF.Interfaces.FormCadastros;
using ProjetoPF.Modelos.Localizacao;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormConsultas
{
    public partial class FrmConsultaCidade : ProjetoPF.FormConsultas.FrmConsultaPai
    {
        private FrmCadastroCidade frmCadastroCidade;
        private BaseServicos<Cidade> cidadeServices = new BaseServicos<Cidade>(new BaseDao<Cidade>("Cidades"));
        private BaseServicos<Estado> estadoServices = new BaseServicos<Estado>(new BaseDao<Estado>("Estados"));

        public FrmConsultaCidade()
        {
            InitializeComponent();
            frmCadastroCidade = new FrmCadastroCidade();
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
            List<Cidade> cidades = cidadeServices.BuscarTodos(pesquisa);
            List<Estado> estados = estadoServices.BuscarTodos();

            if (cidades != null && cidades.Count > 0)
            {
                foreach (var cidade in cidades)
                {
                    var nomeEstado = estados.FirstOrDefault(e => e.Id == cidade.IdEstado)?.Nome ?? "N/A";

                    ListViewItem item = new ListViewItem(cidade.Id.ToString())
                    {
                        SubItems =
                        {
                            cidade.Nome,
                            cidade.DDD,
                            nomeEstado,
                            cidade.DataCriacao.ToString("dd/MM/yyyy"),
                            cidade.DataAtualizacao.ToString("dd/MM/yyyy")
                        }
                    };
                    listViewFormaPagamento.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Nenhuma cidade encontrada!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmConsultaCidade_Load_1(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                listViewFormaPagamento.Columns.Add("Código", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Nome", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("DDD", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Nome do Estado", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Data de Criação", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Data de Atualização", -2, HorizontalAlignment.Left);
            }

            AjustarLarguraColunas();
            PopularListView(string.Empty);

        }

        private void FrmCadastroCidade_FormClosed(object sender, FormClosedEventArgs e)
        {
            PopularListView(string.Empty);
            frmCadastroCidade.FormClosed -= FrmCadastroCidade_FormClosed;
        }

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {
            frmCadastroCidade = new FrmCadastroCidade();
            frmCadastroCidade.LimparCampos();
            frmCadastroCidade.DesbloquearCampos();
            frmCadastroCidade.FormClosed += FrmCadastroCidade_FormClosed;
            frmCadastroCidade.ShowDialog();
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewFormaPagamento.SelectedItems[0];
                if (!int.TryParse(selectedItem.Text, out int id))
                {
                    MessageBox.Show("ID inválido.", "Erro");
                    return;
                }

                var cidade = cidadeServices.BuscarPorId(id);
                if (cidade == null)
                {
                    MessageBox.Show("Cidade não encontrada.");
                    return;
                }

                frmCadastroCidade = new FrmCadastroCidade();
                frmCadastroCidade.CarregarDados(cidade, true, false);
                frmCadastroCidade.DesbloquearCampos();
                frmCadastroCidade.FormClosed += FrmCadastroCidade_FormClosed;
                frmCadastroCidade.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione uma cidade para editar.");
            }
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewFormaPagamento.SelectedItems[0];
                if (!int.TryParse(selectedItem.Text, out int id))
                {
                    MessageBox.Show("ID inválido.", "Erro");
                    return;
                }

                var cidade = cidadeServices.BuscarPorId(id);
                if (cidade == null)
                {
                    MessageBox.Show("Cidade não encontrada.");
                    return;
                }

                frmCadastroCidade = new FrmCadastroCidade();
                frmCadastroCidade.CarregarDados(cidade, false, true);
                frmCadastroCidade.BloquearCampos();
                frmCadastroCidade.FormClosed += FrmCadastroCidade_FormClosed;
                frmCadastroCidade.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione uma cidade para excluir.");
            }
        }

    }
}
