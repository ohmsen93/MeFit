using AutoMapper;
using webapi.Models;
using webapi.Models.DTO.TrainingprogramDTO;
using webapi.Models.Entities;

namespace webapi.Profiles
{
    public class TrainingprogramPF:Profile
    {
        public TrainingprogramPF()
        {
            CreateMap<TrainingprogramCreateDto, Trainingprogram>();
            CreateMap<Trainingprogram, TrainingprogramReadDto>()
                .ForMember(dto => dto.Goals, options =>
                    options.MapFrom(trainingprogramDomain => trainingprogramDomain.Goals.Select(goal => $"api/goals/{goal.Id}").ToList()))
                .ForMember(dto => dto.Workouts, options =>
                    options.MapFrom(trainingprogramDomain => trainingprogramDomain.Workouts.Select(workout => workout.Id).ToList()))
                .ForMember(dto => dto.Categories, options =>
                    options.MapFrom(trainingprogramDomain => trainingprogramDomain.Categories.Select(category => new CategoryRc (category.Id,category.Category1)).ToList()));
            CreateMap<TrainingprogramUpdateDto, Trainingprogram>();
        }

    }
}
