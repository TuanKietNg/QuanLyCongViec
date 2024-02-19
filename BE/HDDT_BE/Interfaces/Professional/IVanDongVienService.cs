using HDDT_BE.Models.Professional;

namespace HDDT_BE.Interfaces.Professional
{
    public interface IVanDongVienService
    {
        Task<dynamic> Create(VanDongVienModel model);
    }
}