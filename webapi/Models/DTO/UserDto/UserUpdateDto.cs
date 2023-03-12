namespace webapi.Models.DTO.UserDTO
{
    public class UserUpdateDTO
    {
        public int Id { get; set; }

        public string Token { get; set; } = null!;

        public string Username { get; set; } = null!;
    }
}
