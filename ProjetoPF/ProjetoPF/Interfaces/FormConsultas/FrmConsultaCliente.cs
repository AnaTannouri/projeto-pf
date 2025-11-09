using ProjetoPF.Dao;
using ProjetoPF.Interfaces.FormCadastros;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Modelos.Pessoa;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormConsultas
{
    public partial class FrmConsultaCliente : ProjetoPF.FormConsultas.FrmConsultaPai
    {
        private FrmCadastroCliente frmCadastroCliente = new FrmCadastroCliente();
        private BaseServicos<Cliente> clienteServices = new BaseServicos<Cliente>(new BaseDao<Cliente>("Clientes"));
        public Cliente ClienteSelecionado { get; private set; }
        public bool ModoSelecao { get; set; } = false;


        public FrmConsultaCliente()
        {
            InitializeComponent();
            this.listViewFormaPagamento.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewFormaPagamento_MouseDoubleClick);

        }

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {
            frmCadastroCliente = new FrmCadastroCliente();
            frmCadastroCliente.LimparFormulario();
            frmCadastroCliente.DesbloquearCampos();
            frmCadastroCliente.FormClosed += FrmCadastroCliente_FormClosed;
            frmCadastroCliente.ShowDialog();
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
            List<Cliente> clientes = clienteServices.BuscarTodos(pesquisa);

            if (clientes != null && clientes.Count > 0)
            {
                foreach (var cliente in clientes)
                {
                    ListViewItem item = new ListViewItem(cliente.Id.ToString())
                    {
                        SubItems =
    {
        cliente.TipoPessoa,
        cliente.NomeRazaoSocial,
        cliente.Telefone,
        cliente.Ativo ? "SIM" : "NÃO",
        cliente.DataCriacao.ToString("dd/MM/yyyy"),
        cliente.DataAtualizacao.ToString("dd/MM/yyyy")
    }
                    };

                    if (!cliente.Ativo)
                        item.ForeColor = System.Drawing.Color.Red;

                    listViewFormaPagamento.Items.Add(item);
                }
            }
            else if (!string.IsNullOrEmpty(pesquisa))
            {
                MessageBox.Show("Nenhum resultado encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmConsultaCliente_Load(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                listViewFormaPagamento.Columns.Add("Código", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Tipo Pessoa", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Nome/Razão Social", -2, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Telefone", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Ativo", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Criação", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Atualização", -2, HorizontalAlignment.Right);
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

                Cliente clienteSelecionado = clienteServices.BuscarPorId(id);

                if (clienteSelecionado == null)
                {
                    MessageBox.Show("Cliente não encontrado.", "Erro");
                    return;
                }

                frmCadastroCliente = new FrmCadastroCliente();
                frmCadastroCliente.CarregarDados(clienteSelecionado, true, false);
                frmCadastroCliente.FormClosed += FrmCadastroCliente_FormClosed;
                frmCadastroCliente.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione um cliente para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmCadastroCliente_FormClosed(object sender, FormClosedEventArgs e)
        {
            PopularListView(string.Empty);
            frmCadastroCliente.FormClosed -= FrmCadastroCliente_FormClosed;
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

                Cliente clienteSelecionado = clienteServices.BuscarPorId(id);

                if (clienteSelecionado == null)
                {
                    MessageBox.Show("Cliente não encontrado.", "Erro");
                    return;
                }

                FrmCadastroCliente frmCadastroCliente = new FrmCadastroCliente();
                frmCadastroCliente.CarregarDados(clienteSelecionado, false, true); 
                frmCadastroCliente.FormClosed += FrmCadastroCliente_FormClosed;
                frmCadastroCliente.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione um cliente para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void listViewFormaPagamento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!ModoSelecao)
                return;

            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                var itemSelecionado = listViewFormaPagamento.SelectedItems[0];
                Cliente clienteSelecionado = clienteServices.BuscarPorId(int.Parse(itemSelecionado.Text));

                if (clienteSelecionado == null)
                {
                    MessageBox.Show("Cliente não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!clienteSelecionado.Ativo)
                {
                    MessageBox.Show("Este cliente está inativo e não pode ser selecionado.",
                                    "Cliente inativo",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    return;
                }

                this.ClienteSelecionado = clienteSelecionado;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
