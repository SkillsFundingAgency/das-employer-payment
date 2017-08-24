using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.EmployerPayments.Domain.Models.Payments;

namespace SFA.DAS.EmployerPayments.Domain.Data.Repositories
{
    public interface IPaymentsRepository
    {
        Task CreateNewPeriodEnd(PeriodEnd periodEnd);
        Task<PeriodEnd> GetLatestPeriodEnd();
        Task CreatePaymentData(IEnumerable<PaymentDetails> payments);
        Task<Payment> GetPaymentData(Guid paymentId);
        Task<IEnumerable<Guid>> GetAccountPaymentIds(long accountId);
        Task<IEnumerable<long>> GetAccounts();
        Task CreateAccountReference(long accountId);
    }
}
