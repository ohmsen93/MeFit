using webapi.Models;
using webapi.Models.DTO.SetDTO;

namespace webapi.Models.DTO.ExerciseDTO
{
    public class ExerciseReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public List<string> Sets { get; set; }

        public List<string> Musclegroups { get; set; }
    }
}
