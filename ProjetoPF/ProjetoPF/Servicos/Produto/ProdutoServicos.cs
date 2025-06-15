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

        public void CadastrarProduto(Produtos produto)
        {
            if (!ValidarEntrada(produto))
                throw new Exception("Validação de entrada falhou. Preencha todos os campos obrigatórios.");

            produto.DataCriacao = DateTime.Now;
            produto.DataAtualizacao = DateTime.Now;

            Criar(produto);
        }

        public void AtualizarProduto(Produtos produto)
        {
            if (produto.Id == 0)
                throw new Exception("ID inválido para atualização.");

            if (!ValidarEntrada(produto))
                throw new Exception("Validação de entrada falhou. Preencha todos os campos obrigatórios.");

            produto.DataAtualizacao = DateTime.Now;

            Atualizar(produto);
        }

        private bool ValidarEntrada(Produtos produto)
        {
            if (string.IsNullOrWhiteSpace(produto.Nome))
                return false;

            if (produto.IdUnidadeMedida <= 0 || produto.IdCategoria <= 0 || produto.IdFornecedor <= 0 || produto.IdMarca <= 0)
                return false;

            if (produto.PrecoCusto <= 0 || produto.PrecoVenda <= 0)
                return false;

            return true;
        }

        public bool NomeDuplicado(Produtos produto)
        {
            var todosProdutos = BuscarTodos();

            if (produto.Id != 0)
                todosProdutos = todosProdutos.Where(p => p.Id != produto.Id).ToList();

            return todosProdutos.Any(p => p.Nome.Equals(produto.Nome, StringComparison.OrdinalIgnoreCase));
        }
    }
}
