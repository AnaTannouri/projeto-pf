using ProjetoPF.Modelos.Comercial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Modelos.Pessoa
{
    public class Fornecedor:BasePessoa
    { 
        public int CondicaoPagamentoId { get; set; }
        public int IdCidade { get; set; }
        public decimal? ValorMinimoPedido { get; set; } 
    }
}
