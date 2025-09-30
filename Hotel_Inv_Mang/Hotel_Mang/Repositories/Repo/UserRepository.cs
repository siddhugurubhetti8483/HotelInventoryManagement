using Hotel_Mang.Configurations;
using Hotel_Mang.Models;
using MongoDB.Driver;

namespace Hotel_Mang.Repositories.Repo
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public UserRepository(MongoDBContext context)
        {
            _users = context.Users;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task CreateUserAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task<bool> UserExistsAsync(string username)
        {
            return await _users.Find(u => u.Username == username).AnyAsync();
        }
    }
}
