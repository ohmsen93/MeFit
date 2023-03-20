using webapi.Models.DTO.SetDTO;
using webapi.Models.Entities;

namespace webapi.Models.DTO.TrainingprogramDTO
{
    public class TrainingprogramReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public List<string> Goals { get; set; }

        public List<int> Workouts { get; set; }

        public List<CategoryRc> Categories { get; set; }


    }
}
