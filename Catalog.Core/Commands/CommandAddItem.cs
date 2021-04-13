using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Catalog.Core.Commands
{
    public class CommandAddItem : Notifiable, ICommand
    {
        public CommandAddItem() { }
        public CommandAddItem(int id, decimal quantity)
        {
            Id = id;
            Quantity = quantity;
        }

        public int Id { get; set; }
        public decimal Quantity { get; set; }
        public void Validate()
        {
            AddNotifications(new Contract()
                .Requires()
                .IsGreaterThan(Id, 0, "CatalogItem.Id", "O id informado deve ser maior que 0(zero)")
                .IsGreaterOrEqualsThan(Quantity, 0, "Quantity", "A quantidade a ser inserida deve ser igual ou maior que 0 (zero)"));
        }
    }
}