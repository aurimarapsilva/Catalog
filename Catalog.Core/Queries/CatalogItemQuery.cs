using System;
using System.Linq.Expressions;
using Catalog.Core.Entities;

namespace Catalog.Core.Queries
{
    public static class CatalogItemQuery
    {
        public static Expression<Func<CatalogItem, bool>> GetById(int id)
        {
            return x => x.Id == id;
        }

        public static Expression<Func<CatalogItem, bool>> GetByName(string name)
        {
            return x => x.Name.StartsWith(name); ;
        }

        public static Expression<Func<CatalogItem, bool>> GetByCatalogBrandId(int catalogBrandId)
        {
            return x => x.CatalogBrandId == catalogBrandId;
        }

        public static Expression<Func<CatalogItem, bool>> GetByCatalogTypeId(int catalogTypeId)
        {
            return x => x.CatalogTypeId == catalogTypeId;
        }
    }
}