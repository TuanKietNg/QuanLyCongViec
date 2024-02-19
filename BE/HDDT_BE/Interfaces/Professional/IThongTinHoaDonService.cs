using HDDT_BE.Models.Professional;

namespace HDDT_BE.Interfaces.Professional
{
    public interface IThongTinHoaDonService
    {
        Task<dynamic> Create(ThongTinHangHoaModel model);
        
        
        Task Delete(ThongTinHangHoaModel model);
    }
}