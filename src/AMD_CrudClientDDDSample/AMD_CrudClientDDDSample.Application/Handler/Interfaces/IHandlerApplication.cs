using AMD_CrudClientDDDSample.Application.Command.Interfaces;

namespace AMD_CrudClientDDDSample.Application.Handler.Interfaces
{
    public interface IHandlerApplication<T> where T : ICommandApplication
    {
        Task<ICommandResultModel> Handle(T command);
    }
}