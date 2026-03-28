using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallet.Api.Application.Extensions;
using Wallet.Api.Application.Services;
using Wallet.Api.DTOs.Wallet;

namespace Wallet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly WalletApplicationService walletService;

        public WalletController(WalletApplicationService walletService)//TODO In ottica più pulita potrei in futuro dipendere da un’interfaccia
        {
            this.walletService = walletService;
        }

        [Authorize]
        [HttpPost("current")]
        [ProducesResponseType(typeof(WalletResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetOrCreateWallet([FromBody] WalletRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.GetUserId();
            var resp = await walletService.GetOrCreateWalletAsync(userId, request.Year);
            return Ok(resp);
        }
    }
}
