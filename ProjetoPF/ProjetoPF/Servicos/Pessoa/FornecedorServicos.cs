using ProjetoPF.Dao;
using ProjetoPF.Modelos.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Servicos.Pessoa
{
    public class FornecedorServicos : BaseServicos<Fornecedor>
    {
        public FornecedorServicos() : base(new BaseDao<Fornecedor>("Fornecedores")) { }
        public void CadastrarFornecedor(Fornecedor fornecedor)
        {
            if (!ValidarEntrada(fornecedor))
            {
                throw new Exception("Validação de entrada falhou. Preencha todos os campos obrigatórios.");
            }

            if (!fornecedor.Estrangeiro && VerificarDuplicidade("CpfCnpj", fornecedor.CpfCnpj, fornecedor))
                throw new Exception("Já existe um fornecedor com este CNPJ.");
            else if (fornecedor.Estrangeiro && VerificarDuplicidade("RgInscricaoEstadual", fornecedor.RgInscricaoEstadual, fornecedor))
                throw new Exception("Já existe um fornecedor com esta inscrição.");

            fornecedor.DataCriacao = DateTime.Now;
            fornecedor.DataAtualizacao = DateTime.Now;

            Criar(fornecedor);
        }

        public void AtualizarFornecedor(Fornecedor fornecedor)
        {
            if (fornecedor.Id == 0)
                throw new Exception("ID inválido para atualização.");

            if (!ValidarEntrada(fornecedor))
            {
                throw new Exception("Validação de entrada falhou. Preencha todos os campos obrigatórios.");
            }

            if (!fornecedor.Estrangeiro && VerificarDuplicidade("CpfCnpj", fornecedor.CpfCnpj, fornecedor))
                throw new Exception("Já existe um fornecedor com este CNPJ.");
            else if (fornecedor.Estrangeiro && VerificarDuplicidade("RgInscricaoEstadual", fornecedor.RgInscricaoEstadual, fornecedor))
                throw new Exception("Já existe um fornecedor com este RG.");

            fornecedor.DataAtualizacao = DateTime.Now;

            Atualizar(fornecedor);
        }

        private bool ValidarEntrada(Fornecedor fornecedor)
        {
            if (string.IsNullOrWhiteSpace(fornecedor.NomeRazaoSocial))
            {
                return false;
            }

            if (!fornecedor.Estrangeiro && string.IsNullOrWhiteSpace(fornecedor.CpfCnpj))
            {
                return false;
            }

            if (fornecedor.Estrangeiro && string.IsNullOrWhiteSpace(fornecedor.RgInscricaoEstadual))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(fornecedor.Email))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(fornecedor.Telefone))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(fornecedor.Rua) || string.IsNullOrWhiteSpace(fornecedor.Numero) || string.IsNullOrWhiteSpace(fornecedor.Bairro))
            {
                return false;
            }

            if (fornecedor.IdCidade <= 0)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(fornecedor.Cep))
            {
                return false;
            }

            return true;
        }

    }
}
