using AMD_CrudClientDDDSample.Domain.Command.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Helper;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using Flunt.Notifications;
using Flunt.Validations;

namespace AMD_CrudClientDDDSample.Domain.Command.User
{
    public class GetByIdUserCommand : Notifiable, ICommand
    {
        public Guid Id { get; private set; }
        public GetByIdUserCommand(Guid id)
        {
            Id = id;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .AreEquals(false, ValidationsHelper.IsValidGuid(Id), "id", UserMessage.IdInvalid)
            );
        }
    }
}