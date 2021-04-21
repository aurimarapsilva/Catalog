using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Catalog.Core.Commands
{
    public class CommandUpdateBrand : Notifiable, ICommand
    {
        public CommandUpdateBrand() { }
        public CommandUpdateBrand(int id, string brand)
        {
            Id = id;
            Brand = brand;
        }

        public int Id { get; set; }
        public string Brand { get; set; }
        public void Validate()
        {
            AddNotifications(new Contract()
            .Requires()
            .IsNotNullOrEmpty(Brand, "CatalogBrand.Brand", "É necessário informar uma marca")
            .IsGreaterThan(Id, 0, "CatalogBrand.Id", "É necessário informar um id")
            .HasMaxLengthIfNotNullOrEmpty(Brand, 100, "CatalogBrand.Brand", "Tamanho 100 caracteres"));
        }
    }
}