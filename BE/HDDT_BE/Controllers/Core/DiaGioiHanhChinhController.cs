using HDDT_BE.Exceptions;
using HDDT_BE.Helpers;
using HDDT_BE.Interfaces.Core;
using HDDT_BE.Models.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HDDT_BE.Controllers.Core
{
    [Route("api/v1/[controller]")]
    /*[Authorize]*/
    public class DiaGioiHanhChinhController : ControllerBase
    {
        private IDiaGioiHanhChinhService _diaGioiHanhChinhService;
        private string NameCollection = "DiaGioiHanhChinh";

        public DiaGioiHanhChinhController( IDiaGioiHanhChinhService service)
        {
            _diaGioiHanhChinhService = service;
        }
        
        
        
        [HttpGet]
        [Route("get-list-Child/{code}")]
        public async Task<IActionResult> GetListChild(string code)
        {
            try
            {
                var response = await _diaGioiHanhChinhService.GetListChildByCode(code);
                return Ok(
                    new ResultMessageResponse()
                        .WithData(response)
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage(DefaultMessage.GET_DATA_SUCCESS)
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
        [HttpGet]
        [Route("get-list-by-level/{id}")]
        public async Task<IActionResult> GetListByLevel(int  id = 0)
        {
            try
            {
                var response = await _diaGioiHanhChinhService.GetListByLevel(id);
                return Ok(
                    new ResultMessageResponse()
                        .WithData(response)
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage(DefaultMessage.GET_DATA_SUCCESS)
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
        [HttpGet]
        [Route("get-by-id/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var response = await _diaGioiHanhChinhService.GetById(id);

                return Ok(
                    new ResultMessageResponse()
                        .WithData(response)
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage(DefaultMessage.GET_DATA_SUCCESS)
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
