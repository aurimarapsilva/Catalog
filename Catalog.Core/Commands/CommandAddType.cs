using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Catalog.Core.Commands
{
    /// <summary>
    /// Esta classe é responsável por adicionar um tipo ao produto
    /// </summary>
    public class CommandAddType : Notifiable, ICommand
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CommandAddType() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="type">Informar o tipo do produto</param>
        public CommandAddType(string type)
        {
            Type = type;
        }

        /// <summary>
        /// Tipo do produto
        /// </summary>
        /// <example>
        /// Lata
        /// </example>
        public string Type { get; set; }

        /// <summary>
        /// Valida se foi informado nos criterios desejados 
        /// </summary>
        public void Validate()
        {
            //Cria uma notificação caso não atenda os criterios
            AddNotifications(new Contract()
            .Requires()
            .IsNotNullOrEmpty(Type, "CatalogType.Type", "Deve-se informar o tipo para realizar o cadastro")
            .HasMaxLengthIfNotNullOrEmpty(Type, 100, "CatalogType.Type", "Tamanho 100 caracteres"));
        }
    }
}