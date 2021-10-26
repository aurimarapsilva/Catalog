using catalog.infra.DataContext;
using Catalog.Core.Entities;
using Catalog.Core.Queries;
using Catalog.Core.Repositories;
using Catalog.Core.Response;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace catalog.infra.Repositories
{

    /// <summary>
    /// Classe de que faz conexão com o repositorio de Marcas de produtos
    /// </summary>
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private StoreDataContext _context;
        public CatalogBrandRepository(StoreDataContext context) => _context = context;


        public async Task<CatalogBrand> CatalogBrandIdAsync(int id) => await _context.Brand.AsNoTracking().FirstOrDefaultAsync(CatalogBrandQuery.GetByBrandId(id));
    

        public async Task<IResponse> CatalogBrandsAsync(int pageSize = 10, int pageIndex = 0)
        {
            int pagination = pageIndex > 1 ? (pageIndex - 1) * pageSize : 0;

            IEnumerable<CatalogBrand> consult = await _context.Brand.AsNoTracking().Skip(pagination).Take(pageSize).ToListAsync();

            if (consult.Count() != 0)
                return new ResponseOk<IEnumerable<CatalogBrand>>(consult);

            return new ResponseError<IEnumerable<CatalogBrand>>(new Error("404","Not Found",null));
        }

        public bool CreateBrandAsync(CatalogBrand brand)
        {
            _context.Entry<CatalogBrand>(brand).State = EntityState.Added;
            return SaveData();
        }

        public bool UpdateBrandAsync(CatalogBrand brand)
        {
            _context.Entry<CatalogBrand>(brand).State = EntityState.Modified;
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
    }
}
