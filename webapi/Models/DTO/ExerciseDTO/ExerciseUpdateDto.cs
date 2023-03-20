namespace webapi.Models.DTO.ExerciseDTO
{
    public class ExerciseUpdateDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

    }
}
