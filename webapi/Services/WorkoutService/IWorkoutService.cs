using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.DatabaseContext;
using webapi.Exceptions;
using webapi.Models;
using webapi.Models.DTO.Workout;

namespace webapi.Services.WorkoutService
{
    public interface IWorkoutService : IServices<Workout, int>
    {
        public Task UpdateWorkoutExercises(int WorkoutId, List<int> exercisesId);

    }
}
