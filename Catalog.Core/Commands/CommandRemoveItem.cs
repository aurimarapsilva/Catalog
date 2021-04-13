using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Catalog.Core.Commands
{
    public class CommandRemoveItem : Notifiable, ICommand
    {
        public CommandRemoveItem() { }
        public CommandRemoveItem(int id, decimal quantityDesired)
        {
            Id = id;
            QuantityDesired = quantityDesired;
        }

        public int Id { get; set; }
        public decimal QuantityDesired { get; set; }
        public void Validate()
        {
            AddNotifications(new Contract()
            .Requires()
            .IsGreaterThan(Id, 0, "CatalogItem.Id", "O id informado deve maior que 0 (zero)")
            .IsGreaterThan(QuantityDesired, 0, "QauntityDesired", "A quantidade que deseja retirar do estoque deve ser maior que 0 (zero)"));
        }
    }
}