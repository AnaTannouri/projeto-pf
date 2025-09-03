using ProjetoPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Modelos.Compra
{
    public class ParcelaCompra : BaseModelos
    {
        public int IdCompra { get; set; }
        public int NumeroParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorParcela { get; set; }
    }
}
