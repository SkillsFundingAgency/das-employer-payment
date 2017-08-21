using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerAccounts.Events.Messages;
using SFA.DAS.EmployerPayments.Application.Commands.CreateAccountReference;
using SFA.DAS.EmployerPayments.Domain.Attributes;
using SFA.DAS.Messaging;
using SFA.DAS.NLog.Logger;

namespace SFA.DAS.EmployerPayments.Worker.Providers
{
    
    public class PaymentAccountCreated : MessageProcessor<AccountCreatedMessage>
    {
        private readonly ILog _log;
        private readonly IMediator _mediator;

        [QueueName("employer_shared")]
        public string account_created_message { get; set; }

        public PaymentAccountCreated(IPollingMessageReceiver pollingMessageReceiver, ILog log, IMediator mediator) : base(pollingMessageReceiver, log)
        {
            _log = log;
            _mediator = mediator;
        }

        protected override async Task ProcessMessage(AccountCreatedMessage messageContent)
        {
            _log.Info($"Processing adding account reference for AccountId:{messageContent.AccountId}");

            await _mediator.SendAsync(new CreateAccountReferenceCommand {AccountId = messageContent.AccountId});
        }
    }
}
