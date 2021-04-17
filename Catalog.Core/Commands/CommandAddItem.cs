using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Catalog.Core.Commands
{
    public class CommandAddItem : Notifiable, ICommand
    {
        public CommandAddItem() { }
        public CommandAddItem(string id, decimal quantity)
        {
            Id = id;
            Quantity = quantity;
        }

        public string Id { get; set; }
        public decimal Quantity { get; set; }
        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsNotNullOrEmpty(Id, "CatalogItem.Id", "É necessário informar um id")
                .IsGreaterOrEqualsThan(Quantity, 0, "Quantity", "A quantidade a ser inserida deve ser igual ou maior que 0 (zero)"));
        }
    }
}