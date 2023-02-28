using AMD_CrudClientDDDSample.Application.Command.Interfaces;

namespace AMD_CrudClientDDDSample.Application.Command
{
    public class CommandResultModel : ICommandResultModel
    {
        public bool Success { get; private set; }
        public string Message { get; private set; }
        public object Data { get; private set; }

        public CommandResultModel(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}