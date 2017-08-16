using MediatR;

namespace SFA.DAS.EmployerPayments.Application.Commands.Payments.RefreshPaymentData
{
    public class RefreshPaymentDataCommand : IAsyncRequest
    {
        public long AccountId { get; set; }
        public string PaymentUrl { get; set; }
        public string PeriodEnd { get; set; }
    }
}
