using ProjetoPF.Dao;
using System.Collections.Generic;

namespace ProjetoPF.Servicos
{
    public class BaseServicos<T> where T : ProjetoPF.Model.BaseModelos, new()
    {
        protected readonly BaseDao<T> _dao;

        public BaseServicos(BaseDao<T> dao)
        {
            _dao = dao;
        }

        public void Criar(T entidade)
        {
            _dao.Criar(entidade);
        }

        public List<T> BuscarTodos(string filtro = null)
        {
            return _dao.BuscarTodos(filtro);
        }

        public T BuscarPorId(int id)
        {
            return _dao.BuscarPorId(id);
        }

        public void Atualizar(T entidade)
        {
            _dao.Atualizar(entidade);
        }

        public void Remover(int id)
        {
            _dao.Remover(id);
        }

        public bool VerificarDuplicidade(string campo, string valor, T entidade)
        {
            return _dao.VerificarDuplicidade(campo, valor, entidade);
        }
    }
}