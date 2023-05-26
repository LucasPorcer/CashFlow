using MediatR;
using Cashflow.Domain.Entities.Cashflow;
using Cashflow.Domain.Interfaces.Repository;

namespace Cashflow.Application.Entry.Query
{
    public class DailyBalanceQueryHandler : IRequestHandler<DailyBalanceQuery, List<CashflowModel>>
    {
        private readonly ICashflowRepository _transactionRepository;

        public DailyBalanceQueryHandler(ICashflowRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<List<CashflowModel>> Handle(DailyBalanceQuery request, CancellationToken cancellationToken)
        {
            return await _transactionRepository.GetConsolidatedBalance();
        }
    }
}