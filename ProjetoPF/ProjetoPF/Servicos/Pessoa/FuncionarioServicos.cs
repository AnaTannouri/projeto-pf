using ProjetoPF.Dao;
using ProjetoPF.Modelos.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Servicos.Pessoa
{

    public class FuncionarioServicos : BaseServicos<Funcionario>
    {
        public FuncionarioServicos() : base(new BaseDao<Funcionario>("Funcionarios")) { }

        public void CadastrarFuncionario(Funcionario funcionario)
        {
            if (!ValidarEntrada(funcionario))
            {
                throw new Exception("Validação de entrada falhou. Preencha todos os campos obrigatórios.");
            }

            if (!funcionario.Estrangeiro && VerificarDuplicidade("CpfCnpj", funcionario.CpfCnpj, funcionario))
                throw new Exception("Já existe um funcionário com este CPF.");
            else if (funcionario.Estrangeiro && VerificarDuplicidade("RgInscricaoEstadual", funcionario.RgInscricaoEstadual, funcionario))
                throw new Exception("Já existe um funcionário com este RG.");

            funcionario.DataCriacao = DateTime.Now;
            funcionario.DataAtualizacao = DateTime.Now;

            Criar(funcionario);
        }

        public void AtualizarFuncionario(Funcionario funcionario)
        {
            if (funcionario.Id == 0)
                throw new Exception("ID inválido para atualização.");

            if (!ValidarEntrada(funcionario))
            {
                throw new Exception("Validação de entrada falhou. Preencha todos os campos obrigatórios.");
            }

            if (!funcionario.Estrangeiro && VerificarDuplicidade("CpfCnpj", funcionario.CpfCnpj, funcionario))
                throw new Exception("Já existe um funcionário com este CPF.");
            else if (funcionario.Estrangeiro && VerificarDuplicidade("RgInscricaoEstadual", funcionario.RgInscricaoEstadual, funcionario))
                throw new Exception("Já existe um funcionário com este RG.");

            funcionario.DataAtualizacao = DateTime.Now;

            Atualizar(funcionario);
        }

        private bool ValidarEntrada(Funcionario funcionario)
        {
            if (string.IsNullOrWhiteSpace(funcionario.Matricula))
                return false;

            if (string.IsNullOrWhiteSpace(funcionario.NomeRazaoSocial))
                return false;

            if (!funcionario.Estrangeiro && string.IsNullOrWhiteSpace(funcionario.CpfCnpj))
                return false;

            if (string.IsNullOrWhiteSpace(funcionario.Email))
                return false;

            if (string.IsNullOrWhiteSpace(funcionario.Telefone))
                return false;

            if (string.IsNullOrWhiteSpace(funcionario.Rua) || string.IsNullOrWhiteSpace(funcionario.Numero) || string.IsNullOrWhiteSpace(funcionario.Bairro))
                return false;

            if (funcionario.IdCidade <= 0)
                return false;

            if (string.IsNullOrWhiteSpace(funcionario.Cep))
                return false;

            if (string.IsNullOrWhiteSpace(funcionario.Cargo))
                return false;

            if (funcionario.Salario <= 0)
                return false;

            if (string.IsNullOrWhiteSpace(funcionario.Turno))
                return false;

            if (string.IsNullOrWhiteSpace(funcionario.CargaHoraria))
                return false;

            if (string.IsNullOrWhiteSpace(funcionario.Classificacao))
                return false;

            return true;
        }
    }
}
