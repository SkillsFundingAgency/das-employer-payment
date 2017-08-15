using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.EAS.Domain.Models.Transaction;

namespace SFA.DAS.EAS.Domain.Interfaces
{
    public interface IDasLevyService
    {
        
        
        Task<ICollection<T>> GetAccountProviderPaymentsByDateRange<T>(
            long accountId, long ukprn, DateTime fromDate, DateTime toDate)
            where T : TransactionLine;

        Task<ICollection<T>> GetAccountCoursePaymentsByDateRange<T>(
            long accountId, long ukprn, string courseName, int courseLevel, int? pathwayCode, DateTime fromDate, DateTime toDate)
            where T : TransactionLine;

        
    }
}