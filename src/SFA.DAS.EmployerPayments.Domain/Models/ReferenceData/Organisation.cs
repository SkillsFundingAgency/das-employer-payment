﻿using System;
using SFA.DAS.EmployerPayments.Domain.Models.Organisation;

namespace SFA.DAS.EmployerPayments.Domain.Models.ReferenceData
{
    public class Organisation
    {
        public string Name { get; set; }
        public OrganisationType Type { get; set; }
        public OrganisationSubType SubType { get; set; }
        public string Code { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public Address Address { get; set; }
        public string Sector { get; set; }
    }
}