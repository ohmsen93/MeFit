using System;
using System.Collections.Generic;

namespace webapi.Models;

public partial class UserProfile
{
    public int Id { get; set; }

    public int FkUserId { get; set; }

    public int FkAddressId { get; set; }

    public double Weight { get; set; }

    public double Height { get; set; }

    public string? MedicalCondition { get; set; }

    public string? Disabilities { get; set; }

    public string Firstname { get; set; } = null!;

    public string Lastname { get; set; } = null!;

    public int Phone { get; set; }

    public string? Picture { get; set; }

    public string Email { get; set; } = null!;

    public virtual ICollection<Contributionrequest> Contributionrequests { get; } = new List<Contributionrequest>();

    public virtual Address FkAddress { get; set; } = null!;

    public virtual User FkUser { get; set; } = null!;

    public virtual ICollection<Goal> Goals { get; } = new List<Goal>();

    public ICollection<Workout> Workouts { get; set; }
}
