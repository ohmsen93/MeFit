using AutoMapper;
using System.Linq;
using webapi.Models;
using webapi.Models.DTO.Workout;
using webapi.Models.DTO.Exercise;

namespace webapi.Profiles
{

    public class WorkoutProfile : Profile
    {
        public WorkoutProfile()
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
