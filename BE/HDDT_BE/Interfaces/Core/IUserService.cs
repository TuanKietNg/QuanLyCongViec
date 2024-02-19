using HDDT_BE.Models.Core;
using HDDT_BE.Models.PagingParam;
using HDDT_BE.ViewModels;

namespace HDDT_BE.Interfaces.Core
{
    public interface IUserService
    {
        Task<User> Create(User model);
        Task<User> Update(User model);
        Task Delete(string id);
        Task<IEnumerable<UserShort>> Get();
        Task<User> GetById(string id);
        Task<PagingModel<User>> GetPaging(PagingParam param);
        Task<User> GetByUserName(string userName);  
        Task<User> ChangePassword(UserVM model);
        Task<User> ChangeProfile(User model);
    }
}