using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerPayments.Application.Commands.Payments.RefreshPaymentData;
using SFA.DAS.EmployerPayments.Application.Messages;
using SFA.DAS.EmployerPayments.Domain.Attributes;
using SFA.DAS.Messaging;
using SFA.DAS.NLog.Logger;

namespace SFA.DAS.EmployerPayments.Worker.Providers
{
    public class PaymentDataProcessor : MessageProcessor<PaymentProcessorQueueMessage>
    {
        private readonly IMediator _mediator;
        private readonly ILog _logger;

        [ServiceBusConnectionKey("employer_payments")]
        public PaymentDataProcessor(IPollingMessageReceiver pollingMessageReceiver, IMediator mediator, ILog logger) : base(pollingMessageReceiver, logger)
        {
            _mediator = mediator;
            _logger = logger;
        }


        protected override async Task ProcessMessage(PaymentProcessorQueueMessage messageContent)
        {
            _logger.Info($"Processing refresh payment command for AccountId:{messageContent.AccountId} PeriodEnd:{messageContent.PeriodEndId}");

            await _mediator.SendAsync(new RefreshPaymentDataCommand
            {
                AccountId = messageContent.AccountId,
                PeriodEnd = messageContent.PeriodEndId,
                PaymentUrl = messageContent.AccountPaymentUrl
            });

        }
    }
}
