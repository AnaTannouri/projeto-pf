using ProjetoPF.Dao;
using ProjetoPF.Interfaces.FormCadastros;
using ProjetoPF.Modelos.Localizacao;
using ProjetoPF.Servicos;
using ProjetoPF.Servicos.Localizacao;
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
            List<Cidade> cidades = cidadeServices.BuscarTodos(pesquisa);
            List<Estado> estados = estadoServices.BuscarTodos();

            if (cidades != null && cidades.Count > 0)
            {
                foreach (var cidade in cidades)
                {
                    var nomeEstado = estados.FirstOrDefault(e => e.Id == cidade.IdEstado)?.Nome ?? "N/A";
                    ListViewItem item = new ListViewItem(cidade.Id.ToString());
                    item.SubItems.Add(cidade.Nome);
                    item.SubItems.Add(cidade.DDD);
                    item.SubItems.Add(nomeEstado);
                    item.SubItems.Add(cidade.Ativo ? "SIM" : "NÃO");
                    item.SubItems.Add(cidade.DataCriacao.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(cidade.DataAtualizacao.ToString("dd/MM/yyyy"));

                    if (!cidade.Ativo)
                    {
                        item.ForeColor = System.Drawing.Color.Red;
                    }

                    item.Tag = cidade;
                    listViewFormaPagamento.Items.Add(item);
                }
            }
            else if (!string.IsNullOrEmpty(pesquisa))
            {
                MessageBox.Show("Nenhuma cidade encontrada!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmConsultaCidade_Load_1(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                listViewFormaPagamento.Columns.Add("Código", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Cidade", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("DDD", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Nome do Estado", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Ativo", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Data de Criação", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Data de Atualização", -2, HorizontalAlignment.Right);
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
        private void listViewFormaPagamento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.Owner is FrmCadastroCliente frmCadastroCliente)
            {
                if (listViewFormaPagamento.SelectedItems.Count > 0)
                {
                    var itemSelecionado = listViewFormaPagamento.SelectedItems[0];
                    var cidadeSelecionada = (Cidade)itemSelecionado.Tag;

                    if (!cidadeSelecionada.Ativo)
                    {
                        MessageBox.Show("Esta cidade está inativa e não pode ser selecionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    frmCadastroCliente.txtCidade.Text = cidadeSelecionada.Nome;
                    frmCadastroCliente.txtCodigoCidade.Text = cidadeSelecionada.Id.ToString();

                    var estadoServices = new EstadoServicos();
                    var estado = estadoServices.BuscarPorId(cidadeSelecionada.IdEstado);
                    frmCadastroCliente.txtUF.Text = estado != null ? estado.UF : "UF?";

                    frmCadastroCliente.Tag = cidadeSelecionada;
                    this.Close();
                }
            }
            if (this.Owner is FrmCadastroFuncionario frmCadastroFuncionario)
            {
                if (listViewFormaPagamento.SelectedItems.Count > 0)
                {
                    var itemSelecionado = listViewFormaPagamento.SelectedItems[0];
                    var cidadeSelecionada = (Cidade)itemSelecionado.Tag;

                    if (!cidadeSelecionada.Ativo)
                    {
                        MessageBox.Show("Esta cidade está inativa e não pode ser selecionada.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    frmCadastroFuncionario.txtCidadeFunc.Text = cidadeSelecionada.Nome;
                    frmCadastroFuncionario.txtCodigoCidadeFunc.Text = cidadeSelecionada.Id.ToString();

                    var estadoServices = new EstadoServicos();
                    var estado = estadoServices.BuscarPorId(cidadeSelecionada.IdEstado);
                    frmCadastroFuncionario.txtUF.Text = estado != null ? estado.UF : "UF?";

                    frmCadastroFuncionario.Tag = cidadeSelecionada;
                    this.Close();
                }
            }
            else if (this.Owner is FrmCadastroFornecedor frmCadastroFornecedor)
            {
                if (listViewFormaPagamento.SelectedItems.Count > 0)
                {
                    var itemSelecionado = listViewFormaPagamento.SelectedItems[0];
                    var cidadeSelecionada = (Cidade)itemSelecionado.Tag;

                    if (!cidadeSelecionada.Ativo)
                    {
                        MessageBox.Show("Esta cidade está inativa e não pode ser selecionada.", "Cidade Inativa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    frmCadastroFornecedor.txtCidade.Text = cidadeSelecionada.Nome;
                    frmCadastroFornecedor.txtCodigoCidade.Text = cidadeSelecionada.Id.ToString();

                    var estadoServices = new EstadoServicos();
                    var estado = estadoServices.BuscarPorId(cidadeSelecionada.IdEstado);
                    frmCadastroFornecedor.txtUF.Text = estado != null ? estado.UF : "UF?";

                    frmCadastroFornecedor.Tag = cidadeSelecionada;
                    this.Close();
                }
            }
        }
    }
}
