using AMD_CrudClientDDDSample.Domain.Command.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Helper;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using Flunt.Notifications;
using Flunt.Validations;

namespace AMD_CrudClientDDDSample.Domain.Command.Client
{
    public class UpdateClientCommand : Notifiable, ICommand
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public UpdateClientCommand(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(Name, 3, "Name", ClientMessage.NameNotInforming)
                    .HasMaxLen(Name, 245, "Name", ClientMessage.NameMaxCharacter)
                    .AreEquals(false, ValidationsHelper.IsValidGuid(Id), "id", ClientMessage.IdInvalid)
            );
        }
    }
}