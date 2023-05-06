using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contract.Prisistence;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsynceRepository<T> where T : EntityBase
    {
        private readonly OrderContext orderContext;
        private DbSet<T> dbSet;
        public RepositoryBase(OrderContext orderContext)
        {
            this.orderContext = orderContext;
            this.dbSet = orderContext.Set<T>();
        }

        public async Task<T> AddAsynce(T entity)
        {
           await dbSet.AddAsync(entity);
            await orderContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(T entity)
        {
            dbSet.Remove(entity);
            await orderContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeString = null, bool disableTracking = true)
        {
            var query = dbSet.AsQueryable();
            if (disableTracking) query = query.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);
            if (predicate != null) query = query.Where(predicate);
            return orderBy == null ? await query.ToListAsync() : await orderBy(query).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, List<Expression<Func<T, object>>>? includes = null, bool disableTracking = true)
        {
            var query = dbSet.AsQueryable();
            if (disableTracking) query = query.AsNoTracking();
            if (includes != null) query = includes.Aggregate(query,
                                                             (current, include) => current.Include(include));
            if (predicate != null) query = query.Where(predicate);
            return orderBy == null ? await dbSet.ToListAsync() : await orderBy(dbSet).ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public Task<T> GetByIdOrDefaultAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Update(T entity)
        {
            orderContext.Entry(entity).State=EntityState.Modified;
            await orderContext.SaveChangesAsync();
        }
    }
}
