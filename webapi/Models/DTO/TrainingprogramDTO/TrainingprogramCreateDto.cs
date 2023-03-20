namespace webapi.Models.DTO.TrainingprogramDTO
{
    public class TrainingprogramCreateDto
    {
        public string Name { get; set; } = null!;        
        public List<int> WorkoutIds { get; set; }
        public List<int> CategoryIds { get; set; }
    }
}
