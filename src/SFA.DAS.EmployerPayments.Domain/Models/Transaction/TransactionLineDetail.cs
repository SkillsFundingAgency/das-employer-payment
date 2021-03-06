using System;

namespace SFA.DAS.EmployerPayments.Domain.Models.Transaction
{
    public class TransactionLineDetail
    {
        public decimal Amount { get;set; }
        public decimal EnglishFraction { get; set; }
        public decimal TopUp { get; set; }
        public string EmpRef { get; set; }
        public TransactionItemType TransactionType { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal LineAmount { get; set; }
    }
}