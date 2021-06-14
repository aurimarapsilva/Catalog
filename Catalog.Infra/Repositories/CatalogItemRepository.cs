using catalog.infra.DataContext;
using Catalog.Core.Entities;
using Catalog.Core.Repositories;
using Catalog.Core.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace catalog.infra.Repositories
{
    /// <summary>
    /// Classe responsável por popular o repositorio de produtos
    /// </summary>
    public class CatalogItemRepository : ICatalogItemRepository
    {
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="database"></param>
        public CatalogItemRepository(DatabaseComunicator database)
        {
            _database = database;
        }
        private readonly DatabaseComunicator _database;

        /// <summary>
        /// Cria um novo produto
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        public bool CreateProductAsync(CatalogItem product)
        {
            var parameterProc = new Dictionary<string, object>();
            parameterProc.TryAdd("@name", product.Name);
            parameterProc.TryAdd("@description", product.Description);
            parameterProc.TryAdd("@price", product.Price);
            parameterProc.TryAdd("@catalogTypeId", product.CatalogTypeId);
            parameterProc.TryAdd("@catalogBrandId", product.CatalogBrandId);
            parameterProc.TryAdd("@restockThreshold", product.RestockThreshold);
            parameterProc.TryAdd("@maxStockThreshold", product.MaxStockThreshold);

            try
            {
                _database.GetResponse("pr_create_product_ins", parameterProc);
                return true;
            }
            catch(Exception e)
            {
                Console.Error.Write($"Erro: {e}");
                return false;
            }
        }

        /// <summary>
        /// Realiza uma query(consulta) com base nos parametros
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<IResponse> GetItemsByIdsAsync(int pageSize = 10, int pageIndex = 0)
        {
            var parameterProc = new Dictionary<string, object>();
            parameterProc.Add("@pageSize", pageSize);
            parameterProc.Add("@pageIndex", pageIndex);
            
            var response = JsonConvert.DeserializeObject<IList<CatalogItem>>(_database.GetResponse("pr_locate_product_sel", parameterProc));

            return new ResponseOk<IList<CatalogItem>>(response, response.Count);
        }

        /// <summary>
        /// Realiza uma query(consulta) com base nos parametros
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CatalogItem> ItemByIdAsync(int id)
        {
            var parameterProc = new Dictionary<string, object>();
            parameterProc.Add("@id", id);

            var response = JsonConvert.DeserializeObject<CatalogItem>(_database.GetResponse("pr_locate_product_id_sel", parameterProc));

            return response;
        }

        /// <summary>
        /// Realiza uma query(consulta) com base nos parametros
        /// </summary>
        /// <param name="catalogBrandId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<IResponse> ItemsByBrandIdAsync(int? catalogBrandId, int pageSize = 10, int pageIndex = 0)
        {
            var parameterProc = new Dictionary<string, object>();
            parameterProc.Add("@catalogBrandId", catalogBrandId);
            parameterProc.Add("@pageSize", pageSize);
            parameterProc.Add("@pageIndex", pageIndex);

            var response = JsonConvert.DeserializeObject< IList<CatalogItem>>(_database.GetResponse("pr_locate_product_brand_id_sel", parameterProc));

            return new ResponseOk<IList<CatalogItem>>(response, response.Count);
        }

        /// <summary>
        /// Realiza uma query(consulta) com base nos parametros
        /// </summary>
        /// <param name="catalogTypeId"></param>
        /// <param name="catalogBrandId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<IResponse> ItemsByTypeIdAndBrandIdAsync(int catalogTypeId, int? catalogBrandId, int pageSize = 10, int pageIndex = 0)
        {
            var parameterProc = new Dictionary<string, object>();
            parameterProc.Add("@catalogTypeId", catalogTypeId);
            parameterProc.Add("@catalogBrandId", catalogBrandId);
            parameterProc.Add("@pageSize", pageSize);
            parameterProc.Add("@pageIndex", pageIndex);

            var response = JsonConvert.DeserializeObject<IList<CatalogItem>>(_database.GetResponse("pr_locate_product_brand_type_id_sel", parameterProc));

            return new ResponseOk<IList<CatalogItem>>(response, response.Count);
        }

        /// <summary>
        /// Realiza uma query(consulta) com base nos parametros
        /// </summary>
        /// <param name="catalogTypeId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<IResponse> ItemsByTypeIdAsync(int? catalogTypeId, int pageSize = 10, int pageIndex = 0)
        {
            var parameterProc = new Dictionary<string, object>();
            parameterProc.Add("@catalogTypeId", catalogTypeId);
            parameterProc.Add("@pageSize", pageSize);
            parameterProc.Add("@pageIndex", pageIndex);

            var response = JsonConvert.DeserializeObject<IList<CatalogItem>>(_database.GetResponse("pr_locate_product_type_id_sel", parameterProc));

            return new ResponseOk<IList<CatalogItem>>(response, response.Count);
        }

        /// <summary>
        /// Realiza uma query(consulta) com base nos parametros
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<IResponse> ItemsWithNameAsync(string name, int pageSize = 10, int pageIndex = 0)
        {
            var parameterProc = new Dictionary<string, object>();
            parameterProc.Add("@name", name);
            parameterProc.Add("@pageSize", pageSize);
            parameterProc.Add("@pageIndex", pageIndex);

            var response = JsonConvert.DeserializeObject<IList<CatalogItem>>(_database.GetResponse("pr_locate_product_name_sel", parameterProc));

            return new ResponseOk<IList<CatalogItem>>(response, response.Count);
        }

        /// <summary>
        /// Atualiza os dados do produto
        /// </summary>
        /// <param name="product"></param>
        /// <param name="raiseProductPriceChangedEvent"></param>
        /// <returns></returns>
        public bool UpdateProductAsync(CatalogItem product, bool raiseProductPriceChangedEvent = false)
        {
            var parameterProc = new Dictionary<string, object>();
            parameterProc.TryAdd("@id", product.Id);
            parameterProc.TryAdd("@name", product.Name);
            parameterProc.TryAdd("@description", product.Description);
            parameterProc.TryAdd("@price", product.Price);
            parameterProc.TryAdd("@catalogTypeId", product.CatalogTypeId);
            parameterProc.TryAdd("@catalogBrandId", product.CatalogBrandId);
            parameterProc.TryAdd("@restockThreshold", product.RestockThreshold);
            parameterProc.TryAdd("@maxStockThreshold", product.MaxStockThreshold);

            try
            {
                _database.GetResponse("pr_update_product_upd", parameterProc);
                return true;
            }
            catch (Exception e)
            {
                Console.Error.Write($"Erro: {e}");
                return false;
            }
        }
    }
}
