

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HDDT_BE.Exceptions;
using HDDT_BE.Extensions;
using HDDT_BE.Helpers;
using HDDT_BE.Installers;
using HDDT_BE.Interfaces.Auth;
using HDDT_BE.Interfaces.Core;
using HDDT_BE.Models.Auth;
using HDDT_BE.Models.Core;
using HDDT_BE.Models.Settings;
using HDDT_BE.Services.Core;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace HDDT_BE.Services.Auth
{
    public class IdentityService : BaseService, IIdentityService
    {
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly JwtSettings _jwtSettings;
        private readonly IRoleService _roleService;
        private readonly IMenuService _menuService;
        private DataContext _context;

        public IdentityService(
            DataContext context,
            JwtSettings jwtSettings,
            IRoleService roleService,
            IMenuService menuService,
            IRefreshTokenService refreshTokenService, IHttpContextAccessor httpContext
        ) : base(context, httpContext)
        {
            _context = context;
            _roleService = roleService;
            _refreshTokenService = refreshTokenService;
            _jwtSettings = jwtSettings;
            _menuService = menuService;
        }


        public async Task<dynamic> Authenticate(AuthRequest model)
        {
            var user = await _context.USERS.Find(x => !x.IsDeleted && x.UserName == model.UserName).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION)
                    .WithMessage("Không tìm thấy tài khoản.");
            }


            if (!PasswordExtensions.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION)
                    .WithMessage("Mật khẩu không chính xác.");
            }
               
       
            var permissions = await _roleService.GetPermissionForCurrentUer(user.UserName);
            var menu = await _roleService.GetMenuForUser(user.UserName);
            var menuItems = await _menuService.Get();
            user.Permissions = permissions;
            user.Menu = menu;
            user.MenuItems = menuItems;
            return user;
         
        }
        
        private async Task<dynamic> GenerateAuthenticationResultForUserAsync(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.UserName),
                new Claim("id", user.Id)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.Add(_jwtSettings.TokenLifetime),
                SigningCredentials =
                    new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                Token = tokenHandler.WriteToken(token),
                CreationDate = DateTime.UtcNow,
                ExpiryDate = DateTime.UtcNow.Add(_jwtSettings.TokenRefreshStore)
            };

             var createdRefreshToken = await _refreshTokenService.Create(refreshToken);

            var userLogin = new UserLogin(user);

            return new AuthResponse(userLogin,
                tokenHandler.WriteToken(token),
                refreshToken.JwtId,
                tokenDescriptor.Expires.HasValue ? tokenDescriptor.Expires.Value : refreshToken.ExpiryDate
            );
        }

        public async Task<dynamic> LoginAsync(AuthRequest model)
        {
            var user = await Authenticate(model);
            if (!user.IsActived)
            {
                throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION)
                    .WithMessage("Tài khoản hiện tại đang bị khóa.");
            }
            else if (user.IsRequireVerify)
            {
                throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION)
                    .WithMessage("Tài khoản chưa được xác minh.");
            }

            return await GenerateAuthenticationResultForUserAsync(user);
            
        }
        
        
    }
}