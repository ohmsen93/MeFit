using AutoMapper;
using webapi.Models;
using webapi.Models.DTO.ExerciseDTO;
using webapi.Models.Entities;

namespace webapi.Profiles
{
    public class ExercisePF:Profile
    {
        public ExercisePF()
        {
            CreateMap<ExerciseCreateDto, Exercise>();
            CreateMap<Exercise, ExerciseReadDto>()
                .ForMember(dto => dto.Sets, options =>
                    options.MapFrom(exerciseDomain => exerciseDomain.Sets.Select(set => new SetRc( set.Id,set.Reps,set.Total)).ToList()))
                .ForMember(dto => dto.Musclegroups, options =>
                    options.MapFrom(exerciseDomain => exerciseDomain.Musclegroups.Select(mg => new MuscleGroupRc( mg.Id,mg.Musclegroup1)).ToList()));
            CreateMap<ExerciseUpdateDto, Exercise>();
        }

    }
}
