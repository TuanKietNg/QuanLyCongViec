using CoreApi.Controllers.Core;
using HDDT_BE.Constants;
using HDDT_BE.Exceptions;
using HDDT_BE.Helpers;
using HDDT_BE.Installers;
using HDDT_BE.Interfaces.Professional;
using HDDT_BE.Models.Professional;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HDDT_BE.Controllers.Professional
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ThongTinNguoiBanController : DefaultReposityController<SellerInfoModel>
    {
        private readonly IThongTinNguoiBanService _service;
        private DataContext _dataContext;
        private static string NameCollection = DefaultNameCollection.THONGTINNGUOIBAN;
        public ThongTinNguoiBanController (DataContext context , IThongTinNguoiBanService service): base(context,NameCollection)
        {
            _service = service;
            _dataContext = context;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] SellerInfoModel model)
        {
            try
            {
                var data = await _service.Create(model);
                return Ok(
                    new ResultMessageResponse()
                        .WithData(data)
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage(DefaultMessage.CREATE_SUCCESS)
                );
            }
            catch (ResponseMessageException ex)
            {
                return Ok(
                    new ResultMessageResponse().WithCode(ex.ResultCode)
                        .WithMessage(ex.ResultString)
                );
            }
        }



    }
}