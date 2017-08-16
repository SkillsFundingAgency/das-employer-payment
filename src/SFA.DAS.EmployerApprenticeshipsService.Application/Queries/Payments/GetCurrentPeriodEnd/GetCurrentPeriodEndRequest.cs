using MediatR;

namespace SFA.DAS.EmployerPayments.Application.Queries.Payments.GetCurrentPeriodEnd
{
    public class GetCurrentPeriodEndRequest : IAsyncRequest<GetPeriodEndResponse>
    {
    }
}
