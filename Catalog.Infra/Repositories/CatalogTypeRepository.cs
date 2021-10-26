using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Core.Queries;
using catalog.infra.DataContext;
using Microsoft.EntityFrameworkCore;

namespace catalog.infra.Repositories
{
    /// <summary>
    /// Classe responsável por implementar os repositorio de tipos de produtos
    /// </summary>
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private StoreDataContext _context;

        public CatalogTypeRepository(StoreDataContext context) => _context = context;

        public async Task<CatalogType> CatalogTypeById(int id) =>
            await _context.Type.AsNoTracking().FirstOrDefaultAsync(CatalogTypeQuery.GetById(id));

        public async Task<IResponse> CatalogTypesAsync(int pageSize = 10, int pageIndex = 0)
        {
            int pagination = pageIndex > 1 ? (pageIndex - 1) * pageSize : 0;

            IEnumerable<CatalogType> consult = await _context.Type.AsNoTracking().Skip(pagination).Take(pageSize).ToListAsync();

            if (consult.Count() > 1)
                return new ResponseOk<IEnumerable<CatalogType>>(consult);

            return new ResponseError<IEnumerable<CatalogType>>(new Error("404", "Not Found", null));
        }

        public bool CreateTypeAsync(CatalogType type)
        {
            _context.Entry<CatalogType>(type).State = EntityState.Added;
            return SaveData();
        }

        public bool UpdateTypeAsync(CatalogType type)
        {
            _context.Entry<CatalogType>(type).State = EntityState.Modified;
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
