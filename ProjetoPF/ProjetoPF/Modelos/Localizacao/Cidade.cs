using ProjetoPF.Model;
using System;

namespace ProjetoPF.Modelos.Localizacao
{
    [Serializable]
    public class Cidade : BaseModelos
    {
        public string Nome { get; set; }       
        public string DDD { get; set; }         
        public int IdEstado { get; set; }
    }
}