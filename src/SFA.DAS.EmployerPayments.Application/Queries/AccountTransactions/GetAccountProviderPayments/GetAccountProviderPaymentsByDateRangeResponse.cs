using System.Collections.Generic;
using SFA.DAS.EmployerPayments.Domain.Models.Transaction;

namespace SFA.DAS.EmployerPayments.Application.Queries.AccountTransactions.GetAccountProviderPayments
{
    public class GetAccountProviderPaymentsByDateRangeResponse
    {
        public List<TransactionLine> Transactions { get; set; }
    }
}