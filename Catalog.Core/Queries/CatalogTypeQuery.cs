using System;
using System.Linq.Expressions;
using Catalog.Core.Entities;

namespace Catalog.Core.Queries
{
    /// <summary>
    /// Expressões de consulta de um Tipo de produto
    /// </summary>
    public static class CatalogTypeQuery
    {
        /// <summary>
        /// Retorna um ou mais tipo de produto conforme o id mencionado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Expression<Func<CatalogType, bool>> GetById(int id)
        {
            return x => x.Id == id;
        }
    }
}