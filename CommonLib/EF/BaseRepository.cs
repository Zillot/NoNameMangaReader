using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace CommonLib.EF
{
    public abstract class BaseRepository<T, Context> : IBaseRepository<T, Context> 
        where T : class
        where Context : DbContext
    {
        public Context _webParserContext { get; set; }

        public BaseRepository(Context webParserContext)
        {
            _webParserContext = webParserContext;
        }

        public bool IsExist(Func<T, bool> predicat)
        {
            return _webParserContext.Set<T>().Any(predicat);
        }

        public T Find(int Id)
        {
            return _webParserContext.Set<T>().Find(Id);
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
        {
            return _webParserContext.Set<T>().Where(predicate).ToArray();
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _webParserContext.Set<T>().FirstOrDefault(predicate);
        }

        public T FirstOrDefault()
        {
            return _webParserContext.Set<T>().FirstOrDefault();
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            return _webParserContext.Set<T>().Count(predicate);
        }

        public IEnumerable<T> ToList()
        {
            return _webParserContext.Set<T>().ToList();
        }

        public T Add(T entity)
        {
            _webParserContext.Entry(entity).State = EntityState.Added;
            _webParserContext.SaveChanges();

            return entity;
        }

        public IEnumerable<T> Add(IEnumerable<T> entities)
        {
            _webParserContext.Set<T>().AddRange(entities);
            _webParserContext.SaveChanges();

            return _webParserContext.Set<T>().ToArray();
        }

        public void Delete(T entity)
        {
            _webParserContext.Entry(entity).State = EntityState.Deleted;
            _webParserContext.SaveChanges();
        }

        public void Delete(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _webParserContext.Entry(entity).State = EntityState.Deleted;
            }

            _webParserContext.SaveChanges();
        }

        public void Update(T entity)
        {
            _webParserContext.Entry(entity).State = EntityState.Modified;
            _webParserContext.SaveChanges();
        }

        public void Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _webParserContext.Entry(entity).State = EntityState.Modified;
            }

            _webParserContext.SaveChanges();
        }
    }
}
