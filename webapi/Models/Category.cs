using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Category1 { get; set; } = null!;

    public ICollection<Trainingprogram> Trainingprograms { get; set; }
}
