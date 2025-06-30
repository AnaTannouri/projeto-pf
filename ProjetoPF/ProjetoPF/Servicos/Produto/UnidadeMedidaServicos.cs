using ProjetoPF.Dao.Produto;
using ProjetoPF.Modelos.Produto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoPF.Servicos.Produto
{
    public class UnidadeMedidaServicos : BaseServicos<UnidadeMedida>
    {
        public UnidadeMedidaServicos() : base(new UnidadeMedidaDAO()) { }

        public void CadastrarUnidadeMedida(UnidadeMedida unidade)
        {
            if (string.IsNullOrWhiteSpace(unidade.Descricao))
                throw new Exception("Descrição é obrigatória!");

            if (string.IsNullOrWhiteSpace(unidade.Sigla))
                throw new Exception("Sigla é obrigatória!");

            unidade.DataCriacao = DateTime.Now;
            unidade.DataAtualizacao = DateTime.Now;
            Criar(unidade);
        }

        public void AtualizarUnidadeMedida(UnidadeMedida unidade)
        {
            if (unidade.Id == 0)
            {
                throw new Exception("ID inválido. Não é possível atualizar o registro.");
            }

            Atualizar(unidade);
        }
    }
}
