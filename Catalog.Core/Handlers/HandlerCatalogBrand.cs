using Catalog.Core.Commands;
using Catalog.Core.Commands.Contracts;
using Catalog.Core.Entities;
using Catalog.Core.Handlers.Contracts;
using Catalog.Core.Repositories;
using Flunt.Notifications;

namespace Catalog.Core.Handlers
{
    public class HandlerCatalogBrand :
        Notifiable,
        IHandler<CommandAddBrand>,
        IHandler<CommandUpdateBrand>
    {
        public HandlerCatalogBrand(ICatalogBrandRepository repository)
        {
            _repository = repository;
        }

        private readonly ICatalogBrandRepository _repository;

        public IResultCommand handle(CommandAddBrand command)
        {
            // validação rapida
            command.Validate();
            if (command.Invalid)
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação",
                                                data: command.Notifications);

            // criar a entidade de marcas
            var brand = new CatalogBrand(brand: command.Brand);

            // faz a persistencia dos dados no banco de dados e retorna false caso apresentar algum erro
            if (!_repository.CreateBrandAsync(brand: brand))
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação, pois foi apresentado um erro sistemico",
                                                data: null);

            // retorna sucesso na operação para adicionar um item no estoque de produto
            return new GenericResultCommand(success: true,
                                            message: $"a nova marca foi adicionada com sucesso!",
                                            data: brand);
        }

        public IResultCommand handle(CommandUpdateBrand command)
        {
            // validação rapida
            command.Validate();
            if (command.Invalid)
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação",
                                                data: command.Notifications);

            // localiza uma marca e verifica se obteve sucesso na localização
            var brand = _repository.CatalogBrandIdAsync(id: command.Id).Result;
            if (brand == null)
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação, pois não foi localizado a marca desejada",
                                                data: command.Notifications);

            // edita os dados da marca do produto
            brand.UpdateCatalogBrand(brand: command.Brand);

            // faz a persistencia dos dados no banco de dados e retorna false caso apresentar algum erro
            if (!_repository.UpdateBrandAsync(brand: brand))
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação, pois foi apresentado um erro sistemico",
                                                data: null);

            // retorna sucesso na operação para adicionar um item no estoque de produto
            return new GenericResultCommand(success: true,
                                            message: $"a marca foi atualizada com sucesso!",
                                            data: brand);
        }
    }
}