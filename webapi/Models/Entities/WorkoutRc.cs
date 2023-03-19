namespace webapi.Models.Entities
{
    public record WorkoutRc (int Id, string Name, string WorkoutType, int? FkUserProfileId, string WorkoutStatus );
}
