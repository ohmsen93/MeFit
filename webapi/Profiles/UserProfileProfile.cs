using AutoMapper;
using webapi.Models.DTO.SetDTO;
using webapi.Models.DTO.UserProfileDTO;
using webapi.Models;

namespace webapi.Profiles
{
    public class UserProfileProfile:Profile
    {
        public UserProfileProfile()
        {
            CreateMap<UserProfileCreateDto, UserProfile>();
            CreateMap<UserProfile, UserProfileReadDto>()
                .ForMember(dto => dto.Goals, options =>
                options.MapFrom(userProfileDomain => userProfileDomain.Contributionrequests.Select(userProfile => $"api/v1/goals/{userProfile.Id}").ToList()))
                .ForMember(dto => dto.Workouts, options =>
                options.MapFrom(userProfileDomain => userProfileDomain.Contributionrequests.Select(userProfile => $"api/v1/workouts/{userProfile.Id}").ToList()));
            CreateMap<UserProfileUpdateDto, UserProfile>();
        }
    }
}
