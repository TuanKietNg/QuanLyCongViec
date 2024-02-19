using HDDT_BE.Models.Core;
using HDDT_BE.Models.PagingParam;
using HDDT_BE.ViewModels;

namespace HDDT_BE.Interfaces.Core
{
    public interface IDonViService
    {
        Task<DonVi> Create(DonVi model);
        Task<DonVi> Update(DonVi model);
        Task Delete(string id);
        Task<IEnumerable<DonVi>> Get();
        Task<DonVi> GetById(string id);
        Task<PagingModel<DonVi>> GetPaging(PagingParam param);
        Task<List<DonViTreeVM>> GetTree();
        
        Task<DonVi> GetDonVisTop();

        List<string> GetListDonViId(string donViId);
        

        
        
    }
}