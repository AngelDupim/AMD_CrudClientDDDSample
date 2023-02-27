using AMD_CrudClientDDDSample.Domain.Command.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Helper;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using Flunt.Notifications;
using Flunt.Validations;

namespace AMD_CrudClientDDDSample.Domain.Command.Client
{
    public class CreateClientCommand : Notifiable, ICommand
    {
        public string Name { get; private set; }
        public string CnpjCpf { get; private set; }
        public DateTime BirthDate { get; private set; }

        public CreateClientCommand(string name, string cnpjCpf, DateTime birthDate)
        {
            Name = name;
            CnpjCpf = cnpjCpf;
            BirthDate = birthDate.Date;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(Name, 3, "Name", ClientMessage.NameNotInforming)
                    .HasMaxLen(Name, 245, "Name", ClientMessage.NameMaxCharacter)
                    .HasMaxLen(CnpjCpf, 14, "CnpjCpf", ClientMessage.CnpjCpfMaxCharacter)
                    .AreEquals(true, CnpjCpf is not null ? ValidationsHelper.IsValidCPFCNPJ(CnpjCpf) :
                               false, "CnpjCpf", ClientMessage.CnpjCpfInvalid)
            );
        }
    }
}