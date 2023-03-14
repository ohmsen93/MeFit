using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Goal
{
    public int Id { get; set; }

    public int FkUserProfileId { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public int? FkTrainingprogramId { get; set; }

    public int FkStatusId { get; set; }

    public virtual UserProfile FkUserProfile { get; set; } = null!;

    public virtual Trainingprogram? FkTrainingprogram { get; set; }

    public virtual Status FkStatus { get; set; } = null!;

    public ICollection<GoalWorkouts> GoalWorkouts { get; set; }
}
