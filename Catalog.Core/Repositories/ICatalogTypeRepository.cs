using System.Threading.Tasks;
using Catalog.Core.Entities;
using Catalog.Core.Response;

namespace Catalog.Core.Repositories
{
    /// <summary>
    /// Interface responsável pelo repositorio do tipo de produto
    /// </summary>
    public interface ICatalogTypeRepository
    {
        /// <summary>
        /// Cria um novo tipo de produto no repositorio
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool CreateTypeAsync(CatalogType type);
        
        /// <summary>
        /// Atualiza o tipo de produto
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        bool UpdateTypeAsync(CatalogType type);

        /// <summary>
        /// Retorna uma lista de tipos de produtos
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        Task<IResponse> CatalogTypesAsync(int pageSize = 10, int pageIndex = 0);
        
        /// <summary>
        /// Retorna um tipo expecifico conforme o di informado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CatalogType> CatalogTypeById(int id);
    }
}