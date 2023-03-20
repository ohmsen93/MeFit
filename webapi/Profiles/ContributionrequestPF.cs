using AutoMapper;
using webapi.Models;
using webapi.Models.DTO.ContributionrequestDTO;

namespace webapi.Profiles
{
    public class ContributionrequestPF : Profile
    {
        public ContributionrequestPF()
        {
            CreateMap<ContributionrequestCreateDto, Contributionrequest>();
            CreateMap<Contributionrequest, ContributionrequestReadDto>()
                .ForMember(dto => dto.FkUserProfile, options =>
                    options.MapFrom(setDomain => setDomain.FkUserProfile))
                .ForMember(dto => dto.FkUserProfileId, options =>
                    options.MapFrom(setDomain => setDomain.FkUserProfile.Id));
            CreateMap<ContributionrequestUpdateDto, Contributionrequest>();
        }
    }
}