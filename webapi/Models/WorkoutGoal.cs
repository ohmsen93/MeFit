using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class WorkoutGoal
{
    public int FkWorkoutId { get; set; }

    public int FkGoalId { get; set; }

    public int FkStatusId { get; set; }

    public virtual Goal FkGoal { get; set; } = null!;

    public virtual Status FkStatus { get; set; } = null!;

    public virtual Workout FkWorkout { get; set; } = null!;
}
