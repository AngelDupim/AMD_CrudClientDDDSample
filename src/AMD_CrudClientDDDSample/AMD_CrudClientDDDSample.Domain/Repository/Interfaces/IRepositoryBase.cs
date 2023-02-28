namespace AMD_CrudClientDDDSample.Domain.Repository.Interfaces
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        Task<TEntity> Create(TEntity entity);
        Task Update(TEntity entity);
        Task<List<TEntity>> Get();
        Task<TEntity> GetById(Guid id);
        Task<List<TEntity>> GetByField(Dictionary<object, object> parameter);
        Task Delete(Guid id);
    }
}