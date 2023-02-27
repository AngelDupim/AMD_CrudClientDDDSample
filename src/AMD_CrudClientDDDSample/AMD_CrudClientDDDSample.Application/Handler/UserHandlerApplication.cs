using AMD_CrudClientDDDSample.Application.Command;
using AMD_CrudClientDDDSample.Application.Command.Interfaces;
using AMD_CrudClientDDDSample.Application.Command.User;
using AMD_CrudClientDDDSample.Application.Handler.Interfaces;
using AMD_CrudClientDDDSample.Domain.Command.User;
using AMD_CrudClientDDDSample.Domain.Handlers.Interfaces;
using AutoMapper;

namespace AMD_CrudClientDDDSample.Application.Handler
{
    public class UserHandlerApplication :
        IUserHendlerApplication,
        IHandlerApplication<CreateUserCommandApplication>,
        IHandlerApplication<DeleteUserCommandApplication>,
        IHandlerApplication<GetByNameCnpjCpfUserCommandApplication>,
        IHandlerApplication<GetByIdUserCommandApplication>
    {
        private IMapper _mapper;
        private IUserHandler _handler;

        public UserHandlerApplication(IMapper mapper, IUserHandler userHadler)
        {
            _mapper = mapper;
            _handler = userHadler;
        }

        public async Task<ICommandResultModel> Handle(CreateUserCommandApplication command)
        {
            var convertCommand = _mapper.Map<CreateUserCommand>(command);
            return _mapper.Map<CommandResultModel>(await _handler.Handle(convertCommand));
        }

        public async Task<ICommandResultModel> Handle(DeleteUserCommandApplication command)
        {
            var convertCommand = _mapper.Map<DeleteUserCommand>(command);
            return _mapper.Map<CommandResultModel>(await _handler.Handle(convertCommand));
        }

        public async Task<ICommandResultModel> Handle(GetByNameCnpjCpfUserCommandApplication command)
        {
            var convertCommand = _mapper.Map<GetByNameCnpjCpfUserCommand>(command);
            return _mapper.Map<CommandResultModel>(await _handler.Handle(convertCommand));
        }

        public async Task<ICommandResultModel> Handle(GetByIdUserCommandApplication command)
        {
            var convertCommand = _mapper.Map<GetByIdUserCommand>(command);
            return _mapper.Map<CommandResultModel>(await _handler.Handle(convertCommand));
        }
    }
}