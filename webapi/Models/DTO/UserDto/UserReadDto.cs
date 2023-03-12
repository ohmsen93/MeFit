namespace webapi.Models.DTO.UserDTO
{
    public class UserReadDTO
    {
        public int Id { get; set; }

        public string Token { get; set; } = null!;

        public string Username { get; set; } = null!;
        public List<string> UserProfiles { get; set; }
    }
}
