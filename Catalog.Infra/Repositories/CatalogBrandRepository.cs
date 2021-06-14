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
    /// Classe de que faz conexão com o repositorio de Marcas de produtos
    /// </summary>
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="database"></param>
        public CatalogBrandRepository(DatabaseComunicator database)
        {
            _database = database;
        }

        private readonly DatabaseComunicator _database;

        /// <summary>
        /// Recupera uma marca de produto conforme o id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CatalogBrand> CatalogBrandIdAsync(int id)
        {
            var parameterProc = new Dictionary<string, object>();
            parameterProc.Add("@id", id);

            var response = _database.GetResponse("pr_locate_brand", parameterProc);

            if (string.IsNullOrEmpty(response))
                return null;

            return JsonConvert.DeserializeObject<CatalogBrand>(response);
        }

        /// <summary>
        /// Lista todas as marcas conforme paginação
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<IResponse> CatalogBrandsAsync(int pageSize = 10, int pageIndex = 0)
        {
            var parameterProc = new Dictionary<string, object>();
            parameterProc.Add("@pageSize", pageSize);
            parameterProc.Add("@pageIndex", pageIndex);

            var response = _database.GetResponse("pr_brand_sel", parameterProc);

            if (string.IsNullOrEmpty(response))
                return new ResponseError<IEnumerable<CatalogBrand>>(
                    errorCode: "404",
                    description: "Não foi possível prosseguir com a solicitação",
                    data: "Nenhum item encontrado"
                );

            var convertJson = JsonConvert.DeserializeObject<IEnumerable<CatalogBrand>>(response);

            return new ResponseOk<IEnumerable<CatalogBrand>>(convertJson);
        }

        /// <summary>
        /// Cria uma nova marca no repositorio
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public bool CreateBrandAsync(CatalogBrand brand)
        {
            var parameterProc = new Dictionary<string, object>();
            parameterProc.Add("@brand", brand.Brand);

            try
            {
                _database.GetResponse("pr_create_brand", parameterProc);
                return true;
            }
            catch (Exception e)
            {
                Console.Error.Write($"Erro: {e}");
                return false;
            }
        }

        /// <summary>
        /// Atualiza uma marca de produto no repositorio
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        public bool UpdateBrandAsync(CatalogBrand brand)
        {
            var parameterProc = new Dictionary<string, object>();
            parameterProc.Add("@id", brand.Id);
            parameterProc.Add("@brand", brand.Brand);

            try
            {
                _database.GetResponse("pr_update_brand", parameterProc);
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
