using HDDT_BE.Models.Professional;

namespace HDDT_BE.Interfaces.Professional
{
    public interface IKhachHangService
    {
        Task<dynamic> Create(KhachHangModel model);
        
        Task<dynamic> Update(KhachHangModel model);
        
        
        
    }
}