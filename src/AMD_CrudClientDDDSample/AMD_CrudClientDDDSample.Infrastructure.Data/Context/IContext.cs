using MongoDB.Driver;

namespace AMD_CrudClientDDDSample.Infrastructure.Data.Context
{
    public interface IContext : IDisposable
    {
        IMongoCollection<T> GetCollection<T>(string document);
    }
}