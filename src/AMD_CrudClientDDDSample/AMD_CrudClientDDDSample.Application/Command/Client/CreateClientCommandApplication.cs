using AMD_CrudClientDDDSample.Application.Command.Interfaces;

namespace AMD_CrudClientDDDSample.Application.Command.Client
{
    public class CreateClientCommandApplication : ICommandApplication
    {
        public string Name { get; private set; }
        public string CnpjCpf { get; private set; }
        public DateTime BirthDay { get; private set; }

        public CreateClientCommandApplication(string name, string cnpjCpf, DateTime birthDay)
        {
            Name = name;
            CnpjCpf = cnpjCpf;
            BirthDay = birthDay.Date;
        }
    }
}