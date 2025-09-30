using System.Security.Claims;
using Hotel_Mang.DTOs;
using Hotel_Mang.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Mang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> GetAllPurchases()
        {
            var result = await _purchaseService.GetAllPurchasesAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> GetPurchaseById(string id)
        {
            var result = await _purchaseService.GetPurchaseByIdAsync(id);

            if (!result.Success)
            {
                return NotFound(result);
            }

            return Ok(result);
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> CreatePurchase([FromBody] PurchaseCreateRequestDto request)
        {
            var username = User.FindFirst(ClaimTypes.Name)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return Unauthorized(new { message = "User not authenticated" });
            }

            var result = await _purchaseService.CreatePurchaseAsync(request, username);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("monthly")]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<IActionResult> GetPurchasesByMonth([FromQuery] int? year, [FromQuery] int? month)
        {
            var result = await _purchaseService.GetPurchasesByMonthAsync(year, month);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("summary")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetPurchaseSummary()
        {
            var result = await _purchaseService.GetPurchaseSummaryAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

    }
}
