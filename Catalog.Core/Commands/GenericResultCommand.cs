using Catalog.Core.Commands.Contracts;

namespace Catalog.Core.Commands
{
    public class GenericResultCommand : IResultCommand
    {
        public GenericResultCommand() { }
        public GenericResultCommand(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}