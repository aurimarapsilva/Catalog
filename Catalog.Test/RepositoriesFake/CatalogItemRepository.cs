using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Core.Entities;
using Catalog.Core.Queries;
using Catalog.Core.Queries.Contracts;
using Catalog.Core.Repositories;

namespace Catalog.Test.RepositoruesFake
{
    public class CatalogItemRepository : ICatalogItemRepository
    {
        public bool CreateProductAsync(CatalogItem product)
        {
            return true;
        }

        public Task<IResultQuery> GetItemsByIdsAsync(int pageSize = 10, int pageIndex = 0)
        {
            return FakeListReturn();
        }

        public Task<CatalogItem> ItemByIdAsync(int id)
        {
            if (id == 1)
                return new Task<CatalogItem>(() => new CatalogItem(
                    name: "Teste",
                    description: "Realizando teste na camada de entidade",
                    price: 0.85m,
                    catalogTypeId: 1,
                    catalogBrandId: 1,
                    restockThreshold: 5,
                    maxStockThreshold: 10
                ));

            return null;
        }

        public Task<IResultQuery> ItemsByBrandIdAsync(int? catalogBrandId, int pageSize = 10, int pageIndex = 0)
        {
            return FakeListReturn();
        }

        public Task<IResultQuery> ItemsByTypeIdAndBrandIdAsync(int catalogTypeId, int? catalogBrandId, int pageSize = 10, int pageIndex = 0)
        {
            return FakeListReturn();
        }

        public Task<IResultQuery> ItemsByTypeIdAsync(int? catalogTypeId, int pageSize = 10, int pageIndex = 0)
        {
            return FakeListReturn();
        }

        public Task<IResultQuery> ItemsWithNameAsync(string name, int pageSize = 10, int pageIndex = 0)
        {
            return FakeListReturn();
        }

        public bool UpdateProductAsync(CatalogItem product, bool raiseProductPriceChangedEvent = false)
        {
            return true;
        }

        private Task<IResultQuery> FakeListReturn()
        {
            var itemsList = new List<CatalogItem>();
            itemsList.Add(new CatalogItem(
                    name: "Teste 0",
                    description: "Realizando teste na camada de repositorio",
                    price: 0.85m,
                    catalogTypeId: 1,
                    catalogBrandId: 1,
                    restockThreshold: 5,
                    maxStockThreshold: 10
                ));
            itemsList.Add(new CatalogItem(
                    name: "Teste 1",
                    description: "Realizando teste na camada de repositorio",
                    price: 0.85m,
                    catalogTypeId: 1,
                    catalogBrandId: 1,
                    restockThreshold: 5,
                    maxStockThreshold: 10
                ));
            itemsList.Add(new CatalogItem(
                    name: "Teste 2",
                    description: "Realizando teste na camada de repositorio",
                    price: 0.85m,
                    catalogTypeId: 1,
                    catalogBrandId: 1,
                    restockThreshold: 5,
                    maxStockThreshold: 10
                ));
            itemsList.Add(new CatalogItem(
                    name: "Teste 3",
                    description: "Realizando teste na camada de repositorio",
                    price: 0.85m,
                    catalogTypeId: 1,
                    catalogBrandId: 1,
                    restockThreshold: 5,
                    maxStockThreshold: 10
                ));
            itemsList.Add(new CatalogItem(
                    name: "Teste 4",
                    description: "Realizando teste na camada de repositorio",
                    price: 0.85m,
                    catalogTypeId: 1,
                    catalogBrandId: 1,
                    restockThreshold: 5,
                    maxStockThreshold: 10
                ));
            itemsList.Add(new CatalogItem(
                    name: "Teste 5",
                    description: "Realizando teste na camada de repositorio",
                    price: 0.85m,
                    catalogTypeId: 1,
                    catalogBrandId: 1,
                    restockThreshold: 5,
                    maxStockThreshold: 10
                ));

            return new Task<IResultQuery>(() => new GenericResultQuery(
                pagIndex: 1,
                pagSize: 1,
                count: itemsList.Count,
                data: itemsList
            ));
        }
    }
}