using HDDT_BE.Interfaces.Core;
using HDDT_BE.Models.Core;
using HDDT_BE.Models.PagingParam;
using HDDT_BE.Models.Professional;

namespace HDDT_BE.Interfaces.Professional;

public interface IThuocService
{
   
    Task<dynamic> Create(ThuocModel model);
    
    
    
    Task<dynamic> UpdateUnsignedName();
    
    
    Task<dynamic> Update(ThuocModel model);
    
    Task<dynamic> GetSum(string Id);
    
    
    Task<dynamic> GetPaging(PagingParam param);
    
        
    Task<dynamic> StandardData();
    
    
    Task<dynamic> GetThuocByLoaiDichVuYTe(string code);
}