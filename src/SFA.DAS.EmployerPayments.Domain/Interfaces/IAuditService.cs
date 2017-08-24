using System.Threading.Tasks;
using SFA.DAS.EmployerPayments.Domain.Models.Audit;

namespace SFA.DAS.EmployerPayments.Domain.Interfaces
{
    public interface IAuditService
    {
        Task SendAuditMessage(EasAuditMessage message);
    }
}