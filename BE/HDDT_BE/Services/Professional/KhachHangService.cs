using CoreApi.Extensions;
using HDDT_BE.Exceptions;
using HDDT_BE.Helpers;
using HDDT_BE.Installers;
using HDDT_BE.Interfaces.Professional;
using HDDT_BE.Models.Professional;
using HDDT_BE.Services.Core;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HDDT_BE.Services.Professional
{
    public class KhachHangService : BaseService, IKhachHangService
    {
        private DataContext _context;
        private BaseMongoDb<KhachHangModel, string> BaseMongoDb;
        public KhachHangService (
            DataContext context,
            IHttpContextAccessor contextAccessor) :
            base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<KhachHangModel, string>(_context.KHACHHANG);
            
        }
        
        public async Task<dynamic> Create(KhachHangModel khachHang)
        {
            if (khachHang == default)
                throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);

            if (khachHang.MaSoThue == null || khachHang.TenKhachHang == null || khachHang.DiaChi == null)
            {
                throw new ResponseMessageException().WithCode(DefaultCode.ERROR_STRUCTURE).WithMessage("Tên khách hàng, mã số thuế và địa chỉ không được bỏ trống");
            }
            var model = await _context.KHACHHANG.Find(x => x.MaSoThue ==  khachHang.MaSoThue && !x.IsDeleted)
                .FirstOrDefaultAsync();
            if (model != null)
            {
                model.ModifiedBy = CurrentUser.FullName; 
                model.ModifiedAt = DateTime.Now;
                model.DiaChi = khachHang.DiaChi;
                model.TaiKhoanNganHang = khachHang.TaiKhoanNganHang;
                model.TenKhachHang = khachHang.TenKhachHang;
                model.TenDonVi = khachHang.TenDonVi;
                var resultUpdate = await BaseMongoDb.UpdateAsync(model);
                if (resultUpdate.Entity.Id == default || !resultUpdate.Success)
                    throw new ResponseMessageException().WithException(DefaultCode.UPDATE_FAILURE);
            }
            else
            {
                if (CurrentUser != null)
                {
                    khachHang.CreatedBy = CurrentUser.FullName; 
                }
                var result = await BaseMongoDb.CreateAsync( khachHang);
                if (result.Entity.Id == default || !result.Success || result == null)
                    throw new ResponseMessageException().WithException(DefaultCode.CREATE_FAILURE);
            }
            return  khachHang;
        }

        public async Task<dynamic> Update(KhachHangModel model)
        {
            if (model == default)
                throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
   
            var entity = _context.KHACHHANG.Find(x => !x.IsDeleted  && x.Id ==  model.Id ).FirstOrDefault();
            if (entity == null)
                throw new ResponseMessageException().WithException(DefaultCode.DATA_NOT_FOUND);
        
            var result = await BaseMongoDb.UpdateAsync(model);
            if (result.Entity.Id == default || !result.Success)
                throw new ResponseMessageException().WithException(DefaultCode.UPDATE_FAILURE);
            return entity;
        }
    }
}