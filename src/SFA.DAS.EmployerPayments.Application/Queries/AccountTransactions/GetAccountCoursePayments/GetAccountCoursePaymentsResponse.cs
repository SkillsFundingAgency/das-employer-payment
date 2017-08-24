using System.Collections.Generic;
using SFA.DAS.EmployerPayments.Domain.Models.Transaction;

namespace SFA.DAS.EmployerPayments.Application.Queries.AccountTransactions.GetAccountCoursePayments
{
    public class GetAccountCoursePaymentsResponse
    {
        public List<TransactionLine> Transactions { get; set; }
    }
}

