using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Status
{
    public int Id { get; set; }

    public string Statustype { get; set; } = null!;

    public virtual ICollection<Goal> Goals { get; } = new List<Goal>();
}
