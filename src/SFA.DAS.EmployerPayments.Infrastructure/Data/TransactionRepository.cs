﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using AutoMapper;
using Dapper;
using SFA.DAS.EmployerPayments.Domain.Configuration;
using SFA.DAS.EmployerPayments.Domain.Data.Entities.Transaction;
using SFA.DAS.EmployerPayments.Domain.Data.Repositories;
using SFA.DAS.EmployerPayments.Domain.Models.Payments;
using SFA.DAS.EmployerPayments.Domain.Models.Transaction;
using SFA.DAS.NLog.Logger;
using SFA.DAS.Sql.Client;

namespace SFA.DAS.EmployerPayments.Infrastructure.Data
{
    public class TransactionRepository : BaseRepository, ITransactionRepository
    {
        private readonly IMapper _mapper;

        public TransactionRepository(EmployerPaymentsConfiguration configuration, IMapper mapper, ILog logger)
            : base(configuration.DatabaseConnectionString, logger)
        {
            _mapper = mapper;
        }

        
        public async Task<List<TransactionLine>> GetAccountTransactionByProviderAndDateRange(
            long accountId, long ukprn, DateTime fromDate, DateTime toDate)
        {
            var result = await WithConnection(async c =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@accountId", accountId, DbType.Int64);
                parameters.Add("@ukprn", ukprn, DbType.Int64);
                parameters.Add("@fromDate", new DateTime(fromDate.Year, fromDate.Month, fromDate.Day), DbType.DateTime);
                parameters.Add("@toDate", new DateTime(toDate.Year, toDate.Month, toDate.Day, 23, 59, 59), DbType.DateTime);

                return await c.QueryAsync<TransactionEntity>(
                    sql: "[GetPaymentDetail_ByAccountProviderAndDateRange]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            });

            return MapTransactions(result);
        }
        
        public async Task<List<TransactionLine>> GetAccountCoursePaymentsByDateRange(
            long accountId, long ukprn, string courseName, int courseLevel, int? pathwayCode, DateTime fromDate, DateTime toDate)
        {
            var result = await WithConnection(async c =>
            {
                var parameters = new DynamicParameters();
                parameters.Add("@accountId", accountId, DbType.Int64);
                parameters.Add("@ukprn", ukprn, DbType.Int64);
                parameters.Add("@courseName", courseName, DbType.String);
                parameters.Add("@courseLevel", courseLevel, DbType.Int32);
                parameters.Add("@pathwayCode", pathwayCode, DbType.Int32);
                parameters.Add("@fromDate", new DateTime(fromDate.Year, fromDate.Month, fromDate.Day), DbType.DateTime);
                parameters.Add("@toDate", new DateTime(toDate.Year, toDate.Month, toDate.Day, 23, 59, 59), DbType.DateTime);

                return await c.QueryAsync<TransactionEntity>(
                    sql: "[GetPaymentDetail_ByAccountProviderCourseAndDateRange]",
                    param: parameters,
                    commandType: CommandType.StoredProcedure);
            });

            return MapTransactions(result);
        }
        

        private List<TransactionLine> MapTransactions(IEnumerable<TransactionEntity> transactionEntities)
        {
            var transactions = new List<TransactionLine>();

            foreach (var entity in transactionEntities)
            {
                switch (entity.TransactionType)
                {
                    case TransactionItemType.Payment:
                        transactions.Add(_mapper.Map<PaymentTransactionLine>(entity));
                        break;

                    case TransactionItemType.Unknown:
                        transactions.Add(_mapper.Map<TransactionLine>(entity));
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return transactions;
        }
    }
}
