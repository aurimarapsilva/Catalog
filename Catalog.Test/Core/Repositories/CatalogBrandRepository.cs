using System.Threading.Tasks;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Response;

namespace catalog.test.Core.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        public bool CreateBrandAsync(CatalogBrand brand) => brand != null ? true : false;

        public bool UpdateBrandAsync(CatalogBrand brand) => brand != null ? true : false;

        public async Task<IResponse> CatalogBrandsAsync(int pageSize = 10, int pageIndex = 0) => Response();

        public async Task<CatalogBrand> CatalogBrandIdAsync(int id) => id == 1 ? ResultBrand() : null;
        
        private CatalogBrand ResultBrand() => new CatalogBrand("Teste");
        private IResponse Response() => new ResponseOk<CatalogBrand>(ResultBrand());
    }
}
