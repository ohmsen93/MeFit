namespace webapi.Models.Entities
{
    public record WorkoutRc (int GwId, int WkId, string Name, string WorkoutType, int? FkUserProfileId,int StatusId, string WorkoutStatus );
}
