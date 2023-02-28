using AMD_CrudClientDDDSample.Application.Command;
using AMD_CrudClientDDDSample.Application.Command.Client;
using AMD_CrudClientDDDSample.Application.Command.Interfaces;
using AMD_CrudClientDDDSample.Application.Handler.Interfaces;
using AMD_CrudClientDDDSample.Domain.Command.Client;
using AMD_CrudClientDDDSample.Domain.Handlers.Interfaces;
using AutoMapper;

namespace AMD_CrudClientDDDSample.Application.Handler
{
    public class ClientHandlerApplication :
        IClientHandlerApplication,
        IHandlerApplication<CreateClientCommandApplication>,
        IHandlerApplication<UpdateClientCommandApplication>,
        IHandlerApplication<GetByNameCnpjCpfClientCommandApplication>,
        IHandlerApplication<GetByIdClientCommandApplication>

    {
        private IMapper _mapper;
        private IClientHandler _handler;
        
        public ClientHandlerApplication(IMapper mapper, IClientHandler handler)
        {
            _mapper = mapper;
            _handler = handler;
        }

        public async Task<ICommandResultModel> Handle(CreateClientCommandApplication command)
        {
            var convertCommand = _mapper.Map<CreateClientCommand>(command);
            return _mapper.Map<CommandResultModel>(await _handler.Handle(convertCommand));
        }

        public async Task<ICommandResultModel> Handle(UpdateClientCommandApplication command)
        {
            var convertCommand = _mapper.Map<UpdateClientCommand>(command);
            return _mapper.Map<CommandResultModel>(await _handler.Handle(convertCommand));
        }

        public async Task<ICommandResultModel> Handle(GetByNameCnpjCpfClientCommandApplication command)
        {
            var convertCommand = _mapper.Map<GetByNameCnpjCpfClientCommand>(command);
            return _mapper.Map<CommandResultModel>(await _handler.Handle(convertCommand));
        }

        public async Task<ICommandResultModel> Handle(GetByIdClientCommandApplication command)
        {
            var convertCommand = _mapper.Map<GetByIdClientCommand>(command);
            return _mapper.Map<CommandResultModel>(await _handler.Handle(convertCommand));
        }
    }
}