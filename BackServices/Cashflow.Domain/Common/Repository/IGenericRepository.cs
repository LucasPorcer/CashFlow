using Cashflow.Domain.Common.Entities;

using System.Linq.Expressions;

namespace Cashflow.Domain.Common.Repository
{
    public interface IGenericRepository<T>
        where T : Entity
    {
        Task AddAsync(T entity);
        Task<IList<T>> FindAsync(Expression<Func<T, bool>> clauses);
        Task UpdateAsync(T entity);
    }
}
