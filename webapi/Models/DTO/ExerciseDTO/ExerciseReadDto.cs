using webapi.Models;
using webapi.Models.DTO.SetDTO;
using webapi.Models.Entities;

namespace webapi.Models.DTO.ExerciseDTO
{
    public class ExerciseReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public List<SetRc> Sets { get; set; }

        public List<MuscleGroupRc> Musclegroups { get; set; }
    }
}
