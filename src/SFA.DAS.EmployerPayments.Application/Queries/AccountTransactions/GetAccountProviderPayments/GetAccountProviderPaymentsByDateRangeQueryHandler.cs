using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerPayments.Application.Validation;
using SFA.DAS.EmployerPayments.Domain.Data.Repositories;
using SFA.DAS.EmployerPayments.Domain.Interfaces;

namespace SFA.DAS.EmployerPayments.Application.Queries.AccountTransactions.GetAccountProviderPayments
{
    public class GetAccountProviderPaymentsByDateRangeQueryHandler : IAsyncRequestHandler<GetAccountProviderPaymentsByDateRangeQuery,GetAccountProviderPaymentsByDateRangeResponse>
    {
        private readonly IValidator<GetAccountProviderPaymentsByDateRangeQuery> _validator;
        private readonly ITransactionRepository _transactionRepository;
        private readonly IHmrcDateService _hmrcDateService;


        public GetAccountProviderPaymentsByDateRangeQueryHandler(IValidator<GetAccountProviderPaymentsByDateRangeQuery> validator, ITransactionRepository transactionRepository, IHmrcDateService hmrcDateService)
        {
            _validator = validator;
            _transactionRepository = transactionRepository;
            _hmrcDateService = hmrcDateService;
        }

        public async Task<GetAccountProviderPaymentsByDateRangeResponse> Handle(GetAccountProviderPaymentsByDateRangeQuery message)
        {
            var validationResult = await _validator.ValidateAsync(message);
            
            if (!validationResult.IsValid())
            {
                throw new InvalidRequestException(validationResult.ValidationDictionary);
            }

            var transactions = await _transactionRepository.GetAccountTransactionByProviderAndDateRange(
                message.AccountId, 
                message.UkPrn,
                message.FromDate,
                message.ToDate);

            foreach (var transaction in transactions)
            {
                if (!string.IsNullOrEmpty(transaction.PayrollYear) && transaction.PayrollMonth != 0)
                {
                    transaction.PayrollDate = _hmrcDateService.GetDateFromPayrollYearMonth(transaction.PayrollYear, transaction.PayrollMonth);
                }
            }

            return new GetAccountProviderPaymentsByDateRangeResponse { Transactions = transactions };
        }
    }
}
