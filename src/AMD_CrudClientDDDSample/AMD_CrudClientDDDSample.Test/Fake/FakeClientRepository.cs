using AMD_CrudClientDDDSample.Domain.Entity;

namespace AMD_CrudClientDDDSample.Test.Repository
{
    public class FakeClientRepository
    {
        public static Client FakeClient(bool isPF)
        {
            if (isPF)
                return new Client("Fake", "13586155000", DateTime.Now.Date);
            else
                return new Client("FakePJ", "67700676000169", DateTime.Now.Date);
        }

        public static List<Client> ListFakeClient()
        {
            return new List<Client>()
            {
                new Client("Fake", "13586155000", DateTime.Now.Date),
                new Client("FakePJ", "67700676000169", DateTime.Now.Date)
            };
        }
    }
}