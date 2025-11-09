using ProjetoPF.Dao.Compras;
using ProjetoPF.Dao.Vendas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Servicos.Venda
{
    public class ItemVendaService : BaseServicos<ProjetoPF.Modelos.Venda.ItemVenda>
    {
        public ItemVendaService() : base(new ItemVendaDao()) { }
    }
}
