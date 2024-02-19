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
    public class ThongTinHoaDonService : BaseService, IThongTinHoaDonService
    {
        private DataContext _context;
        private BaseMongoDb<ThongTinHangHoaModel, string> BaseMongoDb;
        public ThongTinHoaDonService (
            DataContext context,
            IHttpContextAccessor contextAccessor) :
            base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<ThongTinHangHoaModel, string>(_context.THONGTINHOADON);
            
        }
        
        public async Task<dynamic> Create(ThongTinHangHoaModel hoaDon)
        {
       
            if (hoaDon== default)
                throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
            hoaDon.FullName = CurrentUser.FullName;
            hoaDon.CreatedBy = CurrentUser.UserName; 
            var result = await BaseMongoDb.CreateAsync(hoaDon);
            if (result.Entity.Id == default || !result.Success || result == null)
                throw new ResponseMessageException().WithException(DefaultCode.CREATE_FAILURE);
            return hoaDon;
        }

        public async Task  Delete(ThongTinHangHoaModel model)
        {
            if (model == default)
            {
                throw new ResponseMessageException()
                    .WithException(DefaultCode.ERROR_STRUCTURE);
            }
            model.IsDeleted = true;
            var result = await BaseMongoDb.DeleteAsync(model);
            if (!result.Success)
            {
                throw new ResponseMessageException()
                    .WithException(DefaultCode.UPDATE_FAILURE);
            }
            
        }
    }
}