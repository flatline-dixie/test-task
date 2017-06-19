using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestTask.Common.DAL
{
    public interface ICommonRepository<TContext, TEntity, in TPk>
        where TEntity : class
        where TContext : DbContext
    {
        void Insert(TEntity item);

        TContext RepositoryContext { get; }

        TEntity GetById(TPk id);

        Task<TEntity> GetByIdAsync(TPk id);

        void Update(TEntity item);

        void AddOrUpdate(TEntity item);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        Task SaveChangesAsync();
    }
}
