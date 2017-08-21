using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerPayments.Application.Validation;
using SFA.DAS.EmployerPayments.Domain.Data.Repositories;

namespace SFA.DAS.EmployerPayments.Application.Commands.CreateAccountReference
{
    public class CreateAccountReferenceCommandHandler : AsyncRequestHandler<CreateAccountReferenceCommand>
    {
        private readonly IValidator<CreateAccountReferenceCommand> _validator;
        private readonly IPaymentsRepository _paymentsRepository;

        public CreateAccountReferenceCommandHandler(IValidator<CreateAccountReferenceCommand> validator, IPaymentsRepository paymentsRepository)
        {
            _validator = validator;
            _paymentsRepository = paymentsRepository;
        }

        protected override async Task HandleCore(CreateAccountReferenceCommand message)
        {
            var validationResult = _validator.Validate(message);

            if (!validationResult.IsValid())
            {
                throw new InvalidRequestException(validationResult.ValidationDictionary);
            }

            await _paymentsRepository.CreateAccountReference(message.AccountId);
        }
    }
}
