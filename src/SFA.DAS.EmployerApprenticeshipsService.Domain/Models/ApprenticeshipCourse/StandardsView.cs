using System;
using System.Collections.Generic;

namespace SFA.DAS.EmployerPayments.Domain.Models.ApprenticeshipCourse
{
    public class StandardsView
    {
        public DateTime CreationDate { get; set; }
        public List<Standard> Standards { get; set; }
    }
}