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
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] ERMS.Application.DTOs.RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);
            if (!result)
                return BadRequest(new { message = "Kullanıcı kaydı başarısız oldu veya bu e posta zaten kullanılıyor" });
            return Ok(new { message = "Kullanıcı kaydı yapıldı" });
        }
    }
}
