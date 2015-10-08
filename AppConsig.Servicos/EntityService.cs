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
    public class EntityService<T> : IEntityService<T> where T : BaseEntity
    {
        protected IContext Context;
        protected IDbSet<T> Dbset;

        /// <summary>
        /// Serviço base para entidades.
        /// </summary>
        /// <param name="context"></param>
        public EntityService(IContext context)
        {
            Context = context;
            Dbset = context.Set<T>();
        }

        /// <summary>
        /// Retorna a entidade buscando pelo seu Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(long id)
        {
            return Dbset.Find(id);
        }

        /// <summary>
        /// Retorna todas as entidades.
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> filterExpression = null)
        {
            return filterExpression != null ? Dbset.Where(filterExpression).AsEnumerable() : Dbset.AsEnumerable();
        }

        /// <summary>
        /// Retorna todas as entidades.
        /// </summary>
        /// <param name="filterExpression"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageLenght"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAllPaged(Expression<Func<T, bool>> filterExpression = null, int pageNumber = 1,
            int pageLenght = 5)
        {
            return filterExpression != null
                ? Dbset.Where(filterExpression).Skip(pageLenght * (pageNumber - 1)).Take(pageLenght).AsEnumerable()
                : Dbset.Skip(pageLenght * (pageNumber - 1)).Take(pageLenght).AsEnumerable();
        }

        /// <summary>
        /// Cria uma nova entidade.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Insert(T entity)
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
        public virtual void Update(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        /// <summary>
        /// Exclui uma entidade.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            Dbset.Remove(entity);
            Context.SaveChanges();
        }
    }
}