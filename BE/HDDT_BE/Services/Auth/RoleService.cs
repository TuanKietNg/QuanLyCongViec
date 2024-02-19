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
using HDDT_BE.ViewModels;
using MongoDB.Driver;

namespace HDDT_BE.Services.Auth
{
    public class RoleService : BaseService, IRoleService
    {
        private DataContext _context;
        private BaseMongoDb<Role, string> BaseMongoDb;
        private IMenuService _menuService;


        public RoleService(
            IMenuService menuService,
            DataContext context,
            IHttpContextAccessor contextAccessor) :
            base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<Role, string>(_context.ROLES);
            _menuService = menuService;
        }

        public async Task<Role> Create(Role model)
        {
            if (model == default)
                throw new ResponseMessageException().WithCode(DefaultCode.ERROR_STRUCTURE);
            var checkName = _context.ROLES.Find(x => x.Ten == model.Ten && x.IsDeleted != true).FirstOrDefault();

            if (checkName != default)
                throw new ResponseMessageException().WithCode(DefaultCode.DATA_NOT_FOUND);

            var entity = new Role
            {
                Ten = model.Ten,
                Code = model.Code,
                ThuTu = model.ThuTu,
                // CreatedBy = CurrentUserName,
                // ModifiedBy = CurrentUserName,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
                // ModifiedBy = CurrentUser.Id,
                // CreatedBy = CurrentUser.Id
            };

            var result = await BaseMongoDb.CreateAsync(entity);
            
            if (result.Entity.Id == default || !result.Success)
                throw new ResponseMessageException().WithCode(DefaultCode.CREATE_FAILURE)
                    .WithMessage(DefaultMessage.CREATE_FAILURE);


            await UpdateRolesInUser(entity);
            return entity;
        }

        public async Task<Role> Update(Role model)
        {
            if (model == default)
                throw new ResponseMessageException().WithCode(DefaultCode.ERROR_STRUCTURE);
            var entity = _context.ROLES.Find(x => x.Id == model.Id).FirstOrDefault();
            if (entity == default)
                throw new ResponseMessageException().WithCode(DefaultCode.DATA_NOT_FOUND);

            var checkName = _context.ROLES.Find(x => x.Id != model.Id && x.Ten == model.Ten && x.IsDeleted != true)
                .FirstOrDefault();

            if (checkName != default)
                throw new ResponseMessageException().WithCode(DefaultCode.DATA_NOT_FOUND);

            entity.Ten = model.Ten;
            entity.Code = model.Code;
            entity.Permissions = model.Permissions;
            // entity.ModifiedBy = CurrentUserName;
            // entity.ModifiedAt = DateTime.Now;
            var result = await BaseMongoDb.UpdateAsync(entity);
            if (!result.Success)
                throw new ResponseMessageException().WithCode(DefaultCode.UPDATE_FAILURE);

            // Cập nhật quyền toàn user.
            await UpdateRolesInUser(entity);
            return entity;
        }

        private async Task UpdateRolesInUser(Role role)
        {
            var filter = Builders<User>.Filter.Where(z => z.Roles.Id  == role.Id);
            var userUseRole = await _context.USERS.Find(filter).ToListAsync();
            if (userUseRole != default)
            {
                foreach (var item in userUseRole)
                {
                    var filterUser = Builders<User>.Filter.Eq(x => x.Id, item.Id);
                        var update = Builders<User>.Update
                            .Set(s => s.Roles, role)
                            .Set(s => s.ModifiedBy, CurrentUser.UserName)
                            .Set(s => s.ModifiedAt, DateTime.Now);
                        UpdateResult actionResult
                            = await _context.USERS.UpdateOneAsync(filterUser, update);
                }
            }
        }

        public async Task Delete(string id)
        {
            if (id == default)
                throw new ResponseMessageException().WithCode(DefaultCode.ERROR_STRUCTURE);
            var entity = _context.ROLES.Find(x => x.Id == id && x.IsDeleted != true).FirstOrDefault();
            if (entity == default)
                throw new ResponseMessageException().WithCode(DefaultCode.DATA_NOT_FOUND);

            entity.ModifiedAt = DateTime.Now;
            entity.IsDeleted = true;

            var result = await BaseMongoDb.DeleteAsync(entity);

            if (!result.Success)
                throw new ResponseMessageException().WithCode(DefaultCode.DELETE_FAILURE);
        }

        public async Task<IEnumerable<Role>> Get()
        {
            return await _context.ROLES.Find(x => x.IsDeleted != true).ToListAsync();
        }

        public async Task<Role> GetById(string id)
        {
            return await _context.ROLES.Find(x => x.Id == id && x.IsDeleted != true).FirstOrDefaultAsync();
        }

        public async Task<PagingModel<Role>> GetPaging(PagingParam param)
        {
            PagingModel<Role> result = new PagingModel<Role>();
            var builder = Builders<Role>.Filter;
            var filter = builder.Empty;
            filter = builder.And(filter, builder.Where(x => x.IsDeleted == false));
            if (!String.IsNullOrEmpty(param.Content))
            {
                filter = builder.And(filter,
                    builder.Where(x => x.Ten.Trim().ToLower().Contains(param.Content.Trim().ToLower())));
            }

            string sortBy = "ThuTu";
            result.TotalRows = await _context.ROLES.CountDocumentsAsync(filter);
            result.Data = await _context.ROLES.Find(filter)
                .Sort(param.SortDesc
                ? Builders<Role>
                .Sort.Ascending(sortBy)    
                : Builders<Role>
                        .Sort.Descending(sortBy))
                .Skip(param.Skip)
                .Limit(param.Limit)
                .ToListAsync();
            return result;
        }

        public async Task<List<NavMenuVM>> GetMenuForUser(string userName)
        {
            var result = new List<NavMenuVM>();
            var user = _context.USERS.Find(x => x.UserName == userName && !x.IsDeleted ).FirstOrDefault();
            if (user != default)
            {
                var permissionsView = user.Roles.Permissions
                    .Where(x => x.Action != null && x.Action.Contains("viewpage"))
                    .Select(p => p.Action)
                    .Distinct()
                    .ToList();
                if (permissionsView.Count > 0)
                {
                    var listMenus = await _context.MENU.Find(x => permissionsView.Contains(x.Action)).ToListAsync();
                    if (listMenus.Count > 0)
                    {
                        result = await _menuService.GetTreeListMenuForCurrentUser(listMenus);
                    }
                }
            }

            return result;
        }

        public async Task<List<string>> GetPermissionForCurrentUer(string userName)
        {
            var currentUser = await _context.USERS.Find(x => x.UserName == userName && x.IsDeleted != true)
                .FirstOrDefaultAsync();
            if (currentUser == null)
                return new List<string>();
            else
            {
                if (currentUser.Roles == null)
                    return new List<string>();
                var permissions = currentUser.Roles
                    .Permissions
                    .Select(x => x.Action)
                    .Distinct()
                    .ToList();
                return permissions;
            }
        }
    }
}