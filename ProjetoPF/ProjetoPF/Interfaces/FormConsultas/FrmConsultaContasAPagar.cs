using ProjetoPF.Dao;
using ProjetoPF.Dao.Compras;
using ProjetoPF.Dao.Pessoa;
using ProjetoPF.Interfaces.FormCadastros;
using ProjetoPF.Modelos.Compra;
using ProjetoPF.Servicos.Compra;
using ProjetoPF.Servicos.Pessoa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static ProjetoPF.Interfaces.FormConsultas.FrmConsultaCompra;

namespace ProjetoPF.Interfaces.FormConsultas
{
    public partial class FrmConsultaContasAPagar : ProjetoPF.FormConsultas.FrmConsultaPai
    {
        FrmCadastroContaAPagar frmCadastroContaAPagar = new FrmCadastroContaAPagar();
        FrmContasAPagarBaixa frmContasAPagarBaixa = new FrmContasAPagarBaixa();

        public bool ModoVisualizacao { get; set; } = false;
        public FrmConsultaContasAPagar()
        {
            InitializeComponent();
        }

        private void FrmConsultaContasAPagar_Load(object sender, EventArgs e)
        {
            if (ModoVisualizacao)
                return;
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                listViewFormaPagamento.View = View.Details;
                listViewFormaPagamento.FullRowSelect = true;
                listViewFormaPagamento.GridLines = true;

                listViewFormaPagamento.Columns.Add("Núm. Parcela", 100, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Modelo", 100, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Série", 100, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Núm. Nota", 150, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Cód. Fornecedor", 120, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Fornecedor", 200, HorizontalAlignment.Center);
                listViewFormaPagamento.Columns.Add("Forma Pagamento", 180, HorizontalAlignment.Center);
                listViewFormaPagamento.Columns.Add("Valor da Parcela", 100, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Data Vencimento", 100, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Status", 120, HorizontalAlignment.Center);
            }
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Em aberto");
            comboBox1.Items.Add("Paga");
            comboBox1.Items.Add("Vencida");
            comboBox1.Items.Add("Cancelada");
            comboBox1.Items.Add("Todas");
            comboBox1.SelectedIndex = 4;

            btnAdicionar.Text = "Incluir";
            btnEditar.Text = "Dar baixa";
            btnExcluir.Text = "Vizualizar";
            PopularListView(string.Empty);
        }
        public override void PopularListView(string pesquisa)
        {
            listViewFormaPagamento.Items.Clear();

            var daoContas = new ContasAPagarDao();
            var daoForma = new FormaPagamentoDAO();
            var daoFornecedor = new FornecedorDAO();

            var contas = daoContas.BuscarTodos()
                                  .OrderBy(c => c.DataCriacao)
                                  .ToList();

            foreach (var conta in contas)
            {
                if (conta.Ativo &&
                    !string.Equals(conta.Situacao, "Paga", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(conta.Situacao, "Cancelada", StringComparison.OrdinalIgnoreCase) &&
                    conta.DataVencimento < DateTime.Now)
                {
                    if (!string.Equals(conta.Situacao, "Vencida", StringComparison.OrdinalIgnoreCase))
                    {
                        conta.Situacao = "Vencida";
                        daoContas.AtualizarSituacao(conta);
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(pesquisa))
            {
                pesquisa = pesquisa.Trim().ToLowerInvariant();

                contas = contas.Where(c =>
                {
                    var fornecedor = daoFornecedor.BuscarPorId(c.IdFornecedor);
                    string nomeFornecedor = fornecedor?.NomeRazaoSocial?.ToLowerInvariant() ?? "";

                    return c.Modelo.ToString().Contains(pesquisa)
                        || c.Serie.ToLowerInvariant().Contains(pesquisa)
                        || c.NumeroNota.ToLowerInvariant().Contains(pesquisa)
                        || nomeFornecedor.Contains(pesquisa)
                        || c.IdFornecedor.ToString().Contains(pesquisa);
                }).ToList();
            }

            foreach (var conta in contas)
            {
                var forma = daoForma.BuscarPorId(conta.IdFormaPagamento);
                var fornecedor = daoFornecedor.BuscarPorId(conta.IdFornecedor);

                string nomeForma = forma?.Descricao ?? "-";
                string nomeFornecedor = fornecedor?.NomeRazaoSocial ?? "-";

                ListViewItem item = new ListViewItem(conta.NumeroParcela.ToString());
                item.SubItems.Add(conta.Modelo.ToString());
                item.SubItems.Add(conta.Serie.ToString());
                item.SubItems.Add(conta.NumeroNota.ToString());
                item.SubItems.Add(conta.IdFornecedor.ToString());
                item.SubItems.Add(nomeFornecedor);
                item.SubItems.Add(nomeForma);
                item.SubItems.Add(conta.ValorParcela.ToString("C2"));
                item.SubItems.Add(conta.DataVencimento.ToString("dd/MM/yyyy"));
                item.SubItems.Add(conta.Situacao ?? "Em aberto");

                item.ForeColor = Color.Black;
                listViewFormaPagamento.Items.Add(item);
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filtro = comboBox1.SelectedItem.ToString();
            FiltrarPorStatus(filtro);
        }
        private void FiltrarPorStatus(string status)
        {
            listViewFormaPagamento.Items.Clear();

            var daoContas = new ContasAPagarDao();
            var daoForma = new FormaPagamentoDAO();
            var daoFornecedor = new FornecedorDAO();

            var contas = daoContas.BuscarTodos();

            foreach (var conta in contas)
            {
                if (conta.Ativo &&
                    !string.Equals(conta.Situacao, "Paga", StringComparison.OrdinalIgnoreCase) &&
                    !string.Equals(conta.Situacao, "Cancelada", StringComparison.OrdinalIgnoreCase) &&
                    conta.DataVencimento < DateTime.Now)
                {
                    if (!string.Equals(conta.Situacao, "Vencida", StringComparison.OrdinalIgnoreCase))
                    {
                        conta.Situacao = "Vencida";
                        daoContas.AtualizarSituacao(conta);
                    }
                }
            }

            IEnumerable<ContasAPagar> filtradas = contas;

            switch (status)
            {
                case "Em aberto":
                    filtradas = contas.Where(c =>
                        string.Equals(c.Situacao, "Em aberto", StringComparison.OrdinalIgnoreCase));
                    break;
                case "Vencida":
                    filtradas = contas.Where(c =>
                        string.Equals(c.Situacao, "Vencida", StringComparison.OrdinalIgnoreCase));
                    break;
                case "Paga":
                    filtradas = contas.Where(c =>
                        string.Equals(c.Situacao, "Paga", StringComparison.OrdinalIgnoreCase));
                    break;
                case "Cancelada":
                    filtradas = contas.Where(c =>
                        string.Equals(c.Situacao, "Cancelada", StringComparison.OrdinalIgnoreCase));
                    break;
                case "Todas":
                default:
                    filtradas = contas;
                    break;
            }

            foreach (var conta in filtradas)
            {
                var forma = daoForma.BuscarPorId(conta.IdFormaPagamento);
                var fornecedor = daoFornecedor.BuscarPorId(conta.IdFornecedor);

                string nomeForma = forma?.Descricao ?? "-";
                string nomeFornecedor = fornecedor?.NomeRazaoSocial ?? "-";

                ListViewItem item = new ListViewItem(conta.NumeroParcela.ToString());
                item.SubItems.Add(conta.Modelo.ToString());
                item.SubItems.Add(conta.Serie.ToString());
                item.SubItems.Add(conta.NumeroNota);
                item.SubItems.Add(conta.IdFornecedor.ToString());
                item.SubItems.Add(nomeFornecedor);
                item.SubItems.Add(nomeForma);
                item.SubItems.Add(conta.ValorParcela.ToString("C2"));
                item.SubItems.Add(conta.DataVencimento.ToString("dd/MM/yyyy"));
                item.SubItems.Add(conta.Situacao ?? "Em aberto");

                item.ForeColor = Color.Black;

                listViewFormaPagamento.Items.Add(item);
            }

            if (listViewFormaPagamento.Items.Count > 0)
            {
                var ultimoItem = listViewFormaPagamento.Items[listViewFormaPagamento.Items.Count - 1];
                ultimoItem.EnsureVisible();
                ultimoItem.Selected = true;
            }
        }

        private void btnAdicionar_Click_1(object sender, EventArgs e)
        {
            using (var frm = new FrmCadastroContaAPagar())
            {
                frm.Owner = this;
                frm.ShowDialog();
            }
        }


        private void btnEditar_Click_1(object sender, EventArgs e)
        {

            if (listViewFormaPagamento.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione uma parcela antes de dar baixa.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var item = listViewFormaPagamento.SelectedItems[0];
            string situacao = item.SubItems[9].Text; 

            if (situacao.Equals("Paga", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Esta parcela já foi paga e não pode ser baixada novamente.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (situacao.Equals("Cancelada", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Esta parcela está cancelada e não pode ser baixada.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int numeroParcela = int.Parse(item.SubItems[0].Text);
            int modelo = int.Parse(item.SubItems[1].Text);
            string serie = item.SubItems[2].Text;
            string numeroNota = item.SubItems[3].Text;
            int idFornecedor = int.Parse(item.SubItems[4].Text);

            var dao = new ContasAPagarDao();

            bool existeAnteriorEmAberto = dao.ExisteParcelaAnteriorEmAberto(
                modelo.ToString(),
                serie,
                numeroNota,
                idFornecedor,
                numeroParcela
            );

            if (existeAnteriorEmAberto)
            {
                MessageBox.Show(
                    $"Não é possível dar baixa na parcela nº {numeroParcela}, pois há parcelas anteriores ainda em aberto.",
                    "Operação não permitida",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            var parcelaCompleta = dao.BuscarParcelaCompleta(modelo, serie, numeroNota, idFornecedor, numeroParcela);

            if (parcelaCompleta == null)
            {
                MessageBox.Show("Não foi possível carregar os detalhes da parcela selecionada.",
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (parcelaCompleta.DataEmissao < new DateTime(1753, 1, 1))
                parcelaCompleta.DataEmissao = DateTime.Today;

            if (parcelaCompleta.DataVencimento < new DateTime(1753, 1, 1))
                parcelaCompleta.DataVencimento = DateTime.Today.AddDays(1);

            if (parcelaCompleta.DataCriacao < new DateTime(1753, 1, 1))
                parcelaCompleta.DataCriacao = DateTime.Now;

            if (parcelaCompleta.DataAtualizacao < new DateTime(1753, 1, 1))
                parcelaCompleta.DataAtualizacao = DateTime.Now;

            var frm = new FrmContasAPagarBaixa();
            frm.Owner = this;
            frm.Shown += (s, ev) => frm.CarregarParcela(parcelaCompleta);
            frm.ShowDialog(this);

            PopularListView(string.Empty);

        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string termo = txtPesquisa.Text.Trim();
            PopularListView(termo);
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {

            if (listViewFormaPagamento.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione uma parcela para visualizar.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var item = listViewFormaPagamento.SelectedItems[0];

            int numeroParcela = int.Parse(item.SubItems[0].Text);
            int modelo = int.Parse(item.SubItems[1].Text);
            string serie = item.SubItems[2].Text;
            string numeroNota = item.SubItems[3].Text;
            int idFornecedor = int.Parse(item.SubItems[4].Text);
            string situacao = item.SubItems[9].Text;

            var dao = new ContasAPagarDao();
            var parcela = dao.BuscarParcelaCompleta(modelo, serie, numeroNota, idFornecedor, numeroParcela);

            if (parcela == null)
            {
                MessageBox.Show("Não foi possível carregar os dados da parcela selecionada.",
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (situacao.Equals("Paga", StringComparison.OrdinalIgnoreCase))
            {
                var frm = new FrmContasAPagarBaixa();
                frm.ModoVisualizacao = true; 
                frm.Shown += (s, ev) =>
                {
                    frm.CarregarParcela(parcela);
                    frm.CarregarParcela(parcela);
                    frm.BloquearCamposVisualizar();
                };
                frm.ShowDialog();
            }
            else
            {
                var frm = new FrmCadastroContaAPagar();
                frm.ModoVisualizacao = true;
                frm.Shown += (s, ev) =>
                {
                    frm.CarregarConta(parcela);
                    frm.BloquearCamposVisualizar();
                };
                frm.ShowDialog();
            }
        }

        private void btnCancelarConta_Click(object sender, EventArgs e)
        {
            if (listViewFormaPagamento.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione uma conta para cancelar.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var item = listViewFormaPagamento.SelectedItems[0];

            int numeroParcela = int.Parse(item.SubItems[0].Text);
            int modelo = int.Parse(item.SubItems[1].Text);
            string serie = item.SubItems[2].Text;
            string numeroNota = item.SubItems[3].Text;
            int idFornecedor = int.Parse(item.SubItems[4].Text);

            var dao = new ContasAPagarDao();
            var conta = dao.BuscarParcelaCompleta(modelo, serie, numeroNota, idFornecedor, numeroParcela);

            if (conta == null)
            {
                MessageBox.Show("Conta não encontrada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (conta.Situacao == "Paga")
            {
                MessageBox.Show("Não é possível cancelar uma conta que já foi paga.",
                                "Operação inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (conta.Situacao == "Cancelada")
            {
                MessageBox.Show("Esta conta já está cancelada.",
                                "Operação inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool veioDeCompra = dao.ContaVemDeCompra(conta.Modelo.ToString(), conta.Serie, conta.NumeroNota, conta.IdFornecedor);
            if (veioDeCompra && conta.Situacao != "Paga")
            {
                MessageBox.Show("Não é possível cancelar uma conta gerada a partir de uma compra.",
                                "Operação inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var frmVisualizar = new FrmCadastroContaAPagar();
            frmVisualizar.ModoVisualizacao = true;
            frmVisualizar.Shown += (s, ev) =>
            {
                frmVisualizar.CarregarConta(conta);
                frmVisualizar.BloquearCamposVisualizar();
            };
            frmVisualizar.ShowDialog();

            DialogResult confirmar = MessageBox.Show(
                "Deseja realmente cancelar esta conta?",
                "Confirmar Cancelamento",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmar != DialogResult.Yes)
                return;

            string motivo = string.Empty;

            while (true)
            {
                motivo = InputBox.Show("Digite o motivo do cancelamento (mínimo 10 caracteres):", "Cancelar Conta");

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

            bool sucesso = dao.CancelarConta(modelo, serie, numeroNota, idFornecedor, numeroParcela, motivo);

            if (sucesso)
            {
                MessageBox.Show("Conta cancelada com sucesso!",
                                "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                PopularListView(string.Empty);
            }
            else
            {
                MessageBox.Show("Não foi possível cancelar a conta. Verifique seu status.",
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
           
    }
}
