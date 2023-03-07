using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class User
{
    public int Id { get; set; }

    public string Token { get; set; } = null!;

    public string Username { get; set; } = null!;

    public virtual ICollection<Profile> Profiles { get; } = new List<Profile>();
}
