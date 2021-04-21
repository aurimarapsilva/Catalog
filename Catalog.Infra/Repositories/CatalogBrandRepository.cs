using System.Linq;
using System.Threading.Tasks;
using Catalog.Core.Entities;
using Catalog.Core.Queries;
using Catalog.Core.Queries.Contracts;
using Catalog.Core.Repositories;
using Catalog.Infra.DataContext;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        public CatalogBrandRepository(StoreDataContext context)
        {
            _context = context;
        }
        private readonly StoreDataContext _context;
        public async Task<CatalogBrand> CatalogBrandIdAsync(int id)
        {
            // valida se o id informado e menor ou igual a 0 (zero) caso positivo retirna nulo
            if (id <= 0)
                return null;

            // procura no database uma marca do produto com o id
            var brand = await _context.Brand
                .AsNoTracking()
                .FirstOrDefaultAsync(CatalogBrandQuery.GetByBrandId(id));

            // valida se foi encontrado alguma marca caso negativo retorna nulo
            if (brand == null)
                return null;

            return brand;
        }

        public async Task<IResultQuery> CatalogBrandsAsync(int pageSize = 10, int pageIndex = 0)
        {
            // retorna todos os item para a variavel
            var root = (IQueryable<CatalogBrand>)_context.Brand;

            // atraves desse select é registrado o numero de registros 
            var totalItems = await root
                .AsNoTracking()
                .LongCountAsync();

            // faz paginação no select
            var itemsOnPage = await root
                .AsNoTracking()
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            // retorna uma classe com os dados recebidos
            return new GenericResultQuery(pagIndex: pageIndex, pagSize: pageSize, count: totalItems, itemsOnPage);
        }

        public bool CreateBrandAsync(CatalogBrand brand)
        {
            _context.Entry<CatalogBrand>(brand).State = EntityState.Added;
            return SaveDatabase().Result;
        }

        public bool UpdateBrandAsync(CatalogBrand brand)
        {
            _context.Entry<CatalogBrand>(brand).State = EntityState.Modified;
            return SaveDatabase().Result;
        }

        private async Task<bool> SaveDatabase()
        {
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}