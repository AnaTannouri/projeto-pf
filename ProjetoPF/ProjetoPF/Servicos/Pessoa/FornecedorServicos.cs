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

            fornecedor.DataAtualizacao = DateTime.Now;

            Atualizar(fornecedor);
        }

        private bool ValidarEntrada(Fornecedor fornecedor)
        {
            if (string.IsNullOrWhiteSpace(fornecedor.NomeRazaoSocial))
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
        public bool DocumentoDuplicado(Fornecedor fornecedor)
        {
            var todosFornecedores = BuscarTodos();

            if (fornecedor.Id != 0)
                todosFornecedores = todosFornecedores.FindAll(f => f.Id != fornecedor.Id);

            if (!string.IsNullOrWhiteSpace(fornecedor.CpfCnpj))
            {
                return todosFornecedores.Any(f => f.CpfCnpj == fornecedor.CpfCnpj);
            }

            if (!string.IsNullOrWhiteSpace(fornecedor.RgInscricaoEstadual))
            {
                return todosFornecedores.Any(f => f.RgInscricaoEstadual == fornecedor.RgInscricaoEstadual);
            }

            return false;
        }
    }
}
