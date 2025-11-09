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
    public partial class FrmConsultaEstado : ProjetoPF.FormConsultas.FrmConsultaPai
    {
        private FrmCadastroEstado frmCadastroEstado;
        private BaseServicos<Estado> estadoServices = new BaseServicos<Estado>(new BaseDao<Estado>("Estados"));
        private BaseServicos<Pais> paisServices = new BaseServicos<Pais>(new BaseDao<Pais>("Paises"));

        public FrmConsultaEstado()
        {
            InitializeComponent();
            frmCadastroEstado = new FrmCadastroEstado();
            this.listViewFormaPagamento.MouseDoubleClick += new MouseEventHandler(this.listViewFormaPagamento_MouseDoubleClick);
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
            List<Estado> estados = estadoServices.BuscarTodos(pesquisa);
            List<Pais> paises = paisServices.BuscarTodos();

            if (estados != null && estados.Count > 0)
            {
                foreach (var estado in estados)
                {
                    var nomePais = paises.FirstOrDefault(p => p.Id == estado.IdPais)?.Nome ?? "N/A";

                    ListViewItem item = new ListViewItem(estado.Id.ToString());
                    item.SubItems.Add(estado.Nome);
                    item.SubItems.Add(estado.UF);
                    item.SubItems.Add(nomePais);
                    item.SubItems.Add(estado.Ativo ? "SIM" : "NÃO");
                    item.SubItems.Add(estado.DataCriacao.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(estado.DataAtualizacao.ToString("dd/MM/yyyy"));  



                    if (!estado.Ativo)
                    {
                        item.ForeColor = System.Drawing.Color.Red;
                    }
                    item.Tag = estado;
                    listViewFormaPagamento.Items.Add(item);
                }
            }
            else if (!string.IsNullOrEmpty(pesquisa))
            {
                MessageBox.Show("Nenhum resultado encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmCadastroEstado_FormClosed(object sender, FormClosedEventArgs e)
        {
            PopularListView(string.Empty);
            frmCadastroEstado.FormClosed -= FrmCadastroEstado_FormClosed;
        }

        private void btnExcluir_Click_1(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewFormaPagamento.SelectedItems[0];

                if (!int.TryParse(selectedItem.Text, out int id))
                {
                    MessageBox.Show("ID inválido selecionado.", "Erro");
                    return;
                }

                Estado estadoSelecionado = estadoServices.BuscarPorId(id);

                if (estadoSelecionado == null)
                {
                    MessageBox.Show("Estado não encontrado.", "Erro");
                    return;
                }

                frmCadastroEstado = new FrmCadastroEstado();
                frmCadastroEstado.CarregarDados(estadoSelecionado, false, true);
                frmCadastroEstado.BloquearCampos();
                frmCadastroEstado.FormClosed += FrmCadastroEstado_FormClosed;
                frmCadastroEstado.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione um estado para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmConsultaEstado_Load_1(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                listViewFormaPagamento.Columns.Add("Código", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Estado", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("UF", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Nome do País", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Ativo", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Data de Criação", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Data de Atualização", -2, HorizontalAlignment.Right);
            }

            AjustarLarguraColunas();
            PopularListView(string.Empty);
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listViewFormaPagamento.SelectedItems[0];

                if (!int.TryParse(selectedItem.Text, out int id))
                {
                    MessageBox.Show("ID inválido selecionado.", "Erro");
                    return;
                }

                Estado estadoSelecionado = estadoServices.BuscarPorId(id);

                if (estadoSelecionado == null)
                {
                    MessageBox.Show("Estado não encontrado.", "Erro");
                    return;
                }

                frmCadastroEstado = new FrmCadastroEstado();
                frmCadastroEstado.CarregarDados(estadoSelecionado, true, false);
                frmCadastroEstado.FormClosed += FrmCadastroEstado_FormClosed;
                frmCadastroEstado.ShowDialog();

            }
            else
            {
                MessageBox.Show("Selecione um estado para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {
            frmCadastroEstado = new FrmCadastroEstado();
            frmCadastroEstado.LimparCampos();
            frmCadastroEstado.DesbloquearCampos();
            frmCadastroEstado.FormClosed += FrmCadastroEstado_FormClosed;
            frmCadastroEstado.ShowDialog();
        }

        private void listViewFormaPagamento_MouseDoubleClick(object sender, EventArgs e)
        {
            if (!(this.Owner is FrmCadastroCidade frmCadastroCidade))
                return;

            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                var itemSelecionado = listViewFormaPagamento.SelectedItems[0];
                var estadoSelecionado = (Estado)itemSelecionado.Tag;

                if (!estadoSelecionado.Ativo)
                {
                    MessageBox.Show("Este estado está inativo. Selecione um estado ativo.", "Estado inativo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                frmCadastroCidade.txtEstado.Text = estadoSelecionado.Nome;
                frmCadastroCidade.txtCodEstado.Text = estadoSelecionado.Id.ToString();
                frmCadastroCidade.Tag = estadoSelecionado;

                this.Close();
            }                   
        }
    }
}
