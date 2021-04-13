using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Catalog.Core.Commands
{
    public class CommandUpdateBrand : Notifiable, ICommand
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public void Validate()
        {
            AddNotifications(new Contract()
            .Requires()
            .IsNotNullOrEmpty(Brand, "CatalogBrand.Brand", "É necessário informar uma marca")
            .IsNotNullOrEmpty(Id, "CatalogBrand.Id", "É necessário informar um id")
            .HasMaxLengthIfNotNullOrEmpty(Brand, 100, "CatalogBrand.Brand", "Tamanho 100 caracteres"));
        }
    }
}