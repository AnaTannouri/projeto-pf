using ProjetoPF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Modelos.Venda
{
    public class ContasAReceber : BaseModelos
    {
        public string Modelo { get; set; }
        public string Serie { get; set; }
        public string NumeroNota { get; set; }
        public int IdCliente { get; set; }
        public int NumeroParcela { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataEmissao { get; set; }
        public decimal ValorParcela { get; set; }
        public int IdFormaPagamento { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorFinalParcela { get; set; }
        public string Observacao { get; set; }
        public decimal Multa { get; set; }
        public decimal Juros { get; set; }
        public decimal Desconto { get; set; }
        public string Situacao { get; set; } = "Em Aberto";
        public string MotivoCancelamento { get; set; }

        public decimal MultaValor { get; set; }
        public decimal JurosValor { get; set; }
        public decimal DescontoValor { get; set; }

        [NotMapped]
        public string FormaPagamentoDescricao { get; set; }

        public string NomeCliente { get; set; }
    }
}
