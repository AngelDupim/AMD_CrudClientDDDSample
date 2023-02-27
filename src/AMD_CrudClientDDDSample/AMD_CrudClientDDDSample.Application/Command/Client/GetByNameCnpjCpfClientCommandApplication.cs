using AMD_CrudClientDDDSample.Application.Command.Interfaces;

namespace AMD_CrudClientDDDSample.Application.Command.Client
{
    public class GetByNameCnpjCpfClientCommandApplication : ICommandApplication
    {
        public string? Name { get; private set; }
        public string? CnpjCpf { get; private set; }

        public GetByNameCnpjCpfClientCommandApplication(string? name, string? cnpjCpf)
        {
            Name = name;
            CnpjCpf = cnpjCpf;
        }
    }
}