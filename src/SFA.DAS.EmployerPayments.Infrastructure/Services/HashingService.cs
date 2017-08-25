﻿using HashidsNet;
using SFA.DAS.EmployerPayments.Domain.Configuration;
using SFA.DAS.EmployerPayments.Domain.Interfaces;

namespace SFA.DAS.EmployerPayments.Infrastructure.Services
{
    public class HashingService : IHashingService
    {
        
        private readonly Hashids _hashIds;
        private const string Hashstring = "SFA: digital apprenticeship service";
        private const string AllowedCharacters = "46789BCDFGHJKLMNPRSTVWXY";

        public HashingService(EmployerPaymentsConfiguration configuration)
        {
            var hashstring = string.IsNullOrEmpty(configuration.Hashstring) 
                    ? Hashstring 
                    : configuration.Hashstring;

            _hashIds = new Hashids(hashstring, 6, AllowedCharacters);
        }

        public string HashValue(long id)
        {
            return _hashIds.EncodeLong(id);
        }

        public long DecodeValue(string id)
        {
            return _hashIds.DecodeLong(id)[0];
        }
    }
}