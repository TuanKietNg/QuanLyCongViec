namespace HDDT_BE.Constants
{
    public static class DefaultNameCollection
    {
        public const string USERS = "Users";
        public const string REFRESHTOKEN = "CR_REFRESHTOKEN";
        
        public const string DONVI = "DonVi";
        
                
        public const string LOGGING = "CR_LOGGING";
        public const string MENU = "Menu";
        
        public const string UNITROLE = "UnitRole";

        
        
        public const string MODULE = "Modules";
        
        public const string THUOC = "DM_THUOC";
        public const string ROLES = "Roles";

        public const string FILES = "CR_FILES";
        
        
        
        
        public const string VANDONGVIEN = "NV_VANDONGVIEN";
        public const string THONGTINNGUOIBAN = "NV_THONGTINNGUOIBAN";
        
        public const string DIAGIOIHANHCHINH = "CR_DIAGIOIHANHCHINH";
        public const string THONGTINHOADON = "NV_THONGTINHOADON";
        public const string THONGTINPHATHANH = "NV_THONGTINPHATHANH";
        public const string  HOADON = "NV_HOADON";
        
        public const string KHACHHANG = "NV_KHACHHANG";
        public const string  LOAIDICHVUYTE = "DM_LOAIDICHVUYTE";
    }
    
    public static class ConfigurationDb
    {
        #region MONGODB 
        public const string MONGO_CONNECTION_STRING = "DbSettings:ConnectionString";
        public const string MONGO_DATABASE_NAME = "DbSettings:DatabaseName";
        #endregion

        #region POSTGRE
        public const string POSTGRE_CONNECTION = "DbSettings:PostgreConnection";
        #endregion
    }
}