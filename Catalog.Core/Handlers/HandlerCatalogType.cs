using Catalog.Core.Commands;
using Catalog.Core.Commands.Contracts;
using Catalog.Core.Entities;
using Catalog.Core.Handlers.Contracts;
using Catalog.Core.Repositories;
using Flunt.Notifications;

namespace Catalog.Core.Handlers
{
    public class HandlerCatalogType :
        Notifiable,
        IHandler<CommandAddType>,
        IHandler<CommandUpdateType>
    {
        public HandlerCatalogType(ICatalogTypeRepository repository)
        {
            _repository = repository;
        }

        private ICatalogTypeRepository _repository;

        public IResultCommand handle(CommandAddType command)
        {
            // Validação rapida
            command.Validate();
            if (command.Invalid)
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação",
                                                data: command.Notifications);

            // cria a entidade catalogType
            var type = new CatalogType(type: command.Type);

            // Salva os dados no repositorio e retorna false caso ocorrá algum erro
            if (!_repository.CreateTypeAsync(type: type))
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação, pois apresenta erro sistemico",
                                                data: null);

            // retorna successo para operação para adicionar um novo tipo para o produto
            return new GenericResultCommand(success: true,
                                            message: "Foi realizado o cadastro do tipo de produto com successo",
                                            data: null);
        }

        public IResultCommand handle(CommandUpdateType command)
        {
            // Validação rapida
            command.Validate();
            if (command.Invalid)
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação",
                                                data: command.Notifications);

            // localizar o tipo de produto e valida se foi localizado o tipo
            var type = _repository.CatalogTypeById(id: command.Id).Result;
            if (type == null)
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação, pois não foi localizado o tipo informado",
                                                data: null);

            // atualiza o tipo do produto
            type.UpdateCatalogType(type: command.Type);

            // Salva os dados no repositorio e retorna false caso ocorrá algum erro
            if (!_repository.UpdateTypeAsync(type: type))
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação, pois apresenta erro sistemico",
                                                data: null);
            // retorna successo para operação para adicionar um novo tipo para o produto
            return new GenericResultCommand(success: true,
                                            message: "Foi realizado a atualização do tipo de produto com successo",
                                            data: null);
        }
    }
}