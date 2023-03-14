using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public bool FirstLogin { get; set; }

    public virtual ICollection<UserProfile> UserProfiles { get; } = new List<UserProfile>();
}
