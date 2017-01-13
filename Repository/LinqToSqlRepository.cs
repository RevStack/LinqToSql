using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Linq;
using RevStack.Pattern;

namespace RevStack.LinqToSql
{
    public class LinqToSqlRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected readonly DataContext _context;
        public LinqToSqlRepository(DataContext context)
        {
            _context = context;
        }

        public TEntity Add(TEntity entity)
        {
            _context.GetTable<TEntity>().InsertOnSubmit(entity);
            _context.SubmitChanges();
            return entity;
        }

        public void Delete(TEntity entity)
        {
            _context.GetTable<TEntity>().DeleteOnSubmit(entity);
            _context.SubmitChanges();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.GetTable<TEntity>().Where(predicate).AsQueryable();
        }

        public IEnumerable<TEntity> Get()
        {
            return _context.GetTable<TEntity>();
        }

        public TEntity Update(TEntity entity)
        {
            _context.SubmitChanges();
            return entity;
        }
    }
}