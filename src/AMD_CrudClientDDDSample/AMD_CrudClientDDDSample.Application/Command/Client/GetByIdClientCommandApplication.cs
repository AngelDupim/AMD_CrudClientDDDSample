using AMD_CrudClientDDDSample.Application.Command.Interfaces;

namespace AMD_CrudClientDDDSample.Application.Command.Client
{
    public class GetByIdClientCommandApplication : ICommandApplication
    {
        public Guid Id { get; private set; }

        public GetByIdClientCommandApplication(Guid id)
        {
           Id = id;
        }
    }
}