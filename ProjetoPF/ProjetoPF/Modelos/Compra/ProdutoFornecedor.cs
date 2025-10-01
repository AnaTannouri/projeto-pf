using ProjetoPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Modelos.Compra
{
    public class ProdutoFornecedor : BaseModelos
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public int IdFornecedor { get; set; }
        public decimal PrecoUltimaCompra { get; set; }
        public DateTime? DataUltimaCompra { get; set; }
        public string Observacao { get; set; }
    }
}
