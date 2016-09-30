﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerApprenticeshipsService.Application.Queries.GetEmployerAccount;
using SFA.DAS.EmployerApprenticeshipsService.Application.Validation;
using SFA.DAS.EmployerApprenticeshipsService.Domain.Data;
using SFA.DAS.EmployerApprenticeshipsService.Domain.Entities.Account;

namespace SFA.DAS.EmployerApprenticeshipsService.Application.UnitTests.Queries.GetEmployerAccountTests
{
    public class WhenIGetEmployerAccountsByAccountId : QueryBaseTest<GetEmployerAccountHandler, GetEmployerAccountQuery, GetEmployerAccountResponse>
    {
        private Mock<IEmployerAccountRepository> _employerAccountRepository;
        public override GetEmployerAccountQuery Query { get; set; }
        public override GetEmployerAccountHandler RequestHandler { get; set; }
        public override Mock<IValidator<GetEmployerAccountQuery>> RequestValidator { get; set; }

        private const long ExpectedAccountId = 4564878;
        private const string ExpectedUserId = "123dfv";

        [SetUp]
        public void Arrange()
        {
            SetUp();

            _employerAccountRepository = new Mock<IEmployerAccountRepository>();

            _employerAccountRepository.Setup(x => x.GetAccountById(ExpectedAccountId)).ReturnsAsync(new Account {HashedId = "123"});

            RequestHandler = new GetEmployerAccountHandler(_employerAccountRepository.Object, RequestValidator.Object);
            Query = new GetEmployerAccountQuery();
        }

        [Test]
        public override async Task ThenIfTheMessageIsValidTheRepositoryIsCalled()
        {
            //Act
            await RequestHandler.Handle(new GetEmployerAccountQuery{
                    AccountId = ExpectedAccountId,
                    ExternalUserId = ExpectedUserId
                });

            //Assert
            _employerAccountRepository.Verify(x=>x.GetAccountById(ExpectedAccountId));
        }

        [Test]
        public override async Task ThenIfTheMessageIsValidTheValueIsReturnedInTheResponse()
        {
            //Act
            var result = await RequestHandler.Handle(new GetEmployerAccountQuery
            {
                AccountId = ExpectedAccountId,
                ExternalUserId = ExpectedUserId
            });

            //Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Account);
        }

        [Test]
        public void ThenIfValidationResponseIsUnauthorizedAnUnauthorizedAccessExceptionIsThrown()
        {
            //Arrange
            RequestValidator.Setup(x => x.ValidateAsync(It.IsAny<GetEmployerAccountQuery>())).ReturnsAsync(new ValidationResult {IsUnauthorized = true});

            //Act Assert
            Assert.ThrowsAsync<UnauthorizedAccessException>(async ()=> await RequestHandler.Handle(new GetEmployerAccountQuery()));

        }

    }
}