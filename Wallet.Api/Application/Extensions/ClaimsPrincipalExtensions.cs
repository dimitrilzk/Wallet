using System.Security.Claims;

namespace Wallet.Api.Application.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid GetUserId(this ClaimsPrincipal user)
        {
            var value = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("User id claim was not found in the current principal.");
            }

            if (!Guid.TryParse(value, out var userId))
            {
                throw new InvalidOperationException("User id claim is not a valid Guid.");
            }

            return userId;
        }
    }
}
