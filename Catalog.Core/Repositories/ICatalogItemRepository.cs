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
        Task<IResultQuery> GetItemsByIdsAsync(int pageSize = 10, int pageIndex = 0);
        Task<CatalogItem> ItemByIdAsync(int id);
        Task<IResultQuery> ItemsWithNameAsync(string name, int pageSize = 10, int pageIndex = 0);
        Task<IResultQuery> ItemsByTypeIdAndBrandIdAsync(int catalogTypeId, int? catalogBrandId, int pageSize = 10, int pageIndex = 0);
        Task<IResultQuery> ItemsByBrandIdAsync(int? catalogBrandId, int pageSize = 10, int pageIndex = 0);
        Task<IResultQuery> ItemsByTypeIdAsync(int? catalogTypeId, int pageSize = 10, int pageIndex = 0);
    }
}