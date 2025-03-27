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

        public void CadastrarCidade(Cidade cidade)
        {
            Validar(cidade);

            if (VerificarDuplicidade("Nome", cidade.Nome, cidade))
                throw new Exception("Já existe uma cidade cadastrada com este nome.");

            cidade.DataCriacao = DateTime.Now;
            cidade.DataAtualizacao = DateTime.Now;

            Criar(cidade);
        }

        public void AtualizarCidade(Cidade cidade)
        {
            if (cidade.Id == 0)
                throw new Exception("ID inválido para atualização.");

            Validar(cidade);

            if (VerificarDuplicidade("Nome", cidade.Nome, cidade))
                throw new Exception("Já existe uma cidade cadastrada com este nome.");

            cidade.DataAtualizacao = DateTime.Now;

            Atualizar(cidade);
        }

        private void Validar(Cidade cidade)
        {
            if (string.IsNullOrWhiteSpace(cidade.Nome))
                throw new Exception("O nome da cidade é obrigatório.");

            if (string.IsNullOrWhiteSpace(cidade.DDD))
                throw new Exception("O DDD da cidade é obrigatório.");

            if (cidade.IdEstado <= 0)
                throw new Exception("A cidade precisa estar vinculada a um estado.");

            if (VerificarDuplicidade("Nome", cidade.Nome, cidade))
                throw new Exception("Já existe uma cidade com esse nome.");
        }
    }
}
