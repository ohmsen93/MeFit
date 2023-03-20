using AutoMapper;
using webapi.Models.DTO.GoalWorkoutDTO;
using webapi.Models;

namespace webapi.Profiles
{
    public class GoalWorkoutPF:Profile
    {
        public GoalWorkoutPF()
        {
            CreateMap<GoalWorkoutCreateDto, GoalWorkouts>();
            CreateMap<GoalWorkouts, GoalWorkoutReadDto>()
                .ForMember(dto=> dto.WorkoutName, options=>
                options.MapFrom(goalWoroutDomain=>goalWoroutDomain.FkWorkout.Name));
            CreateMap<GoalWorkoutUpdateDto, GoalWorkouts>();
        }
    }
}
