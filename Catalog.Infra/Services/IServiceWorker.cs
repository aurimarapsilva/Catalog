using Catalog.Core.Response;

namespace catalog.infra.Services
{
    public interface IServiceWorker
    {
        IResponse BeginTransaction(string msg);
        IResponse AddBrand(string msg);
        IResponse AddItem(string msg);
        IResponse AddType(string msg);
        IResponse RegisterItem(string msg);
        IResponse RemoveItem(string msg);
        IResponse UpdateBrand(string msg);
        IResponse UpdateItem(string msg);
        IResponse UpdateType(string msg);
    }
}
