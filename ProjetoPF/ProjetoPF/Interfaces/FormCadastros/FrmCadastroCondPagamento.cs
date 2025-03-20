using ProjetoPF.Dao;
using ProjetoPF.FormCadastros;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroCondPagamento : FrmCadastroPai
    {
        private FrmCadastroFormaPagamento frmCadastroFormaPagamento = new FrmCadastroFormaPagamento();
        private CondicaoPagamento condicaoPagamento = new CondicaoPagamento();
        private BaseServicos<CondicaoPagamento> condicaoPagamentoServices = new BaseServicos<CondicaoPagamento>(new BaseDao<CondicaoPagamento>("CondicaoPagamentos"));
        private BaseServicos<FormaPagamento> formaPagamentoServicos = new BaseServicos<FormaPagamento>(new BaseDao<FormaPagamento>("FormaPagamentos"));
        private List<CondicaoPagamentoParcelas> parcelas = new List<CondicaoPagamentoParcelas>();
        private decimal porcentagemRestante = 100;
        private bool isExclusao = false;

        public FrmCadastroCondPagamento()
        {
            InitializeComponent();
        }

        private bool ValidarEntrada()
        {
            if (string.IsNullOrEmpty(txtCondPagamento.Text.Trim()))
            {
                MessageBox.Show("Informe uma descrição.");
                return false;
            }

            if (!decimal.TryParse(txtJuros.Text.Trim().Replace(",", "."), out _) || !decimal.TryParse(txtMulta.Text.Trim().Replace(",", "."), out _))
            {
                MessageBox.Show("Informe valores válidos para a taxa de juros e multa.");
                return false;
            }

            return true;
        }

        private void AtualizarObjeto()
        {
            condicaoPagamento.Descricao = txtCondPagamento.Text.Trim();
            condicaoPagamento.TaxaJuros = decimal.Parse(txtJuros.Text);
            condicaoPagamento.Multa = decimal.Parse(txtMulta.Text);
            condicaoPagamento.DataCriacao = DateTime.Now;
            condicaoPagamento.DataAtualizacao = DateTime.Now;
        }

        private void LimparCampos()
        {
            txtCondPagamento.Clear();
            txtJuros.Clear();
            txtMulta.Clear();
            condicaoPagamento = new CondicaoPagamento();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            FrmCadastroFormaPagamento frmCadastroForma = new FrmCadastroFormaPagamento();
            frmCadastroForma.FormClosed += (s, args) => CarregarFormasDePagamento();
            frmCadastroForma.ShowDialog();
        }
        public override void CarregarFormasDePagamento()
        {
            try
            {
                var formasDePagamento = formaPagamentoServicos.BuscarTodos();

                if (formasDePagamento != null && formasDePagamento.Any())
                {
                    comboFormPagamento.DataSource = formasDePagamento;
                    comboFormPagamento.DisplayMember = "Descricao";
                    comboFormPagamento.ValueMember = "Id";
                    comboFormPagamento.SelectedIndex = -1;
                }
                else
                {
                    comboFormPagamento.DataSource = null;
                    comboFormPagamento.Items.Clear();
                    MessageBox.Show("Nenhuma forma de pagamento encontrada.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar formas de pagamento: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FrmCadastroCondPagamento_Load(object sender, EventArgs e)
        {
            CarregarFormasDePagamento();
        }

        private void btnGerarParcela_Click(object sender, EventArgs e)
        {
            try
            {
                int numParcela = int.Parse(txtParcela.Text); // Número da parcela
                if (comboFormPagamento.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, selecione uma forma de pagamento.");
                    return; // Encerra a execução se não houver forma de pagamento selecionada.
                }

                string formaPagamento = comboFormPagamento.SelectedItem.ToString(); // Forma de pagamento
                int prazo = int.Parse(txtPrazo.Text); // Prazo

                // Calcular a porcentagem restante da parcela
                decimal porcentagemParcela = (porcentagemRestante > 0) ? (100 - porcentagemRestante) : 0;
                porcentagemRestante -= porcentagemParcela;

                // Criar a nova parcela
                CondicaoPagamentoParcelas novaParcela = new CondicaoPagamentoParcelas
                {
                    NumParcela = numParcela,
                    IdFormaPagamento = 1, // Forma de pagamento (exemplo: 1 para um tipo específico)
                    Prazo = prazo,
                    Porcentagem = porcentagemParcela
                };

                // Adicionar a nova parcela à lista de parcelas
                parcelas.Add(novaParcela);

                // Adicionar a nova parcela na ListView
                ListViewItem lvi = new ListViewItem(novaParcela.NumParcela.ToString());
                lvi.SubItems.Add(formaPagamento); // Forma de pagamento
                lvi.SubItems.Add(novaParcela.Prazo.ToString()); // Prazo
                lvi.SubItems.Add(novaParcela.Porcentagem.ToString("F2") + "%"); // Porcentagem

                // Adicionar o item na ListView
                listView1.Items.Add(lvi);

                // Mostrar mensagem de sucesso
                MessageBox.Show($"Parcela {numParcela} gerada com sucesso!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar parcela: {ex.Message}");
            }
        }
    }
}
