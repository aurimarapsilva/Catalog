using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Catalog.Core.Commands
{
    /// <summary>
    /// Classe responsável pelo comando de retirada de um item
    /// </summary>
    public class CommandRemoveItem : Notifiable, ICommand
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CommandRemoveItem() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <param name="quantityDesired">Quantidade de itens a ser retirado do estoque</param>
        public CommandRemoveItem(int id, decimal quantityDesired)
        {
            Id = id;
            QuantityDesired = quantityDesired;
        }

        /// <summary>
        /// Id do produto
        /// </summary>
        /// <example>
        /// 1
        /// </example>
        public int Id { get; set; }

        /// <summary>
        /// Quantidade desejada
        /// </summary>
        /// <example>
        /// 3.500
        /// </example>
        public decimal QuantityDesired { get; set; }

        /// <summary>
        /// Valida se foi informado tudo nos criterios desejados 
        /// </summary>
        public void Validate()
        {
            //Cria uma notificação caso não atenda os criterios
            AddNotifications(new Contract()
            .Requires()
            .IsGreaterThan(Id, 0, "CatalogItem.Id", "É necessário informar um id")
            .IsGreaterThan(QuantityDesired, 0, "QauntityDesired", "A quantidade que deseja retirar do estoque deve ser maior que 0 (zero)"));
        }
    }
}