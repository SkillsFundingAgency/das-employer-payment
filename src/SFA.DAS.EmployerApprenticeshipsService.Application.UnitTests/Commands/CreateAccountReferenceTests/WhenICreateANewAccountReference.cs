using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerPayments.Application.Commands.CreateAccountReference;
using SFA.DAS.EmployerPayments.Application.Validation;
using SFA.DAS.EmployerPayments.Domain.Data.Repositories;

namespace SFA.DAS.EmployerPayments.Application.UnitTests.Queries.CreateAccountReferenceTests
{
    public class WhenICreateANewAccountReference 
    {
        private CreateAccountReferenceCommandHandler _handler;
        private Mock<IValidator<CreateAccountReferenceCommand>> _validator;
        private Mock<IPaymentsRepository> _paymentRepository;

        [SetUp]
        public void Arrange()
        {
            _validator = new Mock<IValidator<CreateAccountReferenceCommand>>();
            _validator.Setup(x => x.Validate(It.IsAny<CreateAccountReferenceCommand>())).Returns(new ValidationResult { ValidationDictionary = new Dictionary<string, string> () });

            _paymentRepository = new Mock<IPaymentsRepository>();

            _handler = new CreateAccountReferenceCommandHandler(_validator.Object, _paymentRepository.Object);
        }

        [Test]
        public async Task ThenTheCommandIsValidated()
        {
            //Act
            await _handler.Handle(new CreateAccountReferenceCommand());

            //Assert
            _validator.Verify(x=>x.Validate(It.IsAny<CreateAccountReferenceCommand>()));
        }

        [Test]
        public void ThenAInvalidRequestExceptionIsThrownIfTheCommandIsNotValid()
        {
            //Arrange
            _validator.Setup(x => x.Validate(It.IsAny<CreateAccountReferenceCommand>())).Returns(new ValidationResult {ValidationDictionary = new Dictionary<string, string> { {"",""} } });

            //Act Assert
            Assert.ThrowsAsync<InvalidRequestException>(async () => await _handler.Handle(new CreateAccountReferenceCommand()));

            //Assert
            _paymentRepository.Verify(x=>x.CreateAccountReference(It.IsAny<long>()), Times.Never);
        }

        [Test]
        public async Task ThenTheRepositoryIsCalledIfTheCommandIsValid()
        {
            //Arrange
            var expectedAccountId = 3443;

            //Act
            await _handler.Handle(new CreateAccountReferenceCommand {AccountId = expectedAccountId});

            //Assert
            _paymentRepository.Verify(x=>x.CreateAccountReference(expectedAccountId),Times.Once);
        }
    }
}
