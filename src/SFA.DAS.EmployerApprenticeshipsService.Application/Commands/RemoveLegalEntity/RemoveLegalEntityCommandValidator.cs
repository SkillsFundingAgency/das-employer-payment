﻿using System;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.EAS.Application.Validation;
using SFA.DAS.EAS.Domain.Data.Repositories;
using SFA.DAS.EAS.Domain.Interfaces;
using SFA.DAS.EAS.Domain.Models.EmployerAgreement;
using SFA.DAS.EAS.Domain.Models.UserProfile;

namespace SFA.DAS.EAS.Application.Commands.RemoveLegalEntity
{
    public class RemoveLegalEntityCommandValidator : IValidator<RemoveLegalEntityCommand>
    {
        private readonly IMembershipRepository _membershipRepository;
        private readonly IEmployerAgreementRepository _employerAgreementRepository;
        private readonly IHashingService _hashingService;

        public RemoveLegalEntityCommandValidator(IMembershipRepository membershipRepository, IEmployerAgreementRepository employerAgreementRepository, IHashingService hashingService)
        {
            _membershipRepository = membershipRepository;
            _employerAgreementRepository = employerAgreementRepository;
            _hashingService = hashingService;
        }

        public ValidationResult Validate(RemoveLegalEntityCommand item)
        {
            throw new NotImplementedException();
        }

        public async Task<ValidationResult> ValidateAsync(RemoveLegalEntityCommand item)
        {
            var validationResult = new ValidationResult();

            if (string.IsNullOrEmpty(item.HashedAccountId))
            {
                validationResult.AddError(nameof(item.HashedAccountId));
            }
            if (string.IsNullOrEmpty(item.HashedLegalEntityId))
            {
                validationResult.AddError(nameof(item.HashedLegalEntityId));
            }
            if (string.IsNullOrEmpty(item.UserId))
            {
                validationResult.AddError(nameof(item.UserId));
            }
            if (item.LegalAgreementId == 0)
            {
                validationResult.AddError(nameof(item.LegalAgreementId));
            }

            if (!validationResult.IsValid())
            {
                return validationResult;
            }

            var member = await _membershipRepository.GetCaller(item.HashedAccountId, item.UserId);

            if (member == null || !member.RoleId.Equals((short)Role.Owner))
            {
                validationResult.IsUnauthorized = true;
                return validationResult;
            }

            var accountId = _hashingService.DecodeValue(item.HashedAccountId);
            var legalEntites = await _employerAgreementRepository.GetLegalEntitiesLinkedToAccount(accountId, false);

            if (legalEntites != null && legalEntites.Count == 1)
            {
                validationResult.AddError(nameof(item.HashedLegalEntityId), "There must be at least one legal entity on the account");
                return validationResult;
            }

            var agreement = await _employerAgreementRepository.GetEmployerAgreement(item.LegalAgreementId);

            if (agreement.Status == EmployerAgreementStatus.Signed)
            {
                validationResult.AddError(nameof(item.HashedLegalEntityId), "Agreement has already been signed");
                return validationResult;
            }

            
            var legalEntityId = _hashingService.DecodeValue(item.HashedLegalEntityId);

            if (agreement.AccountId != accountId || agreement.LegalEntityId != legalEntityId)
            {
                validationResult.IsUnauthorized = true;
                return validationResult;
            }

            return validationResult;
        }
    }
}
