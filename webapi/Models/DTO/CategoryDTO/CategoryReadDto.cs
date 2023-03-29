namespace webapi.Models.DTO.CategoryDTO
{
    public class CategoryReadDto
    {
        public int Id { get; set; }

        public string Category1 { get; set; }

        public ICollection<int> Trainingprograms { get; set; }
    }
}
