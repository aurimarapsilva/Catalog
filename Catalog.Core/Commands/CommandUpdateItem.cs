using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Catalog.Core.Commands
{
    public class CommandUpdateItem : Notifiable, ICommand
    {
        public CommandUpdateItem() { }
        public CommandUpdateItem(int id, string name, string description, decimal price, string pictureFileName, int catalogTypeId, int catalogBrandId, decimal restockThreshold, decimal maxStockThreshold)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            PictureFileName = pictureFileName;
            CatalogTypeId = catalogTypeId;
            CatalogBrandId = catalogBrandId;
            RestockThreshold = restockThreshold;
            MaxStockThreshold = maxStockThreshold;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureFileName { get; set; }
        public int CatalogTypeId { get; set; }
        public int CatalogBrandId { get; set; }
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
            .IsGreaterThan(Id, 0, "CatalogItem.Id", "É necessário informar um id")
            .IsGreaterThan(CatalogTypeId, 0, "CatalogType.Id", "É necessário informar um id")
            .IsGreaterThan(CatalogBrandId, 0, "CatalogBrand.Id", "É necessário informar um id"));
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