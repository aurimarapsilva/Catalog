using System.Threading.Tasks;
using Catalog.Core.Entities;
using Catalog.Core.Queries.Contracts;

namespace Catalog.Core.Repositories
{
    public interface ICatalogTypeRepository
    {
        bool CreateTypeAsync(CatalogType type);
        bool UpdateTypeAsync(CatalogType type);
        Task<IResultQuery<CatalogType>> CatalogTypesAsync(int pageSize = 10, int pageIndex = 0);
        Task<CatalogType> CatalogTypeById(string id);
    }
}