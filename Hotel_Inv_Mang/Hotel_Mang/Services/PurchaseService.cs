using Hotel_Mang.DTOs;
using Hotel_Mang.Models;
using Hotel_Mang.Repositories;

namespace Hotel_Mang.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IVendorRepository _vendorRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository, IVendorRepository vendorRepository)
        {
            _purchaseRepository = purchaseRepository;
            _vendorRepository = vendorRepository;
        }

        public async Task<ApiResponseDto<string>> CreatePurchaseAsync(PurchaseCreateRequestDto request, string username)
        {
            try
            {
                // Validate quantity and price
                if (request.Quantity <= 0)
                {
                    return new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = "Quantity must be greater than 0"
                    };
                }

                if (request.Price <= 0)
                {
                    return new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = "Price must be greater than 0"
                    };
                }

                // Validate vendor exists
                if (!await _vendorRepository.VendorExistsAsync(request.VendorId))
                {
                    return new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = "Vendor not found"
                    };
                }

                // Validate unit
                var validUnits = new[] { "Kg", "Litre", "Tin" };
                if (!validUnits.Contains(request.Unit))
                {
                    return new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = "Unit must be one of: Kg, Litre, Tin"
                    };
                }

                var purchase = new Purchase
                {
                    Date = request.Date,
                    ItemName = request.ItemName,
                    Brand = request.Brand,
                    Quantity = request.Quantity,
                    Unit = request.Unit,
                    Price = request.Price,
                    VendorId = request.VendorId,
                    CreatedBy = username,
                    CreatedAt = DateTime.UtcNow
                };

                await _purchaseRepository.CreatePurchaseAsync(purchase);

                return new ApiResponseDto<string>
                {
                    Success = true,
                    Data = purchase.Id,
                    Message = "Purchase added successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<string>
                {
                    Success = false,
                    Message = $"Error creating purchase: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponseDto<List<Purchase>>> GetAllPurchasesAsync()
        {
            try
            {
                var purchases = await _purchaseRepository.GetAllPurchasesAsync();
                return new ApiResponseDto<List<Purchase>>
                {
                    Success = true,
                    Data = purchases,
                    Message = "Purchases retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<List<Purchase>>
                {
                    Success = false,
                    Message = $"Error retrieving purchases: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponseDto<Purchase>> GetPurchaseByIdAsync(string id)
        {
            try
            {
                var purchase = await _purchaseRepository.GetPurchaseByIdAsync(id);
                if (purchase == null)
                {
                    return new ApiResponseDto<Purchase>
                    {
                        Success = false,
                        Message = "Purchase not found"
                    };
                }

                return new ApiResponseDto<Purchase>
                {
                    Success = true,
                    Data = purchase,
                    Message = "Purchase retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<Purchase>
                {
                    Success = false,
                    Message = $"Error retrieving purchase: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponseDto<List<Purchase>>> GetPurchasesByMonthAsync(int? year, int? month)
        {
            try
            {
                List<Purchase> purchases;

                if (year.HasValue && month.HasValue)
                {
                    purchases = await _purchaseRepository.GetPurchasesByMonthAsync(year.Value, month.Value);
                }
                else
                {
                    purchases = await _purchaseRepository.GetAllPurchasesAsync();
                }

                return new ApiResponseDto<List<Purchase>>
                {
                    Success = true,
                    Data = purchases,
                    Message = "Purchases retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<List<Purchase>>
                {
                    Success = false,
                    Message = $"Error retrieving purchases: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponseDto<object>> GetPurchaseSummaryAsync()
        {
            try
            {
                var totalAmount = await _purchaseRepository.GetTotalPurchaseAmountAsync();
                var quantityByUnit = await _purchaseRepository.GetQuantityByUnitAsync();

                var summary = new
                {
                    TotalPurchaseAmount = totalAmount,
                    QuantityByUnit = quantityByUnit
                };

                return new ApiResponseDto<object>
                {
                    Success = true,
                    Data = summary,
                    Message = "Purchase summary retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<object>
                {
                    Success = false,
                    Message = $"Error retrieving purchase summary: {ex.Message}"
                };
            }
        }
    }
}
