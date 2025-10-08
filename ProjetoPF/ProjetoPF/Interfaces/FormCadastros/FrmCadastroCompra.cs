using ProjetoPF.Dao;
using ProjetoPF.FormConsultas;
using ProjetoPF.Interfaces.FormConsultas;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Modelos.Pessoa;
using ProjetoPF.Servicos;
using ProjetoPF.Servicos.Pessoa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using ProjetoPF.Modelos.Produto;
using ProjetoPF.Modelos.Compra;
using ProjetoPF.Servicos.Compra;
using ProjetoPF.Dao.Compras;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroCompra : ProjetoPF.FormCadastros.FrmCadastroPai
    {
        private BaseServicos<Fornecedor> fornecedorServices = new BaseServicos<Fornecedor>(new BaseDao<Fornecedor>("Fornecedores"));
        private BaseServicos<CondicaoPagamento> condicaoPagamentoServices = new BaseServicos<CondicaoPagamento>(new BaseDao<CondicaoPagamento>("CondicaoPagamentos"));
        private List<ItemCompra> itensCompra = new List<ItemCompra>();
        private List<ContasAPagar> parcelas = new List<ContasAPagar>();

        private bool carregandoDuplicada = false;

        private bool notaDuplicada = false;

        public bool ModoSomenteLeitura { get; set; } = false;

        public FrmCadastroCompra()
        {
            InitializeComponent();
            label1.Text = "Modelo:";
            checkAtivo.Visible = false;

            dtpEmissao.ValueChanged += (s, e) =>
            {
                dtpEmissao.CustomFormat = "dd/MM/yyyy";
            };

            dtpEntrega.ValueChanged += (s, e) =>
            {
                dtpEntrega.CustomFormat = "dd/MM/yyyy";
            };

        }
        private void TravarCamposDeCodigo()
        {
            foreach (var tb in new[] { txtCodFornecedor, txtCodProduto, txtCodCondicao })
            {
                tb.ReadOnly = true;     
                tb.TabStop = false;    
                tb.BackColor = SystemColors.ControlLight; 
            }
        }

        private void btnPesquisarCondicao_Click(object sender, EventArgs e)
        {
            if (itensCompra.Count == 0)
            {
                MessageBox.Show("Inclua ao menos um item antes de escolher a condição de pagamento.");
                return;
            }

            if (!int.TryParse(txtCodFornecedor.Text, out int idFornecedor))
            {
                MessageBox.Show("Selecione um fornecedor antes de escolher a condição de pagamento.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var fornecedor = fornecedorServices.BuscarPorId(idFornecedor);
            if (fornecedor == null)
            {
                MessageBox.Show("Fornecedor não encontrado.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var condicaoAVista = condicaoPagamentoServices.BuscarTodos("")
                .FirstOrDefault(c => c.Descricao.Equals("A VISTA", StringComparison.OrdinalIgnoreCase));

            if (condicaoAVista == null)
            {
                MessageBox.Show("Condição 'À Vista' não encontrada no banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var idsPermitidos = new List<int> { condicaoAVista.Id };
            if (fornecedor.CondicaoPagamentoId != condicaoAVista.Id)
                idsPermitidos.Add(fornecedor.CondicaoPagamentoId);

            using (var formConsulta = new FrmConsultaCondPagamentoFiltrado(idsPermitidos))
            {
                formConsulta.ShowDialog();

                if (formConsulta.CondicaoSelecionada != null)
                {
                    txtCondicao.Text = formConsulta.CondicaoSelecionada.Descricao;
                    txtCodCondicao.Text = formConsulta.CondicaoSelecionada.Id.ToString();

                    btnLimparParcelas.Enabled = true;
                    btnGerarParcelas.Enabled = true;
                }
            }
        }

        private void btnPesquisarProduto_Click(object sender, EventArgs e)
        {
            using (var formConsulta = new FrmConsultaProduto())
            {
                if (formConsulta.ShowDialog() == DialogResult.OK && formConsulta.ProdutoSelecionado != null)
                {
                    var produto = formConsulta.ProdutoSelecionado;
                    txtProduto.Text = produto.Nome;
                    txtCodProduto.Text = produto.Id.ToString();
                }
            }
        }

        private void btPesquisarFornecedor_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtCodFornecedor.Text))
            {
                txtCodigo.Text = string.Empty;
                txtSerie.Text = string.Empty;
                txtNumeroNota.Text = string.Empty;
                txtCodFornecedor.Text = string.Empty;
                txtCondicao.Text = string.Empty;
                txtCodCondicao.Text = string.Empty;

                txtCodProduto.Text = string.Empty;
                txtProduto.Text = string.Empty;
                txtQuantidade.Text = string.Empty;
                txtValorUnitario.Text = string.Empty;
                txtTotal.Text = "R$ 0,00";

                txtValorFrete.Text = string.Empty;
                txtSeguro.Text = string.Empty;
                txtDespesas.Text = string.Empty;
                txtValorTotal.Text = string.Empty;

                dtpEmissao.Format = DateTimePickerFormat.Custom;
                dtpEmissao.CustomFormat = " ";

                dtpEntrega.Format = DateTimePickerFormat.Custom;
                dtpEntrega.CustomFormat = " ";

                itensCompra.Clear();
                AtualizarListViewItens();
            }

            FrmConsultaFornecedor frmConsultaFornecedor = new FrmConsultaFornecedor();
            frmConsultaFornecedor.Owner = this;
            frmConsultaFornecedor.ShowDialog();

            bool fornecedorSelecionado = false; 
            if (int.TryParse(txtCodFornecedor.Text, out var idForn))
            {
                PreencherCondicaoPadraoDoFornecedor(idForn);
                fornecedorSelecionado = true;
            }
            if (fornecedorSelecionado)
            {
                TentarLiberarItens(); 
            }
        }
        private void BloquearTudoExcetoCabecalho()
        {
            txtCodigo.Enabled = true;
            txtSerie.Enabled = true;
            txtNumeroNota.Enabled = true;
            txtCodFornecedor.Enabled = true;

            dtpEmissao.Enabled = false;
            dtpEntrega.Enabled = false;

            SetItensEnabled(false);
            SetTotaisEnabled(false);
            SetCondicaoEnabled(false);
            SetSalvarEnabled(false);

            listViewProduto.Enabled = false;
            listViewParcelas.Enabled = false;

            btnPesquisarCondicao.Enabled = false;
            btnGerarParcelas.Enabled = false;
            btnLimparParcelas.Enabled = false;
        }

        private void dtpEmissao_ValueChanged(object sender, EventArgs e)
        {
            dtpEmissao.CustomFormat = "dd/MM/yyyy";
        }

        private void dtpEntrega_ValueChanged(object sender, EventArgs e)
        {
            dtpEntrega.CustomFormat = "dd/MM/yyyy";
        }

        private void AtualizarListViewItens()
        {
            listViewProduto.Items.Clear();

            foreach (var item in itensCompra)
            {
                var produto = new BaseServicos<Produtos>(new BaseDao<Produtos>("Produtos"))
                    .BuscarPorId(item.IdProduto);

                ListViewItem lvi = new ListViewItem(item.IdProduto.ToString());
                lvi.SubItems.Add(produto?.Nome ?? "");
                lvi.SubItems.Add(item.Quantidade.ToString("F4"));   
                lvi.SubItems.Add(item.ValorUnitario.ToString("C2")); 
                lvi.SubItems.Add(item.Total.ToString("C2"));
                lvi.Tag = item;

                listViewProduto.Items.Add(lvi);
            }

            AtualizarTotalProdutos();

            btnEditar.Enabled = listViewProduto.Items.Count > 0;
            btnRemover.Enabled = listViewProduto.Items.Count > 0;
        }
        private decimal ConverterMoeda(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return 0;

            texto = texto.Replace("R$", "")
                         .Replace(" ", "")
                         .Replace(".", "");

            texto = texto.Replace(",", ".");

            if (decimal.TryParse(texto, System.Globalization.NumberStyles.Any,
                                 System.Globalization.CultureInfo.InvariantCulture,
                                 out decimal valor))
            {
                return Math.Round(valor, 4);
            }

            return 0;

        }

        private decimal ConverterNumero(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return 0;

            texto = texto.Replace(",", ".");
            if (decimal.TryParse(texto, System.Globalization.NumberStyles.AllowDecimalPoint,
                                 System.Globalization.CultureInfo.InvariantCulture,
                                 out decimal valor))
            {
                return Math.Round(valor, 4); 
            }
            return 0;
        }


        private void btnAdicionar_Click(object sender, EventArgs e)
        {
            decimal quantidade = ConverterNumero(txtQuantidade.Text);
            decimal valorUnitario = ConverterMoeda(txtValorUnitario.Text);

            if (quantidade <= 0 || valorUnitario <= 0)
            {
                MessageBox.Show("Quantidade ou valor inválido.");
                return;
            }

            if (!int.TryParse(txtCodProduto.Text, out int idProduto))
            {
                MessageBox.Show("Produto inválido.");
                return;
            }

            var existente = itensCompra.FirstOrDefault(i => i.IdProduto == idProduto);
            if (existente != null)
                itensCompra.Remove(existente);

            var item = new ItemCompra
            {
                IdProduto = idProduto,
                Quantidade = quantidade,
                ValorUnitario = valorUnitario,
                Total = quantidade * valorUnitario
            };

            itensCompra.Add(item);
            AtualizarListViewItens();

            txtCodProduto.Text = string.Empty;
            txtProduto.Text = string.Empty;
            txtQuantidade.Text = string.Empty;
            txtValorUnitario.Text = string.Empty;
            txtTotal.Text = string.Empty;

            if (listViewParcelas.Items.Count > 0)
            {
                parcelas.Clear();
                listViewParcelas.Items.Clear();
                label21.Text = "R$ 0,00";
            }
            if (itensCompra.Count == 1)
            {
                SetCabecalhoEnabled(false);
                SetItensEnabled(true);
                SetTotaisEnabled(true);    
                SetCondicaoEnabled(false); 
                btnLimparParcelas.Enabled = false;
                SetSalvarEnabled(false);
            }
            AtualizarValorTotalCompra();
        }
        private void FrmCadastroCompra_Load(object sender, EventArgs e)
        {
            label33.Visible = false;
            label34.Visible = false;
            checkAtivo.Visible = false;
            txtObservacao.Enabled = false;

            if (ModoSomenteLeitura)
            {
                btPesquisarFornecedor.Enabled = false;
            }

            var hoje = DateTime.Today;

            dtpEmissao.MinDate = DateTimePicker.MinimumDateTime;
            dtpEmissao.MaxDate = hoje;

            dtpEntrega.MinDate = hoje;
            dtpEntrega.MaxDate = hoje.AddYears(10);

            if (!idCompra.HasValue || idCompra.Value == 0)
            {
                dtpEmissao.Format = DateTimePickerFormat.Custom;
                dtpEmissao.CustomFormat = " ";

                dtpEntrega.Format = DateTimePickerFormat.Custom;
                dtpEntrega.CustomFormat = " ";
            }

            dtpEmissao.ValueChanged += (s, ev) =>
            {
                if (dtpEmissao.CustomFormat == " ")
                {
                    dtpEmissao.Format = DateTimePickerFormat.Short;
                    dtpEmissao.CustomFormat = null;
                }
            };
            dtpEmissao.CloseUp += (s, ev) =>
            {
                if (dtpEmissao.CustomFormat == " ")
                {
                    dtpEmissao.Format = DateTimePickerFormat.Short;
                    dtpEmissao.CustomFormat = null;
                }
            };

            dtpEntrega.ValueChanged += (s, ev) =>
            {
                if (dtpEntrega.CustomFormat == " ")
                {
                    dtpEntrega.Format = DateTimePickerFormat.Short;
                    dtpEntrega.CustomFormat = null;
                    TentarLiberarItens();
                }
            };
            dtpEntrega.CloseUp += (s, ev) =>
            {
                if (dtpEntrega.CustomFormat == " ")
                {
                    dtpEntrega.Format = DateTimePickerFormat.Short;
                    dtpEntrega.CustomFormat = null;
                    TentarLiberarItens();
                }
            };
            txtNumeroNota.Leave += (_, __) => ValidarNotaDuplicada();

            listViewProduto.View = View.Details;
            listViewProduto.Columns.Clear();
            listViewProduto.Columns.Add("Cód. Produto", 100, HorizontalAlignment.Right);
            listViewProduto.Columns.Add("Descrição", 250, HorizontalAlignment.Left);
            listViewProduto.Columns.Add("Quantidade", 120, HorizontalAlignment.Right);
            listViewProduto.Columns.Add("Valor Unitário", 150, HorizontalAlignment.Right);
            listViewProduto.Columns.Add("Total (R$)", 150, HorizontalAlignment.Right);

            listViewProduto.View = View.Details;
            listViewProduto.FullRowSelect = true;
            listViewProduto.MultiSelect = false;
            listViewProduto.HideSelection = false;
            listViewProduto.GridLines = true;

            listViewProduto.MouseClick += (s, ev) =>
            {
                if (listViewProduto.SelectedItems.Count > 0)
                    listViewProduto.Focus();
            };


            listViewParcelas.View = View.Details;
            listViewParcelas.Columns.Clear();
            listViewParcelas.Columns.Add("Nº Parcela", 80, HorizontalAlignment.Right);
            listViewParcelas.Columns.Add("Vencimento", 150, HorizontalAlignment.Left);
            listViewParcelas.Columns.Add("Valor Parcela", 150, HorizontalAlignment.Right);
            listViewParcelas.Columns.Add("Forma Pagamento", 150, HorizontalAlignment.Left);

            txtQuantidade.TextChanged += txtQuantidade_TextChanged;
            txtValorUnitario.TextChanged += txtValorUnitario_TextChanged;
            txtValorFrete.TextChanged += (s, ev) => { AtualizarValorTotalCompra(); SetCondicaoEnabled(true); };
            txtSeguro.TextChanged += (s, ev) => { AtualizarValorTotalCompra(); SetCondicaoEnabled(true); };
            txtDespesas.TextChanged += (s, ev) => { AtualizarValorTotalCompra(); SetCondicaoEnabled(true); };

            SetCabecalhoEnabled(true);
            SetItensEnabled(false);
            SetCondicaoEnabled(false);
            SetSalvarEnabled(false);
            listViewProduto.Enabled = false;
            listViewParcelas.Enabled = false;
            SetTotaisEnabled(false);

            txtCodigo.Leave += (_, __) => TentarLiberarItens();
            txtSerie.Leave += (_, __) => TentarLiberarItens();
            txtNumeroNota.Leave += (_, __) => TentarLiberarItens();
            txtCodFornecedor.Leave += (_, __) => TentarLiberarItens();

            txtCodigo.TextChanged += (_, __) => ValidarNotaDuplicada();
            txtSerie.TextChanged += (_, __) => ValidarNotaDuplicada();
            txtNumeroNota.TextChanged += (_, __) => ValidarNotaDuplicada();
            txtCodFornecedor.TextChanged += (_, __) => ValidarNotaDuplicada();

            dtpEntrega.ValueChanged += (s, ev) =>
            {
                if (dtpEntrega.CustomFormat == " ")
                {
                    dtpEntrega.Format = DateTimePickerFormat.Short;
                    dtpEntrega.CustomFormat = null;
                }
                TentarLiberarItens(); 
            };
            dtpEntrega.CloseUp += (s, ev) =>
            {
                if (dtpEntrega.CustomFormat == " ")
                {
                    dtpEntrega.Format = DateTimePickerFormat.Short;
                    dtpEntrega.CustomFormat = null;
                }
                TentarLiberarItens(); 
            };

            dtpEmissao.ValueChanged += (s, ev) =>
            {
                if (dtpEmissao.CustomFormat == " ")
                {
                    dtpEmissao.Format = DateTimePickerFormat.Short;
                    dtpEmissao.CustomFormat = null;
                }
                TentarLiberarItens(); 
            };
            dtpEmissao.CloseUp += (s, ev) =>
            {
                if (dtpEmissao.CustomFormat == " ")
                {
                    dtpEmissao.Format = DateTimePickerFormat.Short;
                    dtpEmissao.CustomFormat = null;
                }
                TentarLiberarItens(); 
            };


            TravarCamposDeCodigo();

            txtValorFrete.KeyPress += ApenasNumerosDecimais;
            txtValorFrete.Leave += FormatarMoedaAoSair;
            txtSeguro.KeyPress += ApenasNumerosDecimais;
            txtSeguro.Leave += FormatarMoedaAoSair;
            txtDespesas.KeyPress += ApenasNumerosDecimais;
            txtDespesas.Leave += FormatarMoedaAoSair;
            txtValorUnitario.KeyPress += ApenasNumerosDecimais;
            txtValorUnitario.Leave += FormatarMoedaAoSair;
            txtTotal.KeyPress += ApenasNumerosDecimais;
            txtTotal.Leave += FormatarMoedaAoSair;
            txtValorTotal.KeyPress += ApenasNumerosDecimais;
            txtValorTotal.Leave += FormatarMoedaAoSair;


            txtCodigo.KeyPress += ApenasNumeros;
            txtSerie.KeyPress += ApenasNumeros;
            txtNumeroNota.KeyPress += ApenasNumeros;
            txtQuantidade.KeyPress += ApenasNumerosDecimais;

            if (idCompra.HasValue && idCompra.Value > 0)
            {
                var compraDao = new CompraDao();
                var compra = compraDao.BuscarPorId(idCompra.Value);

                if (compra != null)
                {
                    txtCodigo.Text = compra.Id.ToString();
                    txtSerie.Text = compra.Serie;
                    txtNumeroNota.Text = compra.NumeroNota;

                    dtpEmissao.MinDate = DateTimePicker.MinimumDateTime;
                    dtpEntrega.MinDate = DateTimePicker.MinimumDateTime;
                    dtpEntrega.MaxDate = DateTimePicker.MaximumDateTime;

                    dtpEmissao.Format = DateTimePickerFormat.Short;
                    dtpEntrega.Format = DateTimePickerFormat.Short;

                    dtpEmissao.Value = compra.DataEmissao;
                    dtpEntrega.Value = compra.DataEntrega;

                    txtValorFrete.Text = compra.ValorFrete.ToString("N2");
                    txtSeguro.Text = compra.ValorSeguro.ToString("N2");
                    txtDespesas.Text = compra.OutrasDespesas.ToString("N2");
                    txtValorTotal.Text = compra.ValorTotal.ToString("N2");
                    txtObservacao.Text = compra.Observacao ?? "";

                    labelCriacao.Text = compra.DataCriacao != DateTime.MinValue
                        ? compra.DataCriacao.ToString("dd/MM/yyyy HH:mm") : "-";

                    lblAtualizacao.Text = compra.DataAtualizacao != DateTime.MinValue
                        ? compra.DataAtualizacao.ToString("dd/MM/yyyy HH:mm") : "-";

                    var fornecedor = new BaseServicos<Fornecedor>(new BaseDao<Fornecedor>("Fornecedores"))
                                        .BuscarPorId(compra.IdFornecedor);
                    txtCodFornecedor.Text = fornecedor?.Id.ToString();
                    txtFornecedor.Text = fornecedor?.NomeRazaoSocial ?? "Desconhecido";

                    var condicao = new BaseServicos<CondicaoPagamento>(new BaseDao<CondicaoPagamento>("CondicaoPagamentos"))
                                        .BuscarPorId(compra.IdCondicaoPagamento);
                    txtCodCondicao.Text = condicao?.Id.ToString();
                    txtCondicao.Text = condicao?.Descricao ?? "Desconhecida";

                    listViewProduto.Items.Clear();
                    var itens = new ItemCompraDao().BuscarPorCompraId(compra.Id);
                    itensCompra = itens;

                    foreach (var it in itens)
                    {
                        var produto = new BaseServicos<Produtos>(new BaseDao<Produtos>("Produtos"))
                                         .BuscarPorId(it.IdProduto);
                        var lvi = new ListViewItem(it.IdProduto.ToString());
                        lvi.SubItems.Add(produto?.Nome ?? "Desconhecido");
                        lvi.SubItems.Add(it.Quantidade.ToString("N2"));
                        lvi.SubItems.Add(it.ValorUnitario.ToString("C2"));
                        lvi.SubItems.Add(it.Total.ToString("C2"));
                        listViewProduto.Items.Add(lvi);
                    }
                    AtualizarTotalProdutos();

                    listViewParcelas.Items.Clear();
                    List<ContasAPagar> listaParcelas;

                    if (compra.Ativo)
                    {
                        listaParcelas = new ContasAPagarDao().BuscarPorCompraId(compra.Id);
                    }
                    else
                    {
                        listaParcelas = new ContasAPagarDao().BuscarParcelasCanceladas(compra.Id);
                    }

                    parcelas = listaParcelas;


                    foreach (var p in listaParcelas)
                    {
                        var forma = new BaseServicos<FormaPagamento>(new BaseDao<FormaPagamento>("FormaPagamentos"))
                                         .BuscarPorId(p.IdFormaPagamento);

                        var lvi = new ListViewItem(p.NumeroParcela.ToString());
                        lvi.SubItems.Add(p.DataVencimento.ToString("dd/MM/yyyy"));
                        lvi.SubItems.Add(p.ValorParcela.ToString("C2"));
                        lvi.SubItems.Add(forma?.Descricao ?? p.IdFormaPagamento.ToString());
                        listViewParcelas.Items.Add(lvi);
                    }
                    AtualizarTotalParcelas();

                    label1.Text = "Modelo";
                    txtCodigo.Enabled = false;
                    txtSerie.Enabled = false;
                    txtNumeroNota.Enabled = false;
                    dtpEmissao.Enabled = false;
                    dtpEntrega.Enabled = false;

                    btnVoltar.Text = compra.Ativo ? "Cancelar" : "Voltar";

                    if (!compra.Ativo)
                    {
                        label33.Visible = true;
                        label34.Visible = true;
                        label34.Text = compra.MotivoCancelamento ?? "Não informado";
                    }
                    else
                    {
                        label33.Visible = false;
                        label34.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Compra não encontrada!", "Erro",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (ModoSomenteLeitura)
                {
                    AplicarSomenteLeitura();
                }
            }
            listViewProduto.SelectedIndexChanged += (s, ev) =>
            {
                bool selecionado = listViewProduto.SelectedItems.Count > 0;
                btnEditar.Enabled = selecionado;
                btnRemover.Enabled = selecionado;
            };
        }
        private void AtualizarTotalParcelas()
        {
            decimal total = parcelas.Sum(p => p.ValorParcela);
            label21.Text = total.ToString("C2");
        }
        private int? idCompra;
        public FrmCadastroCompra(int idCompra)
        {
            InitializeComponent();
            this.idCompra = idCompra;

            
        }
        private bool DataEstaVazia(DateTimePicker dtp)
        {
            return dtp.Format == DateTimePickerFormat.Custom && dtp.CustomFormat == " ";
        }
        private void ApenasNumerosDecimais(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }
        private void ApenasNumeros(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void FormatarMoedaAoSair(object sender, EventArgs e)
        {
            var txt = sender as TextBox;
            if (decimal.TryParse(txt.Text, out decimal valor))
                txt.Text = valor.ToString("C2"); 
            else
                txt.Text = "R$ 0,00";
        }
        private void FormatarMoedaEmTempoReal(object sender, EventArgs e)
        {
            TextBox txt = sender as TextBox;

            if (string.IsNullOrWhiteSpace(txt.Text))
                return;

            string apenasNumeros = new string(txt.Text.Where(char.IsDigit).ToArray());

            if (decimal.TryParse(apenasNumeros, out decimal valor))
            {
                valor /= 100; 
                txt.Text = valor.ToString("C2"); 
                txt.SelectionStart = txt.Text.Length; 
            }
        }
        private void AtualizarTotalProdutos()
        {
            decimal total = itensCompra.Sum(i => i.Total);
            label4.Text = total.ToString("C2"); 
        }
        private void txtQuantidade_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalUnitario();
        }

        private void txtValorUnitario_TextChanged(object sender, EventArgs e)
        {
            CalcularTotalUnitario();
        }

        private void CalcularTotalUnitario()
        {
            decimal quantidade = ConverterNumero(txtQuantidade.Text);
            decimal valorUnitario = ConverterMoeda(txtValorUnitario.Text);

            if (quantidade > 0 && valorUnitario > 0)
            {
                decimal total = quantidade * valorUnitario;
                txtTotal.Text = total.ToString("C2");
            }
            else
            {
                txtTotal.Text = "R$ 0,00";
            }

        }
        private void GerarParcelasCondicaoPagamento()
        {
            if (!int.TryParse(txtCodCondicao.Text, out int idCondicao))
            {
                MessageBox.Show("Condição de pagamento não selecionada.");
                return;
            }

            var condicao = new BaseServicos<CondicaoPagamento>(new BaseDao<CondicaoPagamento>("CondicaoPagamentos")).BuscarPorId(idCondicao);
            var parcelasCondicao = new BaseServicos<CondicaoPagamentoParcelas>(
    new BaseDao<CondicaoPagamentoParcelas>("CondicaoPagamentoParcelas"))
    .BuscarTodos()
    .Where(p => p.IdCondicaoPagamento == idCondicao)
    .OrderBy(p => p.NumParcela)
    .ToList();

            decimal totalProdutos = itensCompra.Sum(i => i.Total);
            decimal frete = ConverterMoeda(txtValorFrete.Text);
            decimal seguro = ConverterMoeda(txtSeguro.Text);
            decimal despesas = ConverterMoeda(txtDespesas.Text);

            decimal totalCompra = totalProdutos + frete + seguro + despesas;
            DateTime dataBase = dtpEmissao.Value;

            parcelas.Clear();

            foreach (var p in parcelasCondicao)
            {
                decimal porcentagem = p.Porcentagem / 100m;

                parcelas.Add(new ContasAPagar
                {
                    NumeroParcela = p.NumParcela,
                    DataVencimento = dataBase.AddDays(p.Prazo),
                    ValorParcela = Math.Round(porcentagem * totalCompra, 2),
                    IdFormaPagamento = p.IdFormaPagamento,
                    FormaPagamentoDescricao = BuscarDescricaoFormaPagamento(p.IdFormaPagamento) 
                });

            }
            AtualizarListViewParcelas(parcelas);
        }
        private string BuscarDescricaoFormaPagamento(int idForma)
        {
            var servicoForma = new BaseServicos<FormaPagamento>(new BaseDao<FormaPagamento>("FormaPagamentos"));
            var forma = servicoForma.BuscarPorId(idForma);
            return forma?.Descricao ?? "";
        }
        private void AtualizarListViewParcelas(List<ContasAPagar> parcelas)
        {
            listViewParcelas.Items.Clear();
            decimal totalParcelas = 0;

            foreach (var parcela in parcelas)
            {
                ListViewItem item = new ListViewItem(parcela.NumeroParcela.ToString());
                item.SubItems.Add(parcela.DataVencimento.ToString("dd/MM/yyyy"));
                item.SubItems.Add(parcela.ValorParcela.ToString("C2"));
                item.SubItems.Add(parcela.FormaPagamentoDescricao ?? "");
                listViewParcelas.Items.Add(item);

                totalParcelas += parcela.ValorParcela;
            }

            label21.Text = totalParcelas.ToString("C2");

        }

        private void btnGerarParcelas_Click(object sender, EventArgs e)
        {
            GerarParcelasCondicaoPagamento();

            if (listViewParcelas.Items.Count > 0)
            {
                btnGerarParcelas.Enabled = false;
                SetTotaisEnabled(false); 
                SetItensEnabled(false);
                SetCondicaoEnabled(true);
                SetSalvarEnabled(true);

                txtObservacao.Enabled = true;

            }
        }
        private void AtualizarValorTotalCompra()
        {
            decimal totalProdutos = itensCompra.Sum(i => i.Total);
            decimal frete = ConverterMoeda(txtValorFrete.Text);
            decimal seguro = ConverterMoeda(txtSeguro.Text);
            decimal despesas = ConverterMoeda(txtDespesas.Text);

            decimal totalCompra = totalProdutos + frete + seguro + despesas;
            txtValorTotal.Text = totalCompra.ToString("C2");

            bool totaisPreenchidos =
                !string.IsNullOrWhiteSpace(txtValorFrete.Text) &&
                !string.IsNullOrWhiteSpace(txtSeguro.Text) &&
                !string.IsNullOrWhiteSpace(txtDespesas.Text);

            if (totaisPreenchidos && itensCompra.Count > 0)
                SetCondicaoEnabled(true);
            else
                SetCondicaoEnabled(false);
        }
        private void btnLimparParcelas_Click(object sender, EventArgs e)
        {
            parcelas.Clear();
            listViewParcelas.Items.Clear();
            label21.Text = "R$ 0,00";
            btnGerarParcelas.Enabled = itensCompra.Count > 0 && !string.IsNullOrWhiteSpace(txtCodCondicao.Text);

            txtObservacao.Text = string.Empty; 
            txtObservacao.Enabled = false;

            SetTotaisEnabled(true);   
            SetSalvarEnabled(false);
            SetCondicaoEnabled(true);
            SetItensEnabled(true);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (listViewProduto.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione um item para editar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var itemSelecionado = listViewProduto.SelectedItems[0];
            var itemCompra = (ItemCompra)itemSelecionado.Tag;

            txtCodProduto.Text = itemCompra.IdProduto.ToString();
            var produto = new BaseServicos<Produtos>(new BaseDao<Produtos>("Produtos")).BuscarPorId(itemCompra.IdProduto);
            txtProduto.Text = produto?.Nome ?? "";
            txtQuantidade.Text = itemCompra.Quantidade.ToString("F4");
            txtValorUnitario.Text = itemCompra.ValorUnitario.ToString("F2");
            txtTotal.Text = itemCompra.Total.ToString("C2");


            itensCompra.Remove(itemCompra);
            AtualizarListViewItens();

            parcelas.Clear();
            listViewParcelas.Items.Clear();
            label21.Text = "R$ 0,00";

            AtualizarValorTotalCompra();
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {

            if (listViewProduto.SelectedItems.Count == 0)
            {
                MessageBox.Show("Selecione um item para remover.", "Aviso",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Remover item(ns) selecionado(s)?", "Confirmar",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            foreach (ListViewItem lvi in listViewProduto.SelectedItems)
                if (lvi.Tag is ItemCompra item)
                    itensCompra.Remove(item);

            AtualizarListViewItens();
            AtualizarValorTotalCompra();

            if (itensCompra.Count == 0)
            {
                SetTotaisEnabled(false);  
                SetCondicaoEnabled(false);
                btnLimparParcelas.Enabled = false;
                SetSalvarEnabled(false);
                SetCabecalhoEnabled(true);
                SetItensEnabled(true);
            }
            AtualizarValorTotalCompra();
        }
        private void LimparCompra(bool confirmarSeTemDados = true)
        {
            txtCodProduto.Text = string.Empty;
            txtProduto.Text = string.Empty;
            txtQuantidade.Text = string.Empty;
            txtValorUnitario.Text = string.Empty;
            txtTotal.Text = "R$ 0,00";
            txtObservacao.Text = string.Empty;

            itensCompra.Clear();
            listViewProduto.Items.Clear();
            label4.Text = "R$ 0,00";

            parcelas.Clear();
            listViewParcelas.Items.Clear();
            label21.Text = "R$ 0,00";

            txtValorFrete.Text = string.Empty;
            txtSeguro.Text = string.Empty;
            txtDespesas.Text = string.Empty;
            txtValorTotal.Text = "R$ 0,00";

            SetTotaisEnabled(false);
            SetCondicaoEnabled(false);
            btnLimparParcelas.Enabled = false;
            SetSalvarEnabled(false);
            SetCabecalhoEnabled(true);
            SetItensEnabled(false);

            AtualizarValorTotalCompra();
        }

        private void btnLimparProduto_Click(object sender, EventArgs e)
        {
            LimparCompra();          
            LimparParcelasUiOnly();

            if (int.TryParse(txtCodFornecedor.Text, out int idForn))
                PreencherCondicaoPadraoDoFornecedor(idForn);

            SetCondicaoEnabled(false);
            btnLimparParcelas.Enabled = false;
            SetSalvarEnabled(false);
            SetCabecalhoEnabled(true);
            SetItensEnabled(true);

            SetTotaisEnabled(false);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!int.TryParse(txtCodigo.Text, out int modelo) || modelo <= 0)
                {
                    MessageBox.Show("Informe o modelo da nota.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtCodigo.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtSerie.Text))
                {
                    MessageBox.Show("Informe a série da nota.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSerie.Focus();
                    return;
                }
                if (string.IsNullOrWhiteSpace(txtNumeroNota.Text))
                {
                    MessageBox.Show("Informe o número da nota.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNumeroNota.Focus();
                    return;
                }
                if (!int.TryParse(txtCodFornecedor.Text, out int idFornecedor))
                {
                    MessageBox.Show("Selecione o fornecedor.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (NotaJaExiste(modelo, txtSerie.Text, txtNumeroNota.Text, idFornecedor))
                {
                    MessageBox.Show("Já existe uma compra cadastrada com esse Modelo, Série, Número e Fornecedor.",
                                    "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dtpEmissao.CustomFormat == " ")
                {
                    MessageBox.Show("Informe a data de emissão.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpEmissao.Focus();
                    return;
                }
                if (dtpEntrega.CustomFormat == " ")
                {
                    MessageBox.Show("Informe a data de entrega.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dtpEntrega.Focus();
                    return;
                }
                if (itensCompra.Count == 0)
                {
                    MessageBox.Show("Inclua pelo menos um produto.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtCodCondicao.Text, out int idCond))
                {
                    MessageBox.Show("Selecione a condição de pagamento.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (parcelas.Count == 0)
                {
                    MessageBox.Show("Gere ao menos uma parcela.", "Validação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                decimal TryMoeda(string s)
                {
                    if (string.IsNullOrWhiteSpace(s)) return 0m;
                    s = s.Replace("R$", "").Trim();
                    decimal.TryParse(s, out var v);
                    return v;
                }

                var compra = new Compra
                {
                    Modelo = modelo,
                    Serie = txtSerie.Text,
                    NumeroNota = txtNumeroNota.Text,
                    DataEmissao = dtpEmissao.Value.Date,
                    DataEntrega = dtpEntrega.Value.Date,
                    IdFornecedor = idFornecedor,
                    IdCondicaoPagamento = idCond,
                    ValorFrete = TryMoeda(txtValorFrete.Text),
                    ValorSeguro = TryMoeda(txtSeguro.Text),
                    OutrasDespesas = TryMoeda(txtDespesas.Text),
                    ValorTotal = TryMoeda(txtValorTotal.Text),
                    Itens = itensCompra,
                    Parcelas = parcelas,
                    DataCriacao = DateTime.Now,
                    DataAtualizacao = DateTime.Now,
                    Ativo = true,
                    Observacao = txtObservacao.Text?.Trim()
                };

                var compraService = new CompraServicos();
                compraService.CriarCompraCompleta(compra);

                AbrirConsultaCompras();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar a compra: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void LimparCabecalho()
        {

            txtCodigo.Text = string.Empty;
            txtSerie.Text = string.Empty;
            txtNumeroNota.Text = string.Empty;


            txtCodFornecedor.Text = string.Empty;
            txtCondicao.Text = string.Empty;
            txtCodCondicao.Text = string.Empty;

            dtpEmissao.Format = DateTimePickerFormat.Custom;
            dtpEmissao.CustomFormat = " ";
            dtpEntrega.Format = DateTimePickerFormat.Custom;
            dtpEntrega.CustomFormat = " ";


            txtValorFrete.Text = string.Empty;
            txtSeguro.Text = string.Empty;
            txtDespesas.Text = string.Empty;
            txtValorTotal.Text = string.Empty;
            txtObservacao.Text = string.Empty;
        }


        private void LimparTudo()
        {
            LimparCabecalho();
            LimparCompra();   
        }
        private void AbrirConsultaCompras()
        {
            var frm = new FrmConsultaCompra();

            if (this.MdiParent != null)
            {
                frm.MdiParent = this.MdiParent;
                frm.Show();         
            }
            else
            {
                frm.Show();
            }
        }
        private void PreencherCondicaoPadraoDoFornecedor(int idFornecedor)
        {
            var fornecedor = fornecedorServices.BuscarPorId(idFornecedor);
            if (fornecedor == null)
            {
                txtCondicao.Text = string.Empty;
                txtCodCondicao.Text = string.Empty;
                return;
            }
            var cond = condicaoPagamentoServices.BuscarPorId(fornecedor.CondicaoPagamentoId);
            if (cond != null && cond.Ativo)
            {
                txtCondicao.Text = cond.Descricao;
                txtCodCondicao.Text = cond.Id.ToString();
                return;
            }

            var aVista = condicaoPagamentoServices.BuscarTodos("")
                .FirstOrDefault(c => c.Descricao.Equals("A VISTA", StringComparison.OrdinalIgnoreCase) && c.Ativo);

            if (aVista != null)
            {
                txtCondicao.Text = aVista.Descricao;
                txtCodCondicao.Text = aVista.Id.ToString();
            }
            else
            {
                txtCondicao.Text = string.Empty;
                txtCodCondicao.Text = string.Empty;
            }
            txtCondicao.Enabled = false;
            txtCodCondicao.Enabled = false;
            btnPesquisarCondicao.Enabled = false;
        }
        private void SetCabecalhoEnabled(bool enabled)
        {
            txtCodigo.Enabled = enabled;
            txtSerie.Enabled = enabled;
            txtNumeroNota.Enabled = enabled;
            dtpEmissao.Enabled = enabled;
            dtpEntrega.Enabled = enabled;
        }
        private void SetItensEnabled(bool enabled)
        {
            btnPesquisarProduto.Enabled = enabled;
            txtQuantidade.Enabled = enabled;
            txtValorUnitario.Enabled = enabled;
            btnAdicionar.Enabled = enabled;
            btnEditar.Enabled = enabled && listViewProduto.Items.Count > 0;
            btnRemover.Enabled = enabled && listViewProduto.Items.Count > 0;
            listViewProduto.Enabled = enabled;
        }

        private void SetCondicaoEnabled(bool enabled)
        {
            bool totaisPreenchidos =
        !string.IsNullOrWhiteSpace(txtValorFrete.Text) &&
        !string.IsNullOrWhiteSpace(txtSeguro.Text) &&
        !string.IsNullOrWhiteSpace(txtDespesas.Text);

            txtCondicao.Enabled = false;
            txtCodCondicao.Enabled = false;

            btnPesquisarCondicao.Enabled = enabled && totaisPreenchidos;

            listViewParcelas.Enabled = enabled && totaisPreenchidos;

            btnGerarParcelas.Enabled =
                enabled &&
                totaisPreenchidos &&
                itensCompra.Count > 0 &&
                !string.IsNullOrWhiteSpace(txtCodCondicao.Text);

            btnLimparParcelas.Enabled = enabled && listViewParcelas.Items.Count > 0;
        }

        private void SetSalvarEnabled(bool enabled)
        {
            btnSalvar.Enabled = enabled;
        }

        private bool CabecalhoPreenchidoValido(out int modelo, out string serie, out string numero,
                                       out int idFornecedor, out DateTime emissao, out DateTime entrega)
        {
            modelo = 0; serie = numero = string.Empty; idFornecedor = 0; emissao = entrega = DateTime.MinValue;

            if (!int.TryParse(txtCodigo.Text, out modelo)) return false;
            serie = txtSerie.Text?.Trim();
            numero = txtNumeroNota.Text?.Trim();
            if (string.IsNullOrWhiteSpace(serie) || string.IsNullOrWhiteSpace(numero)) return false;

            if (!int.TryParse(txtCodFornecedor.Text, out idFornecedor)) return false;

            if (DataEstaVazia(dtpEmissao) || DataEstaVazia(dtpEntrega)) return false;

            emissao = dtpEmissao.Value.Date;
            entrega = dtpEntrega.Value.Date;

            if (emissao > DateTime.Today) return false;
            if (entrega < DateTime.Today) return false;

            return true;
        }

        private void TentarLiberarItens()
        {
            if (notaDuplicada)
            {
                BloquearTudoExcetoCabecalho();
                return;
            }

            if (idCompra.HasValue && idCompra.Value > 0)
            {
                SetItensEnabled(true);
                return;
            }

            bool cabecalhoPreenchido = !string.IsNullOrWhiteSpace(txtCodFornecedor.Text) &&
                                       !string.IsNullOrWhiteSpace(txtSerie.Text) &&
                                       !string.IsNullOrWhiteSpace(txtNumeroNota.Text) &&
                                       !string.IsNullOrWhiteSpace(txtCodigo.Text) &&
                                       !DataEstaVazia(dtpEmissao) &&
                                       !DataEstaVazia(dtpEntrega);

            if (cabecalhoPreenchido)
            {
                SetCabecalhoEnabled(false);
                SetItensEnabled(true);
                SetTotaisEnabled(false);
                SetCondicaoEnabled(false);
            }
            else
            {
                SetCabecalhoEnabled(true);
                SetItensEnabled(false);
                SetTotaisEnabled(false);
                SetCondicaoEnabled(false);
            }
        }
        private void LimparAbaixoDoProduto()
        {
            txtValorFrete.Text = string.Empty;
            txtSeguro.Text = string.Empty;
            txtDespesas.Text = string.Empty;
            txtValorTotal.Text = string.Empty;

            txtCondicao.Text = string.Empty;
            txtCodCondicao.Text = string.Empty;

            parcelas.Clear();
            listViewParcelas.Items.Clear();
            label21.Text = "R$ 0,00";

            SetCondicaoEnabled(false);
            SetSalvarEnabled(false);
        }
        private bool NotaJaExiste(int modelo, string serie, string numero, int idFornecedor)
        {
            var dao = new CompraDao();
            return dao.NotaJaExiste(modelo, serie, numero, idFornecedor, idCompra);
        }
        private void LimparParcelasUiOnly()
        {
            parcelas.Clear();
            listViewParcelas.Items.Clear();
            label21.Text = "R$ 0,00";
        }
        private void SetTotaisEnabled(bool enabled)
        {
            txtValorFrete.Enabled = enabled;
            txtSeguro.Enabled = enabled;
            txtDespesas.Enabled = enabled;
        }
        private void AplicarSomenteLeitura()
        {
           
            SetCabecalhoEnabled(false);
            btPesquisarFornecedor.Enabled = false;

        
            SetItensEnabled(false);
            listViewProduto.Enabled = false;


            SetTotaisEnabled(false);

 
            SetCondicaoEnabled(false);
            btnPesquisarCondicao.Enabled = false;
            btnGerarParcelas.Enabled = false;
            btnLimparParcelas.Enabled = false;
            listViewParcelas.Enabled = false;


            txtObservacao.Enabled = false;

            btnSalvar.Enabled = false;
        }
        private void ValidarNotaDuplicada()
        {
            if (carregandoDuplicada) return;

            if (!int.TryParse(txtCodigo.Text, out int modelo) || modelo <= 0) return;
            if (string.IsNullOrWhiteSpace(txtSerie.Text)) return;
            if (string.IsNullOrWhiteSpace(txtNumeroNota.Text)) return;
            if (!int.TryParse(txtCodFornecedor.Text, out int idFornecedor)) return;

            var dao = new CompraDao();
            var compraDuplicada = dao.BuscarPorNota(modelo, txtSerie.Text.Trim(), txtNumeroNota.Text.Trim(), idFornecedor);

            if (compraDuplicada != null)
            {
                carregandoDuplicada = true; 

                MessageBox.Show(
                    "Já existe uma compra com este Modelo, Série, Número e Fornecedor.\n" +
                    "Os dados dessa compra foram carregados para conferência.",
                    "Duplicidade Detectada",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                notaDuplicada = true;

                txtCodigo.Text = compraDuplicada.Modelo.ToString();
                txtSerie.Text = compraDuplicada.Serie;
                txtNumeroNota.Text = compraDuplicada.NumeroNota;
                txtCodFornecedor.Text = compraDuplicada.IdFornecedor.ToString();
                txtCodCondicao.Text = compraDuplicada.IdCondicaoPagamento.ToString();

                dtpEmissao.MinDate = DateTimePicker.MinimumDateTime;
                dtpEntrega.MinDate = DateTimePicker.MinimumDateTime;
                dtpEmissao.MaxDate = DateTimePicker.MaximumDateTime;
                dtpEntrega.MaxDate = DateTimePicker.MaximumDateTime;

                dtpEmissao.Format = DateTimePickerFormat.Short;
                dtpEmissao.Value = compraDuplicada.DataEmissao;

                dtpEntrega.Format = DateTimePickerFormat.Short;
                dtpEntrega.Value = compraDuplicada.DataEntrega;

                
                txtValorFrete.Text = compraDuplicada.ValorFrete.ToString("N2");
                txtSeguro.Text = compraDuplicada.ValorSeguro.ToString("N2");
                txtDespesas.Text = compraDuplicada.OutrasDespesas.ToString("N2");
                txtValorTotal.Text = compraDuplicada.ValorTotal.ToString("N2");
                txtObservacao.Text = compraDuplicada.Observacao ?? "";

               
                var itemDao = new ItemCompraDao();
                itensCompra = itemDao.BuscarPorCompraId(compraDuplicada.Id);
                AtualizarListViewItens();

            
                var parcelaDao = new ContasAPagarDao();
                parcelas = parcelaDao.BuscarPorCompraId(compraDuplicada.Id);
                AtualizarListViewParcelas(parcelas);

                
                labelCriacao.Text = compraDuplicada.DataCriacao != DateTime.MinValue
                    ? compraDuplicada.DataCriacao.ToString("dd/MM/yyyy HH:mm")
                    : "-";

                lblAtualizacao.Text = compraDuplicada.DataAtualizacao != DateTime.MinValue
                    ? compraDuplicada.DataAtualizacao.ToString("dd/MM/yyyy HH:mm")
                    : "-";

             
                AplicarSomenteLeitura();

                carregandoDuplicada = false;
            }
            else
            {
                notaDuplicada = false;
                TentarLiberarItens();
            }
        }
        private void DispararValidacaoDuplicidade()
        {
            if (!string.IsNullOrWhiteSpace(txtCodigo.Text) &&
                !string.IsNullOrWhiteSpace(txtSerie.Text) &&
                !string.IsNullOrWhiteSpace(txtNumeroNota.Text) &&
                !string.IsNullOrWhiteSpace(txtCodFornecedor.Text))
            {
                ValidarNotaDuplicada();
            }
        }


    }
}
