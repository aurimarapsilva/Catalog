using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Core.Entities;
using Catalog.Core.Queries.Contracts;

namespace Catalog.Core.Repositories
{
    public interface ICatalogItemRepository
    {
        bool CreateProductAsync(CatalogItem product);
        bool UpdateProductAsync(CatalogItem product);
        Task<IResultQuery<CatalogItem>> GetItemsByIdsAsync(int pageSize = 10, int pageIndex = 0, string ids = null);
        Task<IResultQuery<CatalogItem>> ItemByIdAsync(string id);
        Task<IResultQuery<CatalogItem>> ItemsWithNameAsync(string name, int pageSize = 10, int pageIndex = 0);
        Task<IResultQuery<CatalogItem>> ItemsByTypeIdAndBrandIdAsync(string catalogTypeId, string catalogBrandId, int pageSize = 10, int pageIndex = 0);
        Task<IResultQuery<CatalogItem>> ItemsByBrandIdAsync(string catalogBrandId, int pageSize = 10, int pageIndex = 0);
        Task<IResultQuery<CatalogItem>> ItemsByTypeIdAsync(string catalogTypeId, int pageSize = 10, int pageIndex = 0);
        IList<CatalogItem> ChangeUriPlaceholder(List<CatalogItem> items);
    }
}