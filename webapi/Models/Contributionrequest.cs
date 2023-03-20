using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Contributionrequest
{
    public int Id { get; set; }

    public int FkUserProfileId { get; set; }

    public virtual UserProfile FkUserProfile { get; set; } = null!;
}
