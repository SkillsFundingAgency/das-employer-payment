using System;
using System.Threading.Tasks;
using SFA.DAS.EmployerAccounts.Events.Messages;
using SFA.DAS.EmployerPayments.Domain.Attributes;
using SFA.DAS.Messaging;
using SFA.DAS.NLog.Logger;

namespace SFA.DAS.EmployerPayments.Worker.Providers
{
    
    public class PaymentAccountCreated : MessageProcessor<AccountCreatedMessage>
    {
        [QueueName("employer_shared")]
        public string account_created_message { get; set; }

        public PaymentAccountCreated(IPollingMessageReceiver pollingMessageReceiver, ILog log) : base(pollingMessageReceiver, log)
        {

        }

        protected override async Task ProcessMessage(AccountCreatedMessage messageContent)
        {
            
        }
    }
}
