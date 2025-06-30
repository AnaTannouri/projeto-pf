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

            if (string.IsNullOrWhiteSpace(txtFornecedor.Text) || string.IsNullOrWhiteSpace(txtCodFornecedor.Text))
            {
                MessageBox.Show("Selecione o fornecedor.");
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
            txtPrecoVenda.Text = produto.PrecoVenda.ToString("C2", new CultureInfo("pt-BR"));
            txtUltCompra.Text = produto.CustoUltimaCompra.HasValue
                              ? produto.CustoUltimaCompra.Value.ToString("C2", new CultureInfo("pt-BR"))
                              : 0m.ToString("C2", new CultureInfo("pt-BR"));

            txtCodCategoria.Text = produto.IdCategoria.ToString();
            var categoria = categoriaServices.BuscarPorId(produto.IdCategoria);
            txtCategoria.Text = categoria != null ? categoria.Descricao : "Desconhecida";

            txtCodMarca.Text = produto.IdMarca.HasValue ? produto.IdMarca.Value.ToString() : string.Empty;
            Marca marca = null;
            if (produto.IdMarca.HasValue)
                marca = marcaServices.BuscarPorId(produto.IdMarca.Value);
            txtMarca.Text = marca != null ? marca.Descricao : "Desconhecida";

            txtCodFornecedor.Text = produto.IdFornecedor.ToString();
            var fornecedor = fornecedorServicos.BuscarPorId(produto.IdFornecedor);
            txtFornecedor.Text = fornecedor != null ? fornecedor.NomeRazaoSocial : "Desconhecido";

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
            labelCriacao.Text = produto.DataCriacao.ToShortDateString();
            lblAtualizacao.Text = produto.DataAtualizacao.ToShortDateString();
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
                            servicoProduto.Remover(produto.Id);
                            MessageBox.Show("Produto removido com sucesso!");
                            LimparFormulario();
                            Sair();
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
                    MessageBox.Show("Produto atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    servicoProduto.Criar(produto);
                    MessageBox.Show("Produto cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

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

            if (!int.TryParse(txtCodFornecedor.Text, out int idFornecedor))
                throw new Exception("Código do fornecedor inválido.");
            produto.IdFornecedor = idFornecedor;

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
            FrmConsultaFornecedor frm = new FrmConsultaFornecedor();
            frm.Owner = this;
            frm.ShowDialog();
        }

        private void SomenteNumerosPontuacao_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) &&
                !char.IsDigit(e.KeyChar) &&
                e.KeyChar != '.' &&
                e.KeyChar != '-' &&
                e.KeyChar != '/')
            {
                e.Handled = true;
            }
        }
        private void CalcularMargemLucro()
        {
            bool custoOk = decimal.TryParse(txtPrecoCusto.Text.Replace("R$", "").Trim(), NumberStyles.Any, new CultureInfo("pt-BR"), out decimal precoCusto);
            bool vendaOk = decimal.TryParse(txtPrecoVenda.Text.Replace("R$", "").Trim(), NumberStyles.Any, new CultureInfo("pt-BR"), out decimal precoVenda);

            if (custoOk && vendaOk && precoCusto > 0)
            {
                decimal margem = ((precoVenda - precoCusto) / precoCusto) * 100;
                txtMargemLucro.Text = margem.ToString("F2") + " %";
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
    }
}
