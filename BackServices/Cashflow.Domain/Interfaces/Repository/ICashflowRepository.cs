using Cashflow.Domain.Common.Repository;
using Cashflow.Domain.Entities.Cashflow;

namespace Cashflow.Domain.Interfaces.Repository
{
    public interface ICashflowRepository: IGenericRepository<CashflowModel>
    {
        Task<List<CashflowModel>> GetConsolidatedBalance();
    }
}
