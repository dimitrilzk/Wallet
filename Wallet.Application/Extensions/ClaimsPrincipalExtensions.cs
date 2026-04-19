using System.Security.Claims;

namespace Wallet.Application.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid? GetUserIdOrNull(this ClaimsPrincipal? principal)
        {
            if (principal == null)
            {
                return null;
            }

            var value = principal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(value))
            {
                return null;
            }

            if (Guid.TryParse(value, out var userId))
            {
                return userId;
            }
            else
            {
                return null;
            }
        }

        public static Guid GetUserId(this ClaimsPrincipal principal)
        {
            var userId = principal.GetUserIdOrNull();

            if (!userId.HasValue)
            {
                throw new ArgumentException("User id claim was not found or is invalid.");
            }

            return userId.Value;
        }
    }
}
