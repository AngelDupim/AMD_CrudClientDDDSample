using AMD_CrudClientDDDSample.Application.Command.Client;
using AMD_CrudClientDDDSample.Application.Command.Interfaces;

namespace AMD_CrudClientDDDSample.Application.Handler.Interfaces
{
    public interface IClientHandlerApplication
    {
        Task<ICommandResultModel> Handle(CreateClientCommandApplication command);

        Task<ICommandResultModel> Handle(UpdateClientCommandApplication command);

        Task<ICommandResultModel> Handle(GetByNameCnpjCpfClientCommandApplication command);

        Task<ICommandResultModel> Handle(GetByIdClientCommandApplication command);
    }
}