using HDDT_BE.Models.Core;

namespace HDDT_BE.Models.PagingParam
{
    public class HoaDonPaging : PagingParam
    {
        public string StatusCode { get; set; }
        public string HinhThucThanhToan { get; set; }
        public DateTime?  StartDate { get; set; }
        
        public DateTime?  EndDate { get; set; }
        public string ContentFill { get; set; }
        public UserShort UserName { get; set; }
        public string fill { get; set; }
        public string TrangThai { get; set; }
        
    }
    
}