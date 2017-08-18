using System;
using System.Threading.Tasks;
using SFA.DAS.EmployerPayments.Application.Validation;

namespace SFA.DAS.EmployerPayments.Application.Commands.CreateAccountReference
{
    public class CreateAccountReferenceCommandValidator : IValidator<CreateAccountReferenceCommand>
    {
        public ValidationResult Validate(CreateAccountReferenceCommand item)
        {
            var validationResult = new ValidationResult();

            if (item.AccountId == 0)
            {
                validationResult.AddError(nameof(item.AccountId));
            }

            return validationResult;
        }

        public Task<ValidationResult> ValidateAsync(CreateAccountReferenceCommand item)
        {
            throw new NotImplementedException();
        }
    }
}
