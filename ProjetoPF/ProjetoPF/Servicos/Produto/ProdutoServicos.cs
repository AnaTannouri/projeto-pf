using ProjetoPF.Dao;
using ProjetoPF.Modelos.Produto;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjetoPF.Servicos
{
    public class ProdutoServicos : BaseServicos<Produtos>
    {
        public ProdutoServicos() : base(new BaseDao<Produtos>("Produtos")) { }

        public int BuscarUltimoId()
        {
            var lista = BuscarTodos();
            if (lista != null && lista.Count > 0)
            {
                return lista.Max(p => p.Id);
            }
            return 0;
        }
    }
}
