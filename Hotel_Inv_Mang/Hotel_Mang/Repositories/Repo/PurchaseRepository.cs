using Hotel_Mang.Configurations;
using Hotel_Mang.Models;
using MongoDB.Driver;

namespace Hotel_Mang.Repositories.Repo
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly IMongoCollection<Purchase> _purchases;

        public PurchaseRepository(MongoDBContext context)
        {
            _purchases = context.Purchases;
        }

        public async Task<List<Purchase>> GetAllPurchasesAsync()
        {
            return await _purchases.Find(_ => true).SortByDescending(p => p.Date).ToListAsync();
        }

        public async Task<Purchase> GetPurchaseByIdAsync(string id)
        {
            return await _purchases.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreatePurchaseAsync(Purchase purchase)
        {
            await _purchases.InsertOneAsync(purchase);
        }

        public async Task<List<Purchase>> GetPurchasesByMonthAsync(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            return await _purchases.Find(p => p.Date >= startDate && p.Date <= endDate)
                                  .SortByDescending(p => p.Date)
                                  .ToListAsync();
        }

        public async Task<decimal> GetTotalPurchaseAmountAsync()
        {
            var purchases = await _purchases.Find(_ => true).ToListAsync();
            return purchases.Sum(p => p.TotalAmount);
        }

        public async Task<Dictionary<string, decimal>> GetQuantityByUnitAsync()
        {
            var purchases = await _purchases.Find(_ => true).ToListAsync();
            return purchases.GroupBy(p => p.Unit)
                           .ToDictionary(g => g.Key, g => g.Sum(p => p.Quantity));
        }
    }
}
