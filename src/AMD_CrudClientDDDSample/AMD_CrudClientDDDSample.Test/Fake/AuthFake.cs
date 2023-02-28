using AMD_CrudClientDDDSample.Domain.Entity;

namespace AMD_CrudClientDDDSample.Test.Fake
{
    public class AuthFake
    {
        public static Auth TokenSucess() {
            return new Auth(true, DateTime.Now.ToString(), DateTime.Now.AddDays(1).ToString(),
                "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJqdGkiOiJmZmY3MmY2MzQ5YmU0NWMzOTg2ZmZiN2VmMDA2NjIwNSIsInN1YiI6IkFkbWluIiwicm9sZSI6IkFkbWluIiwibmJmIjoxNjc3NTMxNDMzLCJleHAiOjE2Nzc1Mzg2MzMsImlhdCI6MTY3NzUzMTQzMywiaXNzIjoiQU1EIn0.c2knSQQenlNyM4QlcX2VtCtg079TNozgRCL1y4R-lUk\"", "OK");
        }
    }
}