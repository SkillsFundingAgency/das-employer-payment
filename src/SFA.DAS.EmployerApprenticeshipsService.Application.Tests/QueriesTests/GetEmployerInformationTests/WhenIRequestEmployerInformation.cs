﻿using System;
using System.Net;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using SFA.DAS.EmployerApprenticeshipsService.Application.Queries.GetEmployerInformation;
using SFA.DAS.EmployerApprenticeshipsService.Domain;
using SFA.DAS.EmployerApprenticeshipsService.Domain.Interfaces;

namespace SFA.DAS.EmployerApprenticeshipsService.Application.Tests.QueriesTests.GetEmployerInformationTests
{
    [TestFixture]
    public class WhenIRequestEmployerInformation
    {
        private Mock<IEmployerVerificationService> _employerService;
        private GetEmployerInformationQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _employerService = new Mock<IEmployerVerificationService>();

            _handler = new GetEmployerInformationQueryHandler(_employerService.Object);
        }

        [Test]
        public async Task WillReturnNullWhenEmployerNotFound()
        {
            const string id = "QWERTYUIOP";

            _employerService.Setup(x => x.GetInformation(id)).ReturnsAsync(null);

            var response = await _handler.Handle(new GetEmployerInformationRequest
            {
                Id = id
            });

            Assert.That(response, Is.Null);
        }

        [Test]
        public async Task WillReturnExpectedEmployer()
        {
            const string id = "QWERTYUIOP";

            var employer = new EmployerInformation
            {
                CompanyNumber = id,
                CompanyName = "Qwerty Corp",
                DateOfIncorporation = new DateTime(1999, 5, 8),
                RegisteredAddress = new Address
                {
                    Line1 = "1 The Strand",
                    Line2 = "London",
                    PostCode = "N1 2TT"
                }
            };

            _employerService.Setup(x => x.GetInformation(id)).ReturnsAsync(employer);

            var response = await _handler.Handle(new GetEmployerInformationRequest
            {
                Id = id
            });

            Assert.That(response.CompanyNumber, Is.EqualTo(employer.CompanyNumber));
            Assert.That(response.CompanyName, Is.EqualTo(employer.CompanyName));
            Assert.That(response.DateOfIncorporation, Is.EqualTo(employer.DateOfIncorporation));
            Assert.That(response.AddressLine1, Is.EqualTo(employer.RegisteredAddress.Line1));
            Assert.That(response.AddressLine2, Is.EqualTo(employer.RegisteredAddress.Line2));
            Assert.That(response.AddressPostcode, Is.EqualTo(employer.RegisteredAddress.PostCode));
        }
    }
}