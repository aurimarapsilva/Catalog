using System;
using System.Linq.Expressions;
using Catalog.Core.Entities;

namespace Catalog.Core.Queries
{
    /// <summary>
    /// Expressões de consulta de marcas
    /// </summary>
    public static class CatalogBrandQuery
    {
        /// <summary>
        /// obtem uma marca pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Expression<Func<CatalogBrand, bool>> GetByBrandId(int id)
        {
            return x => x.Id == id;
        }
    }
}