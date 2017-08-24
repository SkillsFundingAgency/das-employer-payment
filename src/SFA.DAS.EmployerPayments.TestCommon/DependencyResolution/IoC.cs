using Moq;
using SFA.DAS.Commitments.Api.Client.Interfaces;
using SFA.DAS.EAS.TestCommon.MockPolicy;
using SFA.DAS.EmployerPayments.Domain.Configuration;
using SFA.DAS.EmployerPayments.Domain.Interfaces;
using SFA.DAS.EmployerPayments.Domain.Models.Account;
using SFA.DAS.EmployerPayments.Infrastructure.DependencyResolution;
using SFA.DAS.Events.Api.Client;
using SFA.DAS.Messaging;
using StructureMap;

namespace SFA.DAS.EAS.TestCommon.DependencyResolution
{
    public static class IoC
    {
        public static Container CreateContainer(Mock<IMessagePublisher> messagePublisher,  Mock<ICookieStorageService<EmployerAccountData>> cookieService, Mock<IEventsApi> eventsApi, Mock<IEmployerCommitmentApi> commitmentApi)
        {
            return new Container(c =>
            {
                c.Policies.Add(new ConfigurationPolicy<EmployerPaymentsConfiguration>("SFA.DAS.EmployerPayments"));
                c.Policies.Add(new ConfigurationPolicy<AuditApiClientConfiguration>("SFA.DAS.AuditApiClient"));
                c.Policies.Add<CurrentDatePolicy>();
                c.Policies.Add(new MockMessagePolicy(messagePublisher));
                c.AddRegistry(new DefaultRegistry(cookieService, eventsApi, commitmentApi));
            });
        }
        
    }
}
