using AMD_CrudClientDDDSample.Domain.Command;
using AMD_CrudClientDDDSample.Domain.Command.Interfaces;
using AMD_CrudClientDDDSample.Domain.Command.User;
using AMD_CrudClientDDDSample.Domain.Entity;
using AMD_CrudClientDDDSample.Domain.Handlers.Interfaces;
using AMD_CrudClientDDDSample.Domain.Repository.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Helper;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using Flunt.Notifications;

namespace AMD_CrudClientDDDSample.Domain.Handlers
{
    public class UserHandler : Notifiable,
        IUserHandler,
        IHandler<CreateUserCommand>,
        IHandler<DeleteUserCommand>,
        IHandler<GetByNameCnpjCpfUserCommand>,
        IHandler<GetByIdUserCommand>
    {
        private IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<ICommandResult> Handle(CreateUserCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, UserMessage.Validate, command.Notifications);

            var userEntity = new User(command.Name, command.Password, command.CnpjCpf);

            await _userRepository.Create(userEntity);

            return new GenericCommandResult(true, UserMessage.CreateSucess, userEntity);
        }

        public async Task<ICommandResult> Handle(DeleteUserCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, UserMessage.Validate, command.Notifications);

            var userEntity = await _userRepository.GetById(command.Id);

            if (userEntity is not null)
            {
                userEntity.DeleteUser();

                await _userRepository.Update(userEntity);

                return new GenericCommandResult(true, UserMessage.DeleteSucess, userEntity);
            }
            else
                return new GenericCommandResult(false, UserMessage.UserNotFound, null);
        }

        public async Task<ICommandResult> Handle(GetByNameCnpjCpfUserCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, UserMessage.Validate, command.Notifications);

            var userEntity = await _userRepository.GetByField(command.Name is not null ?
                GenericParameterHelper.SetParameter("Name", command.Name) :
                GenericParameterHelper.SetParameter("CnpjCpf", command.CnpjCpf));

            return new GenericCommandResult(true, userEntity is null ? UserMessage.UserNotFound : UserMessage.GetSucess, userEntity);
        }

        public async Task<ICommandResult> Handle(GetByIdUserCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, UserMessage.Validate, command.Notifications);

            var userEntity = await _userRepository.GetById(command.Id);

            return new GenericCommandResult(true, userEntity is null ? UserMessage.UserNotFound : UserMessage.GetSucess,
                userEntity is null ? string.Empty : userEntity);
        }
    }
}