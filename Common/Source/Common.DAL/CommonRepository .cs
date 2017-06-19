using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TestTask.Common.DAL
{
    public class CommonRepository<TContext, TEntity, TPk> : ICommonRepository<TContext, TEntity, TPk> 
        where TEntity : class 
        where TContext : DbContext
    {
        protected readonly TContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public CommonRepository(TContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public TContext RepositoryContext
        {
            get { return Context; }
        }

        public virtual void Insert(TEntity item)
        {
            DbSet.Add(item);
        }

        public virtual async Task<TEntity> GetByIdAsync(TPk id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual TEntity GetById(TPk id)
        {
            return DbSet.Find(id);
        }

        public virtual void Update(TEntity item)
        {
            Context.Entry(item).State = EntityState.Modified;
        }

        public virtual void AddOrUpdate(TEntity item)
        {
            Context.Set<TEntity>().AddOrUpdate(item);
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }


        public async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}
