
using HDDT_BE.Models.Core;
using HDDT_BE.Models.Professional;
using MongoDB.Driver;

namespace HDDT_BE.Constants;

public class Projection
{
    
        public static ProjectionDefinition<CommonModel> Projection_BasicCommon = Builders<CommonModel>.Projection
            .Include("_id")
            .Include("Code") 
            .Include("Name");
        
        
        
        public static ProjectionDefinition<User> Projection_User = Builders<User>.Projection
            .Include("_id")
            .Include("UserName")
            .Include("FullName");

        
        public static ProjectionDefinition<ThuocModel> Projection_THUOC = 
            Builders<ThuocModel>.Projection
                .Exclude(x=>x.IsDeleted)
                .Exclude(x=>x.CreatedAt) 
                .Exclude(x=>x.Sort)
                .Exclude(x=>x.CreatedBy) 
                .Exclude(x=>x.ModifiedAt)
                .Exclude(x=>x.ModifiedBy);
        
        
        public static ProjectionDefinition<LoaiDichVuYTeModel> Projection_LoaiDichVuYTe = 
            Builders<LoaiDichVuYTeModel>.Projection
            .Exclude(x=>x.IsDeleted)
            .Exclude(x=>x.CreatedAt) 
            .Exclude(x=>x.Sort)
            .Exclude(x=>x.CreatedBy) 
            .Exclude(x=>x.ModifiedAt)
            .Exclude(x=>x.ModifiedBy);
        
        public static ProjectionDefinition<ThongTinHangHoaModel> Projection_THONG_TIN_HANG_HOA = 
            Builders<ThongTinHangHoaModel>.Projection
                .Exclude(x=>x.Id)
                .Exclude(x=>x.IsDeleted)
                .Exclude(x=>x.CreatedAt)
                .Exclude(x=>x.CreatedBy) 
                .Exclude(x=>x.ModifiedAt)
                .Exclude(x=>x.ModifiedBy);
        
        
        public static ProjectionDefinition<SellerInfoModel> Projection_THONG_TIN_NGUOI_BAN = 
            Builders<SellerInfoModel>.Projection
                .Exclude(x=>x.Id)
                .Exclude(x=>x.Name)
                .Exclude(x=>x.UnsignedName)
                .Exclude(x=>x.IsDeleted)
                .Exclude(x=>x.CreatedAt)
                .Exclude(x=>x.CreatedBy) 
                .Exclude(x=>x.ModifiedAt)
                .Exclude(x=>x.ModifiedBy);
        
        
        public static ProjectionDefinition<ThongTinPhatHanhModel> Projection_THONG_TIN_PHAT_HANH = 
            Builders<ThongTinPhatHanhModel>.Projection
                .Exclude(x=>x.Id)
                .Exclude(x=>x.UnsignedName)
                .Exclude(x=>x.Name)
                .Exclude(x=>x.IsDeleted)
                .Exclude(x=>x.CreatedAt)
                .Exclude(x=>x.CreatedBy) 
                .Exclude(x=>x.ModifiedAt)
                .Exclude(x=>x.ModifiedBy);
    
}