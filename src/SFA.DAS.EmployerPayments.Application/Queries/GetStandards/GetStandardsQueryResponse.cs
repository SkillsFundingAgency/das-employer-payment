using System.Collections.Generic;
using SFA.DAS.EmployerPayments.Domain.Models.ApprenticeshipCourse;

namespace SFA.DAS.EmployerPayments.Application.Queries.GetStandards
{
    public class GetStandardsQueryResponse
    {
        public List<Standard> Standards { get; set; }
    }
}