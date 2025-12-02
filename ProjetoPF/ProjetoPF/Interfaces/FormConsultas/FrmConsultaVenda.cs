using ProjetoPF.Dao.Vendas;
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
using ProjetoPF.Modelos.Venda;
using ProjetoPF.Servicos.Venda; 
using System.Linq;
using ProjetoPF.Modelos;
using static ProjetoPF.Interfaces.FormConsultas.FrmConsultaCompra;

namespace ProjetoPF.Interfaces.FormConsultas
{
    public partial class FrmConsultaVenda : ProjetoPF.FormConsultas.FrmConsultaPai
    {
        private FrmCadastroVenda frmCadastroVenda;
        private VendaDao vendaDao = new VendaDao();
        private BaseServicos<Cliente> clienteServices =
            new BaseServicos<Cliente>(new BaseDao<Cliente>("Clientes"));
        public bool ModoSomenteLeitura { get; set; } = false;
        public FrmConsultaVenda()
        {
            InitializeComponent();
        }

      
        private void FrmCadastroVenda_FormClosed(object sender, FormClosedEventArgs e)
        {
            PopularListView(string.Empty);
            frmCadastroVenda.FormClosed -= FrmCadastroVenda_FormClosed;
        }

        private void FrmConsultaVenda_Load(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                listViewFormaPagamento.View = View.Details;
                listViewFormaPagamento.FullRowSelect = true;
                listViewFormaPagamento.GridLines = true;

                listViewFormaPagamento.Columns.Add("Cliente", 250, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Funcionário", 250, HorizontalAlignment.Left); 
                listViewFormaPagamento.Columns.Add("Modelo", 100, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Série", 100, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Número Nota", 100, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Emissão", 170, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Valor Total", 170, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Status", 150, HorizontalAlignment.Center);

            }

            if (ModoSomenteLeitura)
            {
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is TextBox tb) tb.ReadOnly = true;
                    else if (ctrl is ComboBox cb) cb.Enabled = false;
                    else if (ctrl is DateTimePicker dtp) dtp.Enabled = false;
                    else if (ctrl is NumericUpDown nud) nud.Enabled = false;
                }

                btnAdicionar.Enabled = false;
            }

            PopularListView(string.Empty);

            btnEditar.Text = "Visualizar Venda";
            btnExcluir.Text = "Cancelar Venda";
        }
        public override void PopularListView(string pesquisa)
        {
            listViewFormaPagamento.Items.Clear();
            List<VendaCabecalho> vendas = vendaDao.BuscarTodosCabecalho("");
            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                string termo = pesquisa.Trim().ToLower();
                var funcionarioService = new BaseServicos<Funcionario>(
                    new BaseDao<Funcionario>("Funcionarios"));

                vendas = vendas.Where(v =>
                {
                    var cliente = clienteServices.BuscarPorId(v.IdCliente);
                    var funcionario = funcionarioService.BuscarPorId(v.IdFuncionario);

                    string nomeCliente = cliente?.NomeRazaoSocial?.ToLower() ?? "";
                    string nomeFuncionario = funcionario?.NomeRazaoSocial?.ToLower() ?? "";
                    string modelo = v.Modelo?.ToLower() ?? "";
                    string serie = v.Serie?.ToLower() ?? "";
                    string numero = v.NumeroNota?.ToLower() ?? "";

                    return nomeCliente.Contains(termo)
                        || nomeFuncionario.Contains(termo)
                        || modelo.Contains(termo)
                        || serie.Contains(termo)
                        || numero.Contains(termo);
                }).ToList();
            }
            if (vendas.Count > 0)
            {
                var funcionarioService = new BaseServicos<Funcionario>(
                    new BaseDao<Funcionario>("Funcionarios"));

                foreach (var venda in vendas)
                {
                    var cliente = clienteServices.BuscarPorId(venda.IdCliente);
                    var funcionario = funcionarioService.BuscarPorId(venda.IdFuncionario);

                    ListViewItem item = new ListViewItem(cliente?.NomeRazaoSocial ?? "")
                    {
                        Tag = venda
                    };

                    item.SubItems.Add(funcionario?.NomeRazaoSocial ?? "(não informado)");
                    item.SubItems.Add(venda.Modelo);
                    item.SubItems.Add(venda.Serie);
                    item.SubItems.Add(venda.NumeroNota);
                    item.SubItems.Add(venda.DataEmissao.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(venda.ValorTotal.ToString("C2"));
                    item.SubItems.Add(venda.Ativo ? "Ativo" : "Cancelado");

                    listViewFormaPagamento.Items.Add(item);
                }
            }
        }

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione uma venda para visualizar.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var itemSelecionado = listViewFormaPagamento.SelectedItems[0];

