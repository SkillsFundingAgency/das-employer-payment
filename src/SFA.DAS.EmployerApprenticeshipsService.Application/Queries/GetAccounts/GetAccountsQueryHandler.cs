using System;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerPayments.Domain.Data.Repositories;

namespace SFA.DAS.EmployerPayments.Application.Queries.GetAccounts
{
    public class GetAccountsQueryHandler : IAsyncRequestHandler<GetAccountsQuery, GetAccountsResponse>
    {
        private readonly IPaymentsRepository _paymentsRepository;

        public GetAccountsQueryHandler(IPaymentsRepository paymentsRepository)
        {
            _paymentsRepository = paymentsRepository;
        }

        public async Task<GetAccountsResponse> Handle(GetAccountsQuery message)
        {
            var accounts = await _paymentsRepository.GetAccounts();

            return new GetAccountsResponse
            {
                AccountIds = accounts
            };
        }
    }
}
