using CoreApi.FromBodyModels;
using HDDT_BE.Exceptions;
using HDDT_BE.Helpers;
using HDDT_BE.Installers;
using HDDT_BE.Interfaces.Core;
using HDDT_BE.Models.PagingParam;
using HDDT_BE.Services.Core;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CoreApi.Controllers.Core
{
    public abstract class DefaultReposityController<T> : ControllerBase where T : class
    {
        protected readonly IDefaultReposityService<T> Repository;
        public IMongoCollection<T> _collection;
        
        public DefaultReposityController(DataContext context ,  string collectionName)
        {
            _collection = context.Database.GetCollection<T>(collectionName);
            Repository = new DefaultReposityService<T>(_collection);
         //   Repository.getCollection(_collection);
        }
       
        
        [HttpGet]
        [Route("get-all-core")]
        public async Task<IActionResult> GetAllData()
        {
            try
            {
                var response = await  Repository.GetAll();
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
        [Route("get-by-id-core/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var response = await  Repository.GetById(id);
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
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] IdFromBodyModel model)
        {
            try
            {
                await  Repository.Delete(model.Id);
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
        [Route("deleted")]
        public async Task<IActionResult> Deleted([FromBody] IdFromBodyModel model)
        {
            try
            {
                await  Repository.Delete(model.Id);
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
        [Route("get-paging-params-core")]
        public async Task<IActionResult> GetPagingCore([FromBody] PagingParam pagingParam)
        {
            try
            {
                var response = await  Repository.GetPagingCore(pagingParam);
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