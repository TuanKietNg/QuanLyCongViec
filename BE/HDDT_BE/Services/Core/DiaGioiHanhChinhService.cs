using CoreApi.Extensions;
using HDDT_BE.Exceptions;
using HDDT_BE.Helpers;
using HDDT_BE.Installers;
using HDDT_BE.Interfaces.Auth;
using HDDT_BE.Interfaces.Core;
using HDDT_BE.Models.Core;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HDDT_BE.Services.Core
{
    public class DiaGioiHanhChinhService : BaseService, IDiaGioiHanhChinhService
    {
        private DataContext _context;
        private BaseMongoDb<DiaGioiHanhChinhModel, string> BaseMongoDb;
        private IMongoCollection<DiaGioiHanhChinhModel> _collection;

        
        public DiaGioiHanhChinhService(ILoggingService logger, DataContext context,
            IHttpContextAccessor contextAccessor)
            : base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<DiaGioiHanhChinhModel, string>(_context.DIAGIOIHANHCHINH);
            _collection = context.DIAGIOIHANHCHINH;
        }
        
        public async Task<dynamic> GetById(string id)
        {
            var model = await _collection.Find(x => x.Id == id && x.IsDeleted != true).FirstOrDefaultAsync();
            return model; 
        }

        public async Task<dynamic> GetListChildByCode(string code)
        {
            var list = _collection.AsQueryable().Where(x => !x.IsDeleted && x.Code != null && x.IdDonViCha == code).
                Select(x=> new {id = x.Id, code = x.Code,name =  x.Name, oldId = x.OldId }).ToList();
            return list;
        }

        public async  Task<dynamic> GetListByLevel(int level)
        {
            var list = _collection.AsQueryable().Where(x => x.IsDeleted != true && x.Id != null && x.Level== level)
                .Select(x=> new {id = x.Id, code = x.Code,name =  x.Name, oldId = x.OldId }).ToList();
            return list;
        }
    }
}