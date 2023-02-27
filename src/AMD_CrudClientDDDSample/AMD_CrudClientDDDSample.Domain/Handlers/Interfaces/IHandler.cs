using AMD_CrudClientDDDSample.Domain.Command.Interfaces;

namespace AMD_CrudClientDDDSample.Domain.Handlers.Interfaces
{
    public interface IHandler<T> where T : ICommand
    {
        Task<ICommandResult> Handle(T command);
    }
}