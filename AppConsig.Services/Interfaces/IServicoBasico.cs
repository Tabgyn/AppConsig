using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AppConsig.Common;

namespace AppConsig.Services.Interfaces
{
    public interface IServicoBasico<T> : IService where T : BaseEntity
    {
        T ObterPeloId(long id);
        Task<T> ObterPeloIdAsync(long id);
        IEnumerable<T> ObterTodos(Expression<Func<T, bool>> filter = null);
        Task<IEnumerable<T>> ObterTodosAsync(Expression<Func<T, bool>> filterExpression = null);
        void Criar(T entity);
        Task CriarAsync(T entity, CancellationToken cancellationToken);
        void Atualizar(T entity);
        Task AtualizarAsync(T entity, CancellationToken cancellationToken);
        void Excluir(T entity);
        Task ExcluirAsync(T entity, CancellationToken cancellationToken);
    }
}