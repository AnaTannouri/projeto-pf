using ProjetoPF.Modelos.Compra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Dao.Compras 
{
    public class CompraDao : BaseDao<Compra>
    {
        public CompraDao() : base("NotaCompra") { }
    }

    public class ItemCompraDao : BaseDao<ItemCompra>
    {
        public ItemCompraDao() : base("Prod_Nota") { }
    }

    public class ParcelaCompraDao : BaseDao<ParcelaCompra>
    {
        public ParcelaCompraDao() : base("ParcelaCompra") { }
    }
}

