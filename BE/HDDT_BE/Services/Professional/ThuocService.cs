using CoreApi.Extensions;
using HDDT_BE.Constants;
using HDDT_BE.Exceptions;
using HDDT_BE.Helpers;
using HDDT_BE.Installers;
using HDDT_BE.Interfaces.Professional;
using HDDT_BE.Models.Core;
using HDDT_BE.Models.PagingParam;
using HDDT_BE.Models.Professional;
using HDDT_BE.Services.Core;
using MongoDB.Driver;

namespace HDDT_BE.Services.Professional;

public class ThuocService:  BaseService , IThuocService
{
       
    private DataContext _context;
    private BaseMongoDb<ThuocModel, string> BaseMongoDb;
    public ThuocService (
        DataContext context,
        IHttpContextAccessor contextAccessor) :
        base(context, contextAccessor)
    {
        _context = context;
        BaseMongoDb = new BaseMongoDb<ThuocModel, string>(_context.THUOC);
            
    }

    public async Task<dynamic> Create(ThuocModel model)
    {
        if (model == default)
        throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
        var code = await _context.THUOC.CountDocumentsAsync(x=>true);
        model.Code = (2000 + code).ToString();
        if (model.GiaMuaVao > model.GiaLamTron)
        {
            throw new ResponseMessageException().WithCode(DefaultCode.ERROR_STRUCTURE).WithMessage("Giá mua vào không được lớn hơn giá làm tròn ! ");
        }

        if (model.GiaLamTron - model.GiaMuaVao > 10000)
        {
            throw new ResponseMessageException().WithCode(DefaultCode.ERROR_STRUCTURE).WithMessage("Giá làm tròn vượt quá nhiều so với giá mua !");
        }

        if (model.DonGiaBanRa != (model.GiaLamTron + model.PhiTuVan + model.PhiTiem))
        {
            throw new ResponseMessageException().WithCode(DefaultCode.ERROR_STRUCTURE).WithMessage("Đơn giá bán ra phải bằng giá làm tròn cộng với phí tiêm và phí tư vấn  ! ");
        }
        model.CreatedBy = CurrentUser.UserName;
        var entity = _context.THUOC.Find(x => !x.IsDeleted  && x.Code == model.Code).FirstOrDefault();
        if (entity != null)
        throw new ResponseMessageException().WithException(DefaultCode.DATA_EXISTED);
        var result = await BaseMongoDb.CreateAsync(model);
        if (result.Entity.Id == default || !result.Success)
            throw new ResponseMessageException().WithException(DefaultCode.CREATE_FAILURE);
        return entity;
    }

    public Task<dynamic> UpdateUnsignedName()
    {

        var list = _context.USERS.Find(x => !x.IsDeleted).ToList();
        foreach (var item in list)
        {
            var filter = Builders<User>.Filter.Where(x => x.Id ==item.Id);
            var update = Builders<User>.Update
                .Set(x => x.UnsignedFullName, FormatterString.ConvertToTenKhongDau(item.FullName));
            var result = _context.USERS.UpdateOneAsync(filter, update);
        }

        return null;
    }

    public async Task<dynamic> Update(ThuocModel model)
    {
        if (model == default)
            throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
   
        var entity = _context.THUOC.Find(x => !x.IsDeleted  && x.Id == model.Id).FirstOrDefault();
        if (entity == null)
            throw new ResponseMessageException().WithException(DefaultCode.DATA_NOT_FOUND);
        
        if (model.GiaMuaVao > model.GiaLamTron)
        {
            throw new ResponseMessageException().WithCode(DefaultCode.ERROR_STRUCTURE).WithMessage("Giá mua vào không được lớn hơn giá làm tròn ! ");
        }

        if (model.GiaLamTron - model.GiaMuaVao > 10000)
        {
            throw new ResponseMessageException().WithCode(DefaultCode.ERROR_STRUCTURE).WithMessage("Giá làm tròn vượt quá nhiều so với giá mua !");
        }
        
        if (model.DonGiaBanRa != (model.GiaLamTron + model.PhiTuVan + model.PhiTiem))
        {
            throw new ResponseMessageException().WithCode(DefaultCode.ERROR_STRUCTURE).WithMessage("Đơn giá bán ra phải bằng giá làm tròn cộng với phí tiêm và phí tư vấn  ! ");
        }
        var entityCode = _context.THUOC.Find(x => !x.IsDeleted  && x.Code == model.Code && x.Id != model.Id).FirstOrDefault();
        if (entityCode != null)
            throw new ResponseMessageException().WithException(DefaultCode.DATA_EXISTED);

        model.DuocGiamGia = entity.DuocGiamGia;
        model.SoTienGiamLanHai = entity.SoTienGiamLanHai;
        model.ModifiedBy = CurrentUser.UserName;
        var result = await BaseMongoDb.UpdateAsync(model);
        if (result.Entity.Id == default || !result.Success)
            throw new ResponseMessageException().WithException(DefaultCode.CREATE_FAILURE);
        return entity;
    }

