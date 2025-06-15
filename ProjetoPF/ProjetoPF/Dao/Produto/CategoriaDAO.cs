using ProjetoPF.Model;
using ProjetoPF.Modelos.Produto;

namespace ProjetoPF.Dao
{
    public class CategoriaDAO : BaseDao<Categoria>
    {
        public CategoriaDAO() : base("Categorias")
        {
        }
    }
}