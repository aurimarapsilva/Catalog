using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Catalog.Core.Commands
{
    public class CommandUpdateType : Notifiable, ICommand
    {
        public CommandUpdateType() { }
        public CommandUpdateType(int id, string type)
        {
            Id = id;
            Type = type;
        }

        public int Id { get; set; }
        public string Type { get; set; }
        public void Validate()
        {
            AddNotifications(new Contract()
            .Requires()
            .IsGreaterThan(Id, 0, "CatalogBrand.Id", "Deve-se informar o id")
            .IsNotNullOrEmpty(Type, "CatalogBrand.Type", "Deve-se informar o tipo")
            .HasMaxLengthIfNotNullOrEmpty(Type, 100, "CatalogBrand.Type", "Tamanho 100 Caracteres"));
        }
    }
}