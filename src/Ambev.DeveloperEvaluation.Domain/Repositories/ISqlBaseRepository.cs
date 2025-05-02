using Ambev.DeveloperEvaluation.Domain.Common;
using System.Linq.Expressions;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ISqlBaseRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
    Task<IEnumerable<T>> FindWithIncludesAsync(
        Expression<Func<T, bool>> predicate,
        Expression<Func<T, object>>[]? includes = null,
        bool asNoTracking = true
    );

    Task<IEnumerable<T>> QueryAsync(QueryOptions<T> options);

    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);

    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
    Task DeleteAsync(T entity);
    Task<int> CountAsync(Expression<Func<T, bool>>? filter = null);

}
