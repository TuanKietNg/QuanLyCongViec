using HDDT_BE.Interfaces.Core;
using HDDT_BE.Models.Core;
using HDDT_BE.Models.PagingParam;
using HDDT_BE.Models.Professional;

namespace HDDT_BE.Interfaces.Professional;

public interface ILoaiDichVuYTeServices
{
   
    Task<dynamic> Create(LoaiDichVuYTeModel model);
    
    Task<dynamic> Update(LoaiDichVuYTeModel model);
    
    
    
    Task<dynamic> GetPaging(PagingParam param);


    Task<dynamic> GetAll();
}