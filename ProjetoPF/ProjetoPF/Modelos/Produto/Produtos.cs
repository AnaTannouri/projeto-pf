using ProjetoPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Modelos.Produto
{
    public class Produtos : BaseModelos
    {
        public string Nome { get; set; }
        public int IdUnidadeMedida { get; set; }
        public int IdCategoria { get; set; }
        public int? IdMarca { get; set; }
        public decimal Estoque { get; set; }
        public decimal PrecoCusto { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal? CustoUltimaCompra { get; set; }
        public decimal MargemLucro { get; set; }
        public string Observacao { get; set; }
    }
}