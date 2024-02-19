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

public class LoaiDichVuYTeService :  BaseService , ILoaiDichVuYTeServices
{
       
    private DataContext _context;
    private BaseMongoDb<LoaiDichVuYTeModel, string> BaseMongoDb;
    public LoaiDichVuYTeService  (
        DataContext context,
        IHttpContextAccessor contextAccessor) :
        base(context, contextAccessor)
    {
        _context = context;
        BaseMongoDb = new BaseMongoDb<LoaiDichVuYTeModel, string>(_context.LOAIDICHVUYTE);
            
    }

    public async Task<dynamic> Create(LoaiDichVuYTeModel model)
    {
        
        if (model == default)
        throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
   
        var entity = _context.LOAIDICHVUYTE.Find(x => !x.IsDeleted  && (x.Name == model.Name || x.Code == model.Code) ).FirstOrDefault();
        if (entity != null)
        throw new ResponseMessageException().WithException(DefaultCode.DATA_EXISTED);
        
        var result = await BaseMongoDb.CreateAsync(model);
        if (result.Entity.Id == default || !result.Success)
            throw new ResponseMessageException().WithException(DefaultCode.CREATE_FAILURE);
        return entity;
    }
    public async Task<dynamic> Update(LoaiDichVuYTeModel model)
    {
        
        if (model == default)
            throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);

        if (model.Code == "09" || model.Id == "64c911cb4d7b78d664cae575")
        {
            throw new ResponseMessageException().WithCode(DefaultCode.EXCEPTION).WithMessage("Dịch vụ y tế tiêm ngừa không được chỉnh sửa !");
        }
        var entity =_context.LOAIDICHVUYTE.Find(x => !x.IsDeleted  && x.Id != model.Id && (x.Name == model.Name || x.Code == model.Code)).FirstOrDefault();
        if (entity != null)
            throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
        
        var result = await BaseMongoDb.UpdateAsync(model);
        if (result.Entity.Id == default || !result.Success)
            throw new ResponseMessageException().WithException(DefaultCode.CREATE_FAILURE);
        return entity;
        
    }
    
    public async Task<dynamic> GetAll()
    {
        List<LoaiDichVuYTeModel> listResult = new List<LoaiDichVuYTeModel>();

        var list = _context.LOAIDICHVUYTE.Find(x => !x.IsDeleted && x.Code != "ALL").ToList();
        foreach (var item in list)
        {
            var filter = Builders<ThuocModel>.Filter.Where(x =>  !x.IsDeleted && x.TrangThai.Code != "NGUNGHOATDONG" && x.LoaiDichVuYTe.Id == item.Id && x.Code != "ALL");
            var listThuoc =  _context.THUOC.Aggregate().Match(filter).SortByDescending(x=>x.Sort)
                .Project<ThuocModelShort>(Projection.Projection_THUOC)
                .ToList();
            item.Thuoc.AddRange(listThuoc);
            listResult.Add(item);
        }
        return listResult;
    }
    
    public async Task<dynamic> GetPaging(PagingParam param)
    {
        try
        {
            PagingModel<LoaiDichVuYTeModel> result = new PagingModel<LoaiDichVuYTeModel>();
            var builder = Builders<LoaiDichVuYTeModel>.Filter;
            var filter = builder.Empty;
            filter = builder.And(filter, builder.Where(x=>!x.IsDeleted && x.Code != "ALL"));
            if (!String.IsNullOrEmpty(param.Content))
            {
                filter = builder.And(filter,
                    builder.Where(x =>
                        x.UnsignedName.Trim().Contains(FormatterString.ConvertToTenKhongDau(param.Content))));
            }

            result.TotalRows = await _context.LOAIDICHVUYTE.CountDocumentsAsync(filter);
            result.Data = await _context.LOAIDICHVUYTE.Find(filter)
                .Sort(param.SortDesc
                    ? Builders<LoaiDichVuYTeModel>
                        .Sort.Ascending("CreatedAt")
                    : Builders<LoaiDichVuYTeModel>
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

    
    
}