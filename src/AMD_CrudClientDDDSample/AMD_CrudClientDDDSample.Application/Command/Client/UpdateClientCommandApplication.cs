using AMD_CrudClientDDDSample.Application.Command.Interfaces;

namespace AMD_CrudClientDDDSample.Application.Command.Client
{
    public class UpdateClientCommandApplication : ICommandApplication
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public UpdateClientCommandApplication(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}