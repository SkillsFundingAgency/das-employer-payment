﻿using System;
using System.Linq;
using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Azure;
using Moq;
using SFA.DAS.Audit.Client;
using SFA.DAS.Commitments.Api.Client.Interfaces;
using SFA.DAS.EmployerPayments.Application.Validation;
using SFA.DAS.EmployerPayments.Domain.Configuration;
using SFA.DAS.EmployerPayments.Domain.Interfaces;
using SFA.DAS.EmployerPayments.Domain.Models.Account;
using SFA.DAS.EmployerPayments.Infrastructure.Caching;
using SFA.DAS.Events.Api.Client;
using SFA.DAS.NLog.Logger;
using StructureMap;
using StructureMap.Graph;
using WebGrease.Css.Extensions;
using IConfiguration = SFA.DAS.EmployerPayments.Domain.Interfaces.IConfiguration;

namespace SFA.DAS.EAS.TestCommon.DependencyResolution
{
    public class DefaultRegistry : Registry
    {

        public DefaultRegistry( Mock<ICookieStorageService<EmployerAccountData>> cookieServiceMock, Mock<IEventsApi> eventApi, Mock<IEmployerCommitmentApi> commitmentsApi)
        {
            Scan(scan =>
            {
                scan.AssembliesFromApplicationBaseDirectory(a => a.GetName().Name.StartsWith("SFA.DAS"));
                scan.RegisterConcreteTypesAgainstTheFirstInterface();
                scan.ConnectImplementationsToTypesClosing(typeof(IValidator<>)).OnAddedPluginTypes(t => t.Singleton());
            });
            
            For<ICookieStorageService<EmployerAccountData>>().Use(() => cookieServiceMock.Object);
            
            For<IConfiguration>().Use<EmployerPaymentsConfiguration>();

            For<ICache>().Use<InMemoryCache>();
            For<IEventsApi>().Use(() => eventApi.Object);

            For<ILog>().Use(Mock.Of<ILog>());

            For<IEmployerCommitmentApi>().Use(commitmentsApi.Object);

            AddMediatrRegistrations();

            RegisterMapper();
			RegisterAuditService();
        }

        private void AddMediatrRegistrations()
        {
            For<SingleInstanceFactory>().Use<SingleInstanceFactory>(ctx => t => ctx.GetInstance(t));
            For<MultiInstanceFactory>().Use<MultiInstanceFactory>(ctx => t => ctx.GetAllInstances(t));

            For<IMediator>().Use<Mediator>();
        }

        private void RegisterMapper()
        {
            var profiles = Assembly.Load("SFA.DAS.EAS.Infrastructure").GetTypes()
                            .Where(t => typeof(Profile).IsAssignableFrom(t))
                            .Select(t => (Profile)Activator.CreateInstance(t));

            var config = new MapperConfiguration(cfg =>
            {
                profiles.ForEach(cfg.AddProfile);
            });

            var mapper = config.CreateMapper();

            For<IConfigurationProvider>().Use(config).Singleton();
            For<IMapper>().Use(mapper).Singleton();
        }
		
		private void RegisterAuditService()
        {
            var environment = Environment.GetEnvironmentVariable("DASENV");
            if (string.IsNullOrEmpty(environment))
            {
                environment = CloudConfigurationManager.GetSetting("EnvironmentName");
            }

            For<IAuditMessageFactory>().Use<AuditMessageFactory>().Singleton();

            if (environment.Equals("LOCAL"))
            {
                For<IAuditApiClient>().Use<StubAuditApiClient>();
            }
            else
            {
                For<IAuditApiClient>().Use<AuditApiClient>();
            }

        }

    }
}
