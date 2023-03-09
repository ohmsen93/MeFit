namespace webapi.Models.DTO.User
{
    public class UserReadDto
    {
        public int Id { get; set; }

        public string Token { get; set; } = null!;

        public string Username { get; set; } = null!;        
    }
}
