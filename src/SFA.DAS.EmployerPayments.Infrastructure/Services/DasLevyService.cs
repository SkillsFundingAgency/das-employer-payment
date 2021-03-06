using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerPayments.Application.Queries.AccountTransactions.GetAccountCoursePayments;
using SFA.DAS.EmployerPayments.Application.Queries.AccountTransactions.GetAccountProviderPayments;
using SFA.DAS.EmployerPayments.Domain.Interfaces;
using SFA.DAS.EmployerPayments.Domain.Models.Transaction;

namespace SFA.DAS.EmployerPayments.Infrastructure.Services
{
    public class DasLevyService : IDasLevyService
    {
        private readonly IMediator _mediator;

        public DasLevyService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<ICollection<T>> GetAccountProviderPaymentsByDateRange<T>(
            long accountId, long ukprn, DateTime fromDate, DateTime toDate) where T : TransactionLine
        {
            var result = await _mediator.SendAsync(new GetAccountProviderPaymentsByDateRangeQuery
            {
                AccountId = accountId,
                UkPrn = ukprn,
                FromDate = fromDate,
                ToDate = toDate
            });

            return result?.Transactions?.OfType<T>().ToList() ?? new List<T>();
        }

        public async Task<ICollection<T>> GetAccountCoursePaymentsByDateRange<T>(
            long accountId, long ukprn, string courseName, int courseLevel, int? pathwayCode, DateTime fromDate,
            DateTime toDate) where T : TransactionLine
        {
            var result = await _mediator.SendAsync(new GetAccountCoursePaymentsQuery
            {
                AccountId = accountId,
                UkPrn = ukprn,
                CourseName = courseName,
                CourseLevel = courseLevel,
                PathwayCode = pathwayCode,
                FromDate = fromDate,
                ToDate = toDate
            });

            return result?.Transactions?.OfType<T>().ToList() ?? new List<T>();
        }

        
    }
}