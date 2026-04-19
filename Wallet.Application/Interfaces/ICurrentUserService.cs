namespace Wallet.Application.Interfaces
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
    }
}
