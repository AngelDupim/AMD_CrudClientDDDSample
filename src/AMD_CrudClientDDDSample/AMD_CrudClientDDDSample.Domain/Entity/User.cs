using AMD_CrudClientDDDSample.Domain.Enum;

namespace AMD_CrudClientDDDSample.Domain.Entity
{
    public class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Password { get; private set; }
        public string CnpjCpf { get; private set; }
        public bool IsActive { get; private set; }
        public string Role { get; private set; }

        public User(string userName, string password, string cnpjCpf)
        {
            Id = Guid.NewGuid();
            Name = userName;
            Password = password;
            CnpjCpf = cnpjCpf;
            IsActive = true;
            Role = RoleEnum.User.ToString();
        }

        public void DeleteUser()
        {
            IsActive = false;
        }
    }
}