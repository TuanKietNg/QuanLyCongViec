using HDDT_BE.Constants;
using HDDT_BE.Models.Auth;
using HDDT_BE.Models.Core;
using HDDT_BE.Models.Professional;
using MongoDB.Driver;

namespace HDDT_BE.Installers
{
    public class DataContext
    {
        private readonly IMongoClient _mongoClient = null;
        private readonly IMongoDatabase _context = null;
        private readonly IMongoCollection<CommonModel> _common;
        private readonly IMongoCollection<VanDongVienModel> _vanDongVien;
        
        private readonly IMongoCollection<User> _users;
        
        private readonly IMongoCollection<UnitRole> _unitRole;
        
        private readonly IMongoCollection<Module> _modules;

        private readonly IMongoCollection<Role> _roles;
        
        private readonly IMongoCollection<Menu> _menu;
            
        private readonly IMongoCollection<DonVi> _donVi;
        
     
        
        private readonly IMongoCollection<ThongTinHangHoaModel> _thongTinHoaDon;
                
        private readonly IMongoCollection<SellerInfoModel> _thongTinNguoiBan;
        
        private readonly IMongoCollection<LoggingModel> _logging;
        
        private readonly IMongoCollection<LoaiDichVuYTeModel> _loaiDichVuYTe;
        private readonly IMongoCollection<ThongTinPhatHanhModel> _thongTinPhatHanh;
        
        private readonly IMongoCollection<ThuocModel> _thuoc;
        
        private readonly IMongoCollection<KhachHangModel> _khachHang;

        
        private readonly IMongoCollection<RefreshToken> _refreshToken;
        
        private readonly IMongoCollection<DiaGioiHanhChinhModel> _diaGioiHanhChinh;

        private readonly Dictionary<string,  IMongoCollection<CommonModel>> _listCommonCollection;
        public DataContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.
               GetValue<string>(ConfigurationDb.MONGO_CONNECTION_STRING));
            if (client != null)
            {
                _context = client.GetDatabase(configuration.GetValue<string>(ConfigurationDb.MONGO_DATABASE_NAME));
                _vanDongVien = _context.GetCollection<VanDongVienModel>(DefaultNameCollection.VANDONGVIEN);
                
               
                _thongTinHoaDon = _context.GetCollection<ThongTinHangHoaModel>(DefaultNameCollection.THONGTINHOADON);
               _khachHang= _context.GetCollection<KhachHangModel>(DefaultNameCollection.KHACHHANG);
                _loaiDichVuYTe = _context.GetCollection<LoaiDichVuYTeModel>(DefaultNameCollection.LOAIDICHVUYTE);
                _modules = _context.GetCollection<Module>(DefaultNameCollection.MODULE);
                _thuoc = _context.GetCollection<ThuocModel>(DefaultNameCollection.THUOC);
                
                _thongTinPhatHanh = _context.GetCollection<ThongTinPhatHanhModel>(DefaultNameCollection.THONGTINPHATHANH);
                
                
                _logging = _context.GetCollection<LoggingModel>(DefaultNameCollection.LOGGING);
                
                _refreshToken = _context.GetCollection<RefreshToken>(DefaultNameCollection.REFRESHTOKEN);                
                _menu = _context.GetCollection<Menu>(DefaultNameCollection.MENU);
                _donVi = _context.GetCollection<DonVi>(DefaultNameCollection.DONVI);
                
                
                _donVi = _context.GetCollection<DonVi>(DefaultNameCollection.DONVI);
                                
                _roles = _context.GetCollection<Role>(DefaultNameCollection.ROLES);
                _unitRole = _context.GetCollection<UnitRole>(DefaultNameCollection.UNITROLE);
                                
                _users = _context.GetCollection<User>(DefaultNameCollection.USERS);
                _thongTinNguoiBan = _context.GetCollection<SellerInfoModel>(DefaultNameCollection.THONGTINNGUOIBAN);
                
                
                _diaGioiHanhChinh = _context.GetCollection<DiaGioiHanhChinhModel>(DefaultNameCollection.DIAGIOIHANHCHINH);
                _listCommonCollection = new Dictionary<string,  IMongoCollection<CommonModel>>();
                foreach ( string i in ListCommon.listCommon)
                {
                    IMongoCollection<CommonModel> colection = Database.GetCollection<CommonModel>(i);
                    _listCommonCollection.Add(i, colection);
                }
            }
        }

        public IMongoDatabase Database
        {
            get { return _context; }
        }
        public IMongoClient Client
        {
            get { return _mongoClient; }
        }
        
        
        
        public IMongoCollection<RefreshToken> REFRESHTOKEN { get => _refreshToken; }
        
        
        public IMongoCollection<DiaGioiHanhChinhModel> DIAGIOIHANHCHINH { get => _diaGioiHanhChinh; }
        
        public IMongoCollection<ThuocModel> THUOC { get => _thuoc; }

        public IMongoCollection<LoaiDichVuYTeModel> LOAIDICHVUYTE { get => _loaiDichVuYTe; }
        
        
        
        public IMongoCollection<ThongTinHangHoaModel> THONGTINHOADON { get => _thongTinHoaDon; }
        
        
        
        public IMongoCollection<KhachHangModel> KHACHHANG { get => _khachHang; }
        
        public IMongoCollection<SellerInfoModel> THONGTINNGUOIBAN { get => _thongTinNguoiBan; }
        
        public IMongoCollection<ThongTinPhatHanhModel> THONGTINPHATHANH { get => _thongTinPhatHanh; }
        
        public IMongoCollection<Role> ROLES { get => _roles; }
        
        
        public IMongoCollection<LoggingModel> LOGGING { get => _logging; }
        
        
        public IMongoCollection<UnitRole> UNITROLE { get => _unitRole; }
        
        
        public IMongoCollection<Module> Modules { get => _modules; }
        
        
        public IMongoCollection<User> USERS { get => _users; }
        
        public IMongoCollection<DonVi> DONVI { get => _donVi; }
        public IMongoCollection<Menu> MENU { get => _menu; }


        private Dictionary<string,  IMongoCollection<CommonModel>> CommonCollection { get => _listCommonCollection; }
        public  IMongoCollection<CommonModel> GetCategoryCollectionAs(string collectionName)
        {
            return  CommonCollection[collectionName];
        }
    }


}