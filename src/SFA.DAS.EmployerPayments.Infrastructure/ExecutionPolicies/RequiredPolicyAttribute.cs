﻿using System;

namespace SFA.DAS.EmployerPayments.Infrastructure.ExecutionPolicies
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public class RequiredPolicyAttribute : Attribute
    {
        public RequiredPolicyAttribute(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }
}
