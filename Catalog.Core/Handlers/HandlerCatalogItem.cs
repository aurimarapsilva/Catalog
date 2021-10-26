using Catalog.Core.Commands;
using Catalog.Core.Entities;
using Catalog.Core.Handlers.Contracts;
using Catalog.Core.Repositories;
using Catalog.Core.Response;
using System;

namespace Catalog.Core.Handlers
{
    /// <summary>
    /// Classe manipuladora dos comandos realizados por parte dos produtos
    /// </summary>
    public class HandlerCatalogItem :
       IHandler<CommandAddItem>,
       IHandler<CommandRegisterItem>,
       IHandler<CommandRemoveItem>,
       IHandler<CommandUpdateItem>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="repository"></param>
        public HandlerCatalogItem(ICatalogItemRepository repository)
        {
            _repository = repository;
        }

        private readonly ICatalogItemRepository _repository;

        /// <summary>
        /// Manipulador do comando para adicionar um item no estoque
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IResponse handle(CommandAddItem command)
        {
            // validação rapida
            command.Validate();
            if (command.Invalid)
                return new ResponseError<CommandAddItem>(
                    errorCode: "400",
                    description: "Não foi possível prosseguir com a solicitação",
                    data: command.Notifications
                );

            // localiza o item e verifica se achou algo 
            var item = _repository.ItemByIdAsync(id: command.Id).Result;
            if (item == null)
                return new ResponseError<CommandAddItem>(
                    errorCode: "404",
                    description: "Não foi possível prosseguir com a solicitação, pois não foi localizado nenhum item",
                    data: null
                );

            // faz a adição do novo item ao estoque e valida se houve sucesso
            try
            {
                if (command.Quantity != item.AddStock(quantity: command.Quantity))
                    return new ResponseError<CommandAddItem>(
                        errorCode: "400",
                        description: "Não foi possível prosseguir com a solicitação, verificar a quantidade de itens que o estoque pode receber, e o que ja tem cadastrado",
                        data: null
                    );
            }
            catch(Exception e)
            {
                return new ResponseError<CommandAddItem>(
                    errorCode: "400",
                    description: "Não foi possível prosseguir com a solicitação, verificar a quantidade de itens que o estoque pode receber, e o que ja tem cadastrado",
                    data: e.Message
                );
            }
            

            // faz a persistencia dos dados no banco de dados e retorna false caso apresentar algum erro
            if (!_repository.UpdateProductAsync(product: item))
                return new ResponseError<CommandAddItem>(
                                    errorCode: "400",
                                    description: "Não foi possível prosseguir com a solicitação, pois foi apresentado um erro sistemico",
                                    data: null
                                );

            // retorna sucesso na operação para adicionar um item no estoque de produto
            return new ResponseOk<CatalogItem>(item);
        }

        /// <summary>
        /// Manipulador do comando que registra um novo produto no banco de dados
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IResponse handle(CommandRegisterItem command)
        {
            // validação rapida
            command.Validate();
            if (command.Invalid)
                return new ResponseError<CommandRegisterItem>(
                    errorCode: "400",
                    description: "Não foi possível prosseguir com a solicitação",
                    data: command.Notifications
                );

            var item = new CatalogItem(
                    name: command.Name,
                    description: command.Description,
                    price: command.Price,
                    catalogTypeId: command.CatalogTypeId,
                    catalogBrandId: command.CatalogBrandId,
                    restockThreshold: command.RestockThreshold,
                    maxStockThreshold: command.MaxStockThreshold
                );

            // faz a persistencia dos dados no banco de dados e retorna false caso apresentar algum erro
            if (!_repository.CreateProductAsync(product: item))
                return new ResponseError<CommandRegisterItem>(
                    errorCode: "400",
                    description: "Não foi possível prosseguir com a solicitação, pois foi apresentado um erro sistemico",
                    data: null
                );

            // retorna sucesso na operação de cadastro de um novo item ana base de dados
            return new ResponseOk<CatalogItem>(item);
        }

        /// <summary>
        /// Manipulador do comando que remove um item do estoque
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public IResponse handle(CommandRemoveItem command)
        {
            // validação rapida
            command.Validate();
            if (command.Invalid)
                return new ResponseError<CommandRemoveItem>(
                    errorCode: "400",
                    description: "Não foi possível prosseguir com a solicitação",
                    data: command.Notifications
                );

            // localiza o item e verifica se achou algo 
            var item = _repository.ItemByIdAsync(id: command.Id).Result;
            if (item == null)
                return new ResponseError<CommandRemoveItem>(
                    errorCode: "404",
                    description: "Não foi possível prosseguir com a solicitação",
                    data: "Nenhum item encontrado"
                );

            // faz a remoção do item no estoque e salva em uma variavel o valor da remoção para validação futura
            try
            {
                if (item.RemoveStock(quantityDesired: command.QuantityDesired) != command.QuantityDesired)
                    return new ResponseError<CommandRemoveItem>(
                        errorCode: "400",
                        description: "Não foi possível prosseguir com a solicitação",
                        data: "Não possui a quantidade desejada em estoque"
                    );
            }
            catch (Exception e)
            {
                return new ResponseError<CommandRemoveItem>(
                    errorCode: "400",
                    description: "Não foi possível prosseguir com a solicitação",
                    data: e
                );
            }

            // faz a persistencia dos dados no banco de dados e retorna false caso apresentar algum erro
            if (!_repository.UpdateProductAsync(product: item))
                return new ResponseError<CommandRemoveItem>(
                    errorCode: "400",
                    description: "Não foi possível prosseguir com a solicitação",
                    data: "Erro sistemico"
                );

            // retorna sucesso na operação para remover um item no estoque de produto
            return new ResponseOk<CatalogItem>(item);
        }

        public IResponse handle(CommandUpdateItem command)
        {
            // validação rapida
            command.Validate();
            if (command.Invalid)
                return new ResponseError<CommandUpdateItem>(
                    errorCode: "400",
                    description: "Não foi possível prosseguir com a solicitação",
                    data: command.Notifications
                );

            // localiza o item e verifica se achou algo 
            var item = _repository.ItemByIdAsync(id: command.Id).Result;
            if (item == null)
                return new ResponseError<CommandUpdateItem>(
                    errorCode: "404",
                    description: "Não foi possível prosseguir com a solicitação",
                    data: "Nenhum item encontrado"
                );

            // valida se o preço do produto foi alterado
            var raiseProductPriceChangedEvent = command.Price != item.Price;

            // atualiza os itens do produto
            item.UpdateCatalogItem(name: command.Name,
                                    description: command.Description,
                                    price: command.Price,
                                    catalogTypeId: command.CatalogTypeId,
                                    catalogBrandId: command.CatalogBrandId,
                                    restockThreshold: command.RestockThreshold,
                                    maxStockThreshold: command.MaxStockThreshold);

            // faz a persistencia dos dados no banco de dados e retorna false caso apresentar algum erro
            if (!_repository.UpdateProductAsync(
                    product: item,
                    raiseProductPriceChangedEvent: raiseProductPriceChangedEvent
                ))
                return new ResponseError<CommandUpdateItem>(
                    errorCode: "400",
                    description: "Não foi possível prosseguir com a solicitação",
                    data: "Erro sistemico"
                );

            // retorna sucesso na operação para remover um item no estoque de produto
            return new ResponseOk<CatalogItem>(item);
        }
    }
}