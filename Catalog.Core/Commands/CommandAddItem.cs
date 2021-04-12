using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Catalog.Core.Commands
{
    public class CommandAddItem : Notifiable, ICommand
    {
        public CommandAddItem() { }
        public CommandAddItem(int id, decimal value)
        {
            Id = id;
            Value = value;
        }

        public int Id { get; set; }
        public decimal Value { get; set; }
        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(Id, 0, "CatalogItem.Id", "Deve informar o id do item maior que 0 (zero)"));
        }
    }
}