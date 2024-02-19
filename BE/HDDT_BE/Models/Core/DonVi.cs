using MongoDB.Bson.Serialization.Attributes;

namespace HDDT_BE.Models.Core
{
    public class DonVi : Audit, TEntity<string>
    {
        public string MaDonVi { get; set; }
        public string Ten { get; set; }
        public string DonViCha { get; set; }
        public bool IsShow { get; set; }
    }

    public class DonViShort 
    {
        public string Id { get; set; }
        public string Ten { get; set; } 
        public string TenKhongDau { get; set; }
        public  string TenMobi { get; set;  }

        [BsonIgnore]
        public string linhVucId { get; set; }
        [BsonIgnore]
        public  string Role { get; set; }


        public DonViShort()
        {
        }
        public DonViShort(string id , string ten)
        {
            this.Id = id;
            this.Ten = ten;
        }
    }

    public class DonViShow : DonViShort
    {
        public DonViShow(DonViLinhVuc item) : base()
        {
            this.Id = item.Id;
            this.Ten = item.Ten;
            this.linhVucId = item.linhVucId;
        }
    }
    
    
    
    public class DonViLinhVuc
    {
        public string Id { get; set; }
        public string Ten { get; set; }
        [BsonIgnore]
        public string linhVucId { get; set; }
        
        public  string RoleId { get; set; }
        
        public  string RoleName { get; set; }
    }

    
    public class DonViQuanLy : DonViShort
    {
        public bool IsParent { get; set; }

        //    public List<CoQuanShort> list { get; set; } = new List<CoQuanShort>();
        public string DuAnId { get; set; }


    }


    public class DonViTree : DonViShort
    {
        public string id { get; set; }
        public string label
        {
            get;
            set;
        }
        public List<DonViTree>children { get; set; }
    }

    public class DonViParams
    {
        public  string DuAnId { get; set; }
        public  List<DonViTree> DonViTree { get; set; }
    }
}