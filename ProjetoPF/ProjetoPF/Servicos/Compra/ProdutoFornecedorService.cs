using ProjetoPF.Dao.Compra;
using ProjetoPF.Modelos.Compra;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Servicos.Compra
{
    public class ProdutoFornecedorService : BaseServicos<ProdutoFornecedor>
    {
        private readonly ProdutoFornecedorDAO _dao;

        public ProdutoFornecedorService() : base(new ProdutoFornecedorDAO())
        {
        }
    }
}
