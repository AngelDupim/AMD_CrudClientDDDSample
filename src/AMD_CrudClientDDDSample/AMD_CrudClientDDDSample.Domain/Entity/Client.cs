namespace AMD_CrudClientDDDSample.Domain.Entity
{
    public class Client
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string CnpjCpf { get; private set; }
        public DateTime BirthDate { get; private set; }
        public DateTime RegisterDate { get; private set; }

        public Client(string name, string cnpjCpf, DateTime birthDate)
        {
            Id = Guid.NewGuid();
            Name = name;
            CnpjCpf = cnpjCpf;
            BirthDate = birthDate;
            RegisterDate = DateTime.Now;
        }

        public void UpdateName(string name)
        {
            Name = name;
        }
    }
}