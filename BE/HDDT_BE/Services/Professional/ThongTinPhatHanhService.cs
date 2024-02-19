using CoreApi.Extensions;
using HDDT_BE.Exceptions;
using HDDT_BE.Helpers;
using HDDT_BE.Installers;
using HDDT_BE.Interfaces.Professional;
using HDDT_BE.Models.Professional;
using HDDT_BE.Services.Core;
using HDDT_BE.ViewModels;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HDDT_BE.Services.Professional
{
    public class ThongTinPhatHanhService : BaseService, IThongTinPhatHanhService
    {
        private DataContext _context;
        private BaseMongoDb<ThongTinPhatHanhModel, string> BaseMongoDb;
        public ThongTinPhatHanhService (
            DataContext context,
            IHttpContextAccessor contextAccessor) :
            base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<ThongTinPhatHanhModel, string>(_context.THONGTINPHATHANH);
        }
        public async Task<dynamic> Create(ThongTinPhatHanhModel model)
        {
            if (model == default)
                throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
   
            var entity = _context.THONGTINPHATHANH.Find(x => !x.IsDeleted ).FirstOrDefault();
            
            var thongTin= new ThongTinPhatHanhModel() 
            { 
                 adjustmentType = model.adjustmentType,
                 currencyCode = model.currencyCode,
                 invoiceSeries = model.invoiceSeries,
                 invoiceType = model.invoiceType,
                 paymentStatus = model.paymentStatus,
                 templateCode = model.templateCode,
                 cusGetInvoiceRight = model.cusGetInvoiceRight
            };
            ResultBaseMongo<ThongTinPhatHanhModel> result;
            if (entity == null)
            {
                thongTin.Id = BsonObjectId.GenerateNewId().ToString();
                result = await BaseMongoDb.CreateAsync(thongTin);
            }
            else
            {
                thongTin.Id = model.Id;
                result = await BaseMongoDb.UpdateAsync(thongTin);
            }
            if (result.Entity.Id == default || !result.Success)
            {
                throw new ResponseMessageException()
                    .WithException(DefaultCode.CREATE_FAILURE);
            }

            return thongTin;
        }


    }
}