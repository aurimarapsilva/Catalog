using Catalog.Core.Commands.Contracts;
using Catalog.Core.Response;

namespace Catalog.Core.Handlers.Contracts
{
    /// <summary>
    /// Interface responsáveis pelo manipuladores dos comandos recebidos
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHandler<T> where T : ICommand
    {
        /// <summary>
        /// Retorna se teve sucesso na solicitação ou não
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        IResponse handle(T command);
    }
}