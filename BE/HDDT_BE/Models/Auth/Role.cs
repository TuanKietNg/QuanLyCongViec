using HDDT_BE.Models.Core;
using MongoDB.Bson.Serialization.Attributes;

namespace HDDT_BE.Models.Auth
{
    public class Role: Audit, TEntity<string>
    {
        public string Code { get; set; }
        public string Ten { get; set; }
        public int ThuTu { get; set; }

        public List<Menu> Menus { get; set; } = new List<Menu>();
        public List<Permission> Permissions { get; set; } = new List<Permission>();


    }
}