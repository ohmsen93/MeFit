using webapi.Models;

namespace webapi.Services.UserServices
{
    public interface IUserService:IServices<User, string>
    {
        Task<User> GetById(string id, bool throwIfNotFound);
    }
}
