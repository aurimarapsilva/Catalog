using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Core.Entities;
using Catalog.Core.Queries;
using Catalog.Core.Queries.Contracts;
using Catalog.Core.Repositories;

namespace Catalog.Test.RepositoruesFake
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        public Task<CatalogBrand> CatalogBrandIdAsync(int id)
        {
            if (id == 1)
                return new Task<CatalogBrand>(() => new CatalogBrand(brand: "Test brand"));

            return null;
        }

        public Task<IResultQuery> CatalogBrandsAsync(int pageSize = 10, int pageIndex = 0)
        {
            var brandList = new List<CatalogBrand>();
            brandList.Add(new CatalogBrand(brand: "Test brand 0"));
            brandList.Add(new CatalogBrand(brand: "Test brand 1"));
            brandList.Add(new CatalogBrand(brand: "Test brand 2"));
            brandList.Add(new CatalogBrand(brand: "Test brand 3"));
            brandList.Add(new CatalogBrand(brand: "Test brand 4"));
            brandList.Add(new CatalogBrand(brand: "Test brand 5"));
            return new Task<IResultQuery>(() => new GenericResultQuery(
                pagIndex: pageIndex,
                pagSize: pageSize,
                count: brandList.Count,
                data: brandList
            ));
        }

        public bool CreateBrandAsync(CatalogBrand brand)
        {
            return true;
        }

        public bool UpdateBrandAsync(CatalogBrand brand)
        {
            return true;
        }
    }
}