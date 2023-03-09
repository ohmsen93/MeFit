namespace webapi.Models.DTO.Exercise
{
    public class ExerciseCreateDto
    {

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public List<int> SetIds { get; set; }

        public List<int> MusclegroupIds { get; set; }


    }
}
