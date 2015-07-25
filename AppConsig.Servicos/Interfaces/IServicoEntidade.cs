using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AppConsig.Comum;

namespace AppConsig.Servicos.Interfaces
{
    public interface IServicoEntidade<T> : IServico where T : EntidadeBase
    {
        T ObterPeloId(long id);
        IEnumerable<T> ObterTodos(Expression<Func<T, bool>> filter = null);
        IEnumerable<T> ObterTodosPaginado(Expression<Func<T, bool>> filter = null, int pageNumber = 1, int pageSize = 5);
        void Criar(T entity);
        void Atualizar(T entity);
        void Excluir(T entity);
    }
}