using MediatR;
using SFA.DAS.EmployerPayments.Domain.Models.Audit;

namespace SFA.DAS.EmployerPayments.Application.Commands.AuditCommand
{
    public class CreateAuditCommand : IAsyncRequest
    {
        public EasAuditMessage EasAuditMessage { get; set; }
    }
}
