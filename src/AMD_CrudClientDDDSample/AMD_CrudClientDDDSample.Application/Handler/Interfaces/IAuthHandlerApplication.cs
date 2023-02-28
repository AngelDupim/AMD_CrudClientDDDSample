using AMD_CrudClientDDDSample.Application.Command.Auth;
using AMD_CrudClientDDDSample.Application.Command.Interfaces;

namespace AMD_CrudClientDDDSample.Application.Handler.Interfaces
{
    public interface IAuthHandlerApplication
    {
        Task<ICommandResultModel> Handle(GetAuthCommandApplication command);
    }
}