using AutoMapper;
using SFA.DAS.ReferenceData.Api.Client.Dto;

namespace SFA.DAS.EmployerPayments.Infrastructure.Mapping.Profiles
{
    public class ReferenceDataMappings : Profile
    {
        public ReferenceDataMappings()
        {
            CreateMap<Charity, EmployerPayments.Domain.Models.ReferenceData.Charity>();
            CreateMap<PublicSectorOrganisation, EmployerPayments.Domain.Models.ReferenceData.PublicSectorOrganisation>();
            CreateMap<Organisation, EmployerPayments.Domain.Models.ReferenceData.Organisation>();
            CreateMap<Address, EmployerPayments.Domain.Models.Organisation.Address>();
        }
    }
}
