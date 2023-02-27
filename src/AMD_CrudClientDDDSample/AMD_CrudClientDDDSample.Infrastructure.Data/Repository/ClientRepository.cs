using AMD_CrudClientDDDSample.Domain.Entity;
using AMD_CrudClientDDDSample.Domain.Repository.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Data.Context;

namespace AMD_CrudClientDDDSample.Infrastructure.Data.Repository
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(IContext context) : base(context) {}
    }
}