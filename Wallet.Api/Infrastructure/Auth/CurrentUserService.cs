using Wallet.Api.Application.Interfaces;

namespace Wallet.Api.Infrastructure.Auth
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public Guid? UserId
        {
            get
            {
                var cntxt = httpContextAccessor.HttpContext;

                if (cntxt is null || cntxt.User is null || cntxt.User.Identity is null || !cntxt.User.Identity.IsAuthenticated)
                {
                    return null;
                }

                var user = cntxt.User;
                var value = user.FindFirst("sub")?.Value;
                
                if (string.IsNullOrWhiteSpace(value))
                {
                    return null;
                }

                if (Guid.TryParse(value, out var id))
                {
                    return id;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
