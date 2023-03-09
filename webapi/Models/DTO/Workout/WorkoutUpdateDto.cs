using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models.DTO.Workout
{

    public class WorkoutUpdateDto
    {
        public string Name { get; set; } = null!;

        public string Type { get; set; } = null!;

        public List<int> ExerciseIds { get; set; }


    }
}
