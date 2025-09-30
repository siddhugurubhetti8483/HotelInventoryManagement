using Hotel_Mang.DTOs;
using Hotel_Mang.Models;

namespace Hotel_Mang.Services
{
    public interface IVendorService
    {
        Task<ApiResponseDto<List<Vendor>>> GetAllVendorsAsync();
        Task<ApiResponseDto<Vendor>> GetVendorByIdAsync(string id);
        Task<ApiResponseDto<string>> CreateVendorAsync(VendorCreateRequestDto request);
    }
}
