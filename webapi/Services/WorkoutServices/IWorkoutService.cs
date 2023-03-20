﻿using webapi.Models;

namespace webapi.Services.WorkoutServices
{
    public interface IWorkoutService : IServices<Workout, int>
    {            
        Task<Workout> Create(Workout entity, List<int> exercises);
        public Task UpdateWorkoutExercises(int WorkoutId, List<int> exercisesId);

        public Task<ICollection<Workout>> GetAllNoCustom();

        public Task<ICollection<Workout>> GetWorkoutsByTrainingprogramId(int id);

    }
}
