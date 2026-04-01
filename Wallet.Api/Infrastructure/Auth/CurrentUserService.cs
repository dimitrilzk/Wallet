using Wallet.Api.Application.Extensions;
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

                return cntxt.User.GetUserIdOrNull();
            }
        }
    }
}
