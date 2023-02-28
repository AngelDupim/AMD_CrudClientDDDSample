using AMD_CrudClientDDDSample.Domain.Command.Interfaces;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Helper;
using AMD_CrudClientDDDSample.Infrastructure.Shared.Messages;
using Flunt.Notifications;
using Flunt.Validations;

namespace AMD_CrudClientDDDSample.Domain.Command.Client
{
    public class GetByNameCnpjCpfClientCommand : Notifiable, ICommand
    {
        public string Name { get; private set; }
        public string CnpjCpf { get; private set; }

        public GetByNameCnpjCpfClientCommand(string name, string cnpjCpf)
        {
            Name = name;
            CnpjCpf = cnpjCpf;
        }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                .Requires()
                .AreEquals(false, string.IsNullOrEmpty(Name) && string.IsNullOrEmpty(CnpjCpf), "", ClientMessage.NameOrCnpjCpf)
                .AreEquals(false, !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(CnpjCpf), "", ClientMessage.NameOrCnpjCpf)
                .AreEquals(true, string.IsNullOrEmpty(CnpjCpf) ? true : ValidationsHelper.IsValidCPFCNPJ(CnpjCpf), "CnpjCpf", ClientMessage.CnpjCpfInvalid)
                );
        }

    }
}