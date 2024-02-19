using HDDT_BE.Interfaces.Auth;
using HDDT_BE.Interfaces.Common;
using HDDT_BE.Interfaces.Core;
using HDDT_BE.Interfaces.Professional;
using HDDT_BE.Services.Auth;
using HDDT_BE.Services.Common;
using HDDT_BE.Services.Core;
using HDDT_BE.Services.Professional;

namespace HDDT_BE.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<DataContext>();

            services.AddScoped<IRefreshTokenService, RefreshTokenService>();
           

            services.AddScoped<IThongTinHoaDonService, ThongTinHoaDonService>();

            services.AddScoped<IRefreshTokenService, RefreshTokenService>();

            
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IDonViService, DonViService>();
            services.AddScoped<IThongTinNguoiBanService, ThongTinNguoiBanService>();
            services.AddScoped<IThongTinPhatHanhService, ThongTinPhatHanhService>();
            services.AddScoped<IThuocService, ThuocService>();
            services.AddScoped<ILoaiDichVuYTeServices, LoaiDichVuYTeService>();

            services.AddScoped<IMenuService, MenuService>();
            
            services.AddScoped<IKhachHangService, KhachHangService>();
            services.AddScoped<IIdentityService, IdentityService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IUnitRoleService, UnitRoleService>();


            services.AddScoped<IUserService, UserService>();
            
          
            
            
            services.AddScoped<ILoggingService,LoggingService>();
            
            
            services.AddScoped<IDiaGioiHanhChinhService, DiaGioiHanhChinhService>();
            

            
        }
    }
}