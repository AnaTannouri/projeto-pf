using ProjetoPF.Dao.Compras;
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
using ProjetoPF.Modelos.Compra;
using ProjetoPF.Servicos.Pessoa;
using System.Linq;
using ProjetoPF.Servicos.Compra;
using ProjetoPF.Modelos;

namespace ProjetoPF.Interfaces.FormConsultas
{
    public partial class FrmConsultaCompra : ProjetoPF.FormConsultas.FrmConsultaPai
    {
        private FrmCadastroCompra frmCadastroCompra;
        private CompraDao compraDao = new CompraDao();
        private BaseServicos<Fornecedor> fornecedorServices =
            new BaseServicos<Fornecedor>(new BaseDao<Fornecedor>("Fornecedores"));
        public bool ModoSomenteLeitura { get; set; } = false;
        public FrmConsultaCompra()
        {
            InitializeComponent();

        }
        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {
            frmCadastroCompra = new FrmCadastroCompra();
            frmCadastroCompra.FormClosed += FrmCadastroCompra_FormClosed;
            frmCadastroCompra.ShowDialog();
        }
        private void FrmCadastroCompra_FormClosed(object sender, FormClosedEventArgs e)
        {
            PopularListView(string.Empty);
            frmCadastroCompra.FormClosed -= FrmCadastroCompra_FormClosed;
        }
        private void FrmConsultaCompra_Load(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                listViewFormaPagamento.View = View.Details;
                listViewFormaPagamento.FullRowSelect = true;
                listViewFormaPagamento.GridLines = true;

                listViewFormaPagamento.Columns.Add("Fornecedor", 380, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Modelo", 100, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Série", 100, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Número", 100, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Emissão", 150, HorizontalAlignment.Center);
                listViewFormaPagamento.Columns.Add("Entrega", 150, HorizontalAlignment.Center);
                listViewFormaPagamento.Columns.Add("Valor Total", 120, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Status", 90, HorizontalAlignment.Center);
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

            btnEditar.Text = "Visualizar Compra";
            btnExcluir.Text = "Cancelar Compra";
        }
        public override void PopularListView(string pesquisa)
        {
            listViewFormaPagamento.Items.Clear();

            List<CompraCabecalho> compras = compraDao.BuscarTodosCabecalho("");

            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                compras = compras.Where(compra =>
    compra.Modelo.ToString().Contains(pesquisa) ||
    (compra.Serie?.IndexOf(pesquisa, StringComparison.OrdinalIgnoreCase) >= 0) ||
    (compra.NumeroNota?.IndexOf(pesquisa, StringComparison.OrdinalIgnoreCase) >= 0) ||
    (fornecedorServices.BuscarPorId(compra.IdFornecedor)?.NomeRazaoSocial?
        .IndexOf(pesquisa, StringComparison.OrdinalIgnoreCase) >= 0) ||
    (pesquisa.Equals("ativo", StringComparison.OrdinalIgnoreCase) && compra.Ativo) ||
    (pesquisa.Equals("cancelado", StringComparison.OrdinalIgnoreCase) && !compra.Ativo)
).ToList();
            }

            if (compras.Count > 0)
            {
                foreach (var compra in compras)
                {
                    var fornecedor = fornecedorServices.BuscarPorId(compra.IdFornecedor);

                    ListViewItem item = new ListViewItem(fornecedor?.NomeRazaoSocial ?? "")
                    {
                        Tag = compra
                    };

                    item.SubItems.Add(compra.Modelo);

                    item.SubItems.Add(compra.Serie);
                    item.SubItems.Add(compra.NumeroNota);
                    item.SubItems.Add(compra.DataEmissao.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(compra.DataEntrega.ToString("dd/MM/yyyy"));
                    item.SubItems.Add(compra.ValorTotal.ToString("C2"));
                    item.SubItems.Add(compra.Ativo ? "Ativo" : "Cancelado");

                    listViewFormaPagamento.Items.Add(item);
                }
            }
            else
            {
                MessageBox.Show("Nenhuma compra encontrada!", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

            if (listViewFormaPagamento.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione uma compra para cancelar.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var itemSelecionado = listViewFormaPagamento.SelectedItems[0];
            var compra = (CompraCabecalho)itemSelecionado.Tag;

            // 🔒 Verifica se já está cancelada
            if (!compra.Ativo)
            {
                MessageBox.Show("Esta compra já está cancelada.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 🔹 Cria a estrutura de chave composta
            var compraStruct = new CompraKey(compra.Modelo, compra.Serie, compra.NumeroNota, compra.IdFornecedor);

            // 🔹 Verifica se há contas pagas associadas
            var contaDao = new ContasAPagarDao();
            bool possuiContaPaga = contaDao.ExisteContaPagaAssociada(
                compra.Modelo,
                compra.Serie,
                compra.NumeroNota,
                compra.IdFornecedor
            );

            if (possuiContaPaga)
            {
                MessageBox.Show(
                    "Não é possível cancelar esta compra, pois há contas a pagar associadas que já foram quitadas.",
                    "Operação Bloqueada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // 🔹 Exibe a compra em modo de visualização (somente leitura)
            using (var frm = new FrmCadastroCompra(compraStruct))
            {
                frm.ModoSomenteLeitura = true;
                frm.ShowDialog();
            }

            // 🔹 Pede confirmação ao usuário
            DialogResult confirmar = MessageBox.Show(
                "Deseja realmente cancelar esta compra?",
                "Confirmar Cancelamento",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmar != DialogResult.Yes)
                return;

            // 🔹 Solicita o motivo
            string motivo = InputBox.Show("Digite o motivo do cancelamento:", "Cancelar Nota");

            if (string.IsNullOrWhiteSpace(motivo))
            {
                MessageBox.Show("Cancelamento abortado. Motivo não informado.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 🔹 Tenta cancelar via serviço
            try
            {
                var compraService = new CompraServicos();
                compraService.CancelarCompra(compraStruct, motivo);

                MessageBox.Show("Compra cancelada com sucesso!",
                                "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 🔄 Atualiza a lista após cancelamento
                PopularListView(string.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao cancelar compra: " + ex.Message,
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

        private void btnEditar_Click_1(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione uma compra para visualizar.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var itemSelecionado = listViewFormaPagamento.SelectedItems[0];

            CompraKey key;
            if (itemSelecionado.Tag is CompraKey k)
                key = k;
            else if (itemSelecionado.Tag is CompraCabecalho cab)
                key = new CompraKey(cab.Modelo, cab.Serie, cab.NumeroNota, cab.IdFornecedor);
            else
            {
                MessageBox.Show("Registro inválido (sem chave da compra).", "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var frm = new FrmCadastroCompra(key)) 
            {
                frm.ModoSomenteLeitura = true;
                frm.ShowDialog(this);
            }
        }
    }
}
