﻿using System;

namespace ProjetoPF.Model
{
    [Serializable]

    public class BaseModelos
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public bool Ativo { get; set; }

    }
}
