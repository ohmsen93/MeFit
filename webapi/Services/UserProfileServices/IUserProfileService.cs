using webapi.Models;

namespace webapi.Services.UserProfileServices
{
    public interface IUserProfileService:IServices<UserProfile,int>
    {
        Task ContributorRequest(int id);

    }
}
