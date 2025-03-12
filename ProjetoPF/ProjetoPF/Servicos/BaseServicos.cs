using ProjetoPF.Dao;
using System.Collections.Generic;

namespace ProjetoPF.Servicos
{
    public class BaseServicos<T> where T : class, new()
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


        public List<T> ObterTodos()
        {
            return _dao.BuscarTodos();
        }

        public T ObterPorId(int id)
        {
            return _dao.BuscarPorId(id);
        }

        public void Remover(int id)
        {
            _dao.Excluir(id);
        }
    }
}
