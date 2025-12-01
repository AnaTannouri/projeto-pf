using ProjetoPF.Dao;
using ProjetoPF.Dao.Vendas;
using ProjetoPF.Dao.Pessoa;
using ProjetoPF.Interfaces.FormCadastros;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ProjetoPF.Modelos.Venda;

namespace ProjetoPF.Interfaces.FormConsultas
{
    public partial class FrmConsultaContasAReceber : ProjetoPF.FormConsultas.FrmConsultaPai
    {
        FrmCadastroContaAReceber frmCadastroContaAReceber = new FrmCadastroContaAReceber();
        public FrmConsultaContasAReceber()
        {
            InitializeComponent();
        }

        private void FrmConsultaContasAReceber_Load(object sender, EventArgs e)
        {
            // ListView
            if (listViewFormaPagamento.Columns.Count == 0)
            {
                listViewFormaPagamento.View = View.Details;
                listViewFormaPagamento.FullRowSelect = true;
                listViewFormaPagamento.GridLines = true;

                listViewFormaPagamento.Columns.Add("Núm. Parcela", 100, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Modelo", 100, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Série", 100, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Núm. Nota", 150, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Cód. Cliente", 120, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Cliente", 200, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Forma Pagamento", 180, HorizontalAlignment.Left);
                listViewFormaPagamento.Columns.Add("Valor da Parcela", 120, HorizontalAlignment.Right);
                listViewFormaPagamento.Columns.Add("Data Vencimento", 120, HorizontalAlignment.Center);
                listViewFormaPagamento.Columns.Add("Status", 120, HorizontalAlignment.Center);
            }

            // Filtros
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Em Aberto");
            comboBox1.Items.Add("Recebida");
            comboBox1.Items.Add("Vencida");
            comboBox1.Items.Add("Cancelada");
            comboBox1.Items.Add("Todas");
            comboBox1.SelectedIndex = 4;

            // Ajustes dos botões
            btnAdicionar.Visible = false;
            btnEditar.Text = "Dar Baixa";
            btnExcluir.Text = "Visualizar";

            PopularListView(string.Empty);
        }
        public override void PopularListView(string pesquisa)
        {
            listViewFormaPagamento.Items.Clear();

            var daoContas = new ContasAReceberDao();
            var daoForma = new FormaPagamentoDAO();
            var daoCliente = new ClientesDAO();

            var contas = daoContas.BuscarTodos()
                                  .OrderBy(c => c.DataCriacao)
                                  .ToList();

            // Atualizar status VENCIDA automaticamente
            foreach (var conta in contas)
            {
                if (conta.Ativo &&
                    !string.Equals(conta.Situacao, "Recebida", StringComparison.OrdinalIgnoreCase) &&
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
                    var cliente = daoCliente.BuscarPorId(c.IdCliente);
                    string nomeCliente = cliente?.NomeRazaoSocial?.ToLowerInvariant() ?? "";

                    return c.Modelo.ToString().Contains(pesquisa)
                        || c.Serie.ToLowerInvariant().Contains(pesquisa)
                        || c.NumeroNota.ToLowerInvariant().Contains(pesquisa)
                        || nomeCliente.Contains(pesquisa)
                        || c.IdCliente.ToString().Contains(pesquisa);
                }).ToList();
            }

