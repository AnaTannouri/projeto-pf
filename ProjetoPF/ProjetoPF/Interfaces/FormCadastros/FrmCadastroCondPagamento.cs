using ProjetoPF.Dao;
using ProjetoPF.FormCadastros;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroCondPagamento : FrmCadastroPai
    {
        private FrmCadastroFormaPagamento frmCadastroFormaPagamento = new FrmCadastroFormaPagamento();
        private CondicaoPagamento condicaoPagamento = new CondicaoPagamento();
        private BaseServicos<CondicaoPagamento> condicaoPagamentoServices = new BaseServicos<CondicaoPagamento>(new BaseDao<CondicaoPagamento>("CondicaoPagamentos"));
        private BaseServicos<FormaPagamento> formaPagamentoServicos = new BaseServicos<FormaPagamento>(new BaseDao<FormaPagamento>("FormaPagamentos"));
        private BaseServicos<CondicaoPagamentoParcelas> condicaoPagamentoParcelasServicos = new BaseServicos<CondicaoPagamentoParcelas>(new BaseDao<CondicaoPagamentoParcelas>("CondicaoPagamentoParcelas"));
        private List<CondicaoPagamentoParcelas> parcelas = new List<CondicaoPagamentoParcelas>();
        private decimal porcentagemRestante = 100;
        private bool isExcluindo = false;
        private bool isEditando = false;
        private int proximoNumeroParcela = 1;


        public FrmCadastroCondPagamento()
        {
            InitializeComponent();
            comboFormPagamento.SelectedIndexChanged += comboFormPagamento_SelectedIndexChanged;
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
            txtCodigo.Clear();
            txtCondPagamento.Clear();
            txtJuros.Clear();
            txtMulta.Clear();
            txtPrazo.Clear();
            txtParcela.Clear();
            txtPorcentagem.Clear();
            txtRestante.Text = "100";
            parcelas.Clear();
            listView1.Items.Clear();
            porcentagemRestante = 100;
            proximoNumeroParcela = 1;
            txtParcela.Text = "1";
            condicaoPagamento = new CondicaoPagamento();
            isEditando = false;
            isExcluindo = false;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            FrmCadastroFormaPagamento frmCadastroForma = new FrmCadastroFormaPagamento();
            frmCadastroForma.FormClosed += (s, args) =>
            {
                CarregarFormasDePagamento();
                var formas = comboFormPagamento.DataSource as List<FormaPagamento>;
                if (formas != null && formas.Any())
                {
                    comboFormPagamento.SelectedItem = formas.Last();
                }
            }; ;
            frmCadastroForma.ShowDialog();
        }
        private void comboFormPagamento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboFormPagamento.SelectedItem is FormaPagamento forma)
            {
                txtCod.Text = forma.Id.ToString();
            }
            else
            {
                txtCod.Clear();
            }
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
            listView1.View = View.Details;
            listView1.Columns.Add("Número Parcela", 100);
            listView1.Columns.Add("Forma Pagamento", 150);
            listView1.Columns.Add("Prazo", 80);
            listView1.Columns.Add("%", 80);
            txtParcela.Text = proximoNumeroParcela.ToString();

            listView1.FullRowSelect = true;

            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";
        }

        private void btnGerarParcela_Click(object sender, EventArgs e)
        {
            try
            {
                int numParcela = proximoNumeroParcela;

                if (comboFormPagamento.SelectedItem == null)
                {
                    MessageBox.Show("Por favor, selecione uma forma de pagamento.");
                    return;
                }

                FormaPagamento pagamentoSelecionado = (FormaPagamento)comboFormPagamento.SelectedItem;
                string formaPagamento = pagamentoSelecionado.Descricao;
                int prazo = int.Parse(txtPrazo.Text);

                if (!decimal.TryParse(txtPorcentagem.Text.Replace(",", "."), out decimal porcentagemParcela))
                {
                    MessageBox.Show("Informe uma porcentagem válida.");
                    return;
                }

                if (porcentagemParcela > porcentagemRestante)
                {
                    MessageBox.Show($"A porcentagem informada excede o restante disponível ({porcentagemRestante:F2}%).");
                    txtParcela.Clear();
                    txtPrazo.Clear();
                    txtPorcentagem.Clear();
                    comboFormPagamento.SelectedIndex = -1;
                    return;

                }

                porcentagemRestante -= porcentagemParcela;
                txtRestante.Text = porcentagemRestante.ToString("F2");

                CondicaoPagamentoParcelas novaParcela = new CondicaoPagamentoParcelas
                {
                    NumParcela = numParcela,
                    IdFormaPagamento = pagamentoSelecionado.Id,
                    Prazo = prazo,
                    Porcentagem = porcentagemParcela
                };

                parcelas.Add(novaParcela);

                ListViewItem lvi = new ListViewItem(novaParcela.NumParcela.ToString());
                lvi.SubItems.Add(formaPagamento);
                lvi.SubItems.Add(novaParcela.Prazo.ToString());
                lvi.SubItems.Add(novaParcela.Porcentagem.ToString("F2") + "%");
                listView1.Items.Add(lvi);

                MessageBox.Show($"Parcela {numParcela} gerada com sucesso!");

                proximoNumeroParcela++;

                if (porcentagemRestante > 0)
                {
                    txtParcela.Text = proximoNumeroParcela.ToString();
                }
                else
                {
                    txtParcela.Clear();
                }

                txtPrazo.Clear();
                txtPorcentagem.Clear();
                comboFormPagamento.SelectedIndex = -1;

                comboFormPagamento.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar parcela: {ex.Message}");
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                var servico = new CondicaoPagamentoServicos();

                if (isExcluindo)
                {
                    DialogResult result = MessageBox.Show(
                        "Você tem certeza que deseja excluir esta condição?",
                        "Confirmação",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning
                    );

                    if (result == DialogResult.Yes)
                    {
                        servico.RemoverComParcelas(condicaoPagamento.Id);
                        MessageBox.Show("Condição de pagamento removida com sucesso.");
                        isExcluindo = false;
                        this.Close();
                    }
                    return;
                }

                if (!ValidarEntrada())
                    return;

                if (parcelas == null || parcelas.Count == 0)
                {
                    MessageBox.Show("Adicione pelo menos uma parcela.");
                    return;
                }

                decimal somaPorcentagens = parcelas.Sum(p => p.Porcentagem);
                if (Math.Round(somaPorcentagens, 2) != 100)
                {
                    MessageBox.Show(
                        $"A soma das parcelas deve ser 100%. Soma atual: {somaPorcentagens:F2}%",
                        "Erro de Validação",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );
                    return;
                }

                AtualizarObjeto();

                if (isEditando)
                {
                    if (condicaoPagamentoServices.BuscarPorId(condicaoPagamento.Id) == null)
                    {
                        MessageBox.Show($"O registro com ID {condicaoPagamento.Id} não existe.");
                        return;
                    }

                    servico.AtualizarCondicaoComParcelas(condicaoPagamento, parcelas); // Atualiza tudo
                    MessageBox.Show("Condição de pagamento atualizada com sucesso!");
                    isEditando = false;
                    this.Close();
                }
                else
                {
                    servico.SalvarCondicaoComParcelas(condicaoPagamento, parcelas);
                    MessageBox.Show("Condição de pagamento salva com sucesso!");
                }

                LimparCampos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao salvar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void BloquearCampos()
        {
            txtCodigo.Enabled = false;
            txtCondPagamento.Enabled = false;
            txtJuros.Enabled = false;
            txtMulta.Enabled = false;
            txtPrazo.Enabled = false;
            txtPorcentagem.Enabled = false;
            comboFormPagamento.Enabled = false;
            btnGerarParcela.Enabled = false;
            btnRemover.Enabled = false;
            btnCadastrar.Enabled = false;
            btnSalvar.Enabled = true;
        }
        public void DesbloquearCampos()
        {
            txtCondPagamento.Enabled = true;
            txtJuros.Enabled = true;
            txtMulta.Enabled = true;
            txtPrazo.Enabled = true;
            txtPorcentagem.Enabled = true;
            comboFormPagamento.Enabled = true;
            btnGerarParcela.Enabled = true;
            btnSalvar.Enabled = true;
            txtCodigo.Enabled = false;
        }
        public void CarregarDados(CondicaoPagamento condicao, bool isEditandoForm, bool isExcluindoForm)
        {
            txtCodigo.Text = condicao.Id.ToString();
            txtCondPagamento.Text = condicao.Descricao;
            txtJuros.Text = condicao.TaxaJuros.ToString("F2");
            txtMulta.Text = condicao.Multa.ToString("F2");

            condicaoPagamento = condicao;
            isEditando = isEditandoForm;
            isExcluindo = isExcluindoForm;

            parcelas = condicaoPagamentoParcelasServicos.BuscarTodos().Where(p => p.IdCondicaoPagamento == condicao.Id).ToList();

            listView1.Items.Clear();
            int contador = 1;
            foreach (var parcela in parcelas)
            {
                var forma = formaPagamentoServicos.BuscarPorId(parcela.IdFormaPagamento);
                string descForma = forma != null ? forma.Descricao : "Desconhecida";

                ListViewItem item = new ListViewItem(contador.ToString());
                item.SubItems.Add(descForma);
                item.SubItems.Add(parcela.Prazo.ToString());
                item.SubItems.Add(parcela.Porcentagem.ToString("F2") + "%");
                listView1.Items.Add(item);

                contador++;
            }

            decimal soma = parcelas.Sum(p => p.Porcentagem);
            porcentagemRestante = 100 - soma;
            txtRestante.Text = porcentagemRestante.ToString("F2");
            proximoNumeroParcela = parcelas.Count + 1;
            txtParcela.Text = proximoNumeroParcela.ToString();

            if (isExcluindo)
            {
                BloquearCampos();
            }
            else
            {
                DesbloquearCampos();
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione uma parcela para remover.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int indiceSelecionado = listView1.SelectedItems[0].Index;

            if (indiceSelecionado >= 0 && indiceSelecionado < parcelas.Count)
            {
                var parcelaRemovida = parcelas[indiceSelecionado];

                porcentagemRestante += parcelaRemovida.Porcentagem;
                txtRestante.Text = porcentagemRestante.ToString("F2");

                parcelas.RemoveAt(indiceSelecionado);
                listView1.Items.RemoveAt(indiceSelecionado);

                listView1.Items.Clear();
                int contador = 1;
                foreach (var parcela in parcelas)
                {
                    var forma = formaPagamentoServicos.BuscarPorId(parcela.IdFormaPagamento);
                    string descricaoForma = forma != null ? forma.Descricao : "Desconhecida";

                    ListViewItem lvi = new ListViewItem(contador.ToString());
                    lvi.SubItems.Add(descricaoForma);
                    lvi.SubItems.Add(parcela.Prazo.ToString());
                    lvi.SubItems.Add(parcela.Porcentagem.ToString("F2") + "%");
                    listView1.Items.Add(lvi);

                    parcela.NumParcela = contador;
                    contador++;
                }

                proximoNumeroParcela = parcelas.Count + 1;
                txtParcela.Text = proximoNumeroParcela.ToString();

                MessageBox.Show("Parcela removida com sucesso!");
            }
        }
    }
}
