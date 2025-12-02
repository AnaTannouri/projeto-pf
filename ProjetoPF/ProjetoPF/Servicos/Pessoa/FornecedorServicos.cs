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
