using AMD_CrudClientDDDSample.Application.Command.Interfaces;

namespace AMD_CrudClientDDDSample.Application.Command.User
{
    public class CreateUserCommandApplication : ICommandApplication
    {
        public string Name { get; private set; }
        public string Password { get; private set; }
        public string CnpjCpf { get; private set; }

        public CreateUserCommandApplication(string name, string password, string cnpjCpf)
        {
            Name = name;
            Password = password;
            CnpjCpf = cnpjCpf;
        }

    }
}