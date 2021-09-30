using catalog.infra.DataContext;
using catalog.infra.RabbitMQ;
using catalog.infra.Repositories;
using Catalog.Core.Commands;
using Catalog.Core.Handlers;
using Catalog.Core.Handlers.Contracts;
using Catalog.Core.Repositories;
using Catalog.Core.Response;

namespace catalog.infra.Services
{
    public static class ServiceWorker
    {

        public static IResponse AddBrand(string msg)
        {
            StoreDataContext dataContext = new StoreDataContext();
            DatabaseComunicator comunicador = new DatabaseComunicator(dataContext);
            ICatalogBrandRepository repository = new CatalogBrandRepository(comunicador);
            CommandAddBrand command = new CommandAddBrand(brand: TratarMenssagem.RecuperarValor(msg, "brand"));
            IHandler<CommandAddBrand> handler = new HandlerCatalogBrand(repository);

            return handler.handle(command);
        }
    }
}
