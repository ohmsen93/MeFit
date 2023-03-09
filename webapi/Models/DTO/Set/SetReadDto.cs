using webapi.Models;
using webapi.Models.DTO.Exercise;

namespace webapi.Models.DTO.Set
{
    public class SetReadDto
    {
        public int Id { get; set; }

        public int Reps { get; set; }

        public int Total { get; set; }

        public ICollection<ExerciseReadDto> Exercises { get; set; }
    }
}
