using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CommonLib.EF
{
    public interface IBaseRepository<T>
    {
        bool IsExist(Func<T, bool> predicat);
        T Find(int Id);
        IEnumerable<T> Where(Expression<Func<T, bool>> predicate);
        T FirstOrDefault(Expression<Func<T, bool>> predicate);
        T FirstOrDefault();

        int Count(Expression<Func<T, bool>> predicate);

        T Add(T entity);
        IEnumerable<T> Add(IEnumerable<T> entities);

        void Delete(T entity);
        void Delete(IEnumerable<T> entities);

        void Update(T entity);
        void Update(IEnumerable<T> entities);
    }
}
