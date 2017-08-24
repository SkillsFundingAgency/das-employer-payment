using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerPayments.Application.Queries.GetAccounts;
using SFA.DAS.EmployerPayments.Domain.Data.Repositories;

namespace SFA.DAS.EmployerPayments.Application.UnitTests.Queries
{
    public class GetAccountsTests
    {
        private GetAccountsQueryHandler _handler;
        private Mock<IPaymentsRepository> _repository;

        [SetUp]
        public void Arrange()
        {
            _repository = new Mock<IPaymentsRepository>();
            _repository.Setup(x => x.GetAccounts()).ReturnsAsync(null);

            _handler = new GetAccountsQueryHandler(_repository.Object);
        }

        [Test]
        public async Task ThenTheRepositoyIsCalled()
        {
            //Act
            await _handler.Handle(new GetAccountsQuery());

            //Assert
            _repository.Verify(x=>x.GetAccounts(),Times.Once);
        }

        [Test]
        public async Task ThenTheAccountIdsAreReturnedInTheResponse()
        {
            //Arrange
            _repository.Setup(x => x.GetAccounts()).ReturnsAsync(new List<long> {12345});

            //Act
            var actual = await _handler.Handle(new GetAccountsQuery());

            //Assert
            Assert.IsNotNull(actual);
            Assert.AreEqual(1,actual.AccountIds.Count());
        }
    }
}
