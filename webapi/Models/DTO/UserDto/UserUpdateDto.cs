namespace webapi.Models.DTO.UserDTO
{
    public class UserUpdateDto
    {
        public int Id { get; set; }

        public string Token { get; set; } = null!;

        public string Username { get; set; } = null!;
    }
}
