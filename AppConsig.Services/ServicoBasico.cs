using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using AppConsig.Common;
using AppConsig.Data;
using AppConsig.Services.Interfaces;

namespace AppConsig.Services
{
    public class ServicoBasico<T> : IServicoBasico<T> where T : BaseEntity
    {
        protected IContext Context;
        protected IDbSet<T> Dbset;

        /// <summary>
        /// Serviço base para entidades.
        /// </summary>
        /// <param name="context"></param>
        public ServicoBasico(IContext context)
        {
            Context = context;
            Dbset = context.Set<T>();
        }

        /// <summary>
        /// Retorna a entidade buscando pelo seu Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T ObterPeloId(long id)
        {
            return Dbset.Find(id);
        }

        /// <summary>
        /// Retorna a entidade buscando pelo seu Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<T> ObterPeloIdAsync(long id)
        {
            return await ((DbSet<T>)Dbset).FindAsync(id);
        }

        /// <summary>
        /// Retorna todas as entidades.
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="includeExpressions"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> ObterTodos(Expression<Func<T, bool>> filterExpression = null, params Expression<Func<T, object>>[] includeExpressions)
        {
            var includeSet = includeExpressions.Aggregate<Expression<Func<T, object>>, IQueryable<T>>
                (Dbset, (current, expression) => current.Include(expression));

            var list = filterExpression != null
                ? includeSet.Where(filterExpression).AsEnumerable()
                : includeSet.AsEnumerable();

            return list;
        }

        /// <summary>
        /// Retorna todas as entidades.
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public virtual async Task<IEnumerable<T>> ObterTodosAsync(Expression<Func<T, bool>> filterExpression = null)
        {
            return filterExpression != null ? await Dbset.Where(filterExpression).ToListAsync() : await Dbset.ToListAsync();
        }

        /// <summary>
        /// Cria uma nova entidade.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Criar(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Dbset.Add(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Cria uma nova entidade.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        public virtual async Task CriarAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            Dbset.Add(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Atualiza uma entidade.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Atualizar(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        /// <summary>
        /// Atualiza uma entidade.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        public virtual async Task AtualizarAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Context.Entry(entity).State = EntityState.Modified;
            await Context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Exclui uma entidade.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Excluir(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dbset.Remove(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Exclui uma entidade.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        public virtual async Task ExcluirAsync(T entity, CancellationToken cancellationToken)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dbset.Remove(entity);
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}