using AutoMapper;
using webapi.Models.DTO.GoalWorkoutDTO;
using webapi.Models;

namespace webapi.Profiles
{
    public class GoalWorkoutPF:Profile
    {
        public GoalWorkoutPF()
        {
            CreateMap<GoalWorkoutCreateDto, GoalWorkout>();
            CreateMap<GoalWorkout, GoalWorkoutReadDto>();
            CreateMap<GoalWorkoutUpdateDto, GoalWorkout>();
        }
    }
}
