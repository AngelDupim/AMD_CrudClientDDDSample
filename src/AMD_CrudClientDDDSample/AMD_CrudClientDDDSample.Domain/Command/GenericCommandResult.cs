using AMD_CrudClientDDDSample.Domain.Command.Interfaces;

namespace AMD_CrudClientDDDSample.Domain.Command
{
    public class GenericCommandResult : ICommandResult
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public object? Data { get; private set; }

        public GenericCommandResult(bool success, string message, object? data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}