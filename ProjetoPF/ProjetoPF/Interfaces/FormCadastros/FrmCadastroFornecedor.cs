using ProjetoPF.Dao;
using ProjetoPF.FormCadastros;
using ProjetoPF.Modelos.Localizacao;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Modelos.Pessoa;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroFornecedor : ProjetoPF.Interfaces.FormCadastros.FrmCadastroPessoa
    {
        private Fornecedor fornecedor = new Fornecedor();
        private BaseServicos<Fornecedor> fornecedorServicos = new BaseServicos<Fornecedor>(new BaseDao<Fornecedor>("Fornecedores"));

        private FrmCadastroFormaPagamento frmCadastroFormaPagamento = new FrmCadastroFormaPagamento();
        private BaseServicos<FormaPagamento> formaServices = new BaseServicos<FormaPagamento>(new BaseDao<FormaPagamento>("FormaPagamentos"));

        private FrmCadastroCondPagamento frmCadastroCondPagamento = new FrmCadastroCondPagamento();
        private BaseServicos<CondicaoPagamento> condicacaoServices = new BaseServicos<CondicaoPagamento>(new BaseDao<CondicaoPagamento>("CondicaoPagamentos"));

        private BaseServicos<Cidade> cidadeServices = new BaseServicos<Cidade>(new BaseDao<Cidade>("Cidades"));

        private bool carregandoFormas = false;
        private bool carregandoCondicoes = false;
        private bool carregandoCidades = false;

        private bool isEditando = false;
        private bool isExcluindo = false;
        public FrmCadastroFornecedor()
        {
            InitializeComponent();
        }

        private bool ValidarEntrada()
        {
            if (string.IsNullOrWhiteSpace(txtNome_RazaoSocial.Text))
            {
                MessageBox.Show("Informe o nome ou razão social do fornecedor.");
                return false;
            }

            if (!checkEstrangeiro.Checked && string.IsNullOrWhiteSpace(txtCpf_Cnpj.Text))
            {
                MessageBox.Show("Informe o CPF/CNPJ para fornecedor não estrangeiros.");
                return false;
            }

            if (checkEstrangeiro.Checked && string.IsNullOrWhiteSpace(txtRg_InscricaoEstadual.Text))
            {
                MessageBox.Show("Informe o RG ou Inscrição Estadual para fornecedores estrangeiros.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Informe o e-mail do fornecedor.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                MessageBox.Show("Informe o telefone do fornecedor.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtRua.Text))
            {
                MessageBox.Show("Informe o nome da rua do fornecedor.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show("Informe o número da residência do fornecedor.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBairro.Text))
            {
                MessageBox.Show("Informe o bairro do fornecedor.");
                return false;
            }

            if (comboCidade.SelectedItem == null)
            {
                MessageBox.Show("Selecione a cidade do fornecedor.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCep.Text))
            {
                MessageBox.Show("Informe o CEP do fornecedor.");
                return false;
            }

            if (comboClassificacao.SelectedItem == null)
            {
                MessageBox.Show("Selecione a classificação do fornecedor.");
                return false;
            }

            if (comboFormas.SelectedItem == null)
            {
                MessageBox.Show("Selecione a forma de pagamento do fornecedor.");
                return false;
            }

            if (comboCondicoes.SelectedItem == null)
            {
                MessageBox.Show("Selecione a condição de pagamento do fornecedor.");
                return false;
            }

            if (checkEstrangeiro.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtRg_InscricaoEstadual.Text))
                {
                    MessageBox.Show("Informe o RG do fornecedor estrangeiro.");
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtCpf_Cnpj.Text))
                {
                    MessageBox.Show("Informe o CPF do fornecedor.");
                    return false;
                }
            }

            return true;
        }
        public void CarregarCidades(int? idSelecionado = null)
        {
            try
            {
                carregandoCidades = true;

                comboCidade.DataSource = null;
                comboCidade.Items.Clear();

                var listaCidades = cidadeServices.BuscarTodos();

                if (listaCidades != null && listaCidades.Any())
                {
                    comboCidade.DataSource = listaCidades;
                    comboCidade.DisplayMember = "Nome";
                    comboCidade.ValueMember = "Id";

                    if (idSelecionado.HasValue)
                    {
                        comboCidade.SelectedValue = idSelecionado.Value;
                        txtCodigoCidade.Text = idSelecionado.Value.ToString();
                    }
                    else
                    {
                        comboCidade.SelectedIndex = -1;
                        txtCodigoCidade.Clear();
                    }
                }
                else
                {
                    comboCidade.SelectedIndex = -1;
                    txtCodigoCidade.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar cidades: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                carregandoCidades = false;
            }
        }

        public void CarregarFormasPagamento(int? idSelecionado = null)
        {
            try
            {
                carregandoFormas = true;

                comboFormas.DataSource = null;
                comboFormas.Items.Clear();

                var listaFormas = formaServices.BuscarTodos();

                if (listaFormas != null && listaFormas.Any())
                {
                    comboFormas.DataSource = listaFormas;
                    comboFormas.DisplayMember = "Descricao";
                    comboFormas.ValueMember = "Id";

                    if (idSelecionado.HasValue)
                    {
                        comboFormas.SelectedValue = idSelecionado.Value;
                        txtCodigoForma.Text = idSelecionado.Value.ToString();
                    }
                    else
                    {
                        comboFormas.SelectedIndex = -1;
                        txtCodigoForma.Clear();
                    }
                }
                else
                {
                    comboFormas.SelectedIndex = -1;
                    txtCodigoForma.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar formas de pagamento: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                carregandoFormas = false;
            }
        }
        public void CarregarCondicoesPagamento(int? idSelecionado = null)
        {
            try
            {
                carregandoCondicoes = true;

                comboCondicoes.DataSource = null;
                comboCondicoes.Items.Clear();

                var listaCondicoes = condicacaoServices.BuscarTodos();

                if (listaCondicoes != null && listaCondicoes.Any())
                {
                    comboCondicoes.DataSource = listaCondicoes;
                    comboCondicoes.DisplayMember = "Descricao";
                    comboCondicoes.ValueMember = "Id";

                    if (idSelecionado.HasValue)
                    {
                        comboCondicoes.SelectedValue = idSelecionado.Value;
                        txtCodigoCondicao.Text = idSelecionado.Value.ToString();
                    }
                    else
                    {
                        comboCondicoes.SelectedIndex = -1;
                        txtCodigoCondicao.Clear();
                    }
                }
                else
                {
                    comboCondicoes.SelectedIndex = -1;
                    txtCodigoCondicao.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar condição de pagamento: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                carregandoCondicoes = false;
            }
        }
        public void LimparFormulario()
        {
            txtCodigo.Clear();
            txtNome_RazaoSocial.Clear();
            txtApelido_NomeFantasia.Clear();
            txtCpf_Cnpj.Clear();
            txtRg_InscricaoEstadual.Clear();
            txtEmail.Clear();
            txtTelefone.Clear();
            txtRua.Clear();
            txtNumero.Clear();
            txtBairro.Clear();
            txtCep.Clear();
            txtCodigoCidade.Clear();
            txtCodigoForma.Clear();
            txtCodigoCondicao.Clear();
            txtValorMin.Clear(); 

            checkEstrangeiro.Checked = false;
            dateTimePicker1.Value = DateTime.Now;

            comboPessoa.SelectedIndex = -1;
            comboClassificacao.SelectedIndex = -1;
            comboCidade.SelectedIndex = -1;
            comboFormas.SelectedIndex = -1;
            comboCondicoes.SelectedIndex = -1;

            isEditando = false;
            isExcluindo = false;
        }

        public void CarregarDados(Fornecedor fornecedorSelecionado, bool isEditandoForm, bool isExcluindoForm)
        {
            fornecedor = fornecedorSelecionado;
            isEditando = isEditandoForm;
            isExcluindo = isExcluindoForm;

            txtCodigo.Text = fornecedor.Id.ToString();
            txtNome_RazaoSocial.Text = fornecedor.NomeRazaoSocial;
            txtApelido_NomeFantasia.Text = fornecedor.ApelidoNomeFantasia;
            txtCpf_Cnpj.Text = fornecedor.CpfCnpj;
            txtRg_InscricaoEstadual.Text = fornecedor.RgInscricaoEstadual;
            txtEmail.Text = fornecedor.Email;
            txtTelefone.Text = fornecedor.Telefone;
            txtRua.Text = fornecedor.Rua;
            txtNumero.Text = fornecedor.Numero;
            txtBairro.Text = fornecedor.Bairro;
            txtCep.Text = fornecedor.Cep;
            comboPessoa.SelectedItem = fornecedor.TipoPessoa;
            comboClassificacao.SelectedItem = fornecedor.Classificacao;
            checkEstrangeiro.Checked = fornecedor.Estrangeiro;
            dateTimePicker1.Value = fornecedor.DataNascimentoCriacao;

            txtValorMin.Text = fornecedor.ValorMinimoPedido.HasValue
                ? fornecedor.ValorMinimoPedido.Value.ToString("N2")
                : string.Empty;

            CarregarCidades(fornecedor.IdCidade);
            CarregarFormasPagamento(fornecedor.FormaPagamentoId);
            CarregarCondicoesPagamento(fornecedor.CondicaoPagamentoId);

            comboCidade.SelectedValue = fornecedor.IdCidade;
            txtCodigoCidade.Text = fornecedor.IdCidade.ToString();

            comboFormas.SelectedValue = fornecedor.FormaPagamentoId;
            comboCondicoes.SelectedValue = fornecedor.CondicaoPagamentoId;

            txtCodigoForma.Text = fornecedor.FormaPagamentoId.ToString();
            txtCodigoCondicao.Text = fornecedor.CondicaoPagamentoId.ToString();

            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";

            if (isExcluindo)
                BloquearCampos();
            else
                DesbloquearCampos();
        }

        public void BloquearCampos()
        {
            txtCodigo.Enabled = false;
            txtNome_RazaoSocial.Enabled = false;
            txtApelido_NomeFantasia.Enabled = false;
            txtCpf_Cnpj.Enabled = false;
            txtRg_InscricaoEstadual.Enabled = false;
            txtEmail.Enabled = false;
            txtTelefone.Enabled = false;
            txtRua.Enabled = false;
            txtNumero.Enabled = false;
            txtBairro.Enabled = false;
            txtCep.Enabled = false;
            txtCodigoCidade.Enabled = false;
            txtCodigoForma.Enabled = false;
            txtCodigoCondicao.Enabled = false;
            txtValorMin.Enabled = false; 

            comboPessoa.Enabled = false;
            comboClassificacao.Enabled = false;
            comboCidade.Enabled = false;
            comboFormas.Enabled = false;
            comboCondicoes.Enabled = false;

            dateTimePicker1.Enabled = false;
            checkEstrangeiro.Enabled = false;

            button1.Enabled = false;
            button2.Enabled = false;
            btnCadastrarCidade.Enabled = false;
        }

        public void DesbloquearCampos()
        {
            txtCodigo.Enabled = false;
            txtNome_RazaoSocial.Enabled = true;
            txtCpf_Cnpj.Enabled = true;
            txtRg_InscricaoEstadual.Enabled = true;
            txtEmail.Enabled = true;
            txtTelefone.Enabled = true;
            txtRua.Enabled = true;
            txtNumero.Enabled = true;
            txtBairro.Enabled = true;
            txtCep.Enabled = true;
            txtValorMin.Enabled = true; 
        }

        private void FrmCadastroFornecedor_Load(object sender, EventArgs e)
        {
            if (comboFormas.Items.Count == 0)
                CarregarFormasPagamento();

            if (comboCondicoes.Items.Count == 0)
                CarregarCondicoesPagamento();

            if (comboCidade.Items.Count == 0)
                CarregarCidades();

            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";
        }

        private void comboFormas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (carregandoFormas) return;

            if (comboFormas.SelectedItem is FormaPagamento formaPagamento)
            {
                txtCodigoForma.Text = formaPagamento.Id.ToString();
            }
            else
            {
                txtCodigoForma.Clear();
            }
        }

        private void comboCondicoes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (carregandoCondicoes) return;

            if (comboCondicoes.SelectedItem is CondicaoPagamento condicaoPagamento)
            {
                txtCodigoCondicao.Text = condicaoPagamento.Id.ToString();
            }
            else
            {
                txtCodigoCondicao.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmCadastroCondPagamento frmCadastroCondPagamento = new FrmCadastroCondPagamento();
            frmCadastroCondPagamento.FormClosed += (s, args) =>
            {
                CarregarCondicoesPagamento(null);

                var listaAtualizada = (List<CondicaoPagamento>)comboCondicoes.DataSource;

                if (listaAtualizada != null && listaAtualizada.Any())
                {
                    var ultimaCondicao = listaAtualizada.OrderByDescending(p => p.Id).FirstOrDefault();

                    if (ultimaCondicao != null)
                    {
                        comboCondicoes.SelectedValue = ultimaCondicao.Id;
                        txtCodigoCondicao.Text = ultimaCondicao.Id.ToString();
                    }
                }
            };

            frmCadastroCondPagamento.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmCadastroFormaPagamento frmCadastroFormaPagamento = new FrmCadastroFormaPagamento();
            frmCadastroFormaPagamento.FormClosed += (s, args) =>
            {
                CarregarFormasPagamento(null);

                var listaAtualizada = (List<FormaPagamento>)comboFormas.DataSource;

                if (listaAtualizada != null && listaAtualizada.Any())
                {
                    var ultimaForma = listaAtualizada.OrderByDescending(p => p.Id).FirstOrDefault();

                    if (ultimaForma != null)
                    {
                        comboFormas.SelectedValue = ultimaForma.Id;
                        txtCodigoForma.Text = ultimaForma.Id.ToString();
                    }
                }
            };

            frmCadastroFormaPagamento.ShowDialog();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                if (isExcluindo)
                {
                    if (fornecedor != null && fornecedor.Id != 0)
                    {
                        if (MessageBox.Show("Deseja realmente excluir este fornecedor?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            fornecedorServicos.Remover(fornecedor.Id);
                            MessageBox.Show("Fornecedor removido com sucesso!");
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

                AtualizarObjeto();

                if (isEditando)
                {
                    AtualizarObjeto();
                    fornecedorServicos.Atualizar(fornecedor);
                    MessageBox.Show("Fornecedor atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    fornecedorServicos.Criar(fornecedor);
                    MessageBox.Show("Fornecedor cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimparFormulario();
                Sair();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE"))
                {
                    MessageBox.Show("Não é possível excluir o fornecedor, pois ele está vinculado a outro(s) registro(s).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Erro ao salvar ou atualizar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void AtualizarObjeto()
        {
            fornecedor.TipoPessoa = comboPessoa.Text;
            fornecedor.NomeRazaoSocial = txtNome_RazaoSocial.Text.Trim();
            fornecedor.ApelidoNomeFantasia = txtApelido_NomeFantasia.Text.Trim();
            fornecedor.CpfCnpj = checkEstrangeiro.Checked ? null : txtCpf_Cnpj.Text.Trim();
            fornecedor.RgInscricaoEstadual = txtRg_InscricaoEstadual.Text.Trim();
            fornecedor.Email = txtEmail.Text.Trim();
            fornecedor.Telefone = txtTelefone.Text.Trim();
            fornecedor.Rua = txtRua.Text.Trim();
            fornecedor.Numero = txtNumero.Text.Trim();
            fornecedor.Bairro = txtBairro.Text.Trim();
            fornecedor.Cep = txtCep.Text.Trim();
            fornecedor.Classificacao = comboClassificacao.SelectedItem?.ToString();
            fornecedor.Estrangeiro = checkEstrangeiro.Checked;
            fornecedor.DataNascimentoCriacao = dateTimePicker1.Value;

            if (comboCidade.SelectedItem is Cidade cidadeSelecionada)
                fornecedor.IdCidade = cidadeSelecionada.Id;
            else
                throw new Exception("Nenhuma cidade foi selecionada.");

            if (comboFormas.SelectedItem is FormaPagamento forma)
                fornecedor.FormaPagamentoId = forma.Id;

            if (comboCondicoes.SelectedItem is CondicaoPagamento condicao)
                fornecedor.CondicaoPagamentoId = condicao.Id;

            fornecedor.Id = (isEditando || isExcluindo) ? int.Parse(txtCodigo.Text) : 0;

            fornecedor.DataCriacao = fornecedor.DataCriacao == DateTime.MinValue ? DateTime.Now : fornecedor.DataCriacao;
            fornecedor.DataAtualizacao = DateTime.Now;

            if (decimal.TryParse(txtValorMin.Text, out var valor))
                fornecedor.ValorMinimoPedido = valor;
            else
                fornecedor.ValorMinimoPedido = null;
        }

        private void btnCadastrarCidade_Click(object sender, EventArgs e)
        {

            FrmCadastroCidade frmCadastroCidade = new FrmCadastroCidade();
            frmCadastroCidade.FormClosed += (s, args) =>
            {
                CarregarCidades(null);

                var listaAtualizada = (List<Cidade>)comboCidade.DataSource;

                if (listaAtualizada != null && listaAtualizada.Any())
                {
                    var ultimaCidade = listaAtualizada.OrderByDescending(p => p.Id).FirstOrDefault();

                    if (ultimaCidade != null)
                    {
                        comboCidade.SelectedValue = ultimaCidade.Id;
                        txtCodigoCidade.Text = ultimaCidade.Id.ToString();
                    }
                }
            };

            frmCadastroCidade.ShowDialog();
        }

        private void comboCidade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (carregandoCidades) return;

            if (comboCidade.SelectedItem is Cidade cidade)
            {
                txtCodigoCidade.Text = cidade.Id.ToString();
            }
            else
            {
                txtCodigoCidade.Clear();
            }

        }
    }
}
