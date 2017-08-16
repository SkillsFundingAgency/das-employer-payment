using MediatR;

namespace SFA.DAS.EmployerPayments.Application.Events.ProcessPayment
{
    public class ProcessPaymentEvent : IAsyncNotification
    {
        public long AccountId { get; set; }
    }
}