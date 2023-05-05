using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Contract.Prisistence
{
    public interface IAsynceRepository<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetAllAsynce();
        Task<IReadOnlyList<T>> GetAsynce(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAsynce(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeString = null, bool disableTracking = true);
        Task<IReadOnlyList<T>> GetAsynce(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, Expression<Func<T,object>>? includes = null, bool disableTracking = true);
        Task<T> GetByIdAsync(int  id);
        Task<T> GetByIdOrDefaultAsync(int id);
        Task<T> AddAsynce(T entity);    
        Task Update(T entity);
        Task Delete(T entity);
    }
}   
