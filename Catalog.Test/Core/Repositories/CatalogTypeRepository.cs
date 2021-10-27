using System.Threading.Tasks;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Response;

namespace catalog.test.Core.Repositories
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        public bool CreateTypeAsync(CatalogType type) => type != null ? true : false;

        public bool UpdateTypeAsync(CatalogType type) => type != null ? true : false;

        public async Task<IResponse> CatalogTypesAsync(int pageSize = 10, int pageIndex = 0) => Response();

        public async Task<CatalogType> CatalogTypeById(int id) => id == 1 ? ResultType() : null;

        private CatalogType ResultType() => new CatalogType("Teste");
        private IResponse Response() => new ResponseOk<CatalogType>(ResultType());
    }
}
