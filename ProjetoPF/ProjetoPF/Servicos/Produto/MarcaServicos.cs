using ProjetoPF.Dao.Produto;
using ProjetoPF.Modelos.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Servicos.Produto
{
    public class MarcaServicos : BaseServicos<Marca>
    {
        public MarcaServicos() : base(new MarcaDAO()) { }
    }
}
