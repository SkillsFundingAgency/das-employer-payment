using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerPayments.Domain.Data.Repositories;

namespace SFA.DAS.EmployerPayments.Application.Queries.Payments.GetCurrentPeriodEnd
{
    public class GetCurrentPeriodEndQueryHandler : IAsyncRequestHandler<GetCurrentPeriodEndRequest, GetPeriodEndResponse>
    {
        private readonly IPaymentsRepository _paymentsRepository;

        public GetCurrentPeriodEndQueryHandler(IPaymentsRepository paymentsRepository)
        {
            _paymentsRepository = paymentsRepository;
        }

        public async Task<GetPeriodEndResponse> Handle(GetCurrentPeriodEndRequest message)
        {
            var response = new GetPeriodEndResponse();

            var result = await _paymentsRepository.GetLatestPeriodEnd();
            response.CurrentPeriodEnd = result;

            return response;
        }
    }
}