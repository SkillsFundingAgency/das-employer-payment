﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.EmployerPayments.Domain.Models.Transaction;

namespace SFA.DAS.EmployerPayments.Domain.Data.Repositories
{
    public interface ITransactionRepository
    {
        Task<List<TransactionLine>> GetAccountTransactionByProviderAndDateRange(long accountId, long ukprn, DateTime fromDate, DateTime toDate);

        Task<List<TransactionLine>> GetAccountCoursePaymentsByDateRange(long accountId, long ukprn, string courseName, int courseLevel, int? pathwayCode, DateTime fromDate, DateTime toDate);
    }
}
