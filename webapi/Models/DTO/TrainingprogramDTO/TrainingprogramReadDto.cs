using webapi.Models.DTO.SetDTO;

namespace webapi.Models.DTO.TrainingprogramDTO
{
    public class TrainingprogramReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Goal> Goals { get; } = new List<Goal>();


        public ICollection<Workout> Workouts { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
