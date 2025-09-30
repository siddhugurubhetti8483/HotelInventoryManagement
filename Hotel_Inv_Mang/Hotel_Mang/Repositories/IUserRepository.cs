using Hotel_Mang.Models;

namespace Hotel_Mang.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task CreateUserAsync(User user);
        Task<bool> UserExistsAsync(string username);
    }
}
