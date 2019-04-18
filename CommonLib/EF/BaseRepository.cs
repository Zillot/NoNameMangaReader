using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

namespace CommonLib.EF
{

    public abstract class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public bool IsExist(Func<T, bool> predicat)
        {
            using (var dbContext = new EFContext())
            {
                return dbContext.Set<T>().Any(predicat);
            }
        }

        public T Find(int Id)
        {
            using (var dbContext = new EFContext())
            {
                return dbContext.Set<T>().Find(Id);
            }
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> predicate)
        {
            using (var dbContext = new EFContext())
            {
                return dbContext.Set<T>().Where(predicate).ToArray();
            }
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            using (var dbContext = new EFContext())
            {
                return dbContext.Set<T>().FirstOrDefault(predicate);
            }
        }

        public T FirstOrDefault()
        {
            using (var dbContext = new EFContext())
            {
                return dbContext.Set<T>().FirstOrDefault();
            }
        }

        public int Count(Expression<Func<T, bool>> predicate)
        {
            using (var dbContext = new EFContext())
            {
                return dbContext.Set<T>().Count(predicate);
            }
        }

        public T Add(T entity)
        {
            using (var dbContext = new EFContext())
            {
                dbContext.Entry(entity).State = EntityState.Added;
                dbContext.SaveChanges();

                return entity;
            }
        }

        public IEnumerable<T> Add(IEnumerable<T> entities)
        {
            using (var dbContext = new EFContext())
            {
                dbContext.Set<T>().AddRange(entities);
                dbContext.SaveChanges();

                return dbContext.Set<T>().ToArray();
            }
        }

        public void Delete(T entity)
        {
            using (var dbContext = new EFContext())
            {
                dbContext.Entry(entity).State = EntityState.Deleted;
                dbContext.SaveChanges();
            }
        }

        public void Delete(IEnumerable<T> entities)
        {
            using (var dbContext = new EFContext())
            {
                foreach (var entity in entities)
                {
                    dbContext.Entry(entity).State = EntityState.Deleted;
                }

                dbContext.SaveChanges();
            }
        }

        public void Update(T entity)
        {
            using (var dbContext = new EFContext())
            {
                dbContext.Entry(entity).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }

        public void Update(IEnumerable<T> entities)
        {
            using (var dbContext = new EFContext())
            {
                foreach (var entity in entities)
                {
                    dbContext.Entry(entity).State = EntityState.Modified;
                }

                dbContext.SaveChanges();
            }
        }
    }
}
