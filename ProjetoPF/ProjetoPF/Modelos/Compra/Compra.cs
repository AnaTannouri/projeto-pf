using ProjetoPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Modelos.Compra
{
    public class Compra : BaseModelos
    {
        public int Modelo { get; set; }
        public string Serie { get; set; }
        public string NumeroNota { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataEntrega { get; set; }
        public int IdFornecedor { get; set; }
        public int IdCondicaoPagamento { get; set; }

        public decimal ValorFrete { get; set; }
        public decimal ValorSeguro { get; set; }
        public decimal OutrasDespesas { get; set; }
        public decimal ValorTotal { get; set; }
        public string MotivoCancelamento { get; set; }
        public List<ItemCompra> Itens { get; set; } = new List<ItemCompra>();
        public List<ParcelaCompra> Parcelas { get; set; } = new List<ParcelaCompra>();
    }
}
