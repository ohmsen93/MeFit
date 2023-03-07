using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Exercise
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Set> Sets { get; } = new List<Set>();
}
