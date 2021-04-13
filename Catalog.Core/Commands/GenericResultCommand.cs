using Catalog.Core.Commands.Contracts;

namespace Catalog.Core.Commands
{
    public class GenericResultCommand : IResultCommand
    {
        public GenericResultCommand() { }
        public GenericResultCommand(bool sucess, string message, object data)
        {
            Sucess = sucess;
            Message = message;
            Data = data;
        }

        public bool Sucess { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}