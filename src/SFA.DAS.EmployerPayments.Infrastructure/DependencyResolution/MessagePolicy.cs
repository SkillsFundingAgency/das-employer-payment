﻿using System;
using System.Configuration;
using System.IO;
using System.Linq;
using Microsoft.Azure;
using SFA.DAS.Configuration;
using SFA.DAS.Configuration.AzureTableStorage;
using SFA.DAS.Configuration.FileStorage;
using SFA.DAS.EmployerPayments.Domain.Attributes;
using SFA.DAS.EmployerPayments.Domain.Interfaces;
using SFA.DAS.Messaging;
using SFA.DAS.Messaging.AzureServiceBus;
using SFA.DAS.Messaging.FileSystem;
using StructureMap;
using StructureMap.Pipeline;

namespace SFA.DAS.EmployerPayments.Infrastructure.DependencyResolution
{
    public class MessagePolicy<T> : ConfiguredInstancePolicy where T : IConfiguration
    {
        private readonly string _serviceName;

        public MessagePolicy(string serviceName)
        {
            _serviceName = serviceName;
        }

        protected override void apply(Type pluginType, IConfiguredInstance instance)
        {
            var messagePublisher = instance?.Constructor?
                .GetParameters().FirstOrDefault(x => x.ParameterType == typeof(IMessagePublisher) || x.ParameterType == typeof(IPollingMessageReceiver));

            var environment = Environment.GetEnvironmentVariable("DASENV");
            if (string.IsNullOrEmpty(environment))
            {
                environment = CloudConfigurationManager.GetSetting("EnvironmentName");
            }

            if (messagePublisher != null)
            {
                var configurationService = new ConfigurationService(GetConfigurationRepository(), new ConfigurationOptions(_serviceName, environment, "1.0"));

                var config = configurationService.Get<T>();
                if (string.IsNullOrEmpty(config.ServiceBusConnectionString))
                {
                    var queueFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    instance.Dependencies.AddForConstructorParameter(messagePublisher, new FileSystemMessageService(Path.Combine(queueFolder)));
                }
                else
                {

                    var sbconnectionKey = instance.Constructor.CustomAttributes.FirstOrDefault(x => x.AttributeType.Name == nameof(ServiceBusConnectionKeyAttribute))
                                .ConstructorArguments.FirstOrDefault()
                                .Value;

                    var serviceBusConnectionString = config.ServiceBusConnectionString;

                    if (sbconnectionKey != null)
                    {
                        serviceBusConnectionString = string.IsNullOrWhiteSpace(sbconnectionKey.ToString()) ? serviceBusConnectionString : config.ServiceBusConnectionStrings[sbconnectionKey.ToString()];
                    }
                        
                    instance.Dependencies.AddForConstructorParameter(messagePublisher, new AzureServiceBusMessageService(serviceBusConnectionString));
                }   
                
            }
        }

        private static IConfigurationRepository GetConfigurationRepository()
        {
            IConfigurationRepository configurationRepository;
            if (bool.Parse(ConfigurationManager.AppSettings["LocalConfig"]))
            {
                configurationRepository = new FileStorageConfigurationRepository();
            }
            else
            {
                configurationRepository = new AzureTableStorageConfigurationRepository(CloudConfigurationManager.GetSetting("ConfigurationStorageConnectionString"));
            }
            return configurationRepository;
        }
        
    }
}