﻿using System;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerPayments.Domain.Interfaces;
using SFA.DAS.EmployerPayments.Domain.Models.ApprenticeshipCourse;

namespace SFA.DAS.EmployerPayments.Application.Queries.GetTrainingProgrammes
{
    public sealed class GetTrainingProgrammesQueryHandler : IAsyncRequestHandler<GetTrainingProgrammesQueryRequest, GetTrainingProgrammesQueryResponse>
    {
        private readonly IApprenticeshipInfoServiceWrapper _apprenticeshipInfoServiceWrapper;

        public GetTrainingProgrammesQueryHandler(IApprenticeshipInfoServiceWrapper apprenticeshipInfoServiceWrapper)
        {
            if (apprenticeshipInfoServiceWrapper == null)
                throw new ArgumentNullException(nameof(apprenticeshipInfoServiceWrapper));

            _apprenticeshipInfoServiceWrapper = apprenticeshipInfoServiceWrapper;
        }

        public async Task<GetTrainingProgrammesQueryResponse> Handle(GetTrainingProgrammesQueryRequest message)
        {
            var standardsTask = _apprenticeshipInfoServiceWrapper.GetStandardsAsync();
            var frameworksTask = _apprenticeshipInfoServiceWrapper.GetFrameworksAsync();

            await Task.WhenAll(standardsTask, frameworksTask);

            var programmes = standardsTask.Result.Standards.Union(frameworksTask.Result.Frameworks.Cast<ITrainingProgramme>())
                .OrderBy(m => m.Title)
                .ToList();

            return new GetTrainingProgrammesQueryResponse
            {
                TrainingProgrammes = programmes
            };
        }
    }
}