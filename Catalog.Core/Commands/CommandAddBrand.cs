using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Catalog.Core.Commands
{
    /// <summary>
    /// Esta classe cuida do comando de adicionar uma nova marca para o produto
    /// </summary>
    public class CommandAddBrand : Notifiable, ICommand
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CommandAddBrand() { }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="brand"></param>
        public CommandAddBrand(string brand)
        {
            Brand = brand;
        }

        /// <summary>
        /// Marca do produto
        /// </summary>
        /// <example>
        /// Nestlé
        /// </example>
        public string Brand { get; set; }

        /// <summary>
        /// Valida se foi informado nos criterios desejados 
        /// </summary>
        public void Validate()
        {
            //Cria uma notificação caso não atenda os criterios
            AddNotifications(new Contract()
            .Requires()
            .IsNotNullOrEmpty(Brand, "CatalogBrand.Brand", "É necessário informar uma marca")
            .HasMaxLengthIfNotNullOrEmpty(Brand, 100, "CatalogBrand.Brand", "Tamanho 100 caracteres"));
        }
    }
}