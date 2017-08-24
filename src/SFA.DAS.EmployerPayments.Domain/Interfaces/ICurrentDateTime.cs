using System;

namespace SFA.DAS.EmployerPayments.Domain.Interfaces
{
    public interface ICurrentDateTime
    {
        DateTime Now { get; }
    }
}
