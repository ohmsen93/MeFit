using webapi.Models;
using webapi.Models.DTO.SetDTO;

namespace webapi.Models.DTO.ExerciseDTO
{
    public class ExerciseReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public ICollection<SetReadDto> Sets { get; set; }

        public ICollection<Musclegroup> Musclegroups { get; set; }
    }
}
