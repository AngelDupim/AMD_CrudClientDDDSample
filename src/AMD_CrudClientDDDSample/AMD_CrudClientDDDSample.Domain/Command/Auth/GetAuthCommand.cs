using AMD_CrudClientDDDSample.Domain.Command.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using Flunt.Notifications;
using Flunt.Validations;

namespace AMD_CrudClientDDDSample.Domain.Command.Auth
{
    public class GetAuthCommand : Notifiable, ICommand
    {
        public string Name { get; private set; }
        public string Password { get; private set; }

        public GetAuthCommand(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public void Validate()
        {
            AddNotifications(
             new Contract()
             .Requires()
             .IsFalse(string.IsNullOrEmpty(Name), "name", AuthMessage.UserNotInformed)
             .IsFalse(string.IsNullOrEmpty(Password),"Password", AuthMessage.PasswordNotInformed)
            );
        }
    }
}