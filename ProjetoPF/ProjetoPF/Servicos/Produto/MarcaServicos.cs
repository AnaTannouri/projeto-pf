using ProjetoPF.Dao.Produto;
using ProjetoPF.Modelos.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Servicos.Produto
{
    public class MarcaServicos : BaseServicos<Marca>
    {
        public MarcaServicos() : base(new MarcaDAO()) { }

        public void CadastrarMarca(Marca marca)
        {
            if (string.IsNullOrWhiteSpace(marca.Descricao))
                throw new Exception("Descrição é obrigatória!");

            marca.DataCriacao = DateTime.Now;
            marca.DataAtualizacao = DateTime.Now;
            Criar(marca);
        }

        public void AtualizarMarca(Marca marca)
        {
            if (marca.Id == 0)
            {
                throw new Exception("ID inválido. Não é possível atualizar o registro.");
            }

            Atualizar(marca);
        }
    }
}
