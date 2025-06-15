using ProjetoPF.Modelos.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Dao.Produto
{
    public class MarcaDAO : BaseDao<Marca>
    {
        public MarcaDAO() : base("Marcas")
        {
        }
    }
}
