using CoreApi.Extensions;
using HDDT_BE.Exceptions;
using HDDT_BE.Helpers;
using HDDT_BE.Installers;
using HDDT_BE.Interfaces.Auth;
using HDDT_BE.Models.Core;
using HDDT_BE.Services.Core;
using MongoDB.Bson;

namespace HDDT_BE.Services.Auth
{
    public class LoggingService : BaseService, ILoggingService
    {
        private DataContext _context;
        private BaseMongoDb<LoggingModel, string> BaseMongoDb;
        public LoggingService(
            DataContext context,
            IHttpContextAccessor contextAccessor) :
            base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<LoggingModel, string>(_context.LOGGING);
        }

        public async Task<dynamic> Create(string content ,string code , string caseName )
        {
            LoggingModel logging = new LoggingModel(content,code , caseName);
            logging.Id = BsonObjectId.GenerateNewId().ToString();
            var result = await BaseMongoDb.CreateAsync(logging);
            if (result.Entity.Id == default || !result.Success || result == null)
                throw new ResponseMessageException().WithException(DefaultCode.CREATE_FAILURE);
            return logging;
        }
    }
}