using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Models.DTO.ExerciseDTO;

namespace webapi.Services.ExerciseService
{
    public interface IExerciseService : IServices<Exercise, int>
    {
        public Task UpdateExerciseSets(int exerciseId, List<int> setsId);

        public Task UpdateExerciseMusclegroups(int exerciseId, List<int> musclegroupsId);

    }
}
