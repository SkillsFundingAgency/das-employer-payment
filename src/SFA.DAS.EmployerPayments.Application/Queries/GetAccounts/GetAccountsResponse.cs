using System.Collections.Generic;

namespace SFA.DAS.EmployerPayments.Application.Queries.GetAccounts
{
    public class GetAccountsResponse
    {
        public IEnumerable<long> AccountIds { get; set; }
    }
}