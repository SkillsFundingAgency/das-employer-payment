using System.Collections.Generic;
using SFA.DAS.EmployerPayments.Domain.Interfaces;
using SFA.DAS.Provider.Events.Api.Client;

namespace SFA.DAS.EmployerPayments.Domain.Configuration
{
    public class PaymentsApiClientConfiguration : IPaymentsEventsApiConfiguration, IConfiguration
    {
        public string ClientToken { get; set; }
        public string ApiBaseUrl { get; set; }
        public string DatabaseConnectionString { get; set; }
        public string ServiceBusConnectionString { get; set; }
        public Dictionary<string,string> ServiceBusConnectionStrings { get; set; }
        public bool PaymentsDisabled { get; set; }
    }
}