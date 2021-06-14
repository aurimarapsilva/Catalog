namespace Catalog.Core.Entities
{
    /// <summary>
    /// Classe modelo marca
    /// </summary>
    public class CatalogBrand : Entity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="brand"> Nome da marca</param>
        public CatalogBrand(string brand)
        {
            Brand = brand;
        }

        /// <summary>
        /// Marca
        /// </summary>
        /// <example>
        /// Nestlé
        /// </example>
        public string Brand { get; private set; }

        /// <summary>
        /// Atualiza o nome da marca
        /// </summary>
        /// <param name="brand">novo nome para marca</param>
        public void UpdateCatalogBrand(string brand)
        {
            Brand = brand;
        }
    }
}