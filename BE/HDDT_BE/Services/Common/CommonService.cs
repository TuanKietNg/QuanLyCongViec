using HDDT_BE.Installers;
using HDDT_BE.Interfaces.Common;
using HDDT_BE.Models.Core;
using HDDT_BE.Services.Core;

namespace HDDT_BE.Services.Common;

public class CommonService: CommmonRepository<CommonModel, string>, ICommonService
{
       
    public CommonService(DataContext context, IHttpContextAccessor contextAccessor) :
        base(context, contextAccessor)
    {
    }
    
        
}