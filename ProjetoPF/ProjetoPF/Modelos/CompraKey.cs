using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Modelos
{
    public struct CompraKey
    {
        public string Modelo { get; }
        public string Serie { get; }
        public string NumeroNota { get; }
        public int IdFornecedor { get; }

        public CompraKey(string modelo, string serie, string numeroNota, int idFornecedor)
        {
            Modelo = modelo;
            Serie = serie ?? throw new ArgumentNullException(nameof(serie));
            NumeroNota = numeroNota ?? throw new ArgumentNullException(nameof(numeroNota));
            IdFornecedor = idFornecedor;
        }

        public override string ToString() => $"{Modelo}-{Serie}-{NumeroNota}-F{IdFornecedor}";
    }
}
