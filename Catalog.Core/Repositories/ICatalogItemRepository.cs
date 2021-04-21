using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Core.Entities;
using Catalog.Core.Queries.Contracts;

namespace Catalog.Core.Repositories
{
    public interface ICatalogItemRepository
    {
        bool CreateProductAsync(CatalogItem product);
        bool UpdateProductAsync(CatalogItem product, bool raiseProductPriceChangedEvent = false);
        Task<IResultQuery<CatalogItem>> GetItemsByIdsAsync(int pageSize = 10, int pageIndex = 0, string ids = null);
        Task<CatalogItem> ItemByIdAsync(int id);
        Task<IResultQuery<CatalogItem>> ItemsWithNameAsync(string name, int pageSize = 10, int pageIndex = 0);
        Task<IResultQuery<CatalogItem>> ItemsByTypeIdAndBrandIdAsync(int catalogTypeId, int catalogBrandId, int pageSize = 10, int pageIndex = 0);
        Task<IResultQuery<CatalogItem>> ItemsByBrandIdAsync(int catalogBrandId, int pageSize = 10, int pageIndex = 0);
        Task<IResultQuery<CatalogItem>> ItemsByTypeIdAsync(int catalogTypeId, int pageSize = 10, int pageIndex = 0);
    }
}