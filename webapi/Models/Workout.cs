using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

public partial class Workout
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int? FkUserProfileId { get; set; }

    [ForeignKey("FkUserProfileId")]
    public virtual UserProfile FkUserProfile { get; set; }

    public ICollection<Trainingprogram> Trainingprograms { get; set; }

    public ICollection<GoalWorkouts> GoalWorkouts { get; set; }

    public ICollection<Exercise> Exercises { get; set; }


}
