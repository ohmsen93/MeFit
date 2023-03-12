using AutoMapper;
using webapi.Models;
using webapi.Models.DTO.TrainingprogramDTO;

namespace webapi.Profiles
{
    public class TrainingprogramPF:Profile
    {
        public TrainingprogramPF()
        {
            CreateMap<TrainingprogramCreateDto, Trainingprogram>();
            CreateMap<Trainingprogram, TrainingprogramReadDto>()
                .ForMember(dto => dto.Workouts, options =>
                    options.MapFrom(trainingprogramDomain => trainingprogramDomain.Workouts.Select(workout => $"api/v1/workouts/{workout.Id}").ToList()))
                .ForMember(dto => dto.Categories, options =>
                    options.MapFrom(trainingprogramDomain => trainingprogramDomain.Categories.Select(category => $"api/v1/categories/{category.Id}").ToList()));
            CreateMap<TrainingprogramUpdateDto, Trainingprogram>();
        }

    }
}
