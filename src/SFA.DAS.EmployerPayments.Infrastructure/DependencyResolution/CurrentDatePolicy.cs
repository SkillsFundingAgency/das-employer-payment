﻿using System;
using System.Linq;
using Microsoft.Azure;
using SFA.DAS.EmployerPayments.Domain.Interfaces;
using SFA.DAS.EmployerPayments.Infrastructure.Services;
using StructureMap;
using StructureMap.Pipeline;

namespace SFA.DAS.EmployerPayments.Infrastructure.DependencyResolution
{
    public class CurrentDatePolicy : ConfiguredInstancePolicy
    {
        protected override void apply(Type pluginType, IConfiguredInstance instance)
        {
            var currentDateTime = instance?.Constructor?.GetParameters().FirstOrDefault(x => x.ParameterType == typeof(ICurrentDateTime));

            if (currentDateTime != null)
            {
                var cloudCurrentTime = CloudConfigurationManager.GetSetting("CurrentTime");

                DateTime currentTime;

                if (!DateTime.TryParse(cloudCurrentTime, out currentTime))
                {
                    currentTime = DateTime.Now;
                }

                instance.Dependencies.AddForConstructorParameter(currentDateTime, new CurrentDateTime(currentTime));
            }
        }
    }
}