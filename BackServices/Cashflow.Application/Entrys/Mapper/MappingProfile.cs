using AutoMapper;
using Cashflow.Application.Entry.Command;
using Cashflow.Domain.Entities.Cashflow;
using Cashflow.Domain.ViewModel.Cashflow;

namespace Cashflow.Application.Entry.Mapper
{
    public class EntryMappingProfile : Profile
    {
        public EntryMappingProfile()
        {
            CreateMap<CreateEntryCommand, CashflowModel>();

            CreateMap<IList<CashflowModel>, IList<DailyBalanceDto>>()
                .ConvertUsing<ConsolidatedDailyBalanceConverter>();
        }
    }

    public class ConsolidatedDailyBalanceConverter : ITypeConverter<IList<CashflowModel>, IList<DailyBalanceDto>>
    {
        public IList<DailyBalanceDto> Convert(IList<CashflowModel> source, IList<DailyBalanceDto> destination, ResolutionContext context)
        {
            return source.GroupBy(x => x.ConsilidationDate.Value.Date)
                .Select(flow =>
                {
                    return new DailyBalanceDto
                    {
                        Date = flow.Key.Date,
                        Balance = flow.Sum(x => x.Value)
                    };
                })
                .ToList();
        }
    }
}
