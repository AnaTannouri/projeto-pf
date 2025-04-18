﻿using ProjetoPF.Dao;
using ProjetoPF.FormCadastros;
using ProjetoPF.Modelos.Localizacao;
using ProjetoPF.Modelos.Pagamento;
using ProjetoPF.Modelos.Pessoa;
using ProjetoPF.Servicos;
using ProjetoPF.Servicos.Localizacao;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ProjetoPF.Interfaces.FormCadastros
{
    public partial class FrmCadastroCliente : FrmCadastroPessoa
    {
        private Cliente cliente = new Cliente();
        private BaseServicos<Cliente> clienteServicos = new BaseServicos<Cliente>(new BaseDao<Cliente>("Clientes"));

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

        public FrmCadastroCliente()
        {
            InitializeComponent();
        }

        private bool ValidarEntrada()
        {
            if (string.IsNullOrWhiteSpace(txtNome_RazaoSocial.Text))
            {
                MessageBox.Show("Informe o nome ou razão social do cliente.");
                return false;
            }

            if (!checkEstrangeiro.Checked && string.IsNullOrWhiteSpace(txtCpf_Cnpj.Text))
            {
                MessageBox.Show("Informe o CPF/CNPJ para clientes não estrangeiros.");
                return false;
            }

            if (checkEstrangeiro.Checked && string.IsNullOrWhiteSpace(txtRg_InscricaoEstadual.Text))
            {
                MessageBox.Show("Informe o RG ou Inscrição Estadual para clientes estrangeiros.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Informe o e-mail do cliente.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTelefone.Text))
            {
                MessageBox.Show("Informe o telefone do cliente.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtRua.Text))
            {
                MessageBox.Show("Informe o nome da rua do cliente.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtNumero.Text))
            {
                MessageBox.Show("Informe o número da residência do cliente.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtBairro.Text))
            {
                MessageBox.Show("Informe o bairro do cliente.");
                return false;
            }

            if (comboCidade.SelectedItem == null)
            {
                MessageBox.Show("Selecione a cidade do cliente.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtCep.Text))
            {
                MessageBox.Show("Informe o CEP do cliente.");
                return false;
            }

            if (comboClassificacao.SelectedItem == null)
            {
                MessageBox.Show("Selecione a classificação do cliente.");
                return false;
            }

            if (comboFormas.SelectedItem == null)
            {
                MessageBox.Show("Selecione a forma de pagamento do cliente.");
                return false;
            }

            if (comboCondicoes.SelectedItem == null)
            {
                MessageBox.Show("Selecione a condição de pagamento do cliente.");
                return false;
            }

            if (checkEstrangeiro.Checked)
            {
                if (string.IsNullOrWhiteSpace(txtRg_InscricaoEstadual.Text))
                {
                    MessageBox.Show("Informe o RG do cliente estrangeiro.");
                    return false;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtCpf_Cnpj.Text))
                {
                    MessageBox.Show("Informe o CPF do cliente.");
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

        public void CarregarDados(Cliente clienteSelecionado, bool isEditandoForm, bool isExcluindoForm)
        {
            cliente = clienteSelecionado;
            isEditando = isEditandoForm;
            isExcluindo = isExcluindoForm;

            txtCodigo.Text = cliente.Id.ToString();
            txtNome_RazaoSocial.Text = cliente.NomeRazaoSocial;
            txtApelido_NomeFantasia.Text = cliente.ApelidoNomeFantasia;
            txtCpf_Cnpj.Text = cliente.CpfCnpj;
            txtRg_InscricaoEstadual.Text = cliente.RgInscricaoEstadual;
            txtEmail.Text = cliente.Email;
            txtTelefone.Text = cliente.Telefone;
            txtRua.Text = cliente.Rua;
            txtNumero.Text = cliente.Numero;
            txtBairro.Text = cliente.Bairro;
            txtCep.Text = cliente.Cep;
            comboPessoa.SelectedItem = cliente.TipoPessoa;
            comboClassificacao.SelectedItem = cliente.Classificacao;
            checkEstrangeiro.Checked = cliente.Estrangeiro;
            dateTimePicker1.Value = cliente.DataNascimentoCriacao;


            CarregarCidades(cliente.IdCidade);
            CarregarFormasPagamento(cliente.FormaPagamentoId);
            CarregarCondicoesPagamento(cliente.CondicaoPagamentoId);

            comboCidade.SelectedValue = cliente.IdCidade;
            txtCodigoCidade.Text = cliente.IdCidade.ToString();

            comboFormas.SelectedValue = cliente.FormaPagamentoId;
            comboCondicoes.SelectedValue = cliente.CondicaoPagamentoId;

            txtCodigoForma.Text = cliente.FormaPagamentoId.ToString();
            txtCodigoCondicao.Text = cliente.CondicaoPagamentoId.ToString();

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
        }

        private void FrmCadastroCliente_Load_1(object sender, EventArgs e)
        {
            if (comboFormas.Items.Count == 0)
                CarregarFormasPagamento();

            if (comboCondicoes.Items.Count == 0)
                CarregarCondicoesPagamento();

            if (comboCidade.Items.Count == 0)
                CarregarCidades();

            btnSalvar.Text = isExcluindo ? "Remover" : "Salvar";
        }

        private void comboFormas_SelectedIndexChanged_1(object sender, EventArgs e)
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

        private void btnSalvar_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (isExcluindo)
                {
                    if (cliente != null && cliente.Id != 0)
                    {
                        if (MessageBox.Show("Deseja realmente excluir este cliente?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            clienteServicos.Remover(cliente.Id);
                            MessageBox.Show("Cliente removido com sucesso!");
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
                    clienteServicos.Atualizar(cliente);
                    MessageBox.Show("Cliente atualizado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    clienteServicos.Criar(cliente);
                    MessageBox.Show("Cliente cadastrado com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                LimparFormulario();
                Sair();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE"))
                {
                    MessageBox.Show("Não é possível excluir o cliente, pois ele está vinculado a outro(s) registro(s).", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show($"Erro ao salvar ou atualizar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void AtualizarObjeto()
        {
            cliente.TipoPessoa = comboPessoa.Text;
            cliente.NomeRazaoSocial = txtNome_RazaoSocial.Text.Trim();
            cliente.ApelidoNomeFantasia = txtApelido_NomeFantasia.Text.Trim();
            cliente.CpfCnpj = checkEstrangeiro.Checked ? null : txtCpf_Cnpj.Text.Trim();
            cliente.RgInscricaoEstadual = txtRg_InscricaoEstadual.Text.Trim();
            cliente.Email = txtEmail.Text.Trim();
            cliente.Telefone = txtTelefone.Text.Trim();
            cliente.Rua = txtRua.Text.Trim();
            cliente.Numero = txtNumero.Text.Trim();
            cliente.Bairro = txtBairro.Text.Trim();
            cliente.Cep = txtCep.Text.Trim();
            cliente.Classificacao = comboClassificacao.SelectedItem?.ToString();
            cliente.Estrangeiro = checkEstrangeiro.Checked;
            cliente.DataNascimentoCriacao = dateTimePicker1.Value;

            if (comboCidade.SelectedItem is Cidade cidadeSelecionada)
            {
                cliente.IdCidade = cidadeSelecionada.Id;
            }
            else
            {
                throw new Exception("Nenhuma cidade foi selecionada.");
            }

            if (comboFormas.SelectedItem is FormaPagamento forma)
            {
                cliente.FormaPagamentoId = forma.Id;
            }
            else
            {
                cliente.FormaPagamentoId.ToString();
            }

            if (comboCondicoes.SelectedItem is CondicaoPagamento condicao)
            {
                cliente.CondicaoPagamentoId = condicao.Id;
            }
            else
            {
                cliente.CondicaoPagamentoId.ToString();
            }

            cliente.Id = (isEditando || isExcluindo) ? int.Parse(txtCodigo.Text) : 0;

            cliente.DataCriacao = cliente.DataCriacao == DateTime.MinValue ? DateTime.Now : cliente.DataCriacao;
            cliente.DataAtualizacao = DateTime.Now;
        }

        private void button3_Click(object sender, EventArgs e)
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
