using MediatR;
using Cashflow.Domain.Entities.Cashflow;

namespace Cashflow.Application.Entry.Query
{
    public class DailyBalanceQuery : IRequest<List<CashflowModel>>
    {
    }
}
