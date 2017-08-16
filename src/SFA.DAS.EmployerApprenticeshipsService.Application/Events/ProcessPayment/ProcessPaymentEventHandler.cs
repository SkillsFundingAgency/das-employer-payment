using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerPayments.Domain.Data.Repositories;
using SFA.DAS.NLog.Logger;

namespace SFA.DAS.EmployerPayments.Application.Events.ProcessPayment
{
    public class ProcessPaymentEventHandler : IAsyncNotificationHandler<ProcessPaymentEvent>

    {
        private readonly IDasLevyRepository _dasLevyRepository;
        private readonly ILog _logger;

        public ProcessPaymentEventHandler(IDasLevyRepository dasLevyRepository, ILog logger)
        {
            _dasLevyRepository = dasLevyRepository;
            _logger = logger;
        }

        public async Task Handle(ProcessPaymentEvent notification)
        {

            //TODO this needs to queue a message to say that the payment data is ready 

            //await _dasLevyRepository.ProcessPaymentData(notification.AccountId);

            _logger.Info("Process Payments Called");
        }
    }
}