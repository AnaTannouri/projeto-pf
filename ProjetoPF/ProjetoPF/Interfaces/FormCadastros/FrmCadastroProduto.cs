using ProjetoPF.Dao;
using ProjetoPF.Modelos.Localizacao;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Modelos.Pessoa;
using ProjetoPF.Servicos.Pessoa;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ProjetoPF.Modelos.Produto;
using ProjetoPF.Servicos.Produto;
using ProjetoPF.Interfaces.FormConsultas;
using System.Globalization;
using System.Linq;
using ProjetoPF.Modelos.Compra;
using ProjetoPF.Dao.Compra;
using System.Data.SqlClient;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroProduto : ProjetoPF.FormCadastros.FrmCadastroPai
    {
        private Produtos produto = new Produtos();

        private BaseServicos<UnidadeMedida> unidadeMedidaServices = new BaseServicos<UnidadeMedida>(new BaseDao<UnidadeMedida>("UnidadeMedidas"));
        private BaseServicos<Marca> marcaServices = new BaseServicos<Marca>(new BaseDao<Marca>("Marcas"));
        private BaseServicos<Categoria> categoriaServices = new BaseServicos<Categoria>(new BaseDao<Categoria>("Categorias"));
        private BaseServicos<Fornecedor> fornecedorServicos = new BaseServicos<Fornecedor>(new BaseDao<Fornecedor>("Fornecedores"));

        private ProdutoServicos servicoProduto = new ProdutoServicos();

        private List<ProdutoFornecedor> fornecedoresSelecionados = new List<ProdutoFornecedor>();

        private bool carregandoCondicoes = false;
        private bool carregandoCidades = false;

        private bool isEditando = false;
        private bool isExcluindo = false;

        public FrmCadastroProduto()
        {
            InitializeComponent();

            txtPrecoCusto.KeyPress += SomenteNumerosPontuacao_KeyPress;
            txtPrecoVenda.KeyPress += SomenteNumerosPontuacao_KeyPress;

            txtPrecoVenda.TextChanged += txtPrecoVenda_TextChanged;
        }
        private bool ValidarEntrada()
        {
            if (string.IsNullOrWhiteSpace(txtProduto.Text))
            {
                MessageBox.Show("Informe o nome do produto.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUnidadeMedida.Text) || string.IsNullOrWhiteSpace(txtCodUnidadeMedida.Text))
            {
                MessageBox.Show("Selecione a unidade de medida.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrecoVenda.Text))
            {
                MessageBox.Show("Informe o preço de venda.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCategoria.Text) || string.IsNullOrWhiteSpace(txtCodCategoria.Text))
            {
                MessageBox.Show("Selecione a categoria.");
                return false;
            }

            if (listFornecedoresProduto.Items.Count == 0)
            {
                MessageBox.Show("Adicione ao menos um fornecedor para o produto.");
                return false;
            }

            return true;
        }

        public void LimparFormulario()
        {
            txtCodigo.Clear();
            txtProduto.Clear();

            txtCodUnidadeMedida.Clear();
            txtUnidadeMedida.Clear();

            txtEstoque.Clear();
            txtPrecoCusto.Clear();
            txtPrecoVenda.Clear();
            txtUltCompra.Clear();

            txtCodCategoria.Clear();
            txtCategoria.Clear();

            txtCodMarca.Clear();
            txtMarca.Clear();

            txtCodFornecedor.Clear();
            txtFornecedor.Clear();

            txtObser.Clear();

            checkAtivo.Checked = true;
            checkAtivo.Enabled = false;

            labelCriacao.Text = "";
            lblAtualizacao.Text = "";

            isEditando = false;
            isExcluindo = false;
        }
        public void CarregarDados(Produtos produtoSelecionado, bool isEditandoForm, bool isExcluindoForm)
        {
            produto = produtoSelecionado;
            isEditando = isEditandoForm;
            isExcluindo = isExcluindoForm;

            txtCodigo.Text = produto.Id.ToString();
            txtProduto.Text = produto.Nome;

            txtCodUnidadeMedida.Text = produto.IdUnidadeMedida.ToString();
            var unidade = unidadeMedidaServices.BuscarPorId(produto.IdUnidadeMedida);
            txtUnidadeMedida.Text = unidade != null ? unidade.Descricao : "Desconhecida";

            txtEstoque.Text = produto.Estoque.ToString();
            txtPrecoCusto.Text = produto.PrecoCusto.ToString("C2", new CultureInfo("pt-BR"));
            txtUltCompra.Text = produto.CustoUltimaCompra.HasValue
                              ? produto.CustoUltimaCompra.Value.ToString("C2", new CultureInfo("pt-BR"))
                              : 0m.ToString("C2", new CultureInfo("pt-BR"));
            txtPrecoVenda.Text = produto.PrecoVenda.ToString("C2", new CultureInfo("pt-BR"));



            txtCodCategoria.Text = produto.IdCategoria.ToString();
            var categoria = categoriaServices.BuscarPorId(produto.IdCategoria);
            txtCategoria.Text = categoria != null ? categoria.Descricao : "Desconhecida";

            txtCodMarca.Text = produto.IdMarca.HasValue ? produto.IdMarca.Value.ToString() : string.Empty;
            Marca marca = null;
            if (produto.IdMarca.HasValue)
                marca = marcaServices.BuscarPorId(produto.IdMarca.Value);

            txtMarca.Text = marca != null ? marca.Descricao : string.Empty;

            txtFornecedor.ReadOnly = isExcluindoForm;
            txtCodFornecedor.ReadOnly = isExcluindoForm;

            txtObser.Text = produto.Observacao;
            checkAtivo.Checked = produto.Ativo;
            CalcularMargemLucro();
            checkAtivo.Enabled = isEditandoForm;

            labelCriacao.Text = produto.DataCriacao.ToString("dd/MM/yyyy");
            lblAtualizacao.Text = produto.DataAtualizacao.ToString("dd/MM/yyyy");

            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";

            if (isExcluindo)
                BloquearCampos();
            else
                DesbloquearCampos();
            listFornecedoresProduto.Enabled = !isExcluindo;
            CarregarFornecedoresDoProduto(produto.Id);

        }
        public void BloquearCampos()
        {
            txtCodigo.Enabled = false;
            txtProduto.Enabled = false;

            txtCodUnidadeMedida.Enabled = false;
            txtUnidadeMedida.Enabled = false;
            txtMargemLucro.Enabled = false;

            txtEstoque.Enabled = false;
            txtPrecoCusto.Enabled = false;
            txtPrecoVenda.Enabled = false;
            txtUltCompra.Enabled = false;

            txtCodCategoria.Enabled = false;
            txtCategoria.Enabled = false;

            txtCodMarca.Enabled = false;
            txtMarca.Enabled = false;

            txtCodFornecedor.Enabled = false;
            txtFornecedor.Enabled = false;

            txtObser.Enabled = false;

            btnMedida.Enabled = false;
            btnCategoria.Enabled = false;
            btnMarca.Enabled = false;
            btnFornecedor.Enabled = false;
            btnRemover.Enabled = false;
        }
        public void DesbloquearCampos()
        {
            txtCodigo.Enabled = false;
            txtProduto.Enabled = true;

            txtCodUnidadeMedida.Enabled = false;
            txtUnidadeMedida.Enabled = false;
            txtMargemLucro.Enabled = false;

            txtEstoque.Enabled = false;
            txtPrecoCusto.Enabled = false;
            txtPrecoVenda.Enabled = true;
            txtUltCompra.Enabled = false;

            txtCodCategoria.Enabled = false;
            txtCategoria.Enabled = false;

            txtCodMarca.Enabled = false;
            txtMarca.Enabled = false;

            txtCodFornecedor.Enabled = false;
            txtFornecedor.Enabled = false;

            txtObser.Enabled = true;

            btnMedida.Enabled = true;
            btnCategoria.Enabled = true;
            btnMarca.Enabled = true;
            btnFornecedor.Enabled = true;
        }

        private void FrmCadastroProduto_Load(object sender, EventArgs e)
        {
            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";
            labelCriacao.Text = produto.DataCriacao > DateTime.MinValue ? produto.DataCriacao.ToShortDateString() : "";
            lblAtualizacao.Text = produto.DataAtualizacao > DateTime.MinValue ? produto.DataAtualizacao.ToShortDateString() : "";

            listFornecedoresProduto.View = View.Details;
            listFornecedoresProduto.FullRowSelect = true;
            listFornecedoresProduto.GridLines = true;

            listFornecedoresProduto.Columns.Add("Código", 80);
            listFornecedoresProduto.Columns.Add("Fornecedor", 250);
            listFornecedoresProduto.Columns.Add("Tipo", 120);

            listFornecedoresProduto.Items.Clear();

            CarregarFornecedoresDoProduto(produto.Id);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {

            try
            {
                if (isExcluindo)
                {
                    if (produto != null && produto.Id != 0)
                    {
                        if (MessageBox.Show("Deseja realmente remover este produto?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            try
                            {
                                servicoProduto.Remover(produto.Id);
                                MessageBox.Show("Produto removido com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LimparFormulario();
                                Sair();
                            }
                            catch (Exception ex)
                            {
                                if (ex.Message.Contains("FK_ItensCompra_Produtos"))
                                {
                                    MessageBox.Show(
                                        "Não é possível remover este produto, pois ele está vinculado a uma ou mais compras.",
                                        "Aviso",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    MessageBox.Show($"Erro ao remover o produto: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    return;
                }

                if (!ValidarEntrada())
                {
                    return;
                }

                if (new BaseDao<Produtos>("Produtos").VerificarDuplicidade("Nome", txtProduto.Text.Trim(), produto))
                {
                    MessageBox.Show("Já existe um produto com esse nome cadastrado.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                AtualizarObjeto();

                if (isEditando)
                {
                    servicoProduto.Atualizar(produto);
                }
                else
                {
                    servicoProduto.Criar(produto);
                }

                var produtoFornecedorDao = new ProjetoPF.Dao.Compra.ProdutoFornecedorDAO();
                var atuais = produtoFornecedorDao.ListarPorProduto(produto.Id);

                foreach (var forn in fornecedoresSelecionados)
                {
                    bool jaExiste = atuais.Any(a => a.IdFornecedor == forn.IdFornecedor);
                    if (!jaExiste)
                    {
                        var associacao = new ProjetoPF.Modelos.Compra.ProdutoFornecedor
                        {
                            IdProduto = produto.Id == 0 ? servicoProduto.BuscarUltimoId() : produto.Id,
                            IdFornecedor = forn.IdFornecedor,
                            PrecoUltimaCompra = forn.PrecoUltimaCompra,
                            DataUltimaCompra = forn.DataUltimaCompra,
                            DataCriacao = DateTime.Now,
                            DataAtualizacao = DateTime.Now,
                            Ativo = true
                        };

                        produtoFornecedorDao.CriarOuAtualizarProdutoFornecedor(associacao);
                    }
                }

      
                foreach (var antigo in atuais)
                {
                    bool aindaExiste = fornecedoresSelecionados.Any(f => f.IdFornecedor == antigo.IdFornecedor);
                    if (!aindaExiste)
                    {
                        if (produtoFornecedorDao.ExisteCompraVinculada(produto.Id, antigo.IdFornecedor))
                        {

                            var daoFornecedor = new BaseDao<Fornecedor>("Fornecedores");
                            var nomeForn = daoFornecedor.BuscarPorId(antigo.IdFornecedor)?.NomeRazaoSocial ?? "Fornecedor";

                            MessageBox.Show(
                                $"O fornecedor '{nomeForn}' não pôde ser removido pois há compras vinculadas.",
                                "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            continue;
                        }
                        produtoFornecedorDao.RemoverVinculo(produto.Id, antigo.IdFornecedor);
                    }
                }

                MessageBox.Show(isEditando
                    ? "Produto atualizado com sucesso!"
                    : "Produto cadastrado com sucesso!",
                    "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LimparFormulario();
                Sair();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE"))
                {
                    MessageBox.Show("Não é possível remover o produto, pois ele está vinculado a outro(s) registro(s).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Erro ao salvar ou atualizar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void AtualizarObjeto()
        {
            produto.Nome = txtProduto.Text.Trim();

            if (!int.TryParse(txtCodUnidadeMedida.Text, out int idUnidade))
                throw new Exception("Código da unidade de medida inválido.");
            produto.IdUnidadeMedida = idUnidade;

            if (!int.TryParse(txtCodCategoria.Text, out int idCategoria))
                throw new Exception("Código da categoria inválido.");
            produto.IdCategoria = idCategoria;

            if (int.TryParse(txtCodMarca.Text, out int idMarca))
                produto.IdMarca = idMarca;
            else
                produto.IdMarca = null;

            string estoqueStr = txtEstoque.Text.Replace("R$", "").Trim();
            if (!decimal.TryParse(estoqueStr, out decimal estoque))
                estoque = 0;
            produto.Estoque = estoque;

            string precoVendaStr = txtPrecoVenda.Text.Replace("R$", "").Trim();
            if (!decimal.TryParse(precoVendaStr, out decimal precoVenda))
                throw new Exception("Preço de venda inválido.");
            produto.PrecoVenda = precoVenda;

            string ultimaCompraStr = txtUltCompra.Text.Replace("R$", "").Trim();
            if (decimal.TryParse(ultimaCompraStr, NumberStyles.Currency, new CultureInfo("pt-BR"), out decimal custoUltima))
                produto.CustoUltimaCompra = custoUltima;
            else
                produto.CustoUltimaCompra = null;

            produto.Observacao = txtObser.Text.Trim();
            produto.Ativo = checkAtivo.Checked;

            produto.Id = (isEditando || isExcluindo) ? int.Parse(txtCodigo.Text) : 0;
            produto.DataCriacao = produto.DataCriacao == DateTime.MinValue ? DateTime.Now : produto.DataCriacao;
            produto.DataAtualizacao = DateTime.Now;
        }

        private void btnMedida_Click(object sender, EventArgs e)
        {
            FrmConsultaUnidadeMedida frm = new FrmConsultaUnidadeMedida();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void btnCategoria_Click(object sender, EventArgs e)
        {
            FrmConsultaCategoria frm = new FrmConsultaCategoria();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void btnMarca_Click(object sender, EventArgs e)
        {
            FrmConsultaMarca frm = new FrmConsultaMarca();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void btnFornecedor_Click(object sender, EventArgs e)
        {
            var frm = new FrmConsultaFornecedor();
            frm.Owner = this;
            frm.ShowDialog();

            if (frm.FornecedorSelecionado != null)
            {
                int idFornecedor = frm.FornecedorSelecionado.Id;

                if (fornecedoresSelecionados.Any(f => f.IdFornecedor == idFornecedor))
                {
                    MessageBox.Show("Este fornecedor já foi adicionado a este produto.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                fornecedoresSelecionados.Add(new ProdutoFornecedor
                {
                    IdProduto = produto.Id, 
                    IdFornecedor = idFornecedor,
                });

                AtualizarListViewFornecedores();
            }
        }
        private void AtualizarListViewFornecedores()
        {
            listFornecedoresProduto.Items.Clear();
            var daoFornecedor = new BaseDao<Fornecedor>("Fornecedores");

            foreach (var pf in fornecedoresSelecionados)
            {
                var fornecedor = daoFornecedor.BuscarPorId(pf.IdFornecedor);
                if (fornecedor != null)
                {
                    var item = new ListViewItem(fornecedor.Id.ToString());
                    item.SubItems.Add(fornecedor.NomeRazaoSocial);
                    item.SubItems.Add(fornecedor.TipoPessoa == "F" ? "Física" : "Jurídica");
                    listFornecedoresProduto.Items.Add(item);
                }
            }
        }

        private void SomenteNumerosPontuacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = sender as TextBox;
            if (char.IsControl(e.KeyChar) || char.IsDigit(e.KeyChar))
                return;

            if ((e.KeyChar == ',' || e.KeyChar == '.') && !txt.Text.Contains(',') && !txt.Text.Contains('.'))
                return;
            e.Handled = true;
        }
        private void CalcularMargemLucro()
        {
            bool custoOk = decimal.TryParse(txtPrecoCusto.Text.Replace("R$", "").Trim(), NumberStyles.Any, new CultureInfo("pt-BR"), out decimal precoCusto);
            bool vendaOk = decimal.TryParse(txtPrecoVenda.Text.Replace("R$", "").Trim(), NumberStyles.Any, new CultureInfo("pt-BR"), out decimal precoVenda);

            if (custoOk && vendaOk && precoVenda > 0)
            {
                decimal lucroBruto = precoVenda - precoCusto;
                txtMargemLucro.Text = lucroBruto.ToString("C2", new CultureInfo("pt-BR"));
            }
            else
            {
                txtMargemLucro.Text = "R$ 0,00";
            }

        }

        private void txtMargemLucro_TextChanged(object sender, EventArgs e)
        {
            CalcularMargemLucro();
        }

        private void txtPrecoVenda_TextChanged(object sender, EventArgs e)
        {
            CalcularMargemLucro();
        }
        private void CarregarFornecedoresDoProduto(int idProduto)
        {
            try
            {
                fornecedoresSelecionados.Clear(); 
                listFornecedoresProduto.Items.Clear();

                if (idProduto <= 0)
                    return;

                var daoProdutoFornecedor = new ProdutoFornecedorDAO();
                var lista = daoProdutoFornecedor.ListarPorProduto(idProduto);

                var daoFornecedor = new BaseDao<Fornecedor>("Fornecedores");

                foreach (var pf in lista)
                {
                    var fornecedor = daoFornecedor.BuscarPorId(pf.IdFornecedor);

                    if (fornecedor != null)
                    {
                        fornecedoresSelecionados.Add(new ProdutoFornecedor
                        {
                            IdProduto = idProduto,
                            IdFornecedor = fornecedor.Id,
                            PrecoUltimaCompra = pf.PrecoUltimaCompra,
                            DataUltimaCompra = pf.DataUltimaCompra,
                        });

                        var item = new ListViewItem(fornecedor.Id.ToString());
                        item.SubItems.Add(fornecedor.NomeRazaoSocial);
                        string tipo = fornecedor.TipoPessoa == "F" ? "FÍSICA" : "JURÍDICA";
                        item.SubItems.Add(tipo);
                        listFornecedoresProduto.Items.Add(item);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar fornecedores do produto: " + ex.Message,
                                "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if (listFornecedoresProduto.SelectedItems.Count == 0)
                return;

            var itemSelecionado = listFornecedoresProduto.SelectedItems[0];
            int idFornecedor = int.Parse(itemSelecionado.SubItems[0].Text);

            if (MessageBox.Show("Deseja realmente remover este fornecedor da lista?",
                "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            var fornecedorARemover = fornecedoresSelecionados
                .FirstOrDefault(f => f.IdFornecedor == idFornecedor);

            if (fornecedorARemover != null)
                fornecedoresSelecionados.Remove(fornecedorARemover);

            listFornecedoresProduto.Items.Remove(itemSelecionado);
        }
    }
}
