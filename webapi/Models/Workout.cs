using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

public partial class Workout
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int? FkProfileId { get; set; }

    [ForeignKey("FkProfileId")]
    public virtual Profile FkProfile { get; set; }

    public ICollection<Trainingprogram> Trainingprograms { get; set; }

    public ICollection<Goal> Goals { get; set; }

    public ICollection<Exercise> Exercises { get; set; }


}
