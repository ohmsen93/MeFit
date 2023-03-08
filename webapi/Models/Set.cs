using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Set
{
    public int Id { get; set; }

    public int Reps { get; set; }

    public int Total { get; set; }

    public ICollection<Exercise> Exercises { get; set; }

}
