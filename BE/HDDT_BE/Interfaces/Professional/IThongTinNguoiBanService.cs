using HDDT_BE.Models.Professional;

namespace HDDT_BE.Interfaces.Professional
{
    public interface IThongTinNguoiBanService
    {
        Task<dynamic> Create(SellerInfoModel model);
        
        
        
        
    }
}