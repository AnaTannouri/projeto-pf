using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Modelos
{
    public struct VendaKey
    {
        public string Modelo { get; }
        public string Serie { get; }
        public string NumeroNota { get; }
        public int IdCliente { get; }

        public VendaKey(string modelo, string serie, string numeroNota, int idCliente)
        {
            Modelo = modelo;
            Serie = serie ?? throw new ArgumentNullException(nameof(serie));
            NumeroNota = numeroNota ?? throw new ArgumentNullException(nameof(numeroNota));
            IdCliente = idCliente;
        }

        public override string ToString() => $"{Modelo}-{Serie}-{NumeroNota}-C{IdCliente}";
    }
}
