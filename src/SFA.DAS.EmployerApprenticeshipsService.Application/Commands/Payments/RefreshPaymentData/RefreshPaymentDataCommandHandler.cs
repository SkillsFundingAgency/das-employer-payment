using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerPayments.Application.Events.ProcessPayment;
using SFA.DAS.EmployerPayments.Application.Validation;
using SFA.DAS.EmployerPayments.Domain.Data.Repositories;
using SFA.DAS.EmployerPayments.Domain.Interfaces;
using SFA.DAS.EmployerPayments.Domain.Models.Payments;
using SFA.DAS.NLog.Logger;

namespace SFA.DAS.EmployerPayments.Application.Commands.Payments.RefreshPaymentData
{
    public class RefreshPaymentDataCommandHandler : AsyncRequestHandler<RefreshPaymentDataCommand>
    {
        private readonly IValidator<RefreshPaymentDataCommand> _validator;
        private readonly IPaymentService _paymentService;
        private readonly IDasLevyRepository _dasLevyRepository;
        private readonly IMediator _mediator;
        private readonly ILog _logger;
       

        public RefreshPaymentDataCommandHandler(
            IValidator<RefreshPaymentDataCommand> validator, 
            IPaymentService paymentService, 
            IDasLevyRepository dasLevyRepository, 
            IMediator mediator,
            ILog logger)
        {
            _validator = validator;
            _paymentService = paymentService;
            _dasLevyRepository = dasLevyRepository;
            _mediator = mediator;
            _logger = logger;
        }

        protected override async Task HandleCore(RefreshPaymentDataCommand message)
        {
            var validationResult = _validator.Validate(message);

            if (!validationResult.IsValid())
            {
                throw new InvalidRequestException(validationResult.ValidationDictionary);
            }

            ICollection<PaymentDetails> payments = null;

            try
            {
                payments = await _paymentService.GetAccountPayments(message.PeriodEnd, message.AccountId);
            }
            catch (WebException ex)
            {
                _logger.Error(ex,$"Unable to get payment information for {message.PeriodEnd} accountid {message.AccountId}");
            }

            if (payments == null || !payments.Any()) return;

            var existingPaymentIds = await _dasLevyRepository.GetAccountPaymentIds(message.AccountId);

            var newPayments = payments.Where(p => !existingPaymentIds.Any(x => x.ToString().Equals(p.Id))).ToArray();
            
            if(!newPayments.Any()) return;

            await _dasLevyRepository.CreatePaymentData(newPayments);

            await _mediator.PublishAsync(new ProcessPaymentEvent { AccountId = message.AccountId});
        }
    }
}