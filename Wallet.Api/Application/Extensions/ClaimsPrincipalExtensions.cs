using System.Security.Claims;

namespace Wallet.Api.Application.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var value = user.FindFirst("sub")?.Value;

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("User id claim 'sub' was not found in the current principal.");
            }

            if (!Guid.TryParse(value, out var userId))
            {
                throw new InvalidOperationException("Claim 'sub' is not a valid Guid");
            }

            return userId;
        }
    }
}
