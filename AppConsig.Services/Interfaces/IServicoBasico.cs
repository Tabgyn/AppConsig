using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AppConsig.Common;

namespace AppConsig.Services.Interfaces
{
    public interface IServicoBasico<T> : IService where T : BaseEntity
    {
        T ObterPeloId(long id);
        IEnumerable<T> ObterTodos(Expression<Func<T, bool>> filter = null);
        void Criar(T entity);
        void Atualizar(T entity);
        void Excluir(T entity);
    }
}