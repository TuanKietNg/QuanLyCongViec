using HDDT_BE.Models.Core;

namespace HDDT_BE.Interfaces.Core
{
    public interface IDiaGioiHanhChinhService
    {
        Task<dynamic> GetById(string id);
        

        Task<dynamic> GetListChildByCode(string code);
        
        
        
        Task<dynamic> GetListByLevel(int level);
        
        
    }
}