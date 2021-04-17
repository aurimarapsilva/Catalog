namespace Catalog.Core.Entities
{
    public class CatalogType : Entity
    {
        public CatalogType(string type)
        {
            Type = type;
        }

        public string Type { get; private set; }

        public void UpdateCatalogType(string type)
        {
            Type = type;
        }
    }
}