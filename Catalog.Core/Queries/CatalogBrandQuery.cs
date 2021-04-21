using System;
using System.Linq.Expressions;
using Catalog.Core.Entities;

namespace Catalog.Core.Queries
{
    public static class CatalogBrandQuery
    {
        public static Expression<Func<CatalogBrand, bool>> GetByBrandId(int id)
        {
            return x => x.Id == id;
        }
    }
}