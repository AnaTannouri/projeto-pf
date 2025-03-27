using ProjetoPF.Dao;
using ProjetoPF.Modelos.Localizacao;
using ProjetoPF.Servicos;
using System;
using System.Collections.Generic;

namespace ProjetoPF.Servicos.Localizacao
{
    public class EstadoServicos : BaseServicos<Estado>
    {
        public EstadoServicos() : base(new BaseDao<Estado>("Estados")) { }

        public void CadastrarEstado(Estado estado)
        {
            Validar(estado);

            estado.DataCriacao = DateTime.Now;
            estado.DataAtualizacao = DateTime.Now;

            Criar(estado);
        }

        public void AtualizarEstado(Estado estado)
        {
            if (estado.Id == 0)
                throw new Exception("ID inválido para atualização.");

            Validar(estado);
            estado.DataAtualizacao = DateTime.Now;

            Atualizar(estado);
        }

        private void Validar(Estado estado)
        {
            if (string.IsNullOrWhiteSpace(estado.Nome))
                throw new Exception("O nome do estado é obrigatório.");

            if (string.IsNullOrWhiteSpace(estado.UF))
                throw new Exception("A UF do estado é obrigatória.");

            if (estado.IdPais <= 0)
                throw new Exception("O estado precisa estar vinculado a um país.");

            if (VerificarDuplicidade("Nome", estado.Nome, estado))
                throw new Exception("Já existe um estado com esse nome.");
        }
    }
}
