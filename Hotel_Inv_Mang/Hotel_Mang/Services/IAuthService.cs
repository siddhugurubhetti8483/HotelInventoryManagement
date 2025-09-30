using Hotel_Mang.DTOs;
using Hotel_Mang.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace Hotel_Mang.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDto> LoginAsync(LoginRequestDto request);
        Task<ApiResponseDto<string>> RegisterAsync(RegisterRequestDto request);
        string GenerateJwtToken(User user);
    }
}
