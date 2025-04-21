using ProjetoPF.Model;
using System;

namespace ProjetoPF.Modelos.Localizacao
{
    public class Estado : BaseModelos
    {
        public string Nome { get; set; }
        public string UF { get; set; }
        public int IdPais { get; set; }

    }
}
