using HDDT_BE.Controllers.Core;
using HDDT_BE.Interfaces.Common;
using HDDT_BE.Models.Core;
using Microsoft.AspNetCore.Mvc;

namespace HDDT_BE.Controllers.Common;
[ApiController]
[Route("api/v1/[controller]")]
public class CommonController : CommonRepositoryController<CommonModel, string>
{
    private readonly ICommonService _service;
    public CommonController(ICommonService service) : base(service)
    {
        this._service = service;
    }
    
    
    
        
}