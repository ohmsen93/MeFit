namespace webapi.Models.DTO.UserDTO
{
    public class UserReadDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public bool FirstLogin { get; set; }

        public List<string> UserProfiles { get; set; }
    }
}
