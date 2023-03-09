using webapi.Models;

namespace webapi.Models.DTO.Set
{
    public class SetReadDto
    {
        public int Id { get; set; }

        public int Reps { get; set; }

        public int Total { get; set; }

        public ICollection<Exercise> Exercises { get; set; }
    }
}