    public async Task<dynamic> GetSum(string Id)
    {
        var entity = _context.THUOC.Find(x => !x.IsDeleted  && x.Id == Id).FirstOrDefault();
        if (entity == null)
            throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);

        var count = entity.DonGia * 2 / 100 + 2.000;


        return count;


    }

    public async Task<dynamic> GetPaging(PagingParam param)
    {
        try
            {
                PagingModel<ThuocModel> result = new PagingModel<ThuocModel>();
                var builder = Builders<ThuocModel>.Filter;
                var filter = builder.Empty;
                filter = builder.And(filter, builder.Where(x=>!x.IsDeleted && x.Code != "ALL"));
                if (!String.IsNullOrEmpty(param.Content))
                {
                    filter = builder.And(filter,
                        builder.Where(x =>
                            x.UnsignedName.Trim().Contains(FormatterString.ConvertToTenKhongDau(param.Content))));
                }

                result.TotalRows = await _context.THUOC.CountDocumentsAsync(filter);
                result.Data = await _context.THUOC.Find(filter)
                    .Sort(param.SortDesc
                        ? Builders<ThuocModel>
                            .Sort.Ascending("CreatedAt")
                        : Builders<ThuocModel>
                            .Sort.Descending("CreatedAt")
                    )
                    .Skip(param.Skip)
                    .Limit(param.Limit)
                    .ToListAsync();
                return result;
            }
            catch (ResponseMessageException e)
            {
                new ResultMessageResponse().WithCode(e.ResultCode)
                    .WithMessage(e.ResultString);
            }
            return null;
            
    }
    
    
    public async Task<dynamic> StandardData()
    {
        var list = _context.THUOC.Find(x => !x.IsDeleted).ToList();
        foreach (var item in list)
        {
            var listShort = new List<CommonModelShort>();
            var filter = Builders<ThuocModel>.Filter.Where(x => x.Id ==item.Id);
            // foreach (var itemThuoc in item.DonViTinhs)
            // {
            //     var donViTinh = _context.GetCategoryCollectionAs("DM_DONVITINH")
            //         .Find(x=>!x.IsDeleted && x.Id == itemThuoc.Id ).FirstOrDefault();
            //     listShort.Add(new CommonModelShort(donViTinh.Id , donViTinh.Code , donViTinh.Name));
            // }
            var update = Builders<ThuocModel>.Update
                .Set(x => x.DonViTinhs, listShort);
            var result = _context.THUOC.UpdateOneAsync(filter, update);
        }
        return list; 
    }

    public async Task<dynamic> GetThuocByLoaiDichVuYTe(string code)
    {
        if (code.Equals("ALL"))
            return  _context.THUOC.AsQueryable().Where(x => !x.IsDeleted)
                .Select(x=> new {code = x.Code, id = x.Id,name =  x.Name }).ToList();
        
        
        
        return  _context.THUOC.AsQueryable().Where(x => !x.IsDeleted && x.LoaiDichVuYTe != null && x.LoaiDichVuYTe.Code == code)
            .Select(x=> new {code = x.Code, id = x.Id,name =  x.Name }).ToList();
    }
}