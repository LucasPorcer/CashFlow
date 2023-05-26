using Cashflow.Application.Entry.Command;
using FluentValidation;

namespace Cashflow.Application.Entrys.Commands
{
    public class CreateEntryCommandValidator : AbstractValidator<CreateEntryCommand>
    {
        public CreateEntryCommandValidator()
        {  
            RuleFor(x => x.Value).NotEqual(0)
                .WithMessage("O Campo {PropertyName} deve ser diferente de 0");
        }
    }
}
