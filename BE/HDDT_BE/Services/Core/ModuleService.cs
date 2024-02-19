
using CoreApi.Extensions;
using HDDT_BE.Exceptions;
using HDDT_BE.Helpers;
using HDDT_BE.Installers;
using HDDT_BE.Interfaces.Core;
using HDDT_BE.Models.Auth;
using HDDT_BE.Models.Core;
using HDDT_BE.Models.PagingParam;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HDDT_BE.Services.Core
{
    public class ModuleService : BaseService, IModuleService
    {
        private DataContext _context;
        private BaseMongoDb<Module, string> BaseMongoDb;

        IMongoCollection<Module> _collection;


        public ModuleService(
            DataContext context,
            IHttpContextAccessor contextAccessor) :
            base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<Module, string>(_context.Modules);

            _collection = context.Modules;
        }

        public async Task<Module> Create(Module model)
        {
            if (model == default)
                throw new ResponseMessageException().WithCode(DefaultCode.ERROR_STRUCTURE);

            var checkName = _context.Modules.Find(x => x.Name == model.Name && x.IsDeleted != true).FirstOrDefault();

            if (checkName != default)
                throw new ResponseMessageException().WithCode(DefaultCode.DATA_EXISTED);

            var entity = new Module
            {
                Name = model.Name,
                Sort = model.Sort,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            var result = await BaseMongoDb.CreateAsync(entity);

            if (result.Entity.Id == default || !result.Success)
                throw new ResponseMessageException().WithCode(DefaultCode.CREATE_FAILURE);

            return entity;
        }

        public async Task<Module> Update(Module model)
        {
            if (model == default)
                throw new ResponseMessageException().WithCode(DefaultCode.ERROR_STRUCTURE);

            var entity = _context.Modules.Find(x => x.Id == model.Id).FirstOrDefault();
            if (entity == default)
                throw new ResponseMessageException().WithCode(DefaultCode.DATA_NOT_FOUND);
            var checkName = _context.Modules.Find(x => x.Id != model.Id && x.Name == model.Name && x.IsDeleted != true)
                .FirstOrDefault();
            if (checkName != default)
                throw new ResponseMessageException().WithCode(DefaultCode.DATA_EXISTED);
            entity.Name = model.Name;
            entity.Sort = model.Sort;
            entity.ModifiedAt = DateTime.Now;
            // entity.ModifiedBy = CurrentUserName;
            entity.ModifiedAt = DateTime.Now;
            var result = await BaseMongoDb.UpdateAsync(entity);
            if (!result.Success)
                throw new ResponseMessageException().WithCode(DefaultCode.UPDATE_FAILURE);
            return entity;
        }

        public async Task<Module> AddPermissionToModule(Permission model)
        {
            if (model == default)
                throw new ResponseMessageException().WithCode(DefaultCode.ERROR_STRUCTURE);
            var entity = _context.Modules.Find(x => x.Id == model.IdModule).FirstOrDefault();
            if (entity == default)
                throw new ResponseMessageException().WithCode(DefaultCode.DATA_NOT_FOUND);
            if (entity.Permissions == default)
            {
                var permission = new Permission()
                {
                    Id = ObjectId.GenerateNewId().ToString(),
                    Name = model.Name,
                    Action = model.Action,
                    Sort = model.Sort,
                    IsView = model.IsView,
                    IdModule = model.IdModule
                };
                entity.Permissions = new List<Permission>()
                {
                    permission
                };
            }
            else
            {
                var checkPermission = entity.Permissions.FindIndex(x => x.Id == model.Id);
                if (checkPermission == -1)
                {
                    var permission = new Permission()
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        Name = model.Name,
                        Action = model.Action,
                        Sort = model.Sort,
                        IsView = model.IsView,
                        IdModule = model.IdModule
                    };
                    entity.Permissions.Add(permission);
                }
                else
                {
                    entity.Permissions[checkPermission].Action = model.Action;
                    entity.Permissions[checkPermission].Name = model.Name;
                    entity.Permissions[checkPermission].Sort = model.Sort;
                    entity.Permissions[checkPermission].IsView = model.IsView;
                }
            }

            entity.ModifiedAt = DateTime.Now;

            var result = await BaseMongoDb.UpdateAsync(entity);
            if (!result.Success)
                throw new ResponseMessageException().WithCode(DefaultCode.UPDATE_FAILURE);
            return entity;
        }
        
        public async Task<Permission> GetPermissionById(Permission model)
        {
            if (model.Id == null && model.IdModule == null)
            {
                throw new ResponseMessageException().WithCode(DefaultCode.ERROR_STRUCTURE);
            }
            var entity = _context.Modules.Find(x => x.Id == model.IdModule).FirstOrDefault();
            if (entity == default)
                throw new ResponseMessageException().WithCode(DefaultCode.DATA_NOT_FOUND);
            var permission = entity.Permissions.Where(x => x.Id == model.Id).FirstOrDefault();
            if (permission == default)
            {
                throw new ResponseMessageException().WithCode(DefaultCode.DATA_NOT_FOUND);
            }
            return  permission;
        }
        
        public async Task DeletePermission(Permission model)
        {
            if (model.IdModule == default)
                throw new ResponseMessageException().WithCode(DefaultCode.ERROR_STRUCTURE)
                    .WithMessage(DefaultMessage.DATA_NOT_EMPTY);
            var entity = _context.Modules.Find(x => x.Id == model.IdModule && x.IsDeleted != true).FirstOrDefault();
            if (entity == default)
                throw new ResponseMessageException().WithCode(DefaultCode.DATA_NOT_FOUND);

            var index = entity?.Permissions.FindIndex(x => x.Id == model.Id);
            if (index.HasValue)
            {
                if (index.Value != -1)
                    entity.Permissions.RemoveAt(index.Value);
            }

            entity.ModifiedAt = DateTime.Now;

            var result = await BaseMongoDb.DeleteAsync(entity);

            if (!result.Success)
                throw new ResponseMessageException().WithCode(DefaultCode.DELETE_FAILURE);
        }

        public async Task<PagingModel<Module>> GetPaging(PagingParam param)
        {
            PagingModel<Module> result = new PagingModel<Module>();
            var builder = Builders<Module>.Filter;
            var filter = builder.Empty;

            filter = builder.And(filter, builder.Where(x => x.IsDeleted == false));
            if (!String.IsNullOrEmpty(param.Content))
            {
                filter = builder.And(filter,
                    builder.Where(x => x.Name.Trim().ToLower().Contains(param.Content.Trim().ToLower())));
            }

            string sortBy = nameof(Module.Sort);
            result.TotalRows = await _collection.CountDocumentsAsync(filter);
            result.Data = await _collection.Find(filter)
                .Sort(param.SortDesc
                ? Builders<Module>
                .Sort.Ascending(sortBy)    
                : Builders<Module>
                        .Sort.Descending(sortBy))
                .Skip(param.Skip)
                .Limit(param.Limit)
                .ToListAsync();
            return result;
        }

        public async Task Delete(string id)
        {
            if (id == default)
                throw new ResponseMessageException().WithCode(DefaultCode.ERROR_STRUCTURE);
            var entity = _context.Modules.Find(x => x.Id == id && x.IsDeleted != true).FirstOrDefault();
            if (entity == default)
                throw new ResponseMessageException().WithCode(DefaultCode.DATA_NOT_FOUND);

            entity.ModifiedAt = DateTime.Now;
            entity.IsDeleted = true;

            var result = await BaseMongoDb.DeleteAsync(entity);

            if (!result.Success)
                throw new ResponseMessageException().WithCode(DefaultCode.DELETE_FAILURE);
        }

        public async Task<IEnumerable<Module>> Get()
        {
            return await _context.Modules.Find(x => x.IsDeleted != true).ToListAsync();
        }

        public async Task<Module> GetById(string id)
        {
            return await _context.Modules.Find(x => x.Id == id && x.IsDeleted != true).FirstOrDefaultAsync();
        }
        
    }
}