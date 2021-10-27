using System;
using Catalog.Core.Commands;
using Catalog.Core.Handlers;
using Catalog.Core.Handlers.Contracts;
using Catalog.Core.Repositories;
using Catalog.Core.Response;
using catalog.infra.RabbitMQ;
using catalog.infra.Services;
using catalog.test.Core.Repositories;

namespace catalog.test.Worker
{
    public class ServiceWorker : IServiceWorker
    {
        public IResponse BeginTransaction(string msg)
        {
            var typeTransaction = Message.FindValue(msg, "TipoTransacao");

            switch (typeTransaction.ToUpper())
            {
                case "ADDBRAND":
                    return AddBrand(msg);
                case "ADDITEM":
                    return AddItem(msg);
                case "ADDTYPE":
                    return AddType(msg);
                case "REGISTERITEM":
                    return RegisterItem(msg);
                case "REMOVEITEM":
                    return RemoveItem(msg);
                case "UPDATEBRAND":
                    return UpdateBrand(msg);
                case "UPDATEITEM":
                    return UpdateItem(msg);
                case "UPDATETYPE":
                    return UpdateType(msg);
                default:
                    return null;
            }
        }





        public IResponse AddBrand(string msg)
        {
            CommandAddBrand command = new CommandAddBrand(
                brand: Message.FindValue(msg, "Brand")
                );
            IHandler<CommandAddBrand> handler = new HandlerCatalogBrand(BrandRepository());
            return handler.handle(command);
        }

        public IResponse AddItem(string msg)
        {
            CommandAddItem command = new CommandAddItem(
                id: Convert.ToInt32(Message.FindValue(msg, "ProductId")),
                quantity: Convert.ToDecimal(Message.FindValue(msg, "Quantity"))
                );
            IHandler<CommandAddItem> handler = new HandlerCatalogItem(ItemRepository());

            return handler.handle(command);
        }

        public IResponse AddType(string msg)
        {
            CommandAddType command = new CommandAddType(
                type: Message.FindValue(msg, "Type")
                );
            IHandler<CommandAddType> handler = new HandlerCatalogType(TypeRepository());

            return handler.handle(command);
        }

        public IResponse RegisterItem(string msg)
        {
            CommandRegisterItem command = new CommandRegisterItem(
                name: Message.FindValue(msg, "Name"),
                description: Message.FindValue(msg, "Description"),
                price: Convert.ToDecimal(Message.FindValue(msg, "Price")),
                catalogTypeId: Convert.ToInt32(Message.FindValue(msg, "CatalogTypeId")),
                catalogBrandId: Convert.ToInt32(Message.FindValue(msg, "CatalogBrandId")),
                restockThreshold: Convert.ToInt32(Message.FindValue(msg, "RestockThreshold")),
                maxStockThreshold: Convert.ToInt32(Message.FindValue(msg, "MaxStockThreshold"))
                );
            IHandler<CommandRegisterItem> handler = new HandlerCatalogItem(ItemRepository());

            return handler.handle(command);
        }

        public IResponse RemoveItem(string msg)
        {
            CommandRemoveItem command = new CommandRemoveItem(
                id: Convert.ToInt32(Message.FindValue(msg, "ProductId")),
                quantityDesired: Convert.ToDecimal(Message.FindValue(msg, "Quantity"))
                );
            IHandler<CommandRemoveItem> handler = new HandlerCatalogItem(ItemRepository());

            return handler.handle(command);
        }

        public IResponse UpdateBrand(string msg)
        {
            CommandUpdateBrand command = new CommandUpdateBrand(
                id: Convert.ToInt32(Message.FindValue(msg, "BrandId")),
                brand: Message.FindValue(msg, "Brand")
                );
            IHandler<CommandUpdateBrand> handler = new HandlerCatalogBrand(BrandRepository());

            return handler.handle(command);
        }

        public IResponse UpdateItem(string msg)
        {
            CommandUpdateItem command = new CommandUpdateItem(
                id: Convert.ToInt32(Message.FindValue(msg, "ProductId")),
                name: Message.FindValue(msg, "Name"),
                description: Message.FindValue(msg, "Description"),
                price: Convert.ToDecimal(Message.FindValue(msg, "Price")),
                catalogTypeId: Convert.ToInt32(Message.FindValue(msg, "CatalogTypeId")),
                catalogBrandId: Convert.ToInt32(Message.FindValue(msg, "CatalogBrandId")),
                restockThreshold: Convert.ToInt32(Message.FindValue(msg, "RestockThreshold")),
                maxStockThreshold: Convert.ToInt32(Message.FindValue(msg, "MaxStockThreshold"))
                );
            IHandler<CommandUpdateItem> handler = new HandlerCatalogItem(ItemRepository());

            return handler.handle(command);
        }

        public IResponse UpdateType(string msg)
        {
            CommandUpdateType command = new CommandUpdateType(
                id: Convert.ToInt32(Message.FindValue(msg, "TypeId")),
                type: Message.FindValue(msg, "Type")
                );
            IHandler<CommandUpdateType> handler = new HandlerCatalogType(TypeRepository());

            return handler.handle(command);
        }

        private ICatalogBrandRepository BrandRepository() => new CatalogBrandRepository();
        private ICatalogItemRepository ItemRepository() => new CatalogItemRepository();
        private ICatalogTypeRepository TypeRepository() => new CatalogTypeRepository();
        
    }
}
