using ProjetoPF.Dao;
using ProjetoPF.Modelos.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Servicos.Produto
{
    public class CategoriaServicos : BaseServicos<Categoria>
    {
        public CategoriaServicos() : base(new CategoriaDAO()) { }

        public void CadastrarCategoria(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Descricao))
                throw new Exception("Descrição é obrigatória!");

            categoria.DataCriacao = DateTime.Now;
            categoria.DataAtualizacao = DateTime.Now;
            Criar(categoria);
        }

        public void AtualizarCategoria(Categoria categoria)
        {
            if (categoria.Id == 0)
            {
                throw new Exception("ID inválido. Não é possível atualizar o registro.");
            }

            Atualizar(categoria);
        }
    }
}
