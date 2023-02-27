using AMD_CrudClientDDDSample.Application.Command.Interfaces;

namespace AMD_CrudClientDDDSample.Application.Command.User
{
    public class GetByIdUserCommandApplication : ICommandApplication
    {
        public Guid Id { get; private set; }

        public GetByIdUserCommandApplication(Guid id)
        {
            Id = id;
       }
    }
}