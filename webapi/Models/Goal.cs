using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class Goal
{
    public int Id { get; set; }

    public int FkProfileId { get; set; }

    public DateTime EndDate { get; set; }

    public bool Achived { get; set; }

    public int? FkTrainingprogramId { get; set; }

    public int FkStatusId { get; set; }

    public virtual Profile FkProfile { get; set; } = null!;

    public virtual Trainingprogram? FkProgram { get; set; }

    public virtual Status FkStatus { get; set; } = null!;
}
