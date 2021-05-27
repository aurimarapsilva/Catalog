using System.Threading.Tasks;
using Catalog.Core.Entities;
using Catalog.Core.Response;

namespace Catalog.Core.Repositories
{
    /// <summary>
    /// Interfface de repositorio de produtos
    /// </summary>
    public interface ICatalogItemRepository
    {
        /// <summary>
        /// Cria um novo produto na base de dados
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        bool CreateProductAsync(CatalogItem product);
        
        /// <summary>
        /// Atualiza as informações do produtos na base de dados
        /// </summary>
        /// <param name="product"></param>
        /// <param name="raiseProductPriceChangedEvent"></param>
        /// <returns></returns>
        bool UpdateProductAsync(CatalogItem product, bool raiseProductPriceChangedEvent = false);

        /// <summary>
        /// Retorna uma lista de produtos
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        Task<IResponse> GetItemsByIdsAsync(int pageSize = 10, int pageIndex = 0);
        
        /// <summary>
        /// Retorna um produto da base de dados conforme o id informado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CatalogItem> ItemByIdAsync(int id);

        /// <summary>
        /// Retorna um produto da base de dados conforme o nome informado
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        Task<IResponse> ItemsWithNameAsync(string name, int pageSize = 10, int pageIndex = 0);

        /// <summary>
        /// Retorna um produto da base de dados conforme a marca e tipo informado
        /// </summary>
        /// <param name="catalogTypeId"></param>
        /// <param name="catalogBrandId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        Task<IResponse> ItemsByTypeIdAndBrandIdAsync(int catalogTypeId, int? catalogBrandId, int pageSize = 10, int pageIndex = 0);

        /// <summary>
        /// Retorna um produto da base de dados conforme a marca informado
        /// </summary>
        /// <param name="catalogBrandId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        Task<IResponse> ItemsByBrandIdAsync(int? catalogBrandId, int pageSize = 10, int pageIndex = 0);

        /// <summary>
        /// Retorna um produto da base de dados conforme o tipo informado
        /// </summary>
        /// <param name="catalogTypeId"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        Task<IResponse> ItemsByTypeIdAsync(int? catalogTypeId, int pageSize = 10, int pageIndex = 0);
    }
}