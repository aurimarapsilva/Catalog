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
    /// Classe responsável por implementar os repositorio de tipos de produtos
    /// </summary>
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="database"></param>
        public CatalogTypeRepository(DatabaseComunicator database)
        {
            _database = database;
        }

        private readonly DatabaseComunicator _database;

        /// <summary>
        /// Realiza uma query(consulta) com base nos parametros
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CatalogType> CatalogTypeById(int id)
        {
            var parameterProc = new Dictionary<string, object>();
            parameterProc.Add("@id", id);

            return JsonConvert.DeserializeObject<CatalogType>(_database.GetResponse("pr_locate_type_id_sel", parameterProc));
        }

        /// <summary>
        /// Realiza uma query(consulta) com base nos parametros
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public async Task<IResponse> CatalogTypesAsync(int pageSize = 10, int pageIndex = 0)
        {
            var parameterProc = new Dictionary<string, object>();
            parameterProc.Add("@pageSize", pageSize);
            parameterProc.Add("@pageIndex", pageIndex);

            var response = JsonConvert.DeserializeObject<IList<CatalogType>>(_database.GetResponse("pr_locate_type_sel", parameterProc));

            return new ResponseOk<IList<CatalogType>>(response, response.Count);
        }

        /// <summary>
        /// Cria um tipo de produto na base de dados
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool CreateTypeAsync(CatalogType type)
        {
            var parameterProc = new Dictionary<string, object>();
            parameterProc.Add("@type", type.Type);

            try
            {
                _database.GetResponse("pr_create_type_sel", parameterProc);
                return true;
            }
            catch (Exception e)
            {
                Console.Error.Write($"Erro: {e}");
                return false;
            }
        }

        /// <summary>
        /// Atualiza um tipo de produto na base de dados
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool UpdateTypeAsync(CatalogType type)
        {
            var parameterProc = new Dictionary<string, object>();
            parameterProc.Add("@id", type.Id);
            parameterProc.Add("@type", type.Type);

            try
            {
                _database.GetResponse("pr_update_type_sel", parameterProc);
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
