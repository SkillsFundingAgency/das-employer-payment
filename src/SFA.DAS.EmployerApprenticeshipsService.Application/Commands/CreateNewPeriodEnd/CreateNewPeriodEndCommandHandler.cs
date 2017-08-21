using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerPayments.Application.Validation;
using SFA.DAS.EmployerPayments.Domain.Data.Repositories;

namespace SFA.DAS.EmployerPayments.Application.Commands.CreateNewPeriodEnd
{
    public class CreateNewPeriodEndCommandHandler : AsyncRequestHandler<CreateNewPeriodEndCommand>
    {
        private readonly IValidator<CreateNewPeriodEndCommand> _validator;
        private readonly IPaymentsRepository _paymentsRepository;

        public CreateNewPeriodEndCommandHandler(IValidator<CreateNewPeriodEndCommand> validator, IPaymentsRepository paymentsRepository)
        {
            _validator = validator;
            _paymentsRepository = paymentsRepository;
        }

        protected override async Task HandleCore(CreateNewPeriodEndCommand message)
        {
            var validationResult = _validator.Validate(message);

            if (!validationResult.IsValid())
            {
                throw new InvalidRequestException(validationResult.ValidationDictionary);
            }

            await _paymentsRepository.CreateNewPeriodEnd(message.NewPeriodEnd);
        }
    }
}