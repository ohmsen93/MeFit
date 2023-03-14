namespace webapi.Models.DTO.UserDTO
{
    public class UserUpdateDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public bool FirstLogin { get; set; }

    }
}
