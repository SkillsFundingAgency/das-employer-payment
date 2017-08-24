using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerPayments.Application.Validation;
using SFA.DAS.EmployerPayments.Domain.Interfaces;

namespace SFA.DAS.EmployerPayments.Application.Commands.AuditCommand
{
    public class CreateAuditCommandHandler : AsyncRequestHandler<CreateAuditCommand>
    {
        private readonly IAuditService _auditService;
        private readonly IValidator<CreateAuditCommand> _validator;

        public CreateAuditCommandHandler(IAuditService auditService, IValidator<CreateAuditCommand> validator)
        {
            _auditService = auditService;
            _validator = validator;
        }

        protected override async Task HandleCore(CreateAuditCommand message)
        {
            var validationResult = _validator.Validate(message);

            if (!validationResult.IsValid())
            {
                throw new InvalidRequestException(validationResult.ValidationDictionary);
            }

            await _auditService.SendAuditMessage(message.EasAuditMessage);
        }
    }
}