using AMD_CrudClientDDDSample.Infrastructure.Shared.Helper.Configuration;
using MongoDB.Driver;

namespace AMD_CrudClientDDDSample.Infrastructure.Data.Context
{
    public class Context : IContext
    {
        private IMongoDatabase? _database { get; set; }
        private MongoClient? _mongoClient { get; set; }

        private void MongoDBConnection()
        {
            if (_mongoClient != null) return;

            _mongoClient = new MongoClient(AppSettingsHelper.GetConfigurationAppSettings("MongoDBSettings", "Connection"));
            _database = _mongoClient.GetDatabase(AppSettingsHelper.GetConfigurationAppSettings("MongoDBSettings", "DataBase"));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public IMongoCollection<T> GetCollection<T>(string document)
        {
            MongoDBConnection();
            return _database.GetCollection<T>(document);
        }
    }
}