namespace webapi.Models.DTO.UserDto
{
    public class UserUpdateDTO
    {
        public int Id { get; set; }

        public string Token { get; set; } = null!;

        public string Username { get; set; } = null!;
    }
}
