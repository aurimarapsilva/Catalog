using Catalog.Core.Commands;
using Catalog.Core.Commands.Contracts;
using Catalog.Core.Entities;
using Catalog.Core.Handlers.Contracts;
using Catalog.Core.Repositories;
using Flunt.Notifications;

namespace Catalog.Core.Handlers
{
    public class HandlerCatalogItem :
       Notifiable,
       IHandler<CommandAddItem>,
       IHandler<CommandRegisterItem>,
       IHandler<CommandRemoveItem>,
       IHandler<CommandUpdateItem>
    {
        public HandlerCatalogItem(ICatalogItemRepository repository)
        {
            _repository = repository;
        }

        private readonly ICatalogItemRepository _repository;

        public IResultCommand handle(CommandAddItem command)
        {
            // validação rapida
            command.Validate();
            if (command.Invalid)
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação",
                                                data: command.Notifications);

            // localiza o item e verifica se achou algo 
            var item = _repository.ItemByIdAsync(id: command.Id).Result;
            if (item == null)
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação, pois não foi localizado nenhum item",
                                                data: null);

            // faz a adição do novo item ao estoque e valida se houve sucesso
            if (command.Quantity != item.AddStock(quantity: command.Quantity))
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação, verificar a quantidade de itens que o estoque pode receber, e o que ja tem cadastrado",
                                                data: item);

            // faz a persistencia dos dados no banco de dados e retorna false caso apresentar algum erro
            if (!_repository.UpdateProductAsync(product: item))
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação, pois foi apresentado um erro sistemico",
                                                data: null);

            // retorna sucesso na operação para adicionar um item no estoque de produto
            return new GenericResultCommand(success: true,
                                            message: $"O valor de {command.Quantity}, foi adicionado no estoque com sucesso",
                                            data: item);
        }

        public IResultCommand handle(CommandRegisterItem command)
        {
            // validação rapida
            command.Validate();
            if (command.Invalid)
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação",
                                                data: command.Notifications);

            var item = new CatalogItem(name: command.Name,
                                        description: command.Description,
                                        price: command.Price,
                                        catalogTypeId: command.CatalogTypeId,
                                        catalogBrandId: command.CatalogBrandId,
                                        restockThreshold: command.RestockThreshold,
                                        maxStockThreshold: command.MaxStockThreshold,
                                        pictureFileName: command.PictureFileName);

            // faz a persistencia dos dados no banco de dados e retorna false caso apresentar algum erro
            if (!_repository.CreateProductAsync(product: item))
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação, pois foi apresentado um erro sistemico",
                                                data: null);

            // retorna sucesso na operação de cadastro de um novo item ana base de dados
            return new GenericResultCommand(success: true,
                                            message: "O item foi cadastrado com sucesso",
                                            data: item);
        }

        public IResultCommand handle(CommandRemoveItem command)
        {
            // validação rapida
            command.Validate();
            if (command.Invalid)
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação",
                                                data: command.Notifications);

            // localiza o item e verifica se achou algo 
            var item = _repository.ItemByIdAsync(id: command.Id).Result;
            if (item == null)
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação, pois não foi localizado nenhum item",
                                                data: null);

            // faz a adição do novo item ao estoque e valida se houve sucesso
            if (command.QuantityDesired != item.RemoveStock(quantityDesired: command.QuantityDesired))
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação, verificar a quantidade de itens que o estoque pode receber, e o que ja tem cadastrado",
                                                data: null);

            // faz a persistencia dos dados no banco de dados e retorna false caso apresentar algum erro
            if (!_repository.UpdateProductAsync(product: item))
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação, pois foi apresentado um erro sistemico",
                                                data: null);

            // retorna sucesso na operação para remover um item no estoque de produto
            return new GenericResultCommand(success: true,
                                            message: $"O valor de {command.QuantityDesired}, foi retirado do seu estoque com sucesso",
                                            data: item);
        }

        public IResultCommand handle(CommandUpdateItem command)
        {
            // validação rapida
            command.Validate();
            if (command.Invalid)
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação",
                                                data: command.Notifications);

            // localiza o item e verifica se achou algo 
            var item = _repository.ItemByIdAsync(id: command.Id).Result;
            if (item == null)
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação, pois não foi localizado nenhum item",
                                                data: null);

            // valida se o preço do produto foi alterado
            var raiseProductPriceChangedEvent = command.Price != item.Price;

            // atualiza os itens do produto
            item.UpdateCatalogItem(name: command.Name,
                                    description: command.Description,
                                    price: command.Price,
                                    catalogTypeId: command.CatalogTypeId,
                                    catalogBrandId: command.CatalogBrandId,
                                    restockThreshold: command.RestockThreshold,
                                    maxStockThreshold: command.MaxStockThreshold,
                                    pictureFileName: command.PictureFileName);

            // faz a persistencia dos dados no banco de dados e retorna false caso apresentar algum erro
            if (!_repository.UpdateProductAsync(product: item,
                                                raiseProductPriceChangedEvent: raiseProductPriceChangedEvent))
                return new GenericResultCommand(success: false,
                                                message: "Não foi possível prosseguir com a solicitação, pois foi apresentado um erro sistemico",
                                                data: null);

            // retorna sucesso na operação para remover um item no estoque de produto
            return new GenericResultCommand(success: true,
                                            message: $"o produto foi alterado com sucesso",
                                            data: item);
        }
    }
}