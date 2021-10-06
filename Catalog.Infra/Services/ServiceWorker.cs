using catalog.infra.DataContext;
using catalog.infra.RabbitMQ;
using catalog.infra.Repositories;
using Catalog.Core.Commands;
using Catalog.Core.Handlers;
using Catalog.Core.Handlers.Contracts;
using Catalog.Core.Repositories;
using Catalog.Core.Response;
using System;

namespace catalog.infra.Services
{
    public class ServiceWorker : IServiceWorker
    {

        public IResponse AddBrand(string msg)
        {
            StoreDataContext dataContext = new StoreDataContext();
            DatabaseComunicator comunicador = new DatabaseComunicator(dataContext);
            ICatalogBrandRepository repository = new CatalogBrandRepository(comunicador);
            CommandAddBrand command = new CommandAddBrand(
                brand: TratarMenssagem.RecuperarValor(msg, "brand")
                );
            IHandler<CommandAddBrand> handler = new HandlerCatalogBrand(repository);

            return handler.handle(command);
        }

        public IResponse AddItem(string msg)
        {
            StoreDataContext dataContext = new StoreDataContext();
            DatabaseComunicator comunicador = new DatabaseComunicator(dataContext);
            ICatalogItemRepository repository = new CatalogItemRepository(comunicador);
            CommandAddItem command = new CommandAddItem(
                id: Convert.ToInt32(TratarMenssagem.RecuperarValor(msg, "id")), 
                quantity: Convert.ToDecimal(TratarMenssagem.RecuperarValor(msg, "quantity"))
                );
            IHandler<CommandAddItem> handler = new HandlerCatalogItem(repository);

            return handler.handle(command);
        }

        public IResponse AddType(string msg)
        {
            StoreDataContext dataContext = new StoreDataContext();
            DatabaseComunicator comunicador = new DatabaseComunicator(dataContext);
            ICatalogTypeRepository repository = new CatalogTypeRepository(comunicador);
            CommandAddType command = new CommandAddType(
                type: TratarMenssagem.RecuperarValor(msg, "type")
                );
            IHandler<CommandAddType> handler = new HandlerCatalogType(repository);

            return handler.handle(command);
        }
        public IResponse RegisterItem(string msg)
        {
            StoreDataContext dataContext = new StoreDataContext();
            DatabaseComunicator comunicador = new DatabaseComunicator(dataContext);
            ICatalogItemRepository repository = new CatalogItemRepository(comunicador);
            CommandRegisterItem command = new CommandRegisterItem(
                name: TratarMenssagem.RecuperarValor(msg, "name"),
                description: TratarMenssagem.RecuperarValor(msg, "description"),
                price: Convert.ToDecimal(TratarMenssagem.RecuperarValor(msg, "price")),
                catalogTypeId: Convert.ToInt32(TratarMenssagem.RecuperarValor(msg, "catalogTypeId")),
                catalogBrandId: Convert.ToInt32(TratarMenssagem.RecuperarValor(msg, "catalogBrandId")),
                restockThreshold: Convert.ToInt32(TratarMenssagem.RecuperarValor(msg, "restockThreshold")),
                maxStockThreshold: Convert.ToInt32(TratarMenssagem.RecuperarValor(msg, "maxStockThreshold"))
                );
            IHandler<CommandRegisterItem> handler = new HandlerCatalogItem(repository);

            return handler.handle(command);
        }
        public IResponse RemoveItem(string msg)
        {
            StoreDataContext dataContext = new StoreDataContext();
            DatabaseComunicator comunicador = new DatabaseComunicator(dataContext);
            ICatalogItemRepository repository = new CatalogItemRepository(comunicador);
            CommandRemoveItem command = new CommandRemoveItem(
                id: Convert.ToInt32(TratarMenssagem.RecuperarValor(msg, "id")),
                quantityDesired: Convert.ToDecimal(TratarMenssagem.RecuperarValor(msg, "quantityDesired"))
                );
            IHandler<CommandRemoveItem> handler = new HandlerCatalogItem(repository);

            return handler.handle(command);
        }
        public IResponse UpdateBrand(string msg)
        {
            StoreDataContext dataContext = new StoreDataContext();
            DatabaseComunicator comunicador = new DatabaseComunicator(dataContext);
            ICatalogBrandRepository repository = new CatalogBrandRepository(comunicador);
            CommandUpdateBrand command = new CommandUpdateBrand(
                id: Convert.ToInt32(TratarMenssagem.RecuperarValor(msg, "id")),
                brand: TratarMenssagem.RecuperarValor(msg, "brand")
                );
            IHandler<CommandUpdateBrand> handler = new HandlerCatalogBrand(repository);

            return handler.handle(command);
        }
        public IResponse UpdateItem(string msg)
        {
            StoreDataContext dataContext = new StoreDataContext();
            DatabaseComunicator comunicador = new DatabaseComunicator(dataContext);
            ICatalogItemRepository repository = new CatalogItemRepository(comunicador);
            CommandUpdateItem command = new CommandUpdateItem(
                id: Convert.ToInt32(TratarMenssagem.RecuperarValor(msg, "id")),
                name: TratarMenssagem.RecuperarValor(msg, "name"),
                description: TratarMenssagem.RecuperarValor(msg, "description"),
                price: Convert.ToDecimal(TratarMenssagem.RecuperarValor(msg, "price")),
                catalogTypeId: Convert.ToInt32(TratarMenssagem.RecuperarValor(msg, "catalogTypeId")),
                catalogBrandId: Convert.ToInt32(TratarMenssagem.RecuperarValor(msg, "catalogBrandId")),
                restockThreshold: Convert.ToInt32(TratarMenssagem.RecuperarValor(msg, "restockThreshold")),
                maxStockThreshold: Convert.ToInt32(TratarMenssagem.RecuperarValor(msg, "maxStockThreshold"))
                );
            IHandler<CommandUpdateItem> handler = new HandlerCatalogItem(repository);

            return handler.handle(command);
        }
        public IResponse UpdateType(string msg)
        {
            StoreDataContext dataContext = new StoreDataContext();
            DatabaseComunicator comunicador = new DatabaseComunicator(dataContext);
            ICatalogTypeRepository repository = new CatalogTypeRepository(comunicador);
            CommandUpdateType command = new CommandUpdateType(
                id: Convert.ToInt32(TratarMenssagem.RecuperarValor(msg, "id")),
                type: TratarMenssagem.RecuperarValor(msg, "type")
                );
            IHandler<CommandUpdateType> handler = new HandlerCatalogType(repository);

            return handler.handle(command);
        }

    }
}
