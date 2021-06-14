using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Catalog.Core.Commands
{
    /// <summary>
    /// Esta classe cuida do comando de adicionar um novo item ao produto ja cadastrado em sistema
    /// </summary>
    public class CommandAddItem : Notifiable, ICommand
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CommandAddItem() { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="quantity"></param>
        public CommandAddItem(int id, decimal quantity)
        {
            Id = id;
            Quantity = quantity;
        }

        /// <summary>
        /// Id do produto cadastrado
        /// </summary>
        /// <example>
        /// 2
        /// </example>
        public int Id { get; set; }
        
        /// <summary>
        /// Quantidade que deseja adicionar no estoque
        /// </summary>
        /// <example>
        /// 1.600
        /// </example>
        public decimal Quantity { get; set; }

        /// <summary>
        /// Valida se foi informado uma quantidade nos criterios desejados 
        /// </summary>
        public void Validate()
        {
            //Cria uma notificação caso não atenda os criterios
            AddNotifications(new Contract()
                .Requires()
                //Deve se informar um id maior que 0
                .IsGreaterThan(Id, 0, "CatalogItem.Id", "É necessário informar um id")
                //Deve informar uma quantidade maior ou igal a 0
                .IsGreaterThan(Quantity, 0, "Quantity", "A quantidade a ser inserida deve ser igual ou maior que 0 (zero)"));
        }
    }
}