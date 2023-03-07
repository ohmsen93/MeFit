using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class ExerciseMusclegroup
{
    public int FkExerciseId { get; set; }

    public int FkMusclegroupId { get; set; }

    public virtual Exercise FkExercise { get; set; } = null!;

    public virtual Musclegroup FkMusclegroup { get; set; } = null!;
}
