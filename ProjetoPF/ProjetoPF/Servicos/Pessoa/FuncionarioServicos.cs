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

        public bool DocumentoDuplicado(Funcionario funcionario)
        {
            var todosFuncionarios = BuscarTodos();
            if (funcionario.Id != 0)
                todosFuncionarios = todosFuncionarios.FindAll(f => f.Id != funcionario.Id);

            if (!string.IsNullOrWhiteSpace(funcionario.CpfCnpj))
            {
                return todosFuncionarios.Any(f => f.CpfCnpj == funcionario.CpfCnpj);
            }
            if (!string.IsNullOrWhiteSpace(funcionario.RgInscricaoEstadual))
            {
                return todosFuncionarios.Any(f => f.RgInscricaoEstadual == funcionario.RgInscricaoEstadual);
            }

            return false;
        }
    }
}
