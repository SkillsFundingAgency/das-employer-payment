﻿using System;
using System.Linq;
using System.Reflection;
using SFA.DAS.EmployerPayments.Infrastructure.ExecutionPolicies;
using StructureMap;
using StructureMap.Pipeline;

namespace SFA.DAS.EmployerPayments.Infrastructure.DependencyResolution
{
    public class ExecutionPolicyPolicy : ConfiguredInstancePolicy
    {
        protected override void apply(Type pluginType, IConfiguredInstance instance)
        {
            var policies = instance?.Constructor?.GetParameters().Where(x => x.ParameterType == typeof(ExecutionPolicy)) ?? new ParameterInfo[0];
            foreach (var policyDependency in policies)
            {
                var policyName = policyDependency.GetCustomAttribute<RequiredPolicyAttribute>()?.Name;
                instance.Dependencies.AddForConstructorParameter(policyDependency, new ReferencedInstance(policyName));
            }
        }
    }
}