            VendaKey key;
            if (itemSelecionado.Tag is VendaKey k)
                key = k;
            else if (itemSelecionado.Tag is VendaCabecalho cab)
                key = new VendaKey(cab.Modelo, cab.Serie, cab.NumeroNota, cab.IdCliente);
            else
            {
                MessageBox.Show("Registro inválido (sem chave da venda).", "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var frm = new FrmCadastroVenda(key))
            {
                frm.ModoSomenteLeitura = true;
                frm.ShowDialog(this);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione uma venda para cancelar.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var itemSelecionado = listViewFormaPagamento.SelectedItems[0];
            var venda = (VendaCabecalho)itemSelecionado.Tag;

            if (!venda.Ativo)
            {
                MessageBox.Show("Esta venda já está cancelada.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var vendaStruct = new VendaKey(venda.Modelo, venda.Serie, venda.NumeroNota, venda.IdCliente);

            var contaDao = new ContasAReceberDao();
            bool possuiContaRecebida = contaDao.ExisteContaRecebidaAssociada(
                venda.Modelo,
                venda.Serie,
                venda.NumeroNota,
                venda.IdCliente
            );

            if (possuiContaRecebida)
            {
                MessageBox.Show(
                    "Não é possível cancelar esta venda, pois há contas a receber associadas que já foram quitadas.",
                    "Operação Bloqueada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            using (var frm = new FrmCadastroVenda(vendaStruct))
            {
                frm.ModoSomenteLeitura = true;
                frm.ShowDialog();
            }

            DialogResult confirmar = MessageBox.Show(
                "Deseja realmente cancelar esta venda?",
                "Confirmar Cancelamento",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmar != DialogResult.Yes)
                return;

            string motivo = string.Empty;

            while (true)
            {
                motivo = InputBox.Show("Digite o motivo do cancelamento (mínimo 10 caracteres):", "Cancelar Nota");

                if (string.IsNullOrWhiteSpace(motivo))
                {
                    MessageBox.Show("O cancelamento foi cancelado, pois o motivo não foi informado.",
                                    "Operação cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (motivo.Trim().Length < 10)
                {
                    MessageBox.Show("O motivo do cancelamento deve conter pelo menos 10 caracteres.",
                                    "Motivo muito curto", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    continue;
                }

                break;
            }

            try
            {
                var vendaService = new VendaServicos();
                vendaService.CancelarVenda(vendaStruct, motivo);

                MessageBox.Show("Venda cancelada com sucesso!",
                                "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PopularListView(string.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cancelar venda: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public static class InputBox
        {
            public static string Show(string prompt, string title)
            {
                Form form = new Form();
                Label label = new Label();
                TextBox textBox = new TextBox();
                Button buttonOk = new Button();
                Button buttonCancel = new Button();

                form.Text = title;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.ClientSize = new Size(420, 160);

                label.Text = prompt;
                label.SetBounds(9, 20, 400, 13);

                textBox.Multiline = true;
                textBox.ScrollBars = ScrollBars.Vertical;
                textBox.SetBounds(12, 40, 390, 60);

                buttonOk.Text = "Confirmar";
                buttonCancel.Text = "Cancelar";
                buttonOk.SetBounds(230, 110, 80, 30);
                buttonCancel.SetBounds(320, 110, 80, 30);

                buttonOk.DialogResult = DialogResult.OK;
                buttonCancel.DialogResult = DialogResult.Cancel;

                form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
                form.AcceptButton = buttonOk;
                form.CancelButton = buttonCancel;

                return form.ShowDialog() == DialogResult.OK ? textBox.Text.Trim() : null;
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string termo = txtPesquisa.Text.Trim();
            PopularListView(termo);
        }

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {
            frmCadastroVenda = new FrmCadastroVenda();
            frmCadastroVenda.FormClosed += FrmCadastroVenda_FormClosed;
            frmCadastroVenda.ShowDialog();
        }
    }
}
