using System;
using System.Linq.Expressions;
using Catalog.Core.Entities;

namespace Catalog.Core.Queries
{
    /// <summary>
    /// Expressões para consulta de produtos
    /// </summary>
    public static class CatalogItemQuery
    {
        /// <summary>
        /// Obtem um produto com mesmo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Expression<Func<CatalogItem, bool>> GetById(int id) => 
            x => x.Id == id;

        /// <summary>
        /// Obtem um ou mais produtos com mesmo nome
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Expression<Func<CatalogItem, bool>> GetByName(string name) => 
            x => x.Name.StartsWith(name);

        /// <summary>
        /// Obtem um ou mais produtos com mesma marca
        /// </summary>
        /// <param name="catalogBrandId"></param>
        /// <returns></returns>
        public static Expression<Func<CatalogItem, bool>> GetByCatalogBrandId(int? catalogBrandId) => 
            x => x.CatalogBrandId == catalogBrandId;

        /// <summary>
        /// Obtem um ou mais produtos com mesmo tipo 
        /// </summary>
        /// <param name="catalogTypeId"></param>
        /// <returns></returns>
        public static Expression<Func<CatalogItem, bool>> GetByCatalogTypeId(int catalogTypeId) => 
            x => x.CatalogTypeId == catalogTypeId;

        /// <summary>
        /// Obtem um ou mais produtos com mesmo tipo e com marca podendo ser nulavel 
        /// </summary>
        /// <param name="catalogTypeId"></param>
        /// <returns></returns>
        public static Expression<Func<CatalogItem, bool>> GetByCatalogTypeIdOrCatalogBrand(int catalogTypeId, int? catalogBrandId) => 
            x => x.CatalogTypeId == catalogTypeId && x.CatalogBrandId == catalogBrandId;

        /// <summary>
        /// Obtem um ou mais produtos com mesmo tipo e com marca podendo ser nulavel 
        /// </summary>
        /// <param name="catalogTypeId"></param>
        /// <returns></returns>
        public static Expression<Func<CatalogItem, bool>> GetByCatalogTypeNullable(int? catalogTypeId) =>
            x => x.CatalogTypeId == catalogTypeId;
    }
}