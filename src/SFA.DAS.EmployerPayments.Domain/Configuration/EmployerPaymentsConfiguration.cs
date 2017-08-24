using System.Collections.Generic;
using SFA.DAS.EmployerPayments.Domain.Interfaces;

namespace SFA.DAS.EmployerPayments.Domain.Configuration
{
    public class EmployerPaymentsConfiguration : IConfiguration
    {
        public string DatabaseConnectionString { get; set; }
        public string ServiceBusConnectionString { get; set; }
        public Dictionary<string, string> ServiceBusConnectionStrings { get; set; }
        public string Hashstring { get; set; }
    }
}
