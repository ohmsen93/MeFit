using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Contributionrequest
{
    public int Id { get; set; }

    public int FkProfileId { get; set; }

    public virtual Profile FkProfile { get; set; } = null!;
}
