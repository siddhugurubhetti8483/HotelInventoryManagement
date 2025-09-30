using Hotel_Mang.Models;

namespace Hotel_Mang.Repositories
{
    public interface IVendorRepository
    {
        Task<List<Vendor>> GetAllVendorsAsync();
        Task<Vendor> GetVendorByIdAsync(string id);
        Task CreateVendorAsync(Vendor vendor);
        Task<bool> VendorExistsAsync(string id);
    }
}
