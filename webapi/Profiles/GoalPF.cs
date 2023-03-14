using webapi.Models.DTO.SetDTO;
using webapi.Models;
using AutoMapper;
using webapi.Models.DTO.GoalDTO;

namespace webapi.Profiles
{
    public class GoalPF:Profile
    {
        public GoalPF()
        {
            CreateMap<GoalCreateDto, Goal>();
            CreateMap<Goal, GoalReadDto>()
                .ForMember(dto => dto.Workouts, options =>
                options.MapFrom(goalDomain => goalDomain.GoalWorkouts.Select(gw => $"api/v1/workouts/{gw.Id}").ToList()));
            CreateMap<GoalUpdateDto, Goal>();
        }
    }
}
