using HDDT_BE.Models.Core;
using MongoDB.Bson.Serialization.Attributes;

namespace HDDT_BE.Models.Auth
{
    public class UnitRole : Audit , TEntity<string>
    {
        public string Ten { get; set; }
        public string Code { get; set; }
        public string MoTa { get; set; }
        public int ThuTu { get; set; }

        public List<DonViShort> DonVis { get; set; } = new List<DonViShort>();
        
        public List<string> DonViIds { get; set; } = new List<string>();
        
        
    }

    public class UnitRoleShort 
    {
        public string Id { get; set; }
        public string Ten { get; set; }
        public List<DonViShort> DonVis { get; set; } = new List<DonViShort>();
    }
}