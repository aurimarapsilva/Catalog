using Catalog.Core.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Handlers.Contracts;
using Catalog.Core.Repositories;
using Catalog.Core.Response;
using Flunt.Notifications;

namespace Catalog.Core.Handlers
{
    /// <summary>
    /// Manipulador dos comandos de tipo de produtos
    /// </summary>
    public class HandlerCatalogType :
        Notifiable,
        IHandler<CommandAddType>,
        IHandler<CommandUpdateType>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public HandlerCatalogType(ICatalogTypeRepository repository)
        {
            _repository = repository;
        }

        private ICatalogTypeRepository _repository;

        /// <summary>
        /// Método responsável por manipular a adição uma tipo ao repositorio
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IResponse handle(CommandAddType command)
        {
            // Validação rapida
            command.Validate();
            if (command.Invalid)
                return new ResponseError<CommandAddType>(
                    errorCode: "400",
                    description: "Não foi possível prosseguir com a solicitação",
                    data: command.Notifications
                );

            // cria a entidade catalogType
            var type = new CatalogType(type: command.Type);

            // Salva os dados no repositorio e retorna false caso ocorrá algum erro
            if (!_repository.CreateTypeAsync(type: type))
                return new ResponseError<CommandAddType>(
                        errorCode: "400",
                        description: "Não foi possível prosseguir com a solicitação",
                        data: "Erro sistemico"
                    );

            // retorna successo para operação para adicionar um novo tipo para o produto
            return new ResponseOk<CatalogType>(responseObj: type);
        }

        /// <summary>
        /// Método responsável por manipular a Atualização de um tipo de produto
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IResponse handle(CommandUpdateType command)
        {
            // Validação rapida
            command.Validate();
            if (command.Invalid)
                return new ResponseError<CommandUpdateType>(
                    errorCode: "400",
                    description: "Não foi possível prosseguir com a solicitação",
                    data: command.Notifications
                );

            // localizar o tipo de produto e valida se foi localizado o tipo
            var type = _repository.CatalogTypeById(id: command.Id).Result;
            if (type == null)
                return new ResponseError<CommandUpdateType>(
                    errorCode: "404",
                    description: "Não foi possível prosseguir com a solicitação",
                    data: "Nenhum item encontrado"
                );

            // atualiza o tipo do produto
            type.UpdateCatalogType(type: command.Type);

            // Salva os dados no repositorio e retorna false caso ocorrá algum erro
            if (!_repository.UpdateTypeAsync(type: type))
                return new ResponseError<CommandAddType>(
                        errorCode: "400",
                        description: "Não foi possível prosseguir com a solicitação",
                        data: "Erro sistemico"
                    );

            // retorna successo para operação para adicionar um novo tipo para o produto
            return new ResponseOk<CatalogType>(responseObj: type);
        }
    }
}