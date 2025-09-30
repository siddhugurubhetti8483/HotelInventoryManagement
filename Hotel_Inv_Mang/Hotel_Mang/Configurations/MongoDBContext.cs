using Hotel_Mang.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Hotel_Mang.Configurations
{
    public class MongoDBContext
    {
        private readonly IMongoDatabase _database;

        public MongoDBContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("users");
        public IMongoCollection<Purchase> Purchases => _database.GetCollection<Purchase>("purchases");
        public IMongoCollection<Vendor> Vendors => _database.GetCollection<Vendor>("vendors");
    }
}
