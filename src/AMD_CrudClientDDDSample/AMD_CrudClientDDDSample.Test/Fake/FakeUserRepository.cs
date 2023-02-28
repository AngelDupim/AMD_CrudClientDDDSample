using AMD_CrudClientDDDSample.Domain.Entity;

namespace AMD_CrudClientDDDSample.Test.Repository
{
    public class FakeUserRepository
    {
        public static User FakeUser(bool isPF)
        {
            if (isPF)
                return new User("Fake", "Teste123", "13586155000");
            else
                return new User("FakePJ", "Teste123", "67700676000169");
        }

        public static List<User> ListFakeUser()
        {
            return new List<User>()
            {
                new User("Fake", "Teste123", "13586155000"),
                new User("FakePJ", "Teste123", "67700676000169")
            };
        }
    }
}