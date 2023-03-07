using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Workout
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Type { get; set; } = null!;

    public int FkSetId { get; set; }

    public virtual Set FkSet { get; set; } = null!;

    public ICollection<Trainingprogram> Trainingprograms { get; set; }

    public ICollection<Goal> Goals { get; set; }


}
