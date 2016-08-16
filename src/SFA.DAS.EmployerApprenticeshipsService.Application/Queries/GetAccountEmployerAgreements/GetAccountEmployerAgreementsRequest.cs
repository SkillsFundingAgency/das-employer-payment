﻿using MediatR;

namespace SFA.DAS.EmployerApprenticeshipsService.Application.Queries.GetAccountEmployerAgreements
{
    public class GetAccountEmployerAgreementsRequest : IAsyncRequest<GetAccountEmployerAgreementsResponse>
    {
        public long AccountId { get; set; }
        public string ExternalUserId { get; set; }
    }
}