using ProjetoPF.Dao;
using ProjetoPF.Modelos.Localizacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Servicos.Localizacao
{
    public class CidadeServicos : BaseServicos<Cidade>
    {
        public CidadeServicos() : base(new BaseDao<Cidade>("Cidades")) { }

       
    }
}
