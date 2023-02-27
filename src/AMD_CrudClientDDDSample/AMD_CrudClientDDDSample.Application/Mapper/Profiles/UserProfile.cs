using AMD_CrudClientDDDSample.Application.Command.User;
using AMD_CrudClientDDDSample.Domain.Command.User;
using AutoMapper;

namespace AMD_CrudClientDDDSample.Application.Mapper.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserCommandApplication, CreateUserCommand>()
                .ConstructUsing(c => new CreateUserCommand(c.Name, c.Password, c.CnpjCpf));

            CreateMap<DeleteUserCommandApplication, DeleteUserCommand>()
                .ConstructUsing(c => new DeleteUserCommand(c.Id));

            CreateMap<GetByNameCnpjCpfUserCommandApplication, GetByNameCnpjCpfUserCommand>()
                .ConstructUsing(c => new GetByNameCnpjCpfUserCommand(c.Name, c.CnpjCpf));

            CreateMap<GetByIdUserCommandApplication, GetByIdUserCommand>()
                .ConstructUsing(c => new GetByIdUserCommand(c.Id));
        }
    }
}