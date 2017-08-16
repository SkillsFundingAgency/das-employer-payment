﻿namespace SFA.DAS.EmployerPayments.Application.Messages
{
    public class PaymentProcessorQueueMessage
    {
        public string AccountPaymentUrl { get; set; }
        public long AccountId { get; set; }
        public string PeriodEndId { get; set; }
    }
}