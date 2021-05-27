using System.Threading.Tasks;
using Catalog.Core.Entities;
using Catalog.Core.Response;

namespace Catalog.Core.Repositories
{
    /// <summary>
    /// Interface de repositorio de marcas de produtos
    /// </summary>
    public interface ICatalogBrandRepository
    {
        /// <summary>
        /// Cria uma nova marca na base de dados
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        bool CreateBrandAsync(CatalogBrand brand);

        /// <summary>
        /// Atualiza uma marca na base de dados
        /// </summary>
        /// <param name="brand"></param>
        /// <returns></returns>
        bool UpdateBrandAsync(CatalogBrand brand);

        /// <summary>
        /// Retorna uma lista de marcas cadastradas na base
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        Task<IResponse> CatalogBrandsAsync(int pageSize = 10, int pageIndex = 0);
        
        /// <summary>
        /// Retirna uma marca referente ao id informado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CatalogBrand> CatalogBrandIdAsync(int id);
    }
}