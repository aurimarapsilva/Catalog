namespace Catalog.Core.Entities
{
    /// <summary>
    /// Classe modelo para os tipos de produtos
    /// </summary>
    public class CatalogType : Entity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type"> tipo do produto</param>
        public CatalogType(string type)
        {
            Type = type;
        }

        /// <summary>
        /// Tipo do produto
        /// </summary>
        /// <example>
        /// Lata
        /// </example>
        public string Type { get; private set; }

        /// <summary>
        /// Atualização do tipo de produto
        /// </summary>
        /// <param name="type">novo moen para o tipo</param>
        public void UpdateCatalogType(string type)
        {
            Type = type;
        }
    }
}