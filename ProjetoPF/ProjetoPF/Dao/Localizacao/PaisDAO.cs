using ProjetoPF.Model;
using ProjetoPF.Modelos.Localizacao;
using System.Collections.Generic;

namespace ProjetoPF.Dao
{
    public class PaisDAO : BaseDao<Pais>
    {
        public PaisDAO() : base("Paises")
        {
        }
    }
}