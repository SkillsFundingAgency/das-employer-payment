using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.EAS.Domain.Models.Payments;

namespace SFA.DAS.EAS.Domain.Data.Repositories
{
    public interface IDasLevyRepository
    {
        Task CreateNewPeriodEnd(PeriodEnd periodEnd);
        Task<PeriodEnd> GetLatestPeriodEnd();
        Task CreatePaymentData(IEnumerable<PaymentDetails> payments);
        Task<Payment> GetPaymentData(Guid paymentId);
        Task<IEnumerable<Guid>> GetAccountPaymentIds(long accountId);

    }
}
