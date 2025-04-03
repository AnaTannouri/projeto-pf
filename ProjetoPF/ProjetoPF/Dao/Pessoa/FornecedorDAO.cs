using ProjetoPF.Modelos.Pessoa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Dao.Pessoa
{
    public class FornecedorDAO : BaseDao<Fornecedor>
    {
        public FornecedorDAO() : base("Fornecedores")
        {
        }
    }
}
