using HDDT_BE.Models.PagingParam;

namespace HDDT_BE.Interfaces.Core
{
    public interface IDefaultReposityService<T> where T : class
    {
        Task<dynamic> GetAll();
        Task<dynamic> GetById(string id);
        Task<bool> Delete(string id);
        Task<dynamic> GetPagingCore(PagingParam pagingParam);
    }
}
