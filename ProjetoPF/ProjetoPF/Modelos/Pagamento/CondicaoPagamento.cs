using ProjetoPF.Model;
using System;

namespace ProjetoPF.Modelos.Pagamento
{
    public class CondicaoPagamento : BaseModelos
    {
        public string Descricao { get; set; }
        public decimal TaxaJuros { get; set; }
        public decimal Multa { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
