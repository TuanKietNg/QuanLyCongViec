using CoreApi.Extensions;
using HDDT_BE.Exceptions;
using HDDT_BE.Helpers;
using HDDT_BE.Installers;
using HDDT_BE.Interfaces.Auth;
using HDDT_BE.Models.Auth;
using HDDT_BE.Services.Core;
using MongoDB.Driver;

namespace HDDT_BE.Services.Auth
{
    public class RefreshTokenService : BaseService, IRefreshTokenService
    {
        private DataContext _context;
        private BaseMongoDb<RefreshToken, string> BaseMongoDb;
        IMongoCollection<RefreshToken> _collection;
        
        public RefreshTokenService(
            DataContext context,
            IHttpContextAccessor contextAccessor) :
            base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<RefreshToken, string>(_context.REFRESHTOKEN);
        }

        public async Task<RefreshToken> Create(RefreshToken model)
        {
            if (model == default)
            {
                throw new ResponseMessageException().WithException(DefaultCode.EXCEPTION);
            }
            var result = await BaseMongoDb.CreateAsync(model);
            if (result.Entity.Id == default || !result.Success)
            {
                throw new ResponseMessageException().WithException(DefaultCode.CREATE_FAILURE);
            }
            return model;
        }

    


    }
}