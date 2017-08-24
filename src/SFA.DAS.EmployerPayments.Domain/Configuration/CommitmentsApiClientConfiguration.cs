using System.Collections.Generic;
using SFA.DAS.Commitments.Api.Client.Configuration;
using SFA.DAS.EmployerPayments.Domain.Interfaces;

namespace SFA.DAS.EmployerPayments.Domain.Configuration
{
    public class CommitmentsApiClientConfiguration : ICommitmentsApiClientConfiguration, IConfiguration
    {
        public string BaseUrl { get; set; }
        public string ClientToken { get; set; }
        public string DatabaseConnectionString { get; set; }
        public string ServiceBusConnectionString { get; set; }
        public Dictionary<string, string> ServiceBusConnectionStrings { get; set; }
    }
}