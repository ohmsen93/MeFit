using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Set
{
    public int Id { get; set; }

    public int Reps { get; set; }

    public int FkExerciseId { get; set; }

    public virtual Exercise FkExercise { get; set; } = null!;

    public virtual ICollection<Workout> Workouts { get; } = new List<Workout>();
}
