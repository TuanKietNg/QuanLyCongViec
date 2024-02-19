using System.Net.Http.Headers;
using CoreApi.Extensions;
using HDDT_BE.Constants;
using HDDT_BE.Exceptions;
using HDDT_BE.Extensions;
using HDDT_BE.Helpers;
using HDDT_BE.Installers;
using HDDT_BE.Interfaces.Core;
using HDDT_BE.Models.Auth;
using HDDT_BE.Models.Core;
using HDDT_BE.Models.PagingParam;
using HDDT_BE.ViewModels;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace HDDT_BE.Services.Core
{
    public class UserService : BaseService, IUserService
    {
        private DataContext _context;
        private BaseMongoDb<User, string> BaseMongoDb;
        IMongoCollection<User> _collectionUser;
        IMongoCollection<DonVi> _collectionDonVi;
        private IDonViService _donViService;
        IMongoCollection<UnitRole> _collectionUnitRole;
        public UserService(
            DataContext context,
            IDonViService donViService,
            IHttpContextAccessor contextAccessor) :
            base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<User, string>(_context.USERS);
            _donViService = donViService;
            _collectionUser = context.USERS;
            _collectionUnitRole = context.UNITROLE;
            _collectionDonVi = context.DONVI;
        }

        public async Task<IEnumerable<UserShort>> Get()
        {
            return  _context.USERS.Aggregate().Match(x=>!x.IsDeleted).SortByDescending(x=>x.Sort)
                .Project<UserShort>(Projection.Projection_User)
                .ToList();
        }

        public async Task<User> GetById(string id)
        {
          
            var user =    await _context.USERS.Find(x => x.Id == id && x.IsDeleted != true).FirstOrDefaultAsync();
            return user;

        }
        public async Task<PagingModel<User>> GetPaging(PagingParam param)
        {
            PagingModel<User> result = new PagingModel<User>();
            var builder = Builders<User>.Filter;
            var filter = builder.Empty;
            filter = builder.And(filter, builder.Where(x => x.IsDeleted == false && x.UserName != "ALL"));
            if (CurrentUser.Roles.Code != "5555")
            {
                filter = builder.And(filter, builder.Where(x => x.Roles.Code != "5555"));
            }
            if (!String.IsNullOrEmpty(param.Content))
            {
                filter = builder.And(filter,
                    builder.Where(x => x.UserName.Trim().ToLower().Contains(param.Content.Trim().ToLower()) 
                                       || x.UnsignedFullName.Trim().Contains(FormatterString.ConvertToTenKhongDau(param.Content.Trim()))
                          || x.Email.Trim().ToLower().Contains(param.Content.Trim().ToLower())
                          || x.DonVi.Ten.Trim().ToLower().Contains(param.Content.Trim().ToLower())
                    ));
            }

            string sortBy = nameof(User.CreatedAt);
            result.TotalRows = await _context.USERS.CountDocumentsAsync(filter);
            result.Data = await _context.USERS.Find(filter)
                .Sort(param.SortDesc
                ?Builders<User>
                    .Sort.Ascending(sortBy)
                : Builders<User>
                        .Sort.Descending(sortBy)
                     )
                .Skip(param.Skip)
                .Limit(param.Limit)
                .ToListAsync();
            return result;
        }
        
        
        
        
    

        public async Task<User> GetByUserName(string userName)
        {
            return await _context.USERS.Find(x => x.UserName == userName && x.IsDeleted != true).FirstOrDefaultAsync();
        }

        public async Task<User> Create(User model)
        {
            if (model == default)
                throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
            var checkName = _context.USERS.Find(x => x.UserName == model.UserName && x.IsDeleted != true)
                .FirstOrDefault();

            if (checkName != default)
            {
                throw new ResponseMessageException().WithException(DefaultCode.DATA_NOT_FOUND);
            }

            var entity = new User
            {
                UserName = model.UserName,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Note = model.Note,
                Roles = model.Roles,
                IsVerified = model.IsVerified,
                IsSyncPasswordSuccess = model.IsSyncPasswordSuccess,
                IsActived = model.IsActived,
                // CreatedBy = CurrentUserName,
                // ModifiedBy = CurrentUserName,
                CreatedAt = DateTime.Now,
                ModifiedAt = DateTime.Now
            };

            if (model.DonVi != default)
            {
                entity.DonVi = model.DonVi;
                var item = _collectionUnitRole.Find(x => x.DonVis.Any(x => x.Id == model.DonVi.Id)).FirstOrDefault();
                if (item != default)
                {
                    var itemDonVi = _donViService.GetDonVisTop();
                    entity.DonViIds = _donViService.GetListDonViId(itemDonVi.Result.Id);
                }
                else
                {
                    entity.DonViIds = _donViService.GetListDonViId(model.DonVi.Id);
                }

            }

            byte[] passwordHash, passwordSalt;
            if (string.IsNullOrEmpty(model.Password))
            {
                model.Password = "DongThap@123";
            }
            PasswordExtensions.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;

            var result = await BaseMongoDb.CreateAsync(entity);
            if (result.Entity.Id == default || !result.Success)
            {
                throw new ResponseMessageException().WithException(DefaultCode.CREATE_FAILURE);
            }
            return entity;
        }

        public async Task<User> Update(User model)
        {
            if (model == default)
                throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
            var entity = _context.USERS.Find(x => x.Id == model.Id).FirstOrDefault();

            if (entity == default)
                throw new ResponseMessageException().WithException(DefaultCode.DATA_NOT_FOUND);

            var checkName = _context.USERS
                .Find(x => x.Id != model.Id && x.UserName == model.UserName && x.IsDeleted != true).FirstOrDefault();

            if (checkName != default)
                throw new ResponseMessageException().WithCode(DefaultCode.DATA_EXISTED)
                    .WithMessage("Tên tài khoản đã tồn tại");

            entity.UserName = model.UserName;
            entity.FullName = model.FullName;
            entity.PhoneNumber = model.PhoneNumber;
            entity.Email = model.Email;
            entity.Note = model.Note;
            entity.Roles = model.Roles;
            entity.ModifiedAt = DateTime.Now;
            entity.IsVerified = model.IsVerified;
            entity.IsSyncPasswordSuccess = model.IsSyncPasswordSuccess;
            entity.IsActived = model.IsActived;

            if (model.DonVi != default)
            {
                if (model.DonVi.Id != entity.DonVi.Id)
                {
                    // entity.DonVi = model.DonVi;
                    // entity.DonViIds = _donViService.GetListDonViId(model.DonVi.Id);
                    var item = _collectionUnitRole.Find(x => x.DonVis.Any(x => x.Id == model.DonVi.Id)).FirstOrDefault();
                    if (item != default)
                    {
                        entity.DonVi = model.DonVi;
                        var itemDonVi = _donViService.GetDonVisTop();
                        entity.DonViIds = _donViService.GetListDonViId(itemDonVi.Result.Id);
                    }
                    else
                    {
                        entity.DonVi = model.DonVi;
                        entity.DonViIds = _donViService.GetListDonViId(model.DonVi.Id);
                    }
                }
            }
            if (!string.IsNullOrEmpty(model.Password))
            {
                byte[] passwordHash, passwordSalt;
                PasswordExtensions.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
                entity.PasswordHash = passwordHash;
                entity.PasswordSalt = passwordSalt;
            }

            var result = await BaseMongoDb.UpdateAsync(entity);
            if (!result.Success)
            {
                throw new ResponseMessageException().WithException(DefaultCode.UPDATE_FAILURE);
            }
            
            return entity;
        }

        public async Task<User> ChangeProfile(User model)
        {
            if (model == default)
            {
                throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
            }

            var entity = _context.USERS.Find(x => x.Id == model.Id && !x.IsDeleted).FirstOrDefault();
            if (entity == default)
            {
                throw new ResponseMessageException().WithException(DefaultCode.DATA_NOT_FOUND);
            }


            entity.FullName = model.FullName;
            entity.PhoneNumber = model.PhoneNumber;
            entity.Email = model.Email;
            entity.Avatar = model.Avatar;

            var result = await BaseMongoDb.UpdateAsync(entity);
            if (!result.Success)
            {
                throw new ResponseMessageException().WithException(DefaultCode.UPDATE_FAILURE);
            }
            
            return entity;
        }

        public async Task<User> ChangePassword(UserVM model)
        {
            if (model == default)
                throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
            var entity = _context.USERS.Find(x => x.UserName == model.UserName && !x.IsDeleted).FirstOrDefault();
            if (entity == default)
                throw new ResponseMessageException().WithException(DefaultCode.DATA_NOT_FOUND);
            if (!string.IsNullOrEmpty(model.Password))
            {
                byte[] passHash, passSalt;
                passHash = entity.PasswordHash;
                passSalt = entity.PasswordSalt;
                var pass = PasswordExtensions.VerifyPasswordHash(model.Password, passHash, passSalt);
                if (pass != true)
                {
                    throw new ResponseMessageException().WithCode(DefaultCode.UPDATE_FAILURE)
                        .WithMessage("Mật khẩu không chính xác");
                }
                else
                {
                    if (model.newPass == model.confirmPass)
                    {
                        byte[] passwordHash, passwordSalt;
                        PasswordExtensions.CreatePasswordHash(model.newPass, out passwordHash, out passwordSalt);
                        entity.PasswordHash = passwordHash;
                        entity.PasswordSalt = passwordSalt;
                    }
                }
            }

            var result = await BaseMongoDb.UpdateAsync(entity);
            if (!result.Success)
            {
                throw new ResponseMessageException().WithCode(DefaultCode.UPDATE_FAILURE)
                    .WithMessage("Đổi mật khẩu thất bại.");
            }
            
            return entity;
        }

        public async Task Delete(string id)
        {
            if (id == default)
                throw new ResponseMessageException().WithCode(DefaultCode.ERROR_STRUCTURE)
                    .WithMessage(DefaultMessage.DATA_NOT_EMPTY);
            var entity = _context.USERS.Find(x => x.Id == id && x.IsDeleted != true).FirstOrDefault();
            if (entity == default)
                throw new ResponseMessageException().WithCode(DefaultCode.DATA_NOT_FOUND);

            entity.ModifiedAt = DateTime.Now;
            entity.IsDeleted = true;

            var result = await BaseMongoDb.DeleteAsync(entity);

            if (!result.Success)
            {
                throw new ResponseMessageException().WithCode(DefaultCode.DELETE_FAILURE);
            }
            
        }

        public async Task<User> FindUserWithUserNameOrPhoneNumber(string input)
        {
            var builder = Builders<User>.Filter;
            var filter = builder.Where(q => q.UserName.Equals(input) || q.PhoneNumber.Equals(input));

            var query = _context.USERS.Find(filter);
            return await query.FirstOrDefaultAsync();
        }




    }
}
