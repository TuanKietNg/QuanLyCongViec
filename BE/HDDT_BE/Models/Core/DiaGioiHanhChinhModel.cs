namespace HDDT_BE.Models.Core
{
    public class DiaGioiHanhChinhModel : Audit,TEntity<string>
    {
        public  string IdDonViCha { get ; set;  }
        
        public  string Code { get; set; }
        

        public int Sort { get; set; }
        public int Level { get; set; }
        
        public string OldId { get; set; }
    }

    public class DiaGioiHanhChinhShortModel 
    {
        public string Id { get; set; }
        public  string Code { get; set; }
        public string Name { get; set; }
        
        public string OldId { get; set; }
        
        
        public string? IdDonViCha { get; set; }
        
        


        // public DiaGioiHanhChinhShortModel(string id, string code, string name)
        // {
        //     this.Id = id;
        //     this.Code = code;
        //     this.Name = name;
        // }
        
        public DiaGioiHanhChinhShortModel(string id, string code, string name, string oldId, string? idDonViCha)
        {
            this.Id = id;
            this.Code = code;
            this.Name = name;
            this.OldId = oldId;
            this.IdDonViCha = idDonViCha;
        }
    }
}
