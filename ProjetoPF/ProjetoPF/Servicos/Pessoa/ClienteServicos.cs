using ProjetoPF.Dao;
using ProjetoPF.Modelos.Pessoa;
using System;
using System.Linq;

namespace ProjetoPF.Servicos
{
    public class ClienteServicos : BaseServicos<Cliente>
    {
        public ClienteServicos() : base(new BaseDao<Cliente>("Clientes")) { }

        public void CadastrarCliente(Cliente cliente)
        {
            if (!ValidarEntrada(cliente))
            {
                throw new Exception("Validação de entrada falhou. Preencha todos os campos obrigatórios.");
            }

            cliente.DataCriacao = DateTime.Now;
            cliente.DataAtualizacao = DateTime.Now;

            Criar(cliente);
        }

        public void AtualizarCliente(Cliente cliente)
        {
            if (cliente.Id == 0)
                throw new Exception("ID inválido para atualização.");

            if (!ValidarEntrada(cliente))
            {
                throw new Exception("Validação de entrada falhou. Preencha todos os campos obrigatórios.");
            }

            cliente.DataAtualizacao = DateTime.Now;

            Atualizar(cliente);
        }

        private bool ValidarEntrada(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.NomeRazaoSocial))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(cliente.Email))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(cliente.Telefone))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(cliente.Rua) || string.IsNullOrWhiteSpace(cliente.Numero) || string.IsNullOrWhiteSpace(cliente.Bairro))
            {
                return false;
            }

            if (cliente.IdCidade <= 0)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(cliente.Cep))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(cliente.Classificacao.ToString()))
            {
                return false;
            }

            return true;
        }
        public bool DocumentoDuplicado(Cliente cliente)
        {
            var todosClientes = BuscarTodos();

            if (cliente.Id != 0)
                todosClientes = todosClientes.FindAll(c => c.Id != cliente.Id);

            string tipo = cliente.TipoPessoa?.ToUpper() ?? "";

            if ((tipo == "FÍSICA" || tipo == "FISICA") && !string.IsNullOrWhiteSpace(cliente.CpfCnpj))
            {
                return todosClientes.Any(c => c.CpfCnpj == cliente.CpfCnpj);
            }

            if ((tipo == "JURÍDICA" || tipo == "JURIDICA") && !string.IsNullOrWhiteSpace(cliente.CpfCnpj))
            {
                return todosClientes.Any(c => c.CpfCnpj == cliente.CpfCnpj);
            }

            if ((tipo == "FÍSICA" || tipo == "FISICA") && string.IsNullOrWhiteSpace(cliente.CpfCnpj) && !string.IsNullOrWhiteSpace(cliente.RgInscricaoEstadual))
            {
                return todosClientes.Any(c => c.RgInscricaoEstadual == cliente.RgInscricaoEstadual);
            }

            return false;
        }

    }
}
