using System.Collections.Generic;
using SFA.DAS.EmployerPayments.Domain.Models.ApprenticeshipCourse;

namespace SFA.DAS.EmployerPayments.Application.Queries.GetTrainingProgrammes
{
    public sealed class GetTrainingProgrammesQueryResponse
    {
        public List<ITrainingProgramme> TrainingProgrammes { get; set; }
    }
}