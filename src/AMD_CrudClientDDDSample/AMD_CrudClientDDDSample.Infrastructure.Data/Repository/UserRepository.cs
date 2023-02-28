using AMD_CrudClientDDDSample.Domain.Entity;
using AMD_CrudClientDDDSample.Domain.Repository.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Data.Context;

namespace AMD_CrudClientDDDSample.Infrastructure.Data.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(IContext context) : base(context) { }
    }
}