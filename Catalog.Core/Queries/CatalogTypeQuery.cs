using System;
using System.Linq.Expressions;
using Catalog.Core.Entities;

namespace Catalog.Core.Queries
{
    public static class CatalogTypeQuery
    {
        public static Expression<Func<CatalogType, bool>> GetById(int id)
        {
            return x => x.Id == id;
        }
    }
}