using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Catalog.Core.Commands
{
    public class CommandRemoveItem : Notifiable, ICommand
    {
        public CommandRemoveItem() { }
        public CommandRemoveItem(string id, decimal quantityDesired)
        {
            Id = id;
            QuantityDesired = quantityDesired;
        }

        public string Id { get; set; }
        public decimal QuantityDesired { get; set; }
        public void Validate()
        {
            AddNotifications(new Contract()
            .Requires()
            .IsNotNullOrEmpty(Id, "CatalogItem.Id", "É necessário informar um id")
            .IsGreaterThan(QuantityDesired, 0, "QauntityDesired", "A quantidade que deseja retirar do estoque deve ser maior que 0 (zero)"));
        }
    }
}