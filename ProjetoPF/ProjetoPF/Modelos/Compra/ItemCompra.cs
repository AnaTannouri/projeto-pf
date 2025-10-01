using ProjetoPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Modelos.Compra
{
    public class ItemCompra : BaseModelos
    {
        public int IdCompra { get; set; }
        public int IdProduto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        private decimal _valorCustoUnitarioReal;
        public decimal Total { get; set; }
    }
}
