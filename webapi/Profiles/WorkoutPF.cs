using AutoMapper;
using System.Linq;
using webapi.Models;
using webapi.Models.DTO.WorkoutDTO;
using webapi.Models.DTO.ExerciseDTO;

namespace webapi.Profiles
{

    public class WorkoutPF : Profile
    {
        public WorkoutPF()
        {
            CreateMap<WorkoutCreateDto, Workout>();
            CreateMap<Workout, WorkoutReadDto>()
                .ForMember(dto => dto.Exercises, options =>
                    options.MapFrom(workoutDomain => workoutDomain.Exercises.Select(set => new ExerciseReadDto { Id = set.Id, Name = set.Name }).ToList()))
                .ForMember(dto => dto.FkUserProfileId, options => options.MapFrom(workout => workout.FkUserProfileId.HasValue ? workout.FkUserProfileId.Value : 0));
            CreateMap<WorkoutUpdateDto, Workout>();
            CreateMap<Workout, WorkoutUpdateDto>().ReverseMap();
        }
    }
}
