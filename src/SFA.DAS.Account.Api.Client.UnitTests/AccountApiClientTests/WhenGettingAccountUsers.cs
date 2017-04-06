﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions.Common;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using SFA.DAS.EAS.Account.Api.Types;

namespace SFA.DAS.EAS.Account.Api.Client.UnitTests.AccountApiClientTests
{
    public class WhenGettingAccountUsers
    {
        private AccountApiConfiguration _configuration;
        private Mock<SecureHttpClient> _httpClient;
        private AccountApiClient _apiClient;
        private TeamMemberViewModel _teamMember;
        private string _uri;
        private string _accountId;

        [SetUp]
        public void Arrange()
        {
            _accountId = "ABC123";

            _configuration = new AccountApiConfiguration
            {
                ApiBaseUrl = "http://some-url/"
            };

            _uri = $"/api/accounts/{_accountId}/users";
            var absoluteUri = _configuration.ApiBaseUrl.TrimEnd('/') + _uri;

            _teamMember = new TeamMemberViewModel
            {
                Name = "Name",
                UserRef = "2163",
                Email = "test@test.com",
                Role = "Viewer"
            };

            var members = new List<TeamMemberViewModel> {_teamMember};

            _httpClient = new Mock<SecureHttpClient>();
            _httpClient.Setup(c => c.GetAsync(absoluteUri))
                       .Returns(Task.FromResult(JsonConvert.SerializeObject(members)));

            _apiClient = new AccountApiClient(_configuration, _httpClient.Object);
        }

        [Test]
        public async Task ThenThePayeSchemeIsReturned()
        {
            // Act
            var response = await _apiClient.GetAccountUsers(_accountId);
            var viewModel = response?.FirstOrDefault();

            // Assert
            Assert.IsNotNull(response);
            Assert.IsNotNull(viewModel);
            viewModel.IsSameOrEqualTo(_teamMember);
        }
    }
}
