using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Core.Entities;
using Catalog.Core.Queries;
using Catalog.Core.Queries.Contracts;
using Catalog.Core.Repositories;

namespace Catalog.Test.RepositoruesFake
{
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        public Task<CatalogType> CatalogTypeById(int id)
        {
            if (id == 1)
                return new Task<CatalogType>(() => new CatalogType(type: "Tipo Teste"));

            return null;
        }

        public Task<IResultQuery> CatalogTypesAsync(int pageSize = 10, int pageIndex = 0)
        {
            var typeList = new List<CatalogType>();
            typeList.Add(new CatalogType(type: "Tipo Teste 0"));
            typeList.Add(new CatalogType(type: "Tipo Teste 1"));
            typeList.Add(new CatalogType(type: "Tipo Teste 2"));
            typeList.Add(new CatalogType(type: "Tipo Teste 3"));
            typeList.Add(new CatalogType(type: "Tipo Teste 4"));
            typeList.Add(new CatalogType(type: "Tipo Teste 5"));

            return new Task<IResultQuery>(() => new GenericResultQuery(
                pagIndex: 1,
                pagSize: 1,
                count: typeList.Count,
                data: typeList
            ));
        }

        public bool CreateTypeAsync(CatalogType type)
        {
            return true;
        }

        public bool UpdateTypeAsync(CatalogType type)
        {
            return true;
        }
    }
}