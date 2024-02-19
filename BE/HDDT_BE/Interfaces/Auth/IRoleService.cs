using HDDT_BE.Models.Auth;
using HDDT_BE.Models.PagingParam;
using HDDT_BE.ViewModels;

namespace HDDT_BE.Interfaces.Auth
{
    public interface IRoleService
    {
        Task<Role> Create(Role model);
        Task<Role> Update(Role model);
        Task Delete(string id);
        Task<IEnumerable<Role>> Get();
        Task<Role> GetById(string id);
        Task<PagingModel<Role>> GetPaging(PagingParam param);
        Task<List<NavMenuVM>> GetMenuForUser(string userName);
        Task<List<string>> GetPermissionForCurrentUer(string userName);
    }
}