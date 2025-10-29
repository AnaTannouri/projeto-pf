using ProjetoPF.Dao.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Servicos.Compra
{
    public class ItemCompraService : BaseServicos<ProjetoPF.Modelos.Compra.ItemCompra>
    {
        public ItemCompraService() : base(new ItemCompraDao()) { }
    }
}
