using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Trainingprogram
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Goal> Goals { get; } = new List<Goal>();


    public ICollection<Workout> Workouts { get; set; }

    public ICollection<Category> Categories { get; set; }
}
