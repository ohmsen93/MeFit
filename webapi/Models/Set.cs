using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Set
{
    public int Id { get; set; }

    public int Reps { get; set; }

    public ICollection<Exercise> Exercises { get; set; }

    public virtual ICollection<Workout> Workouts { get; } = new List<Workout>();
}
