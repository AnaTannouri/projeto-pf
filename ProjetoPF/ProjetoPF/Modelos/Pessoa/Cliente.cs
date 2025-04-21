using ProjetoPF.Modelos.Comercial;
using ProjetoPF.Modelos.Pagamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Modelos.Pessoa
{
    public class Cliente : BasePessoa
    {
        public int CondicaoPagamentoId { get; set; }
        public int IdCidade { get; set; }
    }
}

