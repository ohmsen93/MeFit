using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class ProgramWorkout
{
    public int FkProgramId { get; set; }

    public int FkWorkoutId { get; set; }

    public virtual Trainingprogram FkProgram { get; set; } = null!;

    public virtual Workout FkWorkout { get; set; } = null!;
}
