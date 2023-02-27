using AMD_CrudClientDDDSample.Domain.Command.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Helper;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using Flunt.Notifications;
using Flunt.Validations;

namespace AMD_CrudClientDDDSample.Domain.Command.User
{
    public class CreateUserCommand : Notifiable, ICommand
    {
        public string Name { get; private set; }
        public string Password { get; private set; }
        public string CnpjCpf { get; private set; }

        public CreateUserCommand(string name, string password, string cnpjCpf)
        {
            Name = name;
            Password = password;
            CnpjCpf = cnpjCpf;
        }

        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(Name, 3, "Name", UserMessage.UserNameNotInforming)
                .HasMaxLen(Name, 30, "Name", UserMessage.NameMaxCharacter)
                .HasMinLen(Password, 8, "Password", UserMessage.PasswordMinCharacter)
                .HasMaxLen(CnpjCpf, 14, "CnpjCpf", UserMessage.CnpjCpfMaxCharacter)
                .AreEquals(true, CnpjCpf is not null ? ValidationsHelper.IsValidCPFCNPJ(CnpjCpf) : false, "CnpjCpf",
                UserMessage.CnpjCpfInvalid)
            );
        }

    }
}