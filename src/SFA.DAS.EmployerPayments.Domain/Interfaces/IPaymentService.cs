using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.EmployerPayments.Domain.Models.Payments;

namespace SFA.DAS.EmployerPayments.Domain.Interfaces
{
    public interface IPaymentService
    {
        Task<ICollection<PaymentDetails>> GetAccountPayments(string periodEnd, long employerAccountId);
    }
}
