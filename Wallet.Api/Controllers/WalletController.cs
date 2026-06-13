using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Wallet.Application.Interfaces;
using Wallet.Application.DTOs.Wallet;
using Wallet.Application.Extensions;

namespace Wallet.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletApplicationService walletService;

        public WalletController(IWalletApplicationService walletService)
        {
            this.walletService = walletService;
        }

        [Authorize]
        [HttpGet("current")]
        [ProducesResponseType(typeof(WalletResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrCreateCurrentWallet()
        {
            var userId = User.GetUserId();
            var resp = await walletService.GetOrCreateWalletAsync(userId, DateTime.UtcNow.Year);
            return Ok(resp);
        }

        [Authorize]
        [HttpGet("{year}")]
        [ProducesResponseType(typeof(WalletResponseDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOrCreateWalletByYear([FromRoute] int year)
        {
            var userId = User.GetUserId();
            var resp = await walletService.GetOrCreateWalletAsync(userId, year);
            return Ok(resp);
        }
    }
}
