using Microsoft.EntityFrameworkCore;
using Cashflow.Infra.Common.Repository;
using Cashflow.Infra.Persistence.Context.SqlServer.Context;
using Cashflow.Domain.Entities.Cashflow;
using Cashflow.Domain.Interfaces.Repository;

namespace Cashflow.Infra.Repositories.Transaction
{
    public class CashflowRepository : GenericRepository<CashflowModel>, ICashflowRepository
    {
        public CashflowRepository(CashflowSqlServerContext context) : base(context)
        {
        }

        public async Task<List<CashflowModel>> GetConsolidatedBalance()
        {
            var query = await _context.Set<CashflowModel>().AsNoTrackingWithIdentityResolution()
                .Where(x => x.IsConsolidated).ToListAsync();

            return query;
        }
    }
}
