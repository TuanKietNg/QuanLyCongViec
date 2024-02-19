using HDDT_BE.Constants;
using MongoDB.Bson.Serialization.Attributes;

namespace HDDT_BE.Models.Core
{
    public class Audit
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        
        
        public string Name { get; set; }

        public string UnsignedName
        {
            get { return Name != null ? FormatterString.ConvertToTenKhongDau(Name) : ""; }
            set { }
        }
        
        [BsonDateTimeOptions]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        
        [BsonDateTimeOptions]
        public DateTime? ModifiedAt { get; set; } = DateTime.Now;
        
        public string CreatedBy { get; set; }
        
        public string ModifiedBy { get; set; }
        
        [BsonDefaultValue(false)]
        public bool IsDeleted { get; set; }
        [BsonIgnore]
        public string CreatedAtShow
        {
            get { return CreatedAt.HasValue ? CreatedAt.Value.ToLocalTime().ToString(FormatTime.FORMAT_DATE) : ""; }
        }
        
        
        [BsonIgnore]
        public string CreatedAtSubmitShow
        {
            get { return CreatedAt.HasValue ? CreatedAt.Value.ToLocalTime().ToString(FormatTime.FORMAT_SUBMIT_DATE) : ""; }
        }
        [BsonIgnore]
        public string LastModifiedShow
        {
            get { return ModifiedAt.HasValue ? ModifiedAt.Value.ToLocalTime().ToString(FormatTime.FORMAT_DATE) : ""; }
        }
    }
    
}
