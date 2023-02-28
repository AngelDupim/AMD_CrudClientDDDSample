using AMD_CrudClientDDDSample.Domain.Command.Client;
using AMD_CrudClientDDDSample.Domain.Command.Interfaces;

namespace AMD_CrudClientDDDSample.Domain.Handlers.Interfaces
{
    public interface IClientHandler
    {
        Task<ICommandResult> Handle(CreateClientCommand command);

        Task<ICommandResult> Handle(UpdateClientCommand command);

        Task<ICommandResult> Handle(GetByNameCnpjCpfClientCommand command);

        Task<ICommandResult> Handle(GetByIdClientCommand command);

    }
}