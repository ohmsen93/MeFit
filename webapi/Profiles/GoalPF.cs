using webapi.Models.DTO.SetDTO;
using webapi.Models;
using AutoMapper;
using webapi.Models.DTO.GoalDTO;
using webapi.Models.Entities;
using System.Linq;

namespace webapi.Profiles
{
    public class GoalPF:Profile
    {
        public GoalPF()
        {
            CreateMap<GoalCreateDto, Goal>();

            CreateMap<Goal, GoalReadDto>()
                .ForMember(dto => dto.Workouts, options =>
                options.MapFrom(goalDomain => goalDomain.GoalWorkouts.Select(gw => new WorkoutRc (
                    gw.Id, //GoalWorkoutId
                    gw.FkWorkout.Id,//GoalId
                    gw.FkWorkout.Name,
                    gw.FkWorkout.Type,
                    gw.FkWorkout.FkUserProfileId,
                    gw.FkStatus.Id,//GoalWorkoutStatusId
                    gw.FkStatus.Statustype)).ToList()))
                .ForMember(dto => dto.ProgramNavn, options =>
                options.MapFrom(goalDomain => goalDomain.FkTrainingprogram.Name));
            
            CreateMap<GoalUpdateDto, Goal>();
        }
    }
}
