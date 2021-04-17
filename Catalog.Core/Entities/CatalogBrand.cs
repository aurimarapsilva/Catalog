namespace Catalog.Core.Entities
{
    public class CatalogBrand : Entity
    {
        public CatalogBrand(string brand)
        {
            Brand = brand;
        }

        public string Brand { get; private set; }

        public void UpdateCatalogBrand(string brand)
        {
            Brand = brand;
        }
    }
}