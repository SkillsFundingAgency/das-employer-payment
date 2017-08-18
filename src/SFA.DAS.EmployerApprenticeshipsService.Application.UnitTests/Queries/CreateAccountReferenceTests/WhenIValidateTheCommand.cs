using System.Collections.Generic;
using NUnit.Framework;
using SFA.DAS.EmployerPayments.Application.Commands.CreateAccountReference;

namespace SFA.DAS.EmployerPayments.Application.UnitTests.Queries.CreateAccountReferenceTests
{
    public class WhenIValidateTheCommand
    {
        private CreateAccountReferenceCommandValidator _validator;

        [SetUp]
        public void Arrange()
        {
            _validator = new CreateAccountReferenceCommandValidator();
        }

        [Test]
        public void ThenTrueIsReturnedForValidWhenAllFieldsArePopulated()
        {
            //Act
            var actual = _validator.Validate(new CreateAccountReferenceCommand {AccountId = 12345L});

            //Assert
            Assert.IsTrue(actual.IsValid());
        }

        [Test]
        public void ThenFalseIsReturnedForValidAndTheErrorDictionaryPopulatedWhenNoFieldsArePopulated()
        {
            //Act
            var actual = _validator.Validate(new CreateAccountReferenceCommand());

            //Assert
            Assert.IsFalse(actual.IsValid());
            Assert.Contains(new KeyValuePair<string,string>("AccountId","AccountId has not been supplied"), actual.ValidationDictionary);
        }
    }
}
