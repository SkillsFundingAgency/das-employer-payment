﻿using System.Threading.Tasks;
using SFA.DAS.EmployerPayments.Domain.Models.ApprenticeshipCourse;
using SFA.DAS.EmployerPayments.Domain.Models.ApprenticeshipProvider;

namespace SFA.DAS.EmployerPayments.Domain.Interfaces
{
    public interface IApprenticeshipInfoServiceWrapper
    {
        Task<StandardsView> GetStandardsAsync(bool refreshCache = false);
        Task<FrameworksView> GetFrameworksAsync(bool refreshCache = false);
        ProvidersView GetProvider(long ukPrn);
    }
}