﻿using System.Collections.Generic;

namespace SFA.DAS.EmployerPayments.Domain.Models.Transaction
{
    public class AggregationLine
    {
        public TransactionItemType TransactionItemType { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public List<AggregationLineItem> Items { get; set; }
        public string Id { get; set; }
    }
}