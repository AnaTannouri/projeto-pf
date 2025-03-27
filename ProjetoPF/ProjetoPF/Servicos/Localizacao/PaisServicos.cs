using ProjetoPF.Dao;
using ProjetoPF.Modelos.Localizacao;
using System;
using System.Collections.Generic;

namespace ProjetoPF.Servicos.Localizacao
{
    public class PaisServicos : BaseServicos<Pais>
    {
        public PaisServicos() : base(new BaseDao<Pais>("Paises")) { }

        public void CadastrarPais(Pais pais)
        {
            Validar(pais);

            if (VerificarDuplicidade("Nome", pais.Nome, pais))
                throw new Exception("Já existe um país cadastrado com este nome.");

            pais.DataCriacao = DateTime.Now;
            pais.DataAtualizacao = DateTime.Now;

            Criar(pais);
        }

        public void AtualizarPais(Pais pais)
        {
            if (pais.Id == 0)
                throw new Exception("ID inválido. Não é possível atualizar o registro.");

            Validar(pais);

            if (VerificarDuplicidade("Nome", pais.Nome, pais))
                throw new Exception("Já existe outro país cadastrado com este nome.");

            pais.DataAtualizacao = DateTime.Now;

            Atualizar(pais);
        }

        private void Validar(Pais pais)
        {
            if (string.IsNullOrWhiteSpace(pais.Nome))
                throw new Exception("O nome do país é obrigatório.");

            if (string.IsNullOrWhiteSpace(pais.Sigla))
                throw new Exception("A sigla do país é obrigatória.");

            if (string.IsNullOrWhiteSpace(pais.DDI))
                throw new Exception("O DDI do país é obrigatório.");
        }
    }
}
