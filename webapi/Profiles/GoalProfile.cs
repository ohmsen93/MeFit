using webapi.Models.DTO.SetDTO;
using webapi.Models;
using AutoMapper;
using webapi.Models.DTO.GoalDTO;

namespace webapi.Profiles
{
    public class GoalProfile:Profile
    {
        public GoalProfile()
        {
            CreateMap<GoalCreateDto, Goal>();
            CreateMap<Goal, GoalReadDto>()
                .ForMember(dto => dto.Workouts, options =>
                options.MapFrom(setDomain => setDomain.Workouts.Select(workout => $"api/v1/workputs/{workout.Id}").ToList()));
            CreateMap<GoalUpdateDto, Goal>();
        }
    }
}
