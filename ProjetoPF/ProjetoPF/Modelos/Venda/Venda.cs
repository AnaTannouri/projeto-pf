using ProjetoPF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Modelos.Venda
{
    public class Venda : BaseModelos
    {
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string NumeroNota { get; set; }
        public DateTime DataEmissao { get; set; }
        public int IdFuncionario { get; set; }
        public int IdCliente { get; set; }
        public int IdCondicaoPagamento { get; set; }
        public decimal ValorTotal { get; set; }
        public string MotivoCancelamento { get; set; }
        public string Observacao { get; set; }

        [NotMapped]
        public List<ItemVenda> Itens { get; set; } = new List<ItemVenda>();

        [NotMapped]
        public List<ContasAReceber> Parcelas { get; set; } = new List<ContasAReceber>();
    }
}
