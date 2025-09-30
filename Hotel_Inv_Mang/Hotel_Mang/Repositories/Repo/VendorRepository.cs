using Hotel_Mang.Configurations;
using Hotel_Mang.Models;
using MongoDB.Driver;

namespace Hotel_Mang.Repositories.Repo
{
    public class VendorRepository : IVendorRepository
    {
        private readonly IMongoCollection<Vendor> _vendors;

        public VendorRepository(MongoDBContext context)
        {
            _vendors = context.Vendors;
        }

        public async Task<List<Vendor>> GetAllVendorsAsync()
        {
            return await _vendors.Find(_ => true).SortBy(v => v.Name).ToListAsync();
        }

        public async Task<Vendor> GetVendorByIdAsync(string id)
        {
            return await _vendors.Find(v => v.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateVendorAsync(Vendor vendor)
        {
            await _vendors.InsertOneAsync(vendor);
        }

        public async Task<bool> VendorExistsAsync(string id)
        {
            return await _vendors.Find(v => v.Id == id).AnyAsync();
        }
    }
}
