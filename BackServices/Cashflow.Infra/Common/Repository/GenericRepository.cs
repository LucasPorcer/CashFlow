using Microsoft.EntityFrameworkCore;
using Cashflow.Domain.Common.Entities;
using Cashflow.Domain.Common.Repository;
using System.Linq.Expressions;

namespace Cashflow.Infra.Common.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : Entity
    {
        protected readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public virtual async Task AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task<IList<T>> FindAsync(Expression<Func<T, bool>> clauses)
        {
            return await this._context
                .Set<T>()
                .AsNoTracking()
                .Where(clauses)
                .ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.Set<T>().Update(entity);

            await _context.SaveChangesAsync();
        }
    }
}

