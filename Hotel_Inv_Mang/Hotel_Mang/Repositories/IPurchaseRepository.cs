using Hotel_Mang.Models;

namespace Hotel_Mang.Repositories
{
    public interface IPurchaseRepository
    {
        Task<List<Purchase>> GetAllPurchasesAsync();
        Task<Purchase> GetPurchaseByIdAsync(string id);
        Task CreatePurchaseAsync(Purchase purchase);
        Task<List<Purchase>> GetPurchasesByMonthAsync(int year, int month);
        Task<decimal> GetTotalPurchaseAmountAsync();
        Task<Dictionary<string, decimal>> GetQuantityByUnitAsync();
    }
}
