using MediatR;
using SFA.DAS.EmployerPayments.Domain.Models.Payments;

namespace SFA.DAS.EmployerPayments.Application.Commands.CreateNewPeriodEnd
{
    public class CreateNewPeriodEndCommand : IAsyncRequest
    {
        public PeriodEnd NewPeriodEnd { get; set; }
    }
}
