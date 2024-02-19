using CoreApi.Extensions;
using HDDT_BE.Exceptions;
using HDDT_BE.Helpers;
using HDDT_BE.Installers;
using HDDT_BE.Interfaces.Auth;
using HDDT_BE.Interfaces.Core;
using HDDT_BE.Models.Auth;
using HDDT_BE.Models.Core;
using HDDT_BE.Models.PagingParam;
using HDDT_BE.Services.Core;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HDDT_BE.Services.Auth
{
    public class UnitRoleService : BaseService, IUnitRoleService
    {
        private DataContext _context;
        private BaseMongoDb<UnitRole, string> BaseMongoDb;
        private BaseMongoDb<User, string> BaseMongoDbUser;
        private IMongoCollection<UnitRole> _collection;
        private IMongoCollection<User> _collectionUser;
        private IDonViService _donViService;

        public UnitRoleService( DataContext context,
            IHttpContextAccessor contextAccessor,
            IDonViService donViService
            )
            : base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<UnitRole, string>(_context.UNITROLE);
            BaseMongoDbUser = new BaseMongoDb<User, string>(_context.USERS);
            _collection = context.UNITROLE;
            _collectionUser = context.USERS;
            _donViService = donViService;
        }

        public async Task<UnitRole> Create(UnitRole model)
        {
            if (model == default)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.ERROR_STRUCTURE);
            }
            var checkName = _context.UNITROLE.Find(x => x.Ten == model.Ten && x.IsDeleted != true).FirstOrDefault();

            if (checkName != default)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.DATA_EXISTED);
            }
        
            
            var entity = new UnitRole
            {
                Id = BsonObjectId.GenerateNewId().ToString(),
                Ten = model.Ten,
                Code = model.Code,
                MoTa = model.MoTa,
                ThuTu = model.ThuTu,
                DonVis = model.DonVis,
                // CreatedBy = CurrentUserName,
                // ModifiedBy = CurrentUserName,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };
            var result = await BaseMongoDb.CreateAsync(entity);
            if (result.Entity.Id == default || !result.Success)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.CREATE_FAILURE);
            }
            return entity;
        }
        public async Task<UnitRole> Update(UnitRole model)
        {
            if (model == default)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.ERROR_STRUCTURE);
            }

            var entity = _context.UNITROLE.Find(x => x.Id == model.Id).FirstOrDefault();
            if (entity == default)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.DATA_NOT_FOUND);
            }

            var checkName = _context.UNITROLE.Find(x => x.Id != model.Id
                                                       && x.Ten.ToLower() == model.Ten.ToLower()
                                                       && x.IsDeleted != true
            ).FirstOrDefault();
            if (checkName != default)
            {
                throw new ResponseMessageException().WithCode(DefaultCode.DATA_EXISTED);
            }

            entity.Ten = model.Ten;
            entity.MoTa = model.MoTa;
            entity.Code = model.Code;
            entity.ThuTu = model.ThuTu;
            // entity.DonVis = model.DonVis;
            entity.ModifiedAt = DateTime.Now;
            // entity.ModifiedBy = CurrentUserName;

            var result = await BaseMongoDb.UpdateAsync(entity);
            if (!result.Success)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.UPDATE_FAILURE);
            }

            return entity;
        }

        public async Task Delete(string id)
        {
            if (id == default)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.ERROR_STRUCTURE);
            }


            var entity = _context.UNITROLE.Find(x => x.Id == id && x.IsDeleted != true).FirstOrDefault();
            if (entity == default)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.DATA_NOT_FOUND);
            }

            entity.ModifiedAt = DateTime.Now;
            // entity.ModifiedBy = CurrentUserName;
            entity.IsDeleted = true;
            var result = await BaseMongoDb.DeleteAsync(entity);

            if (!result.Success)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.DELETE_FAILURE);
            }
        }

        public async Task<List<UnitRole>> Get()
        {
            return await _context.UNITROLE.Find(x => x.IsDeleted != true).SortByDescending(x => x.ThuTu)
                .ToListAsync();
        }

        public async Task<UnitRole> GetById(string id)
        {
            return await _context.UNITROLE.Find(x => x.Id == id && x.IsDeleted != true)
                .FirstOrDefaultAsync();
        }
        
        
    }
}