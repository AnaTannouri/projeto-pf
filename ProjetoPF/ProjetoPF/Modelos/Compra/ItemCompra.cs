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
        public int Modelo { get; set; }
        public string Serie { get; set; }
        public string NumeroNota { get; set; }
        public int IdFornecedor { get; set; }
        public int IdProduto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal ValorRateado { get; set; }
        public decimal Total { get; set; }
    }
}
