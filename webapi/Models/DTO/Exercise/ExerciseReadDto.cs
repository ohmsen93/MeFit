using webapi.Models;

namespace webapi.Models.DTO.Exercise
{
    public class ExerciseReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public ICollection<Set> Sets { get; set; }

        public ICollection<Musclegroup> Musclegroups { get; set; }

        public ICollection<Workout> Workouts { get; set; }
    }
}
