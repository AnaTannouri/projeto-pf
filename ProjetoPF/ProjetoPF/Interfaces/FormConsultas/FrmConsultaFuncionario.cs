using ProjetoPF.Dao;
using ProjetoPF.Interfaces.FormCadastros;
using ProjetoPF.Modelos.Pessoa;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormConsultas
{
    public partial class FrmConsultaFuncionario : ProjetoPF.FormConsultas.FrmConsultaPai
    {
        private FrmCadastroFuncionario frmCadastroFuncionario = new FrmCadastroFuncionario();
        private BaseServicos<Funcionario> funcionarioServices = new BaseServicos<Funcionario>(new BaseDao<Funcionario>("Funcionarios"));
        public Funcionario FuncionarioSelecionado { get; private set; }
        public bool ModoSelecao { get; set; } = false;
        public FrmConsultaFuncionario()
        {
            InitializeComponent();
            this.listViewFormaPagamento.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listViewFormaPagamento_MouseDoubleClick);
        }

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {
            frmCadastroFuncionario = new FrmCadastroFuncionario();
            frmCadastroFuncionario.LimparFormulario();
            frmCadastroFuncionario.DesbloquearCampos();
            frmCadastroFuncionario.FormClosed += FrmCadastroFuncionario_FormClosed;
            frmCadastroFuncionario.ShowDialog();
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
            List<Funcionario> funcionarios = funcionarioServices.BuscarTodos(pesquisa);

            if (funcionarios != null && funcionarios.Count > 0)
            {
                foreach (var funcionario in funcionarios)
                {
                    ListViewItem item = new ListViewItem(funcionario.Id.ToString())
                    {
                        SubItems =
                        {
                            funcionario.Matricula,
                            funcionario.NomeRazaoSocial,
                            funcionario.Telefone,
                            funcionario.Ativo ? "SIM" : "NÃO",
                            funcionario.DataCriacao.ToString("dd/MM/yyyy"),
                            funcionario.DataAtualizacao.ToString("dd/MM/yyyy")
                        }
                    };
                    if (!funcionario.Ativo)
                        item.ForeColor = Color.Red;

                    listViewFormaPagamento.Items.Add(item);
                }
            }
            else if (!string.IsNullOrEmpty(pesquisa))
            {
                MessageBox.Show("Nenhum resultado encontrado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FrmConsultaFuncionario_Load(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                listViewFormaPagamento.Columns.Add("Código", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Matrícula", -2, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Funcionario", -2, HorizontalAlignment.Left);
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

                Funcionario funcionarioSelecionado = funcionarioServices.BuscarPorId(id);

                if (funcionarioSelecionado == null)
                {
                    MessageBox.Show("Funcionário não encontrado.", "Erro");
                    return;
                }

                frmCadastroFuncionario = new FrmCadastroFuncionario();
                frmCadastroFuncionario.CarregarDados(funcionarioSelecionado, true, false);
                frmCadastroFuncionario.FormClosed += FrmCadastroFuncionario_FormClosed;
                frmCadastroFuncionario.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione um funcionário para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void FrmCadastroFuncionario_FormClosed(object sender, FormClosedEventArgs e)
        {
            PopularListView(string.Empty);
            frmCadastroFuncionario.FormClosed -= FrmCadastroFuncionario_FormClosed;
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

                Funcionario funcionarioSelecionado = funcionarioServices.BuscarPorId(id);

                if (funcionarioSelecionado == null)
                {
                    MessageBox.Show("Funcionário não encontrado.", "Erro");
                    return;
                }

                FrmCadastroFuncionario frmCadastroFuncionario = new FrmCadastroFuncionario();
                frmCadastroFuncionario.CarregarDados(funcionarioSelecionado, false, true); // exclusão = true
                frmCadastroFuncionario.FormClosed += FrmCadastroFuncionario_FormClosed;
                frmCadastroFuncionario.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecione um funcionário para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void listViewFormaPagamento_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!ModoSelecao)
                return;

            if (listViewFormaPagamento.SelectedItems.Count > 0)
            {
                var itemSelecionado = listViewFormaPagamento.SelectedItems[0];
                Funcionario funcionarioSelecionado = funcionarioServices.BuscarPorId(int.Parse(itemSelecionado.Text));

                if (funcionarioSelecionado == null)
                {
                    MessageBox.Show("Funcionário não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                this.FuncionarioSelecionado = funcionarioSelecionado;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}
