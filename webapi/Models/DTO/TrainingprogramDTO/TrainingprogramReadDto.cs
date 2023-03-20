using webapi.Models.DTO.SetDTO;

namespace webapi.Models.DTO.TrainingprogramDTO
{
    public class TrainingprogramReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public List<string> Goals { get; set; }

        public List<string> Workouts { get; set; }

        public List<string> Categories { get; set; }


    }
}
