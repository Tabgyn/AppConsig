using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AppConsig.Common;

namespace AppConsig.Services.Interfaces
{
    public interface IEntityService<T> : IService where T : BaseEntity
    {
        T GetById(long id);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null);
        IEnumerable<T> GetAllPaged(Expression<Func<T, bool>> filter = null, int pageNumber = 1, int pageSize = 5);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}