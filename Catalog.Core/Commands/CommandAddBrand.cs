using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Catalog.Core.Commands
{
    public class CommandAddBrand : Notifiable, ICommand
    {
        public CommandAddBrand() { }
        public CommandAddBrand(string brand)
        {
            Brand = brand;
        }

        public string Brand { get; set; }
        public void Validate()
        {
            AddNotifications(new Contract()
            .Requires()
            .IsNotNullOrEmpty(Brand, "CatalogBrand.Brand", "É necessário informar uma marca")
            .HasMaxLengthIfNotNullOrEmpty(Brand, 100, "CatalogBrand.Brand", "Tamanho 100 caracteres"));
        }
    }
}