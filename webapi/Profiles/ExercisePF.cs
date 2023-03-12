using AutoMapper;
using webapi.Models;
using webapi.Models.DTO.ExerciseDTO;

namespace webapi.Profiles
{
    public class ExercisePF:Profile
    {
        public ExercisePF()
        {
            CreateMap<ExerciseCreateDto, Exercise>();
            CreateMap<Exercise, ExerciseReadDto>()
                .ForMember(dto => dto.Sets, options =>
                    options.MapFrom(exerciseDomain => exerciseDomain.Sets.Select(set => $"api/v1/sets/{set.Id}").ToList()))
                .ForMember(dto => dto.Musclegroups, options =>
                    options.MapFrom(exerciseDomain => exerciseDomain.Musclegroups.Select(mg => $"api/v1/musclegroups/{mg.Id}").ToList()));
            CreateMap<ExerciseUpdateDto, Exercise>();
        }

    }
}
