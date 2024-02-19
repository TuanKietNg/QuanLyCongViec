using HDDT_BE.Exceptions;
using HDDT_BE.Helpers;
using HDDT_BE.Interfaces.Core;
using HDDT_BE.Models.Auth;
using HDDT_BE.Models.Core;
using HDDT_BE.Models.PagingParam;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HDDT_BE.Controllers.Auth
{
    [Route("api/v1/[controller]")]
    // [Authorize]
    public class ModuleController : ControllerBase
    {
        private IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] Module model)
        {
            try
            {
                var response = await _moduleService.Create(model);

                return Ok(
                    new ResultMessageResponse()
                        .WithData(response)
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
        [Route("createPermission")]
        public async Task<IActionResult> CreatePermission([FromBody] Permission model)
        {
            try
            {
                var response = await _moduleService.AddPermissionToModule(model);

                return Ok(
                    new ResultMessageResponse()
                        .WithData(response)
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
        [Route("DeletePermission")]
        public async Task<IActionResult> DeletePermission([FromBody] Permission model)
        {
            try
            {
               await _moduleService.DeletePermission(model);

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
        
        [HttpPost]
        [Route("GetPermissionById")]
        public async Task<IActionResult> GetPermissionById([FromBody] Permission model)
        {
            try
            {
                var data =  await _moduleService.GetPermissionById(model);
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
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] Module model)
        {
            try
            {
                var response = await _moduleService.Update(model);

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
        [HttpPost]
        [Route("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _moduleService.Delete(id);

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
                var data = await _moduleService.GetById(id);

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
                var data = await _moduleService.Get();

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
        public async Task<IActionResult> GetPagingParam([FromBody] PagingParam param)
        {
            try
            {
                var response = await _moduleService.GetPaging(param);

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
