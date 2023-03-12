namespace webapi.Models.DTO.TrainingprogramDTO
{
    public class TrainingprogramUpdateDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public virtual ICollection<Goal> Goals { get; } = new List<Goal>();

    }
}
