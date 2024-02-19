using HDDT_BE.Exceptions;
using HDDT_BE.Helpers;
using HDDT_BE.Interfaces.Core;
using HDDT_BE.Models.Core;
using HDDT_BE.Models.PagingParam;
using HDDT_BE.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HDDT_BE.Controllers.Core
{
    [Route("api/v1/[controller]")]
    // [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] Menu model)
        {
            try
            {
                var data = await _menuService.Create(model);
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
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] Menu model)
        {
            try
            {
                var response = await _menuService.Update(model);

                return Ok(
                    new ResultMessageResponse()
                        .WithData(response)
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage(DefaultMessage.UPDATE_SUCCESS)
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
        [Route("getTree")]
        public async Task<IActionResult> GetTree()
        {
            try
            {
                var response = await _menuService.GetTree();

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
        [Route("getTreeList")]
        public async Task<IActionResult> GetTreeList()
        {
            try
            {
                var response = await _menuService.GetTreeList();

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
        [HttpPost]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
              await _menuService.Delete(id);

                return Ok(
                    new ResultMessageResponse()
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage(DefaultMessage.DELETE_SUCCESS)
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
                var data = await _menuService.GetById(id);

                return Ok(
                    new ResultMessageResponse()
                        .WithData(data)
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
        [Route("get-all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await _menuService.Get();

                return Ok(
                    new ResultMessageResponse()
                        .WithData(data)
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


        [HttpPost]
        [Route("get-paging-params")]
        public async Task<IActionResult> GetPagingParam(PagingParam param)
        {
            try
            {
                var data = await _menuService.GetPaging(param);

                return Ok(
                    new ResultMessageResponse()
                        .WithData(data)
                        .WithCode(DefaultCode.SUCCESS)
                        .WithMessage(DefaultMessage.GET_DATA_SUCCESS)
                );
            }
            catch (ResponseMessageException ex)
            {
                return Ok(
                    new ResultMessageResponse().WithCode(ex.ResultCode)
                        .WithMessage(ex.Message)
                );
            }
        }
        
        
    }
}