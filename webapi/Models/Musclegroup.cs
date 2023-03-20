using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Musclegroup
{
    public int Id { get; set; }

    public string Musclegroup1 { get; set; } = null!;

    public ICollection<Exercise> Exercises { get; set; }
}
