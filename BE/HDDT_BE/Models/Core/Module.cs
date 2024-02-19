using HDDT_BE.Models.Auth;
using MongoDB.Bson.Serialization.Attributes;

namespace HDDT_BE.Models.Core
{
    public class Module : Audit, TEntity<string>
    {
        public int Sort { get; set; }
        public List<Permission> Permissions { get; set; } = new List<Permission>();
        
        [BsonIgnore]
        public bool Selected { get; set; } = false;
    }
    
    public class ModuleShort {
        public string Id { get; set; }
        public string Name { get; set; }
    }
    
}
