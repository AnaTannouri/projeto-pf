using ProjetoPF.Dao;
using ProjetoPF.FormCadastros;
using ProjetoPF.FormConsultas;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Globalization;
using System.Windows.Forms;
using ProjetoPF.Modelos.Pessoa;

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
            condicaoPagamento.Ativo = checkAtivo.Checked;
            if (string.IsNullOrEmpty(maskedTxtDesconto.Text.Trim()))
            {
                condicaoPagamento.Desconto = 0;  
            }
            else
            {
                condicaoPagamento.Desconto = decimal.Parse(maskedTxtDesconto.Text);
            }

            if (condicaoPagamento.DataCriacao == DateTime.MinValue)
                condicaoPagamento.DataCriacao = DateTime.Now;

            condicaoPagamento.DataAtualizacao = DateTime.Now;
        }

        private void LimparCampos()
        {
            txtCodigo.Clear();
            txtCondPagamento.Clear();
            txtJuros.Clear();
            txtMulta.Clear();
            maskedTxtDesconto.Clear();
            txtPrazo.Clear();
            txtParcela.Clear();
            txtForma.Clear();   
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
            checkAtivo.Checked = true;      
            checkAtivo.Enabled = false;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            FrmConsultaFormaPagamento frmConsultaFormaPagamento = new FrmConsultaFormaPagamento();
            frmConsultaFormaPagamento.Owner = this;
            frmConsultaFormaPagamento.ShowDialog();
        }
        private void FrmCadastroCondPagamento_Load(object sender, EventArgs e)
        {
            if (!isEditando && !isExcluindo)
            {
                checkAtivo.Checked = true;
                checkAtivo.Enabled = false;
            }
            CarregarFormasDePagamento();
            listView1.View = View.Details;
            listView1.Columns.Add("Número Parcela", 100, HorizontalAlignment.Right);
            listView1.Columns.Add("Prazo", 80, HorizontalAlignment.Right);
            listView1.Columns.Add("Parcela %", 80, HorizontalAlignment.Right);
            listView1.Columns.Add("Taxa de Juros %", 100, HorizontalAlignment.Right);
            listView1.Columns.Add("Taxa de Multa %", 100, HorizontalAlignment.Right);
            listView1.Columns.Add("Taxa de Desconto %", 125, HorizontalAlignment.Right);
            listView1.Columns.Add("Forma de Pagamento", 150);
            txtParcela.Text = proximoNumeroParcela.ToString();

            listView1.FullRowSelect = true;

            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";
            labelCriacao.Text = condicaoPagamento.DataCriacao.ToShortDateString();
            lblAtualizacao.Text = condicaoPagamento.DataAtualizacao.ToShortDateString();
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

                if (VerificarDuplicidade())
                {
                    MessageBox.Show("Já existe uma condição de pagamento com essas informações.", "Erro de duplicidade", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                foreach (var parcela in parcelas)
                {
                    var forma = formaPagamentoServicos.BuscarPorId(parcela.IdFormaPagamento);
                    if (forma == null || !forma.Ativo)
                    {
                        MessageBox.Show($"A parcela {parcela.NumParcela} está associada a uma forma de pagamento inativa. Remova ou substitua para continuar.",
                                        "Forma de pagamento inativa",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                        return;
                    }
                }

                AtualizarObjeto();

                if (isEditando)
                {
                    if (condicaoPagamentoServices.BuscarPorId(condicaoPagamento.Id) == null)
                    {
                        MessageBox.Show($"O registro com ID {condicaoPagamento.Id} não existe.");
                        return;
                    }

                    servico.AtualizarCondicaoComParcelas(condicaoPagamento, parcelas);
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
                Close();
            }
            catch (SqlException ex) when (ex.Number == 547)
            {
                string mensagem;
                if (ex.Message.Contains("FK_CondicaoPagamentoCliente"))
                    mensagem = "Não é possível remover: está associada a um ou mais clientes.";
                else if (ex.Message.Contains("FK_CondicaoPagamentoFornecedor"))
                    mensagem = "Não é possível remover: está associada a um ou mais fornecedores.";
                else
                    mensagem = "Não é possível remover a condição de pagamento, pois ela está em uso.";

                MessageBox.Show(mensagem, "Erro de integridade referencial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void BloquearCampos()
        {
            txtCodigo.Enabled = false;
            txtCondPagamento.Enabled = false;
            txtJuros.Enabled = false;
            txtMulta.Enabled = false;
            maskedTxtDesconto.Enabled = false;
            txtPrazo.Enabled = false;
            txtPorcentagem.Enabled = false;
            txtForma.Enabled = false;
            btnGerarParcela.Enabled = false;
            btnRemover.Enabled = false;
            btnFormasPagamento.Enabled = false;
            btnSalvar.Enabled = true;
        }
        public void DesbloquearCampos()
        {
            txtCondPagamento.Enabled = true;
            txtJuros.Enabled = true;
            txtMulta.Enabled = true;
            maskedTxtDesconto.Enabled = true;
            txtPrazo.Enabled = true;
            txtPorcentagem.Enabled = true;
            btnGerarParcela.Enabled = true;
            btnSalvar.Enabled = true;
            txtCodigo.Enabled = false;
            checkAtivo.Enabled = isEditando;
        }
        public void CarregarDados(CondicaoPagamento condicao, bool isEditandoForm, bool isExcluindoForm)
        {
            txtCodigo.Text = condicao.Id.ToString();
            txtCondPagamento.Text = condicao.Descricao;
            txtJuros.Text = condicao.TaxaJuros.ToString("F2");
            txtMulta.Text = condicao.Multa.ToString("F2");
            checkAtivo.Checked = condicao.Ativo;      
            checkAtivo.Enabled = isEditandoForm;
            maskedTxtDesconto.Text = condicao.Desconto.ToString("F2");

            condicaoPagamento = condicao;
            isEditando = isEditandoForm;
            isExcluindo = isExcluindoForm;
            AtualizarObjeto(); 

            parcelas = condicaoPagamentoParcelasServicos.BuscarTodos().Where(p => p.IdCondicaoPagamento == condicao.Id).ToList();

            listView1.Items.Clear();
            int contador = 1;
            foreach (var parcela in parcelas)
            {
                var forma = formaPagamentoServicos.BuscarPorId(parcela.IdFormaPagamento);
                string descForma = forma != null ? forma.Descricao : "Desconhecida";

                ListViewItem item = new ListViewItem(contador.ToString());
                item.SubItems.Add(parcela.Prazo.ToString());
                item.SubItems.Add(parcela.Porcentagem.ToString("F2") + "%");
                item.SubItems.Add(condicaoPagamento.TaxaJuros.ToString("F2") + "%");
                item.SubItems.Add(condicaoPagamento.Multa.ToString("F2") + "%");
                item.SubItems.Add(condicaoPagamento.Desconto.ToString("F2") + "%");
                item.SubItems.Add(descForma);
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
                    lvi.SubItems.Add(parcela.Prazo.ToString());
                    lvi.SubItems.Add(parcela.Porcentagem.ToString("F2") + "%");
                    lvi.SubItems.Add(condicaoPagamento.TaxaJuros.ToString("F2") + "%");
                    lvi.SubItems.Add(condicaoPagamento.Multa.ToString("F2") + "%");
                    lvi.SubItems.Add(condicaoPagamento.Desconto.ToString("F2") + "%");
                    lvi.SubItems.Add(descricaoForma);
                    listView1.Items.Add(lvi);

                    parcela.NumParcela = contador;
                    contador++;
                }

                proximoNumeroParcela = parcelas.Count + 1;
                txtParcela.Text = proximoNumeroParcela.ToString();

                MessageBox.Show("Parcela removida com sucesso!");
            }
        }

        private void btnGerarParcela_Click_1(object sender, EventArgs e)
        {

            try
            {
                AtualizarObjeto();
                int numParcela = proximoNumeroParcela;

                if (string.IsNullOrWhiteSpace(txtForma.Text))
                {
                    MessageBox.Show("Por favor, informe uma forma de pagamento.");
                    return;
                }

                var formasPagamento = formaPagamentoServicos.BuscarTodos()
                        .Where(f => f.Ativo) 
                        .ToList();
                FormaPagamento pagamentoSelecionado = formasPagamento
                    .FirstOrDefault(f => f.Descricao.Equals(txtForma.Text.Trim(), StringComparison.OrdinalIgnoreCase));

                if (pagamentoSelecionado == null)
                {
                    MessageBox.Show("Forma de pagamento não encontrada.");
                    return;
                }
                else if (!pagamentoSelecionado.Ativo)
                {
                    MessageBox.Show("A forma de pagamento selecionada está inativa. Selecione uma forma ativa.", "Forma inativa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string formaPagamento = pagamentoSelecionado.Descricao;
                int prazo = int.Parse(txtPrazo.Text);

                string textoPorcentagem = txtPorcentagem.Text.Trim().Replace('.', ',');
                if (!decimal.TryParse(textoPorcentagem, NumberStyles.Any, CultureInfo.CurrentCulture, out decimal porcentagemParcela) || porcentagemParcela <= 0)
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
                    txtForma.Clear();
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
                lvi.SubItems.Add(novaParcela.Prazo.ToString());
                lvi.SubItems.Add(novaParcela.Porcentagem.ToString("F2") + "%");
                lvi.SubItems.Add(condicaoPagamento.TaxaJuros.ToString("F2") + "%");
                lvi.SubItems.Add(condicaoPagamento.Multa.ToString("F2") + "%");
                lvi.SubItems.Add(condicaoPagamento.Desconto.ToString("F2") + "%");
                lvi.SubItems.Add(formaPagamento);
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
                txtForma.Clear();

                txtForma.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao gerar parcela: {ex.Message}");
            }
        }
        private bool VerificarDuplicidade()
        {
            var condicoesExistentes = condicaoPagamentoServices.BuscarTodos();

            foreach (var condExistente in condicoesExistentes)
            {
                if (isEditando && condExistente.Id == condicaoPagamento.Id)
                    continue;

                if (!condExistente.Descricao.Equals(txtCondPagamento.Text.Trim(), StringComparison.OrdinalIgnoreCase))
                    continue;

                if (condExistente.TaxaJuros != decimal.Parse(txtJuros.Text))
                    continue;

                if (condExistente.Multa != decimal.Parse(txtMulta.Text))
                    continue;

                var parcelasExistentes = condicaoPagamentoParcelasServicos.BuscarTodos()
                    .Where(p => p.IdCondicaoPagamento == condExistente.Id)
                    .OrderBy(p => p.NumParcela)
                    .ToList();

                if (parcelasExistentes.Count != parcelas.Count)
                    continue;

                bool todasIguais = true;

                for (int i = 0; i < parcelas.Count; i++)
                {
                    var pNova = parcelas[i];
                    var pExistente = parcelasExistentes[i];

                    if (pNova.Prazo != pExistente.Prazo ||
                        pNova.Porcentagem != pExistente.Porcentagem ||
                        pNova.IdFormaPagamento != pExistente.IdFormaPagamento)
                    {
                        todasIguais = false;
                        break;
                    }
                }

                if (todasIguais)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
