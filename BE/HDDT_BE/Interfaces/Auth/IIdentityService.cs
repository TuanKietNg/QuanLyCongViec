using HDDT_BE.Models.Auth;

namespace HDDT_BE.Interfaces.Auth
{
    public interface IIdentityService
    {
        Task<dynamic> Authenticate(AuthRequest model);
        Task<dynamic> LoginAsync(AuthRequest model);
   
    }
}