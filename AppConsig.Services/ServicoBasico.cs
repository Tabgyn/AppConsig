using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
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
        public T ObterPeloId(long id)
        {
            return Dbset.Find(id);
        }

        /// <summary>
        /// Retorna todas as entidades.
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> ObterTodos(Expression<Func<T, bool>> filterExpression = null)
        {
            return filterExpression != null ? Dbset.Where(filterExpression).AsEnumerable() : Dbset.AsEnumerable();
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
        /// Exclui uma entidade.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Excluir(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dbset.Remove(entity);
            Context.SaveChanges();
        }
    }
}