using AMD_CrudClientDDDSample.Domain.Entity;
using MongoDB.Bson.Serialization;

namespace AMD_CrudClientDDDSample.Infrastructure.Data.Mapper
{
    public class UserMapper
    {

        public virtual void Configuration() {
            BsonClassMap.RegisterClassMap<User>(map => {
                map.AutoMap();
                map.MapMember(m => m.Name);
                map.MapMember(m => m.Password);
                map.MapMember(m => m.CnpjCpf);
                map.MapMember(m => m.IsActive);
                map.MapMember(m => m.Role.ToString());
            });
        
        }
    }
}