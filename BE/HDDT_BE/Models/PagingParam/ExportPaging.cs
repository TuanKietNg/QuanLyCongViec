using HDDT_BE.Constants;
using HDDT_BE.Models.Core;
using MongoDB.Bson.Serialization.Attributes;

namespace HDDT_BE.Models.PagingParam
{
    public class ExportPaging : PagingParam
    {
        
        public DateTime?  StartDate { get; set; }
        
        public DateTime?  EndDate { get; set; }
        
        public CommonModelShort Thuoc { get; set; }
     
        
        public CommonModelShort LoaiDichVu { get; set; }
        
        
        public UserShort User { get; set; }
        public string Fill { get; set; }
        public CommonModelShort TrangThai { get; set; }



    }
    
}