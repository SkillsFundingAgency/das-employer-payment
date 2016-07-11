﻿using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using MediatR;
using SFA.DAS.EmployerApprenticeshipsService.Application.Queries.GetUserAccounts;
using SFA.DAS.EmployerApprenticeshipsService.Application.Queries.GetUsers;
using SFA.DAS.EmployerApprenticeshipsService.Web.Models;

namespace SFA.DAS.EmployerApprenticeshipsService.Web.Orchestrators
{
    public class HomeOrchestrator : IOrchestrator
    {
        private readonly IMediator _mediator;

        //Required for running tests
        public HomeOrchestrator()
        {
            
        }

        public HomeOrchestrator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public virtual async Task<SignInUserViewModel> GetUsers()
        {
            var actual = await _mediator.SendAsync(new GetUsersQuery());

            return new SignInUserViewModel
            {
                AvailableUsers = actual.UserList.Select(x =>
                                                new SignInUserModel
                                                {
                                                    Email = x.Email,
                                                    FirstName = x.FirstName,
                                                    LastName = x.LastName,
                                                    UserId = x.UserId
                                                }).ToList()
            };
        }

        public virtual async Task<UserAccountsViewModel> GetUserAccounts()
        {
            var userId =
                ((ClaimsIdentity) HttpContext.Current.User.Identity).Claims.FirstOrDefault(claim => claim.Type == @"sub").Value;
            var actual = await _mediator.SendAsync(new GetUserAccountsQuery() {UserId = userId });

            return new UserAccountsViewModel {Accounts = actual};
        }
    }
}