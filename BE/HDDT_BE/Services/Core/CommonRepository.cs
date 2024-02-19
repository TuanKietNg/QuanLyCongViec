using CoreApi.FromBodyModels;
using HDDT_BE.Constants;
using HDDT_BE.Exceptions;
using HDDT_BE.Helpers;
using HDDT_BE.Installers;
using HDDT_BE.Interfaces.Core;
using HDDT_BE.Models.Core;
using HDDT_BE.Models.PagingParam;
using MongoDB.Driver;

namespace HDDT_BE.Services.Core;

public class CommmonRepository<TEntity, UEntityId> : ICommonRepository<TEntity, UEntityId>
    where TEntity : class, IIdEntity<UEntityId>
{
    private readonly IHttpContextAccessor _contextAccessor;
    protected readonly DataContext _context;
        
    protected IMongoCollection<TEntity> _collection;
    
    
    protected IMongoCollection<dynamic> _collectionDynamic;
    
    protected IMongoCollection<CommonModel> _collectionCommon;

    private HttpContext _httpContext { get { return _contextAccessor.HttpContext; } }
    
    public string CollectionName { get; set; }
        

    public CommmonRepository(DataContext context, IHttpContextAccessor contextAccessor )
    {
        _context = context;
        _contextAccessor = contextAccessor;
    }

    public virtual async Task<long> CountAsync()
    {
        return await _collection.Find(x => true).CountDocumentsAsync();
    }

    public virtual async Task<TEntity> GetByIdAsync(IdFromBodyCommonModel model)
    {
        if (model == null || model.Id == null || model.CollectionName == null)
        {
            throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
        }
        var value = ListCommon.listCommon.Where(x => x == model.CollectionName).FirstOrDefault();
        if (value == null)
            throw new ResponseMessageException().WithException(DefaultCode.COMMON_NOT_FOUND);
        _collection = (IMongoCollection<TEntity>)_context.GetCategoryCollectionAs(model.CollectionName);
        if (_collection.CollectionNamespace.DatabaseNamespace == null)
            throw new ResponseMessageException().WithException(DefaultCode.COMMON_NOT_FOUND);
        var entity=  await _collection.Find(x => x.Id.Equals(model.Id) && !x.IsDeleted).FirstOrDefaultAsync();
        if (entity == default)
        {
            throw new ResponseMessageException().WithException(DefaultCode.DATA_NOT_FOUND);
        }
        return entity;
    }
        

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        if (entity == default)
        {
            throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
        }
        var value = ListCommon.listCommon.Where(x => x == entity.CollectionName).FirstOrDefault();
        if (value == null)
            throw new ResponseMessageException().WithException(DefaultCode.COMMON_NOT_FOUND);
        _collection = (IMongoCollection<TEntity>)_context.GetCategoryCollectionAs(entity.CollectionName);
        if (_collection.CollectionNamespace.DatabaseNamespace == null)
            throw new ResponseMessageException().WithException(DefaultCode.COMMON_NOT_FOUND);

        var model = _collection.Find(x => x.Code == entity.Code && !x.IsDeleted).FirstOrDefault();
        if (model != null)
            throw new ResponseMessageException().WithException(DefaultCode.DATA_EXISTED);
        
        entity.CreatedAt = DateTime.Now;
        entity.ModifiedAt = DateTime.Now;
        await _collection.InsertOneAsync(entity);
        if (entity.Id == null)
            throw new ResponseMessageException().WithException(DefaultCode.CREATE_FAILURE);
        return entity;
    }

    public virtual async Task<TEntity> UpdateAsync(TEntity model)
    {
        if (model == default)
            throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
        var value = ListCommon.listCommon.Where(x => x == model.CollectionName).FirstOrDefault();
        if (value == null)
            throw new ResponseMessageException().WithException(DefaultCode.COMMON_NOT_FOUND);
        _collection = (IMongoCollection<TEntity>)_context.GetCategoryCollectionAs(model.CollectionName);
        if (_collection.CollectionNamespace.DatabaseNamespace == null)
            throw new ResponseMessageException().WithException(DefaultCode.COMMON_NOT_FOUND);
        var entity = _collection.Find(x => x.Id.Equals(model.Id) && !x.IsDeleted).FirstOrDefault();
        if (entity == default)
            throw new ResponseMessageException().WithException(DefaultCode.DATA_NOT_FOUND);
        
        
        var data_existed = _collection.Find(x => !x.Id.Equals(model.Id)  && x.Code.Equals(model.Code) && !x.IsDeleted).FirstOrDefault();
        if (data_existed != default)
            throw new ResponseMessageException().WithException(DefaultCode.DATA_EXISTED);
        model.ModifiedAt = DateTime.Now;
        ReplaceOneResult actionResult
            = await _collection.ReplaceOneAsync(x => x.Id.Equals(model.Id)
                , model
                , new ReplaceOptions { IsUpsert = true });
        var result = actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
        if (!result)
            throw new ResponseMessageException().WithException(DefaultCode.UPDATE_FAILURE);
        return model;
    }

    public virtual async Task DeleteAsync(TEntity model)
    {
        if (model == null || model.Id == null)
            throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
        var value = ListCommon.listCommon.Where(x => x == model.CollectionName).FirstOrDefault();
        if (value == null)
            throw new ResponseMessageException().WithException(DefaultCode.COMMON_NOT_FOUND);
        _collection = (IMongoCollection<TEntity>)_context.GetCategoryCollectionAs(model.CollectionName);
        if (_collection.CollectionNamespace.DatabaseNamespace == null)
            throw new ResponseMessageException().WithException(DefaultCode.COMMON_NOT_FOUND);
        var entity = _collection.Find(x => x.Id.Equals(model.Id) && x.Code.Equals(model.Code) && !x.IsDeleted).FirstOrDefault();
        if (entity == default)
            throw new ResponseMessageException().WithException(DefaultCode.DATA_NOT_FOUND);
        entity.ModifiedAt = DateTime.Now;
        //   entity.ModifiedBy = CurrentUserName;
        entity.IsDeleted = true;
        ReplaceOneResult actionResult
            = await _collection.ReplaceOneAsync(x => x.Id.Equals(entity.Id)
                , entity
                , new ReplaceOptions { IsUpsert = true });
        var result = actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
        if (!result)
            throw new ResponseMessageException().WithException(DefaultCode.DELETE_FAILURE);
    }

    public virtual async Task<dynamic> GetAsync(string collectionName)
    {
        var value = ListCommon.listCommon.Where(x => x == collectionName).FirstOrDefault();
        if (value == null)
            throw new ResponseMessageException().WithException(DefaultCode.COMMON_NOT_FOUND);
        _collectionCommon =(IMongoCollection<CommonModel>) _context.GetCategoryCollectionAs(collectionName);
        if (_collectionCommon.CollectionNamespace.DatabaseNamespace == null)
            throw new ResponseMessageException().WithException(DefaultCode.COMMON_NOT_FOUND);
        var filter = Builders<CommonModel>.Filter.Where(x => !x.IsDeleted);
        var list = await _collectionCommon.Aggregate().Match(filter).SortByDescending(x=>x.Sort)
            .Project<CommonModelShort>(Projection.Projection_BasicCommon).ToListAsync();
        return list;
    }



    public virtual async Task<dynamic> GetPagingAsync( CommonPaging param)
    {
        HDDT_BE.Models.PagingParam.PagingModel<dynamic> result = new HDDT_BE.Models.PagingParam.PagingModel<dynamic>();
        var builder = Builders<CommonModel>.Filter;
        var filter = builder.Empty;
        if (String.IsNullOrEmpty(param.CollectionName))
            throw new ResponseMessageException().WithException(DefaultCode.ERROR_STRUCTURE);
        var value = ListCommon.listCommon.Where(x => x == param.CollectionName).FirstOrDefault();
        if (value == null)
            throw new ResponseMessageException().WithException(DefaultCode.COMMON_NOT_FOUND);
        _collectionCommon = (IMongoCollection<CommonModel>)_context.GetCategoryCollectionAs(value);
        filter = builder.And(filter, builder.Where(x => !x.IsDeleted));
        if (!String.IsNullOrEmpty(param.Content))
        {
            filter = builder.And(filter,
                builder.Where(x => x.Name.Trim().ToLower().Contains(param.Content.Trim().ToLower())
                                   || x.Code.Trim().ToLower().Contains(param.Content.Trim().ToLower())
                ));
        }
        string sortBy = nameof(IIdEntity<string>.CreatedAt);
        result.TotalRows = await _collectionCommon.CountDocumentsAsync(filter);
        result.Data  = await _collectionCommon.Find(filter)
            .Sort(param.SortDesc
                ? Builders<CommonModel>
                    .Sort.Descending(sortBy)
                : Builders<CommonModel>
                    .Sort.Ascending(sortBy))
            .Project<CommonModelShort>(Projection.Projection_BasicCommon)
            .Skip(param.Skip)
            .Limit(param.Limit)
            .ToListAsync();
        return result;
    }
}