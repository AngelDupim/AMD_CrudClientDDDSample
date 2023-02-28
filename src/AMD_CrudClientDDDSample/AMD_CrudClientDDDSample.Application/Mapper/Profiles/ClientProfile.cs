using AMD_CrudClientDDDSample.Application.Command;
using AMD_CrudClientDDDSample.Application.Command.Client;
using AMD_CrudClientDDDSample.Domain.Command;
using AMD_CrudClientDDDSample.Domain.Command.Client;
using AutoMapper;

namespace AMD_CrudClientDDDSample.Application.Mapper.Profiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            CreateMap<CreateClientCommandApplication, CreateClientCommand>()
                .ConstructUsing(c => new CreateClientCommand(c.Name, c.CnpjCpf, c.BirthDay));

            CreateMap<UpdateClientCommandApplication, UpdateClientCommand>()
                .ConstructUsing(c => new UpdateClientCommand(c.Id, c.Name));

            CreateMap<GetByNameCnpjCpfClientCommandApplication, GetByNameCnpjCpfClientCommand>()
                .ConstructUsing(c => new GetByNameCnpjCpfClientCommand(c.Name, c.CnpjCpf));

            CreateMap<GetByIdClientCommandApplication, GetByIdClientCommand>()
                .ConstructUsing(c => new GetByIdClientCommand(c.Id));

            CreateMap<GenericCommandResult, CommandResultModel>()
                .ConstructUsing(c => new CommandResultModel(c.Success, c.Message, c.Data));
        }
    }
}