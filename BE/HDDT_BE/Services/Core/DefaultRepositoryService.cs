using HDDT_BE.Constants;
using HDDT_BE.Exceptions;
using HDDT_BE.Helpers;
using HDDT_BE.Interfaces.Core;
using HDDT_BE.Models.PagingParam;
using MongoDB.Driver;

namespace HDDT_BE.Services.Core
{
    public class DefaultReposityService<T> : IDefaultReposityService<T> where T : class
    {
        protected IMongoCollection<T> _collection ;
        public DefaultReposityService(IMongoCollection<T> collection)
        {
            _collection = collection;
        }
        public async Task<dynamic> GetAll()
        {
            try
          {
              var filter = Builders<T>.Filter.Eq("IsDeleted", false);
              return await _collection.Find(filter).ToListAsync();
          }
          catch (Exception ex)
          {
              // log or manage the exception
              throw ex;
          }
        }

        public async Task<dynamic> GetById(string id)
        {
            try
            {
                var filter = Builders<T>.Filter.Eq("IsDeleted", false);
                filter = Builders<T>.Filter.And(filter, Builders<T>.Filter.Eq("Id", id));
                var data =  await _collection.Find(filter).FirstOrDefaultAsync();
                if (data == default)
                    throw new ResponseMessageException().WithException(DefaultCode.COMMON_NOT_FOUND);
                return data;
            }
            catch (ResponseMessageException e)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.EXCEPTION)
                    .WithMessage(e.ToString());
            }
        }

        public async Task<bool> Delete(string id) 
        {
            var filter = Builders<T>.Filter.Eq("IsDeleted", false);
            filter = Builders<T>.Filter.And(filter, Builders<T>.Filter.Eq("Id", id));
            dynamic data =  await _collection.Find(filter).FirstOrDefaultAsync();
            if (data == null)
                throw new ResponseMessageException().WithException(DefaultCode.COMMON_NOT_FOUND);
            data.IsDeleted = true;
            var result =  _collection.ReplaceOne(filter, data, new UpdateOptions() { IsUpsert = true } );
            if (result.ModifiedCount  ==   0  || result == null)
                throw new ResponseMessageException().WithException(DefaultCode.DELETE_FAILURE);
            return true;
        }
        

        public async Task<dynamic> GetPagingCore(PagingParam pagingParam)
        {
            try
            {
                PagingModel<T> result = new PagingModel<T>();
                var builder = Builders<T>.Filter;
                var filter = builder.Empty;
                filter = builder.And(filter, builder.Eq("IsDeleted", false));
                if (!String.IsNullOrEmpty(pagingParam.Content))
                {
                    filter = builder.And(filter,
                        (builder.Regex("UnsignedName", FormatterString.ConvertToTenKhongDau(pagingParam.Content)) |
                         builder.Regex("Code", pagingParam.Content) 
                        ));
                }
                result.TotalRows = await _collection.CountDocumentsAsync(filter);
                result.Data = await _collection.Find(filter)
                    .Sort(pagingParam.SortDesc
                        ? Builders<T>
                            .Sort.Ascending("CreatedAt")
                        : Builders<T>
                            .Sort.Descending("CreatedAt")
                        )
                    .Skip(pagingParam.Skip)
                    .Limit(pagingParam.Limit)
                    .ToListAsync();
                return result;
            }
            catch (ResponseMessageException e)
            {
                new ResultMessageResponse().WithCode(e.ResultCode)
                    .WithMessage(e.ResultString);
            }
            return null;
        }
    }
    
    
}
