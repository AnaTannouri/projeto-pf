using ProjetoPF.Dao;
using ProjetoPF.Modelos.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Servicos.Produto
{
    public class CategoriaServicos : BaseServicos<Categoria>
    {
        public CategoriaServicos() : base(new CategoriaDAO()) { }

    }
}
