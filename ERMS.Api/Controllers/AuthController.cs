using ERMS.Application.DTOs;
using ERMS.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERMS.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);
            if (result == null)
                return Unauthorized(new { message = "E- posta veya şifre hatalı" }); 

            return Ok(result);
        }
    }
}
