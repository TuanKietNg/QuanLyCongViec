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
    public class ThongTinNguoiBanService : BaseService, IThongTinNguoiBanService
    {
        private DataContext _context;
        private BaseMongoDb<SellerInfoModel, string> BaseMongoDb;
        public ThongTinNguoiBanService (
            DataContext context,
            IHttpContextAccessor contextAccessor) :
            base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<SellerInfoModel, string>(_context.THONGTINNGUOIBAN);
            
        }
        
        public async Task<dynamic> Create(SellerInfoModel model)
        {
            if (model == default)
                throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
   
            var entity = _context.THONGTINNGUOIBAN.Find(x => !x.IsDeleted ).FirstOrDefault();
            
            var seller= new SellerInfoModel() 
            { 
                sellerEmail= model.sellerEmail ,
                sellerAddressLine= model.sellerAddressLine,
                sellerBankAccount  = model.sellerBankAccount,
                sellerBankName = model.sellerBankName,
                sellerFaxNumber= model.sellerFaxNumber,
                sellerLegalName = model.sellerLegalName,
                sellerPhoneNumber = model.sellerPhoneNumber,
                sellerTaxCode = model.sellerTaxCode,
                sellerWebsite = model.sellerWebsite,
                sellerCityName = model.sellerCityName,
                sellerCountryCode = model.sellerCountryCode,
                sellerDistrictName = model.sellerDistrictName
            };
            ResultBaseMongo<SellerInfoModel> result;
            if (entity == null)
            {
                seller.Id = BsonObjectId.GenerateNewId().ToString();
                result = await BaseMongoDb.CreateAsync(seller);
            }
            else
            {
                seller.Id = model.Id;
                result = await BaseMongoDb.UpdateAsync(seller);
            }
            if (result.Entity.Id == default || !result.Success)
            {
                throw new ResponseMessageException()
                    .WithException(DefaultCode.CREATE_FAILURE);
            }
            return seller;


        }
        
    }
}