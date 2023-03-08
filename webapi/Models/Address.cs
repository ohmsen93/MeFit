using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Address
{
    public int Id { get; set; }

    public string AddressLine1 { get; set; } = null!;

    public string? AddressLine2 { get; set; }

    public string? AddressLine3 { get; set; }

    public int PostalCode { get; set; }

    public string City { get; set; } = null!;

    public string Country { get; set; } = null!;

    public virtual ICollection<UserProfile> UserProfiles { get; } = new List<UserProfile>();
}
