using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Exercise
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public ICollection<Set> Sets { get; set;  }

    public ICollection<Musclegroup> Musclegroups { get; set; }

    public ICollection<Workout> Workouts { get; set; }

}
