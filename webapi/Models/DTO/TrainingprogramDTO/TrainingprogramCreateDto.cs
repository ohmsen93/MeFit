namespace webapi.Models.DTO.TrainingprogramDTO
{
    public class TrainingprogramCreateDto
    {


        public string Name { get; set; } = null!;

        public virtual ICollection<Goal> Goals { get; } = new List<Goal>();


        public List<int> WorkoutIds { get; set; }

        public List<int> CategoryIds { get; set; }


    }
}
