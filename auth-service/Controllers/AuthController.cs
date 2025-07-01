using Microsoft.AspNetCore.Mvc;
using auth_service.Services;
using auth_service.Models.DTOs;

namespace auth_service.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto)
        {
            var token = await _authService.LoginAsync(dto);

            if (token == null)
                return Unauthorized("Usuário ou senha inválidos.");

            return Ok(new { token });
        }
    }
}
