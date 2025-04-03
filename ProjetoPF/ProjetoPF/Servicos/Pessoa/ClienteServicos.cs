using ProjetoPF.Dao;
using ProjetoPF.Modelos.Pessoa;
using System;

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

            if (!cliente.Estrangeiro && VerificarDuplicidade("CpfCnpj", cliente.CpfCnpj, cliente))
                throw new Exception("Já existe um cliente com este CPF.");
            else if (cliente.Estrangeiro && VerificarDuplicidade("RgInscricaoEstadual", cliente.RgInscricaoEstadual, cliente))
                throw new Exception("Já existe um cliente com este RG.");

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

            if (!cliente.Estrangeiro && VerificarDuplicidade("CpfCnpj", cliente.CpfCnpj, cliente))
                throw new Exception("Já existe um cliente com este CPF.");
            else if (cliente.Estrangeiro && VerificarDuplicidade("RgInscricaoEstadual", cliente.RgInscricaoEstadual, cliente))
                throw new Exception("Já existe um cliente com este RG.");

            cliente.DataAtualizacao = DateTime.Now;

            Atualizar(cliente);
        }

        private bool ValidarEntrada(Cliente cliente)
        {
            if (string.IsNullOrWhiteSpace(cliente.NomeRazaoSocial))
            {
                return false;
            }

            if (!cliente.Estrangeiro && string.IsNullOrWhiteSpace(cliente.CpfCnpj))
            {
                return false;
            }

            if (cliente.Estrangeiro && string.IsNullOrWhiteSpace(cliente.RgInscricaoEstadual))
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
    }
}
