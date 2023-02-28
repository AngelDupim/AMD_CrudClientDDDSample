using AMD_CrudClientDDDSample.Domain.Command.Auth;
using AMD_CrudClientDDDSample.Domain.Command.Interfaces;

namespace AMD_CrudClientDDDSample.Domain.Handlers.Interfaces
{
    public interface IAuthHandler
    {
        Task<ICommandResult> Handle(GetAuthCommand command);
    }
}