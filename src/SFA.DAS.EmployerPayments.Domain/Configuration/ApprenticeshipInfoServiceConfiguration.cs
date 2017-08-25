using System.Collections.Generic;
using SFA.DAS.EmployerPayments.Domain.Interfaces;

namespace SFA.DAS.EmployerPayments.Domain.Configuration
{
    public class ApprenticeshipInfoServiceConfiguration : IApprenticeshipInfoServiceConfiguration, IConfiguration
    {
        public string BaseUrl { get; set; }
        public string DatabaseConnectionString { get; set; }
        public string ServiceBusConnectionString { get; set; }
        public Dictionary<string, string> ServiceBusConnectionStrings { get; set; }
    }
}
