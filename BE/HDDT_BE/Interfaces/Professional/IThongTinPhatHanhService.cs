using HDDT_BE.Models.Professional;

namespace HDDT_BE.Interfaces.Professional
{
    public interface IThongTinPhatHanhService
    {
        Task<dynamic> Create(ThongTinPhatHanhModel model);
        
        
    }
}