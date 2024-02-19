using HDDT_BE.Installers;
using HDDT_BE.Models.Core;
using MongoDB.Driver;

namespace HDDT_BE.Services.Core
{
    public abstract class BaseService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly DataContext _context;
        private User _appUser;
        
        
        protected User CurrentUser => GetCurrentUser();
        private HttpContext _httpContext { get { return _contextAccessor.HttpContext; } }
        
        

        public BaseService()
        {
        }
        public BaseService(DataContext context,
            IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }


        private User GetCurrentUser()
        {
            if (_appUser != null)
                return _appUser;

            var userId = _httpContext.User?.Claims.FirstOrDefault(x => x.Type == "id")?.Value;
            if (userId != default)
                _appUser = _context.USERS.Find(x => x.Id == userId).FirstOrDefault();
            return _appUser;
        }

        
        
    }
}