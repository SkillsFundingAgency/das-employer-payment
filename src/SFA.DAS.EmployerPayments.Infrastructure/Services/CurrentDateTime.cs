using System;
using SFA.DAS.EmployerPayments.Domain.Interfaces;

namespace SFA.DAS.EmployerPayments.Infrastructure.Services
{
    public sealed class CurrentDateTime : ICurrentDateTime
    {
        public DateTime Now { get; }

        public CurrentDateTime()
        {
            Now = DateTime.UtcNow;
        }

        public CurrentDateTime(DateTime time)
        {
            Now = time;
        }
    }
}
