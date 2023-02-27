using AMD_CrudClientDDDSample.Domain.Command.Interfaces;
using AMD_CrudClientDDDSample.Domain.Command.User;

namespace AMD_CrudClientDDDSample.Domain.Handlers.Interfaces
{
    public interface IUserHandler
    {
        Task<ICommandResult> Handle(CreateUserCommand command);

        Task<ICommandResult> Handle(DeleteUserCommand command);

        Task<ICommandResult> Handle(GetByNameCnpjCpfUserCommand command);

        Task<ICommandResult> Handle(GetByIdUserCommand command);
    }
}