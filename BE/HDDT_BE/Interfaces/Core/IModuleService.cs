using HDDT_BE.Models.Auth;
using HDDT_BE.Models.Core;
using HDDT_BE.Models.PagingParam;

namespace HDDT_BE.Interfaces.Core
{
    public interface IModuleService
    {
        Task<Module> Create(Module model);
        Task<Module> Update(Module model);
        Task Delete(string id);
        Task<IEnumerable<Module>> Get();
        Task<Module> GetById(string id);
        Task<Module> AddPermissionToModule(Permission model);
        Task<Permission> GetPermissionById(Permission model);
        Task DeletePermission(Permission model);
        
        Task<PagingModel<Module>> GetPaging(PagingParam param);
    }
}