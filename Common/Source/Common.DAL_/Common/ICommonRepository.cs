using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Taxcom.Egais.DAL.Repositories
{
    public interface ICommonRepository<TEntity, in TPk> where TEntity : class
    {
        void Insert(TEntity item);

        TEntity GetById(TPk id);

        Task<TEntity> GetByIdAsync(TPk id);

        void Update(TEntity item);

        void AddOrUpdate(TEntity item);

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null);

        Task SaveChangesAsync();
    }
}
