using MediatR;

namespace Cashflow.Application.Entry.Command
{
    public class CreateEntryCommand : IRequest
    {
        public decimal Value { get; set; }
    }
}
