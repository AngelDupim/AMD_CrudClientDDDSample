using AMD_CrudClientDDDSample.Application.Command.Interfaces;

namespace AMD_CrudClientDDDSample.Application.Command.Auth
{
    public class GetAuthCommandApplication : ICommandApplication
    {
        public string Name { get; private set; }
        public string Password { get; private set; }

        public GetAuthCommandApplication(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}