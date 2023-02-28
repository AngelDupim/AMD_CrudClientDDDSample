using AMD_CrudClientDDDSample.Application.Mapper.Profiles;
using AutoMapper;

namespace AMD_CrudClientDDDSample.Application.Mapper
{
    public class MapperConfig
    {
        public static IMapper RegisterMapper()
        {
            return new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ClientProfile());
                mc.AddProfile(new UserProfile());
                mc.AddProfile(new AuthProfile());

            }).CreateMapper();
        }
    }
}