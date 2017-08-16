
using SFA.DAS.EmployerPayments.Domain.Models.Payments;

namespace SFA.DAS.EmployerPayments.Application.Queries.Payments.GetCurrentPeriodEnd
{
    public class GetPeriodEndResponse
    {
        public PeriodEnd CurrentPeriodEnd { get; set; }
    }
}