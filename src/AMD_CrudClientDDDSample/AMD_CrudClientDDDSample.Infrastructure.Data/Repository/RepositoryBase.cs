using AMD_CrudClientDDDSample.Domain.Repository.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Data.Context;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack;

namespace AMD_CrudClientDDDSample.Infrastructure.Data.Repository
{
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly IContext _context;
        protected IMongoCollection<TEntity> _collection;

        public RepositoryBase(IContext context)
        {
            _context = context;
            _collection = context.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public async Task Delete(Guid id)
        {
            await _collection.DeleteOneAsync(Builders<TEntity>.Filter.Eq("_id", BsonBinaryData.Create(id)));
        }

        public async Task<List<TEntity>> Get()
        {
            var listEntity = await _collection.FindAsync(Builders<TEntity>.Filter.Empty);
            return await listEntity.ToListAsync();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            var entity = await _collection.FindAsync(Builders<TEntity>.Filter.Eq("_id", BsonBinaryData.Create(id)));
            return await entity.FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetByField(Dictionary<object, object> parameter)
        {
            var listEntity = await _collection.FindAsync(Builders<TEntity>
                .Filter.Eq(parameter["Field"].ToString(), parameter["Value"].ToString()));

            return await listEntity.ToListAsync();
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task Update(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq("_id", entity.GetId());
            await _collection.ReplaceOneAsync(filter, entity);
        }
    }
}