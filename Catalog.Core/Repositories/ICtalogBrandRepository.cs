using System.Threading.Tasks;
using Catalog.Core.Entities;
using Catalog.Core.Queries.Contracts;

namespace Catalog.Core.Repositories
{
    public interface ICatalogBrandRepository
    {
        bool CreateBrandAsync(CatalogBrand brand);
        bool UpdateBrandAsync(CatalogBrand brand);
        Task<IResultQuery<CatalogBrand>> CatalogBrandsAsync(int pageSize = 10, int pageIndex = 0);
        Task<CatalogBrand> CatalogBrandIdAsync(string id);
    }
}