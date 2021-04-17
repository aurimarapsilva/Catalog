using Catalog.Core.Commands.Contracts;

namespace Catalog.Core.Handlers.Contracts
{
    public interface IHandler<T> where T : ICommand
    {
        IResultCommand handle(T command);
    }
}