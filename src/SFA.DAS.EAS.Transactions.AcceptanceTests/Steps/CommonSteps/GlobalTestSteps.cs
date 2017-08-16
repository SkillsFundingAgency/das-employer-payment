using Moq;
using SFA.DAS.Commitments.Api.Client.Interfaces;
using SFA.DAS.EAS.TestCommon.DbCleanup;
using SFA.DAS.EAS.TestCommon.DependencyResolution;
using SFA.DAS.EmployerPayments.Domain.Interfaces;
using SFA.DAS.EmployerPayments.Domain.Models.Account;
using SFA.DAS.Events.Api.Client;
using SFA.DAS.Messaging;
using StructureMap;
using TechTalk.SpecFlow;

namespace SFA.DAS.EAS.Transactions.AcceptanceTests.Steps.CommonSteps
{
    [Binding]
    public static class GlobalTestSteps
    {
        private static Mock<IMessagePublisher> _messagePublisher;
        private static Container _container;
        private static Mock<ICookieStorageService<EmployerAccountData>> _cookieService;
        private static Mock<IEventsApi> _eventsApi;
        private static Mock<IEmployerCommitmentApi> _employerCommitmentsApi;

        [AfterScenario()]
        public static void Arrange()
        {
            _messagePublisher = new Mock<IMessagePublisher>();
            _cookieService = new Mock<ICookieStorageService<EmployerAccountData>>();
            _eventsApi = new Mock<IEventsApi>();
            _employerCommitmentsApi = new Mock<IEmployerCommitmentApi>();
            
            _container = IoC.CreateContainer(_messagePublisher, _cookieService, _eventsApi, _employerCommitmentsApi);

            var cleanDownDb = _container.GetInstance<ICleanDatabase>();
            cleanDownDb.Execute().Wait();

            var cleanDownTransactionDb = _container.GetInstance<ICleanTransactionsDatabase>();
            cleanDownTransactionDb.Execute().Wait();
        }
    }
}
