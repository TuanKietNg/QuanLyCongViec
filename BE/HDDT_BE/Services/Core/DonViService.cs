using CoreApi.Extensions;
using HDDT_BE.Exceptions;
using HDDT_BE.Helpers;
using HDDT_BE.Installers;
using HDDT_BE.Interfaces.Core;
using HDDT_BE.Models.Core;
using HDDT_BE.Models.PagingParam;
using HDDT_BE.ViewModels;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HDDT_BE.Services.Core
{
    public class DonViService : BaseService, IDonViService
    {
        private DataContext _context;
        private BaseMongoDb<DonVi, string> BaseMongoDb;
        private IMongoCollection<DonVi> _collection;
        private IMongoCollection<User> _collectionUser;


        public DonViService( DataContext context,
            IHttpContextAccessor contextAccessor)
            : base(context, contextAccessor)
        {
            _context = context;
            BaseMongoDb = new BaseMongoDb<DonVi, string>(_context.DONVI);
            _collection = context.DONVI;
            _collectionUser = _context.USERS;
        }

        public async Task<DonVi> Create(DonVi model)
        {
            if (model == default)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.ERROR_STRUCTURE);
            }

            var entity = new DonVi()
            {
                Id = BsonObjectId.GenerateNewId().ToString(),
                Ten = model.Ten,
                MaDonVi = model.MaDonVi,
                IsShow = model.IsShow,
                DonViCha = model.DonViCha,
                // ModifiedBy = CurrentUserName,
                // CreatedBy = CurrentUserName,
                
            };
            var result = await BaseMongoDb.CreateAsync(entity);
            if (model.DonViCha != null)
            {
                var builder = Builders<User>.Filter;
                var filter = builder.Empty;
                filter = builder.And(filter,
                    builder.Where(x => x.DonViIds != default && x.DonViIds.Any(s => s == model.DonViCha)));
                // var count  = await _collectionUser.CountDocumentsAsync(filter);
                var update = Builders<User>.Update.Push(y => y.DonViIds, entity.Id);
                var resultUser = _collectionUser.UpdateManyAsync(filter, update);
                if (resultUser== default)
                {
                    throw new ResponseMessageException()
                        .WithCode(DefaultCode.UPDATE_FAILURE);
                }
            }
            if (result.Entity.Id == default || !result.Success)
            {
                throw new ResponseMessageException().WithCode(DefaultCode.CREATE_FAILURE);
            }

            return entity;
        }

        public async Task<DonVi> GetDonVisTop()
        {
            return _collection.Find(x => x.DonViCha == null).FirstOrDefault();
        }
        
        public async Task<DonVi> Update(DonVi model)
        {
            if (model == default)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.ERROR_STRUCTURE);
            }

            var entity = _collection.Find(x => x.Id == model.Id).FirstOrDefault();

            if (entity == default)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.DATA_NOT_FOUND);
            }

            entity.Ten = model.Ten;
            entity.MaDonVi = model.MaDonVi;
       
            entity.IsShow = model.IsShow;
            entity.DonViCha = model.DonViCha;
            // entity.ModifiedBy = CurrentUserName;
            entity.ModifiedAt = DateTime.Now;

            var result = await BaseMongoDb.UpdateAsync(entity);
            if (!result.Success)
            {
                throw new ResponseMessageException().WithCode(DefaultCode.UPDATE_FAILURE);
            }
            
            return entity;
        }

        public async Task Delete(string id)
        {
            if (id == default)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.ERROR_STRUCTURE);
            }

            var entity = _collection.Find(x => x.Id == id && x.IsDeleted != true).FirstOrDefault();
            if (entity == default)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.DATA_NOT_FOUND);
            }

            entity.ModifiedAt = DateTime.Now;
            entity.IsDeleted = true;

            var result = await BaseMongoDb.DeleteAsync(entity);

            if (!result.Success)
            {
                throw new ResponseMessageException()
                    .WithCode(DefaultCode.DELETE_FAILURE);
            }
        }

        public async Task<IEnumerable<DonVi>> Get()
        {
            return await _collection.Find(x => x.IsDeleted != true).ToListAsync();
        }
        public async Task<DonVi> GetById(string id)
        {
            var model = await _collection.Find(x => x.Id == id && x.IsDeleted != true).FirstOrDefaultAsync();
            return model; 
        }

        public async Task<PagingModel<DonVi>> GetPaging(PagingParam param)
        {
            PagingModel<DonVi> result = new PagingModel<DonVi>();
            var builder = Builders<DonVi>.Filter;
            var filter = builder.Empty;
            filter = builder.And(filter, builder.Eq(x => x.IsDeleted, false));
            if (!String.IsNullOrEmpty(param.Content))
            {
                filter = builder.And(filter,
                    builder.Where(x => x.Ten.Trim().ToLower().Contains(param.Content.Trim().ToLower())));
            }
            // if (!String.IsNullOrEmpty(param.idLinhVuc))
            // {
            //     filter = builder.And(filter,
            //         builder.Where(x => x.Ten.Trim().ToLower().Contains(param.Content.Trim().ToLower())));
            // }
            string sortBy = nameof(DonVi.CreatedAt);
            result.TotalRows = await _collection.CountDocumentsAsync(filter);
            result.Data = await _collection.Find(filter)
                .Sort(param.SortDesc
                    ? Builders<DonVi>.Sort.Ascending(sortBy)
                    : Builders<DonVi>.Sort.Descending(sortBy))
                .Skip(param.Skip).Limit(param.Limit)
                .ToListAsync();
            return result;
        }
        public async Task<List<DonViTreeVM>> GetTree()
        {
            var listDonVi = await _context.DONVI.Find(x  => x.IsDeleted ==false)
                .ToListAsync();
            var parents = listDonVi.Where(x => x.DonViCha == null).ToList();
            List<DonViTreeVM> list = new List<DonViTreeVM>();
            foreach (var item in parents)
            {
                DonViTreeVM donVi = new DonViTreeVM(item);
                list.Add(donVi);
                GetLoopItem(ref list, listDonVi, donVi);
            }
            return list;
        }

  
        


        private List<DonViTreeVM> GetLoopItem(ref List<DonViTreeVM> list, List<DonVi> items, DonViTreeVM target)
        {
            try
            {
                var coquan = items.FindAll((item) => item.DonViCha == target.Id).ToList();
                if (coquan.Count > 0)
                {
                    target.Children = new List<DonViTreeVM>();
                    foreach (var item in coquan)
                    {
                        DonViTreeVM itemDV = new DonViTreeVM(item);
                        target.Children.Add(itemDV);
                        GetLoopItem(ref list, items, itemDV);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
            return null;
        }
        public List<string> GetListDonViId(string coQuanId)
        {
            var listDonVi = _context.DONVI.AsQueryable().Where(x=>x.IsDeleted == false).ToList();
            var parents = listDonVi.Where(x => x.Id == coQuanId).ToList();
            List<string> list = new List<string>();
            foreach (var item in parents)
            {
                list.Add(item.Id);
                GetLoopItemCoQuan(ref list, listDonVi, item.Id);
            }

            return list;
        }

     


        private List<string> GetLoopItemCoQuan(ref List<string> list, List<DonVi> items, string target)
        {
            try
            {
                var coquan = items.FindAll((item) => item.DonViCha == target).ToList();
                if (coquan.Count > 0)
                {
                    foreach (var item in coquan)
                    {
                        list.Add(item.Id);
                        GetLoopItemCoQuan(ref list, items, item.Id);
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }

            return null;
        }
    }
}