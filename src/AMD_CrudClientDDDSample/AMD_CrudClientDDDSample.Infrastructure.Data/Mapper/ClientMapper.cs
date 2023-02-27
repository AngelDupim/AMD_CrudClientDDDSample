using AMD_CrudClientDDDSample.Domain.Entity;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace AMD_CrudClientDDDSample.Infrastructure.Data.Mapper
{
    public class ClientMapper
    {
        public virtual void Configuration()
        {
            BsonClassMap.RegisterClassMap<Client>(map =>
            {
                map.AutoMap();
                map.MapMember(m => m.Name);
                map.MapMember(m => m.CnpjCpf);
                map.MapMember(m => m.BirthDate.Date);
                map.MapMember(m => m.RegisterDate).SetSerializer(new DateTimeSerializer(DateTimeKind.Local));
            });
        }
    }
}