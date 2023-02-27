using AMD_CrudClientDDDSample.Application.Command.Auth;
using AMD_CrudClientDDDSample.Domain.Command.Auth;
using AutoMapper;

namespace AMD_CrudClientDDDSample.Application.Mapper.Profiles
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<GetAuthCommandApplication, GetAuthCommand>()
            .ConstructUsing(c => new GetAuthCommand(c.Name, c.Password));
        }
    }
}