using AutoMapper;
using webapi.Models.DTO.SetDTO;
using webapi.Models.DTO.UserProfileDTO;
using webapi.Models;

namespace webapi.Profiles
{
    public class UserProfilePF:Profile
    {
        public UserProfilePF()
        {
            CreateMap<UserProfileCreateDto, Models.UserProfile>();
            CreateMap<Models.UserProfile, UserProfileReadDto>()
                .ForMember(dto => dto.Goals, options =>
                options.MapFrom(userProfileDomain => userProfileDomain.Goals.Select(goal => $"api/v1/goals/{goal.Id}").ToList()))
                .ForMember(dto => dto.Workouts, options =>
                options.MapFrom(userProfileDomain => userProfileDomain.Workouts.Select(workout => $"api/v1/workouts/{workout.Id}").ToList()));
            CreateMap<UserProfileUpdateDto, Models.UserProfile>();
        }
    }
}
