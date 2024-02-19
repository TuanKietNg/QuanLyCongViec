using HDDT_BE.Interfaces.Core;
using HDDT_BE.Models.Core;
using MongoDB.Bson.Serialization.Attributes;

namespace HDDT_BE.Models.Professional;

public class LoaiDichVuYTeModel : Audit,  TEntity<string>
{
    public string Code { get; set; } // Code của hệ thống mới của mình 
    
    public int Sort { get; set; }
    
    [BsonIgnore]
    public List<ThuocModelShort> Thuoc { get; set; } = new List<ThuocModelShort>(); 
}

public class LoaiDichVuYTeModelShort
{
    [BsonId]
    [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
    public string Id { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }
    public List<ThuocModelShort> Thuoc { get; set; } = new List<ThuocModelShort>();
}