using HDDT_BE.Models.Core;

namespace HDDT_BE.Models.Auth
{
    // AuthenticationResult
    public class RefreshToken : Audit, TEntity<string>
    {
        public string Token { get; set; }

        public string JwtId { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool Used { get; set; }

        public bool Invalidated { get; set; }

        public string UserId { get; set; }
    }
}
