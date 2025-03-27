using ProjetoPF.Modelos.Localizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Dao.Localizacao
{
    public class CidadeDAO : BaseDao<Cidade>
    {
        public CidadeDAO() : base("Cidades")
        {
        }
    }
}
