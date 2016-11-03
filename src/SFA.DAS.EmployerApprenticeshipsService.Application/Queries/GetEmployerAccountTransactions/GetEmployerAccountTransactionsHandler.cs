﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EAS.Application.Validation;
using SFA.DAS.EAS.Domain;
using SFA.DAS.EAS.Domain.Data;
using SFA.DAS.EAS.Domain.Interfaces;
using SFA.DAS.EAS.Domain.Models.Levy;

namespace SFA.DAS.EAS.Application.Queries.GetEmployerAccountTransactions
{
    public class GetEmployerAccountTransactionsHandler : IAsyncRequestHandler<GetEmployerAccountTransactionsQuery, GetEmployerAccountTransactionsResponse>
    {
        private readonly IDasLevyService _dasLevyService;
        private readonly IValidator<GetEmployerAccountTransactionsQuery> _validator;
        private readonly IEmployerAccountRepository _employerAccountRepository;

        public GetEmployerAccountTransactionsHandler(IDasLevyService dasLevyService, IValidator<GetEmployerAccountTransactionsQuery> validator, IEmployerAccountRepository employerAccountRepository)
        {
            _dasLevyService = dasLevyService;
            _validator = validator;
            _employerAccountRepository = employerAccountRepository;
        }

        public async Task<GetEmployerAccountTransactionsResponse> Handle(GetEmployerAccountTransactionsQuery message)
        {
            var result = await _validator.ValidateAsync(message);

            if (!result.IsValid())
            {
                throw new InvalidRequestException(result.ValidationDictionary);
            }

            var response = await _dasLevyService.GetTransactionsByAccountId(message.AccountId);

            var orderedTransactions = response.OrderBy(x => x.TransactionDate).ToList();

            decimal runningTotal = 0;
            orderedTransactions.ForEach(x =>
            {
                runningTotal += x.Amount;
                x.Balance = runningTotal;
            });

            var history = await _employerAccountRepository.GetAccountHistory(message.AccountId);

            var payeSchemeEndDate = DateTime.MinValue;
            history.ForEach(x =>
            {
                var transactions = orderedTransactions.Where(t => t.TransactionDate < x.DateAdded &&
                                               t.TransactionDate > payeSchemeEndDate).ToList();

                if (transactions.Any())
                {
                    var aggregateTransaction = new TransactionLine
                    {
                        AccountId = x.AccountId,
                        EmpRef = x.PayeRef,
                        TransactionDate = x.DateAdded
                    };

                    aggregateTransaction.SubTransactions = transactions;
                    aggregateTransaction.Amount = transactions.Sum(t => t.Amount);
                    aggregateTransaction.Balance = transactions.OrderBy(t => t.TransactionDate).Last().Balance;

                    var lastIndex = orderedTransactions.IndexOf(transactions.Last());

                    orderedTransactions.Insert(lastIndex, aggregateTransaction);

                    transactions.ForEach(t => orderedTransactions.Remove(t));

                    payeSchemeEndDate = x.DateRemoved;
                }
            });

            orderedTransactions.Reverse();

            var returnValue = new AggregationData
            {
                HashedId = message.HashedId,
                AccountId = message.AccountId,
                TransactionLines = orderedTransactions
            };

            return new GetEmployerAccountTransactionsResponse {Data = returnValue };
        }
    }
}
