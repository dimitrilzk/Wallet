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
                var calim = httpContextAccessor?.HttpContext?.User.FindFirst("sub")?.Value;

            }
        }
    }
}
