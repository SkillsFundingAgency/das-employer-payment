using SFA.DAS.Messaging.Attributes;

namespace SFA.DAS.EmployerPayments.Application.Messages
{
    [QueueName("refresh_payments")]
    public class PaymentProcessorQueueMessage
    {
        public string AccountPaymentUrl { get; set; }
        public long AccountId { get; set; }
        public string PeriodEndId { get; set; }
    }
}