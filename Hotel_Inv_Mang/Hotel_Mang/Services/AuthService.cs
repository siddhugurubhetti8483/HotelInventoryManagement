using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Hotel_Mang.DTOs;
using Hotel_Mang.Models;
using Hotel_Mang.Repositories;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.IdentityModel.Tokens;

namespace Hotel_Mang.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public async Task<LoginResponseDto> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return new LoginResponseDto { Message = "Invalid username or password" };
            }

            var token = GenerateJwtToken(user);

            return new LoginResponseDto
            {
                Token = token,
                Role = user.Role,
                Username = user.Username,
                Message = "Login successful"
            };
        }

        public async Task<ApiResponseDto<string>> RegisterAsync(RegisterRequestDto request)
        {
            if (await _userRepository.UserExistsAsync(request.Username))
            {
                return new ApiResponseDto<string>
                {
                    Success = false,
                    Message = "Username already exists"
                };
            }

            if (request.Role != "Admin" && request.Role != "Staff")
            {
                return new ApiResponseDto<string>
                {
                    Success = false,
                    Message = "Role must be either 'Admin' or 'Staff'"
                };
            }

            var user = new User
            {
                Username = request.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = request.Role,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.CreateUserAsync(user);

            return new ApiResponseDto<string>
            {
                Success = true,
                Message = "User registered successfully"
            };
        }

        public string GenerateJwtToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings["SecretKey"]);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.NameIdentifier, user.Id)
                }),
                Expires = DateTime.UtcNow.AddHours(24),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
