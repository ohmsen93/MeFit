using System.ComponentModel.DataAnnotations.Schema;
using webapi.Models.DTO.Exercise;

namespace webapi.Models.DTO.Workout
{


    public class WorkoutReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Type { get; set; } = null!;

        public int? FkUserProfileId { get; set; }

        [ForeignKey("FkUserProfileId")]
        public virtual UserProfile FkUserProfile { get; set; }

        public ICollection<ExerciseReadDto> Exercises { get; set; }
    }

}