using HDDT_BE.Models.Core;
using MongoDB.Bson.Serialization.Attributes;

namespace HDDT_BE.Models.Auth
{
    public class Permission : Audit, TEntity<string>
    {
        public string Action { get; set; }
        public int Sort { get; set; }
        public bool IsView { get; set; }

        [BsonIgnore]
        public string IdModule { get; set; }
        [BsonIgnore]
        public bool Checked { get; set; }
    }

    public class PermissionShort
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Action { get; set; }
        public int Sort { get; set; }
        public bool IsView { get; set; }
    }
}