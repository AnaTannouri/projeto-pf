using ProjetoPF.Modelos.Localizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Dao.Localizacao
{
    public class EstadoDAO : BaseDao<Estado>
    {
        public EstadoDAO() : base("Estados")
        {
        }
    }
}
