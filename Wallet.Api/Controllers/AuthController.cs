using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Wallet.Api.Application.Auth;
using Wallet.Api.Domain.Entities;
using Wallet.Api.DTOs.Auth;

namespace Wallet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtTokenGenerator jwtTokenGenerator;
        private readonly UserManager<AppUser> userManager;

        public AuthController(IJwtTokenGenerator jwtTokenGenerator, UserManager<AppUser> userManager)
        {
            this.jwtTokenGenerator = jwtTokenGenerator;
            this.userManager = userManager;
        }

        [HttpPost("register")]
        [ProducesResponseType(typeof(void), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser is not null)
            {
                return BadRequest(new { error = "Email is already in use." });
            }

            var user = new AppUser(request.FirstName);

            var result = await userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToArray();
                return BadRequest(new { errors });
            }

            // Auto-login
            var tokenResult = jwtTokenGenerator.GenerateTokenJwt(user);

            var response = new AuthResponseDto
            {
                AccessToken = tokenResult.AccessToken,
                ExpiresAtUtc = tokenResult.ExpiresAtUtc,
                ExpiresInSeconds = (int)(tokenResult.ExpiresAtUtc - DateTime.UtcNow).TotalSeconds
            };

            return StatusCode(StatusCodes.Status201Created, response);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return Unauthorized(new { error = "Invalid credentials." });
            }

            var passwordValid = await userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
            {
                return Unauthorized(new { error = "Invalid credentials." });
            }

            var tokenResult = jwtTokenGenerator.GenerateTokenJwt(user);

            var response = new AuthResponseDto
            {
                AccessToken = tokenResult.AccessToken,
                ExpiresAtUtc = tokenResult.ExpiresAtUtc,
                ExpiresInSeconds = (int)(tokenResult.ExpiresAtUtc - DateTime.UtcNow).TotalSeconds
            };

            return Ok(response);
        }

        [HttpPost("debug-token")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public IActionResult GenerateDebugToken()
        {
            var fakeUser = new AppUser("Test");
            var tokenResult = jwtTokenGenerator.GenerateTokenJwt(fakeUser);

            return Ok(new
            {
                access_token = tokenResult.AccessToken,
                expires_at_utc = tokenResult.ExpiresAtUtc
            });
        }
    }
}
