using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class ProgramCategory
{
    public int FkProgramId { get; set; }

    public int FkCategoryId { get; set; }

    public virtual Category FkCategory { get; set; } = null!;

    public virtual Trainingprogram FkProgram { get; set; } = null!;
}
