using MongoDB.Bson.Serialization.Attributes;

namespace HDDT_BE.Models.Core
{
    public class Menu : Audit, TEntity<string>
    {
        public string Ten { get; set; }
        public string Path { get; set; }
        public string Icon { get; set; }
        public bool Active { get; set; }
        public  string Action { get; set; }
        public string ParentId { get; set; }
        public int Level { get; set; }
        public int Sort { get; set; }
        
        [BsonIgnore]
        public bool Selected { get; set; } = false;
    }


    public class MenuShort
    {
        public string Id { get; set; }
        public long SoLuong { get; set; }
    }
}