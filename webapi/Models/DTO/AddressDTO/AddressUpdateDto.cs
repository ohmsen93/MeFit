namespace webapi.Models.DTO.AddressDTO
{
    public class AddressUpdateDto
    {
        public int Id { get; set; }

        public string AddressLine1 { get; set; } = null!;

        public string? AddressLine2 { get; set; }

        public string? AddressLine3 { get; set; }

        public int PostalCode { get; set; }

        public string City { get; set; } = null!;

        public string Country { get; set; } = null!;
    }
}
