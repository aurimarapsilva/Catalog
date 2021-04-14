using System;
using System.Linq.Expressions;
using Catalog.Core.Entities;

namespace Catalog.Core.Queries
{
    public static class CatalogItemQuery
    {
        public static Expression<Func<CatalogItem, bool>> GetById(string id)
        {
            return x => x.Id == id;
        }

        public static Expression<Func<CatalogItem, bool>> GetByName(string name)
        {
            return x => x.Name == name;
        }

        public static Expression<Func<CatalogItem, bool>> GetByCatalogBrandId(string catalogBrandId)
        {
            return x => x.CatalogBrandId == catalogBrandId;
        }

        public static Expression<Func<CatalogItem, bool>> GetByCatalogTypeId(string catalogTypeId)
        {
            return x => x.CatalogTypeId == catalogTypeId;
        }
    }
}