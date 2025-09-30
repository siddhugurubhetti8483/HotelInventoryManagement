using Hotel_Mang.DTOs;
using Hotel_Mang.Models;

namespace Hotel_Mang.Services
{
    public interface IPurchaseService
    {
        Task<ApiResponseDto<List<Purchase>>> GetAllPurchasesAsync();
        Task<ApiResponseDto<Purchase>> GetPurchaseByIdAsync(string id);
        Task<ApiResponseDto<string>> CreatePurchaseAsync(PurchaseCreateRequestDto request, string username);
        Task<ApiResponseDto<List<Purchase>>> GetPurchasesByMonthAsync(int? year, int? month);
        Task<ApiResponseDto<object>> GetPurchaseSummaryAsync();
    }
}
