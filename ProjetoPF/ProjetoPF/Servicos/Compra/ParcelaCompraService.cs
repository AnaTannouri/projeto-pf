using ProjetoPF.Dao.Compras;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Servicos.Compra
{

    public class ParcelaCompraService : BaseServicos<ProjetoPF.Modelos.Compra.ContasAPagar>
    {
        public ParcelaCompraService() : base(new ContasAPagarDao()) { }
    }
}
