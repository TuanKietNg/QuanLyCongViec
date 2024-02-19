using CoreApi.Controllers.Core;
using HDDT_BE.Constants;
using HDDT_BE.Exceptions;
using HDDT_BE.Helpers;
using HDDT_BE.Installers;
using HDDT_BE.Interfaces.Professional;
using HDDT_BE.Models.Core;
using HDDT_BE.Models.PagingParam;
using HDDT_BE.Models.Professional;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HDDT_BE.Controllers.Professional
{
    [Route("api/v1/[controller]")]
    [Authorize]
    public class ThuocController : DefaultReposityController<ThuocModel>
    {
        private readonly IThuocService _service;
        private DataContext _dataContext;
        private static string NameCollection = DefaultNameCollection.THUOC;
        public ThuocController (DataContext context , IThuocService service): base(context,NameCollection)
        {
            _service = service;
            _dataContext = context;
        }
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] ThuocModel model)
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
        
        
        
        [HttpPost]
        [Route("update")]
        public async Task<IActionResult> Update([FromBody] ThuocModel model)
        {
            try
            {
                var data = await _service.Update(model);
                return Ok(
                    new ResultMessageResponse()
                        .WithData(data)
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
        [Route("get-paging-params")]
        public async Task<IActionResult> GetPagingParam([FromBody] PagingParam param)
        {
            try
            {
                var response = await  _service.GetPaging(param);
        
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
        [Route("getCount/{id}")]
        public async Task<IActionResult> GetCount(string id)
        {
            try
            {
                var data = await _service.GetSum(id);
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

        
        
                
 
        
        [HttpGet]
        [Route("get")]
        public async Task<IActionResult> GetConvert()
        {
            try
            {
                var data = await _service.StandardData();
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

        
        
        [HttpGet]
        [Route("get-thuoc-by-loai-dich-vu/{code}")]
        public async Task<IActionResult> GetThuoc(string code)
        {
            try
            {
                var data = await _service.GetThuocByLoaiDichVuYTe(code);
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