namespace SFA.DAS.EmployerPayments.Domain.Models.Transaction
{
    public class TransactionSummary
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal Amount { get; set; }
    }
}