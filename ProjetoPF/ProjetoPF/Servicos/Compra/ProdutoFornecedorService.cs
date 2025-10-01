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
            _dao = new ProdutoFornecedorDAO();
        }

        public void CriarOuAtualizar(int idProduto, int idFornecedor, decimal preco, DateTime dataCompra, string observacao = null)
        {
            var pf = new ProdutoFornecedor
            {
                IdProduto = idProduto,
                IdFornecedor = idFornecedor,
                PrecoUltimaCompra = preco,
                DataUltimaCompra = dataCompra,
                Observacao = observacao,
                DataAtualizacao = DateTime.Now
            };

            _dao.CriarOuAtualizarProdutoFornecedor(pf);
        }

        public ProdutoFornecedor BuscarPorProdutoFornecedor(int idProduto, int idFornecedor)
        {
            string filtro = $"IdProduto = {idProduto} AND IdFornecedor = {idFornecedor}";
            var resultado = BuscarTodos(filtro);
            return resultado.FirstOrDefault();
        }
        public void InserirSeNaoExistir(int idProduto, int idFornecedor)
        {
            if (!_dao.ExisteVinculo(idProduto, idFornecedor))
            {
                var novo = new ProdutoFornecedor
                {
                    IdProduto = idProduto,
                    IdFornecedor = idFornecedor,
                    DataCriacao = DateTime.Now,
                    DataAtualizacao = DateTime.Now,
                    Ativo = true
                };
                _dao.Criar(novo);
            }
        }
    }
}
