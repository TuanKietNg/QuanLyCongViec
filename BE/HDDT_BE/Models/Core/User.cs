using HDDT_BE.Constants;
using HDDT_BE.Models.Auth;
using HDDT_BE.ViewModels;
using MongoDB.Bson.Serialization.Attributes;

namespace HDDT_BE.Models.Core
{
    public class User : Audit, TEntity<string>
    {

        public string UserName { get; set; }
        
        public int Sort { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        
        
        private string _fullName { get; set; }
        
        public string UnsignedFullName
        {
            get { return _fullName != null ? FormatterString.ConvertToTenKhongDau(_fullName) : ""; }
            set { }
        }

        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Note { get; set; }
        public Avatar Avatar { get; set; }
        public DonViShort DonVi { get; set; }
        public Role Roles { get; set; }

        public List<string> DonViIds { get; set; }

        public bool IsAppAuthentication { get; set; } = false;
        public bool IsVerified { get; set; }
        public bool IsActived { get; set; } = true;
        public bool IsSyncPasswordSuccess { get; set; } = true;
        [BsonIgnore]   public bool IsRequireChangePassword { get; set; } = false;
        [BsonIgnore]     public bool IsRequireVerify { get; set; } = false;
        [BsonIgnore] public string Password { get; set; }
        [BsonIgnore] public List<string> Permissions { get; set; }
        [BsonIgnore] public List<NavMenuVM> Menu { get; set; }
        [BsonIgnore] public IEnumerable<Menu> MenuItems { get; set; }
        
        [BsonIgnore] public string NameShow { get; set; }
    }
    
 
    public class UserShort 
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
    
}