            foreach (var conta in contas)
            {
                var forma = daoForma.BuscarPorId(conta.IdFormaPagamento);
                var cliente = daoCliente.BuscarPorId(conta.IdCliente);

                string nomeForma = forma?.Descricao ?? "-";
                string nomeCliente = cliente?.NomeRazaoSocial ?? "-";

                ListViewItem item = new ListViewItem(conta.NumeroParcela.ToString());
                item.SubItems.Add(conta.Modelo.ToString());
                item.SubItems.Add(conta.Serie);
                item.SubItems.Add(conta.NumeroNota);
                item.SubItems.Add(conta.IdCliente.ToString());
                item.SubItems.Add(nomeCliente);
                item.SubItems.Add(nomeForma);
                item.SubItems.Add(conta.ValorParcela.ToString("C2"));
                item.SubItems.Add(conta.DataVencimento.ToString("dd/MM/yyyy"));
                item.SubItems.Add(conta.Situacao ?? "Em Aberto");

                item.ForeColor = Color.Black;

                listViewFormaPagamento.Items.Add(item);
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
            string situacao = item.SubItems[9].Text; // Coluna da Situação

            if (situacao.Equals("Recebida", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Esta parcela já foi recebida e não pode ser baixada novamente.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (situacao.Equals("Cancelada", StringComparison.OrdinalIgnoreCase))
            {
                MessageBox.Show("Esta parcela está cancelada e não pode ser baixada.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Dados principais para identificar a parcela
            int numeroParcela = int.Parse(item.SubItems[0].Text);
            string modelo = item.SubItems[1].Text;
            string serie = item.SubItems[2].Text;
            string numeroNota = item.SubItems[3].Text;
            int idCliente = int.Parse(item.SubItems[4].Text);

            var dao = new ContasAReceberDao();

            bool existeAnteriorEmAberto = dao.ExisteParcelaAnteriorEmAberto(
                modelo,
                serie,
                numeroNota,
                idCliente,
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

            var parcelaCompleta = dao.BuscarParcelaCompleta(
     modelo,
     serie,
     numeroNota,
     idCliente,
     numeroParcela
 );

            if (parcelaCompleta == null)
            {
                MessageBox.Show("Não foi possível carregar os detalhes da parcela selecionada.",
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Correções de datas inválidas do SQL Server
            if (parcelaCompleta.DataEmissao < new DateTime(1753, 1, 1))
                parcelaCompleta.DataEmissao = DateTime.Today;

            if (parcelaCompleta.DataVencimento < new DateTime(1753, 1, 1))
                parcelaCompleta.DataVencimento = DateTime.Today.AddDays(1);

            if (parcelaCompleta.DataCriacao < new DateTime(1753, 1, 1))
                parcelaCompleta.DataCriacao = DateTime.Now;

            if (parcelaCompleta.DataAtualizacao < new DateTime(1753, 1, 1))
                parcelaCompleta.DataAtualizacao = DateTime.Now;

            // Abre a tela de baixa de contas a receber
            var frm = new FrmCadastroContaAReceber();
            frm.Owner = this;

            frm.Shown += (s, ev) => frm.CarregarParcela(parcelaCompleta);
            frm.ShowDialog(this);

            // Atualiza a listview após baixa
            PopularListView(string.Empty);
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
            string modelo = item.SubItems[1].Text;
            string serie = item.SubItems[2].Text;
            string numeroNota = item.SubItems[3].Text;
            int idCliente = int.Parse(item.SubItems[4].Text);

            var dao = new ContasAReceberDao();
            var parcela = dao.BuscarParcelaCompleta(modelo, serie, numeroNota, idCliente, numeroParcela);

            if (parcela == null)
            {
                MessageBox.Show("Não foi possível carregar a parcela.",
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string status = parcela.Situacao?.Trim();

            // RECEBIDA OU CANCELADA → VISUALIZAR
            if (status.Equals("Recebida", StringComparison.OrdinalIgnoreCase) ||
                status.Equals("Cancelada", StringComparison.OrdinalIgnoreCase))
            {
                var frmVisu = new FrmCadastroContaAReceber();
                frmVisu.ModoVisualizacao = true;

                frmVisu.Shown += (s, ev) =>
                {
                    frmVisu.CarregarParcela(parcela);
                    frmVisu.BloquearCamposVisualizacao();
                };

                frmVisu.ShowDialog();
                return;
            }

            // EM ABERTO OU VENCIDA → PERGUNTAR SE QUER BAIXAR
            if (status.Equals("Em Aberto", StringComparison.OrdinalIgnoreCase) ||
                status.Equals("Vencida", StringComparison.OrdinalIgnoreCase))
            {
                var resp = MessageBox.Show(
                    "Esta parcela está EM ABERTO.\n\nDeseja registrar a baixa agora?",
                    "Conta em Aberto",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (resp == DialogResult.Yes)
                {
                    var frm = new FrmCadastroContaAReceber();
                    frm.ModoVisualizacao = false;

                    frm.Shown += (s, ev) => frm.CarregarParcela(parcela);
                    frm.ShowDialog();
                }
                else
                {
                    var frmVisu = new FrmCadastroContaAReceber();
                    frmVisu.ModoVisualizacao = true;

                    frmVisu.Shown += (s, ev) =>
                    {
                        frmVisu.CarregarParcela(parcela);
                        frmVisu.BloquearCamposVisualizacao();
                    };

                    frmVisu.ShowDialog();
                }

                return;
            }
        }
        
     
         private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarPorStatus(comboBox1.SelectedItem.ToString());
        }
        private void FiltrarPorStatus(string status)
        {
            listViewFormaPagamento.Items.Clear();

            var daoContas = new ContasAReceberDao();
            var daoForma = new FormaPagamentoDAO();
            var daoCliente = new ClientesDAO();

            var contas = daoContas.BuscarTodos();

            // PADRONIZA TODAS AS SITUAÇÕES
            foreach (var conta in contas)
            {
                // Situação nula vira EM ABERTO
                if (string.IsNullOrWhiteSpace(conta.Situacao))
                    conta.Situacao = "Em aberto";

                // Marca como vencida automaticamente
                if (conta.Ativo &&
                    !conta.Situacao.Equals("Recebida", StringComparison.OrdinalIgnoreCase) &&
                    !conta.Situacao.Equals("Cancelada", StringComparison.OrdinalIgnoreCase) &&
                    conta.DataVencimento < DateTime.Now)
                {
                    if (!conta.Situacao.Equals("Vencida", StringComparison.OrdinalIgnoreCase))
                    {
                        conta.Situacao = "Vencida";
                        daoContas.AtualizarSituacao(conta);
                    }
                }
            }

            // APLICA O FILTRO
            IEnumerable<ContasAReceber> filtradas = contas;

            switch (status)
            {
                case "Em Aberto":
                    filtradas = contas.Where(c =>
                        c.Situacao.Equals("Em aberto", StringComparison.OrdinalIgnoreCase));
                    break;

                case "Vencida":
                    filtradas = contas.Where(c =>
                        c.Situacao.Equals("Vencida", StringComparison.OrdinalIgnoreCase));
                    break;

                case "Recebida":
                    filtradas = contas.Where(c =>
                        c.Situacao.Equals("Recebida", StringComparison.OrdinalIgnoreCase));
                    break;

                case "Cancelada":
                    filtradas = contas.Where(c =>
                        c.Situacao.Equals("Cancelada", StringComparison.OrdinalIgnoreCase));
                    break;

                case "Todas":
                default:
                    filtradas = contas;
                    break;
            }

            // PREENCHER A LISTVIEW
            foreach (var conta in filtradas)
            {
                var forma = daoForma.BuscarPorId(conta.IdFormaPagamento);
                var cliente = daoCliente.BuscarPorId(conta.IdCliente);

                string nomeForma = forma?.Descricao ?? "-";
                string nomeCliente = cliente?.NomeRazaoSocial ?? "-";

                ListViewItem item = new ListViewItem(conta.NumeroParcela.ToString());
                item.SubItems.Add(conta.Modelo.ToString());
                item.SubItems.Add(conta.Serie);
                item.SubItems.Add(conta.NumeroNota);
                item.SubItems.Add(conta.IdCliente.ToString());
                item.SubItems.Add(nomeCliente);
                item.SubItems.Add(nomeForma);
                item.SubItems.Add(conta.ValorParcela.ToString("C2"));
                item.SubItems.Add(conta.DataVencimento.ToString("dd/MM/yyyy"));
                item.SubItems.Add(conta.Situacao);

                listViewFormaPagamento.Items.Add(item);
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            PopularListView(txtPesquisa.Text.Trim());
        }
    }
}
