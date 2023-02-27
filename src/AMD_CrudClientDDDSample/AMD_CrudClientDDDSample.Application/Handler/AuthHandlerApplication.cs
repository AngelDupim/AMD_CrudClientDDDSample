using AMD_CrudClientDDDSample.Application.Command;
using AMD_CrudClientDDDSample.Application.Command.Auth;
using AMD_CrudClientDDDSample.Application.Command.Interfaces;
using AMD_CrudClientDDDSample.Application.Handler.Interfaces;
using AMD_CrudClientDDDSample.Domain.Command.Auth;
using AMD_CrudClientDDDSample.Domain.Handlers.Interfaces;
using AutoMapper;

namespace AMD_CrudClientDDDSample.Application.Handler
{
    public class AuthHandlerApplication :
        IAuthHandlerApplication,
        IHandlerApplication<GetAuthCommandApplication>
    {
        private IMapper _mapper;
        private IAuthHandler _handle;

        public AuthHandlerApplication(IMapper mapper, IAuthHandler handle)
        {
            _mapper = mapper;
            _handle = handle;
        }
        public async Task<ICommandResultModel> Handle(GetAuthCommandApplication command)
        {
            var convertCommand = _mapper.Map<GetAuthCommand>(command);
            return _mapper.Map<CommandResultModel>(await _handle.Handle(convertCommand));
        }
    }
}