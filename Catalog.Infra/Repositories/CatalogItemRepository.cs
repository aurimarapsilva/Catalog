using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Catalog.Core.Queries;
using catalog.infra.DataContext;
using Microsoft.EntityFrameworkCore;

namespace catalog.infra.Repositories
{
    /// <summary>
    /// Classe responsável por popular o repositorio de produtos
    /// </summary>
    public class CatalogItemRepository : ICatalogItemRepository
    {
        private StoreDataContext _context;

        public CatalogItemRepository(StoreDataContext context) => _context = context;

        public bool CreateProductAsync(CatalogItem product)
        {
            _context.Entry<CatalogItem>(product).State = EntityState.Added;
            return SaveData();
        }

        public async Task<IResponse> GetItemsByIdsAsync(int pageSize = 10, int pageIndex = 0)
        {
            int pag = Pagination(pageSize, pageIndex);

            IEnumerable<CatalogItem> consult = await _context.Item.AsNoTracking().Skip(pag).Take(pageSize).ToListAsync();

            if (consult.Count() > 1)
                return new ResponseOk<IEnumerable<CatalogItem>>(consult);

            return new ResponseError<IEnumerable<CatalogItem>>(new Error("404", "Not Found", null));

        }

        public async Task<CatalogItem> ItemByIdAsync(int id) =>
            await _context.Item.AsNoTracking().FirstOrDefaultAsync(CatalogItemQuery.GetById(id));

        public async Task<IResponse> ItemsByBrandIdAsync(int? catalogBrandId, int pageSize = 10, int pageIndex = 0) =>
            ConsultDatabase(CatalogItemQuery.GetByCatalogBrandId(catalogBrandId), pageSize, pageIndex);

        public async Task<IResponse> ItemsByTypeIdAndBrandIdAsync(int catalogTypeId, int? catalogBrandId, int pageSize = 10, int pageIndex = 0) =>
            ConsultDatabase(CatalogItemQuery.GetByCatalogTypeIdOrCatalogBrand(catalogTypeId, catalogBrandId), pageSize, pageIndex);

        public async Task<IResponse> ItemsByTypeIdAsync(int? catalogTypeId, int pageSize = 10, int pageIndex = 0) =>
            ConsultDatabase(CatalogItemQuery.GetByCatalogTypeNullable(catalogTypeId), pageSize, pageIndex);
       
        public async Task<IResponse> ItemsWithNameAsync(string name, int pageSize = 10, int pageIndex = 0) =>
            ConsultDatabase(CatalogItemQuery.GetByName(name), pageSize, pageIndex);

        public bool UpdateProductAsync(CatalogItem product, bool raiseProductPriceChangedEvent = false)
        {
            _context.Entry<CatalogItem>(product).State = EntityState.Modified;
            return SaveData();
        }

        private bool SaveData()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        private int Pagination(int pageSize, int pageIndex) => pageIndex > 1 ? (pageIndex - 1) * pageSize : 0;

        private async Task<IEnumerable<CatalogItem>> CatalogList(Expression<Func<CatalogItem, bool>> param, int pageSize, int pageIndex) =>
            await _context.Item.AsNoTracking().Where(param).Skip(Pagination(pageSize, pageIndex)).Take(pageSize).ToListAsync();

        private IResponse ConsultDatabase(Expression<Func<CatalogItem, bool>> param, int pageSize, int pageIndex) =>
            CatalogList(param, pageSize, pageIndex).Result.Count() > 1 ?
                new ResponseOk<IEnumerable<CatalogItem>>(CatalogList(param, pageSize, pageIndex).Result) :
                new ResponseError<IEnumerable<CatalogItem>>(new Error("404", "Not Found", null));
    }
}
