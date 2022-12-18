using Book.Server.Authorization;
using Book.Shared.Data;
using Book.Shared.Models;

namespace Book.Server.Models
{
    public interface IUserRepository
    {
        AuthenticateResponse Authenticate(AuthenticateRequest request);
        PagedResult<User> GetUsers(string? name, int page);
        Task<User?> GetUser(long Id);
        Task<User> AddUser(User user);
        Task<User?> UpdateUser(User user);
        Task<User?> DeleteUser(long id);
    }
}