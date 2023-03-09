namespace webapi.Models.DTO.User
{
    public class UserUpdateDto
    {
        public int Id { get; set; }

        public string Token { get; set; } = null!;

        public string Username { get; set; } = null!;
    }
}
