using System.Threading.Tasks;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Response;

namespace catalog.test.Core.Repositories
{
    public class CatalogItemRepository : ICatalogItemRepository
    {
        public bool CreateProductAsync(CatalogItem product) => product != null ? true : false;
    
        public bool UpdateProductAsync(CatalogItem product, bool raiseProductPriceChangedEvent = false) => product != null ? true : false;

        public async Task<IResponse> GetItemsByIdsAsync(int pageSize = 10, int pageIndex = 0) => Response();

        public async Task<CatalogItem> ItemByIdAsync(int id) => id == 1 ? ResultItem() : ResultItemRemove(id);

        public async Task<IResponse> ItemsWithNameAsync(string name, int pageSize = 10, int pageIndex = 0) =>
            name == "Teste" ? Response() : null;

        public async Task<IResponse> ItemsByTypeIdAndBrandIdAsync(int catalogTypeId, int? catalogBrandId, int pageSize = 10,
            int pageIndex = 0) => catalogTypeId == 1 ? Response() : null;
        
        public async Task<IResponse> ItemsByBrandIdAsync(int? catalogBrandId, int pageSize = 10, int pageIndex = 0) => catalogBrandId == 1 ? Response() : null;

        public async Task<IResponse> ItemsByTypeIdAsync(int? catalogTypeId, int pageSize = 10, int pageIndex = 0) => catalogTypeId == 1 ? Response() : null;
   
        private CatalogItem ResultItem() => new CatalogItem("Teste", "descrição Teste", 2, 1, 1, 15, 30);
        
        private IResponse Response() =>new ResponseOk<CatalogItem>(ResultItem());

        private CatalogItem ResultItemRemove(int id)
        {
            if (id == 5)
            {
                var item = ResultItem();
                item.AddStock(20);
                return item;
            }

            return null;
        } 

    }
}
