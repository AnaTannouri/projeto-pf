using ProjetoPF.Dao.Compras;
using ProjetoPF.Dao.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Servicos.Venda
{
    public class ContaAReceberService : BaseServicos<ProjetoPF.Modelos.Venda.ContasAReceber>
    {
        public ContaAReceberService() : base(new ContasAReceberDao()) { }
    }
}
