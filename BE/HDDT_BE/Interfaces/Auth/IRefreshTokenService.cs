using HDDT_BE.Models.Auth;

namespace HDDT_BE.Interfaces.Auth
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> Create(RefreshToken model);
    }
}