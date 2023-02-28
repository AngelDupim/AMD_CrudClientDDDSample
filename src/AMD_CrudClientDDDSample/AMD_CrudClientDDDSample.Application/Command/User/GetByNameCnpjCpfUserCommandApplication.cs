using AMD_CrudClientDDDSample.Application.Command.Interfaces;

namespace AMD_CrudClientDDDSample.Application.Command.User
{
    public class GetByNameCnpjCpfUserCommandApplication : ICommandApplication
    {
        public string Name { get; private set; }
        public string CnpjCpf { get; private set; }

        public GetByNameCnpjCpfUserCommandApplication(string name, string cnpjCpf)
        {
            Name = name;
            CnpjCpf = cnpjCpf;
        }
    }
}