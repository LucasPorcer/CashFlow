using AutoMapper;
using FluentValidation;
using MediatR;
using ValidationException = FluentValidation.ValidationException;
using Cashflow.Domain.Entities.Cashflow;
using Cashflow.Domain.Interfaces.Repository;

namespace Cashflow.Application.Entry.Command
{
    public class CreateEntryCommandHandler : IRequestHandler<CreateEntryCommand>
    {
        private readonly IValidator<CreateEntryCommand> _validator;
        private readonly IMapper _mapper;
        private readonly ICashflowRepository _transactionRepository;

        public CreateEntryCommandHandler(IValidator<CreateEntryCommand> validator, ICashflowRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task Handle(CreateEntryCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request);

            if (!validationResult.IsValid)
                throw new ValidationException("", validationResult.Errors);

            var transaction = _mapper.Map<CashflowModel>(request);

            await _transactionRepository.AddAsync(transaction);
        }
    }
}
