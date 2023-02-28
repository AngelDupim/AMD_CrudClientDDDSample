using AMD_CrudClientDDDSample.Domain.Command;
using AMD_CrudClientDDDSample.Domain.Command.Client;
using AMD_CrudClientDDDSample.Domain.Command.Interfaces;
using AMD_CrudClientDDDSample.Domain.Entity;
using AMD_CrudClientDDDSample.Domain.Handlers.Interfaces;
using AMD_CrudClientDDDSample.Domain.Repository.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Helper;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using Flunt.Notifications;

namespace AMD_CrudClientDDDSample.Domain.Handlers
{
    public class ClientHandler : Notifiable,
        IClientHandler,
        IHandler<CreateClientCommand>,
        IHandler<UpdateClientCommand>,
        IHandler<GetByNameCnpjCpfClientCommand>,
        IHandler<GetByIdClientCommand>
    {

        private readonly IClientRepository _clientRepository;
        public ClientHandler(IClientRepository clientRepository) => _clientRepository = clientRepository;

        public async Task<ICommandResult> Handle(CreateClientCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, ClientMessage.Validate, command.Notifications);

            var clientEnity = new Client(command.Name, command.CnpjCpf, command.BirthDate);

            await _clientRepository.Create(clientEnity);

            return new GenericCommandResult(true, ClientMessage.CreateSucess, clientEnity);
        }

        public async Task<ICommandResult> Handle(UpdateClientCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, ClientMessage.Validate, command.Notifications);

            var clientEntity = await _clientRepository.GetById(command.Id);

            if (clientEntity is not null)
            {
                clientEntity.UpdateName(command.Name);

                await _clientRepository.Update(clientEntity);

                return new GenericCommandResult(true, ClientMessage.UpdateSucess, clientEntity);
            }
            else
            {
                return new GenericCommandResult(false, ClientMessage.NotFound, null);
            }
        }

        public async Task<ICommandResult> Handle(GetByNameCnpjCpfClientCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, ClientMessage.Validate, command.Notifications);

            var clientEntity = await _clientRepository.GetByField(command.CnpjCpf is null ? 
                GenericParameterHelper.SetParameter("Name", command.Name) :
                GenericParameterHelper.SetParameter("CnpjCpf", command.CnpjCpf));

            return new GenericCommandResult(true, clientEntity is null ? ClientMessage.NotFound : ClientMessage.GetSucess, clientEntity);
        }

        public async Task<ICommandResult> Handle(GetByIdClientCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, ClientMessage.Validate, command.Notifications);

            var clientEntity = await _clientRepository.GetById(command.Id);

            return new GenericCommandResult(true, clientEntity is null ? ClientMessage.NotFound : ClientMessage.GetSucess,
                clientEntity is null ? string.Empty : clientEntity);
        }
    }
}