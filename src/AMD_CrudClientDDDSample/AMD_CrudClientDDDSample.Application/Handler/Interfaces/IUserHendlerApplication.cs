using AMD_CrudClientDDDSample.Application.Command.Interfaces;
using AMD_CrudClientDDDSample.Application.Command.User;

namespace AMD_CrudClientDDDSample.Application.Handler.Interfaces
{
    public interface IUserHendlerApplication
    {
        Task<ICommandResultModel> Handle(CreateUserCommandApplication command);

        Task<ICommandResultModel> Handle(DeleteUserCommandApplication command);

        Task<ICommandResultModel> Handle(GetByNameCnpjCpfUserCommandApplication command);

        Task<ICommandResultModel> Handle(GetByIdUserCommandApplication command);
    }
}