using HDDT_BE.Models.Auth;
using HDDT_BE.Models.Core;
using HDDT_BE.Models.PagingParam;

namespace HDDT_BE.Interfaces.Auth
{
    public interface IUnitRoleService
    {
        Task<UnitRole> Create(UnitRole model);
        Task<UnitRole> Update(UnitRole model);
        Task Delete(string id);
        Task<List<UnitRole>> Get();
        Task<UnitRole> GetById(string id);
        
    }
}