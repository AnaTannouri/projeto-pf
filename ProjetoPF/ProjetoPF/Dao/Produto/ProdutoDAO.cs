using ProjetoPF.Modelos.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Dao.Item
{
    public class ProdutoDAO : BaseDao<Produtos>
    {
        public ProdutoDAO() : base("Produtos")
        {
        }
    }
}
