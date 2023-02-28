using AMD_CrudClientDDDSample.Application.Command.Interfaces;

namespace AMD_CrudClientDDDSample.Application.Command.User
{
    public class DeleteUserCommandApplication : ICommandApplication
    {
        public Guid Id { get; private set; }

        public DeleteUserCommandApplication(Guid id)
        {
            Id = id;
        }
    }
}