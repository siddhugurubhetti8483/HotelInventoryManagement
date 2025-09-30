using Hotel_Mang.DTOs;
using Hotel_Mang.Models;
using Hotel_Mang.Repositories;

namespace Hotel_Mang.Services
{
    public class VendorService: IVendorService
    {
        private readonly IVendorRepository _vendorRepository;

        public VendorService(IVendorRepository vendorRepository)
        {
            _vendorRepository = vendorRepository;
        }

        public async Task<ApiResponseDto<List<Vendor>>> GetAllVendorsAsync()
        {
            try
            {
                var vendors = await _vendorRepository.GetAllVendorsAsync();
                return new ApiResponseDto<List<Vendor>>
                {
                    Success = true,
                    Data = vendors,
                    Message = "Vendors retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<List<Vendor>>
                {
                    Success = false,
                    Message = $"Error retrieving vendors: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponseDto<Vendor>> GetVendorByIdAsync(string id)
        {
            try
            {
                var vendor = await _vendorRepository.GetVendorByIdAsync(id);
                if (vendor == null)
                {
                    return new ApiResponseDto<Vendor>
                    {
                        Success = false,
                        Message = "Vendor not found"
                    };
                }

                return new ApiResponseDto<Vendor>
                {
                    Success = true,
                    Data = vendor,
                    Message = "Vendor retrieved successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<Vendor>
                {
                    Success = false,
                    Message = $"Error retrieving vendor: {ex.Message}"
                };
            }
        }

        public async Task<ApiResponseDto<string>> CreateVendorAsync(VendorCreateRequestDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Name))
                {
                    return new ApiResponseDto<string>
                    {
                        Success = false,
                        Message = "Vendor name is required"
                    };
                }

                var vendor = new Vendor
                {
                    Name = request.Name,
                    Contact = request.Contact,
                    ItemsSupplied = request.ItemsSupplied ?? new List<string>(),
                    CreatedAt = DateTime.UtcNow
                };

                await _vendorRepository.CreateVendorAsync(vendor);

                return new ApiResponseDto<string>
                {
                    Success = true,
                    Data = vendor.Id,
                    Message = "Vendor added successfully"
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDto<string>
                {
                    Success = false,
                    Message = $"Error creating vendor: {ex.Message}"
                };
            }
        }
    }
}
