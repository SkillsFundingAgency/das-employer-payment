﻿using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.EmployerApprenticeshipsService.Domain;
using SFA.DAS.EmployerApprenticeshipsService.Domain.Data;

namespace SFA.DAS.EmployerApprenticeshipsService.Application.Queries.GetUsers
{
    public class GetUsersQueryHandler : IAsyncRequestHandler<GetUsersQuery, Users>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Users> Handle(GetUsersQuery message)
        {
            var users = await _userRepository.GetAllUsers();

            return users;
        }
    }
}
