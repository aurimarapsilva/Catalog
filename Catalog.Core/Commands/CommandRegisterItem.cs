using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Catalog.Core.Commands
{
    public class CommandRegisterItem : Notifiable, ICommand
    {
        public CommandRegisterItem() { }
        public CommandRegisterItem(string name, string description, decimal price, string pictureFileName, string catalogTypeId, string catalogBrandId, decimal restockThreshold, decimal maxStockThreshold)
        {
            Name = name;
            Description = description;
            Price = price;
            PictureFileName = pictureFileName;
            CatalogTypeId = catalogTypeId;
            CatalogBrandId = catalogBrandId;
            RestockThreshold = restockThreshold;
            MaxStockThreshold = maxStockThreshold;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureFileName { get; set; }
        public string CatalogTypeId { get; set; }
        public string CatalogBrandId { get; set; }
        public decimal RestockThreshold { get; set; }
        public decimal MaxStockThreshold { get; set; }

        private void ConfiguringWorkFields()
        {
            AddNotifications(new Contract()
            .Requires()
            .IsNotNullOrEmpty(Name, "CatalogItem.Name", "Este campo é obrigatório")
            .IsGreaterThan(Price, 0, "CatalogItem.Price", "É necessário colocar um preço maior que 0 (zero)")
            .IsGreaterOrEqualsThan(RestockThreshold, 0, "CatalogItem.RestockThreshold", "É necessário colocar um preço maior que 0 (zero)")
            .IsGreaterOrEqualsThan(MaxStockThreshold, 0, "CatalogItem.MaxStockThreshold", "É necessário colocar um preço maior que 0 (zero)")
            .IsNotNullOrEmpty(CatalogTypeId, "CatalogType.Id", "É necessário informar um id")
            .IsNotNullOrEmpty(CatalogBrandId, "CatalogBrand.Id", "É necessário informar um id"));
        }

        private void SettingTheMaximumFieldSize()
        {
            AddNotifications(new Contract()
            .Requires()
            .HasMaxLengthIfNotNullOrEmpty(Name, 50, "CatalogItem.Name", "Tamanho 50 caracteres")
            .HasExactLengthIfNotNullOrEmpty(Description, 120, "CatalogItem.Description", "Tamanho 120 caracteres")
            .HasExactLengthIfNotNullOrEmpty(PictureFileName, 160, "CatalogItem.PictureFileName", "Tamanho 160 caracteres"));
        }


        public void Validate()
        {
            SettingTheMaximumFieldSize();
            ConfiguringWorkFields();
        }
    }
}