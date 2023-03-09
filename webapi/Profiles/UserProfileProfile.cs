using AutoMapper;
using webapi.Models.DTO.Set;
using webapi.Models.DTO.UserProfile;
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
                options.MapFrom(userProfileDomain => userProfileDomain.Goals.Select(goal => $"api/v1/goals/{goal.Id}").ToList()))
                .ForMember(dto => dto.Workouts, options =>
                options.MapFrom(userProfileDomain => userProfileDomain.Workouts.Select(workout => $"api/v1/workouts/{workout.Id}").ToList()));
            CreateMap<UserProfileUpdateDto, UserProfile>();
        }
    }
}
