using System.Threading;
using System.Threading.Tasks;

namespace SFA.DAS.EmployerPayments.Worker.Providers
{
    public interface IPaymentDataProcessor
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}