using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;

namespace Catalog.Core.Commands
{
    /// <summary>
    /// Classe responsável por atualizar o tipo do pruduto
    /// </summary>
    public class CommandUpdateType : Notifiable, ICommand
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CommandUpdateType() { }
        
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="id">Id do tipo</param>
        /// <param name="type">Novo nome para o tipo</param>
        public CommandUpdateType(int id, string type)
        {
            Id = id;
            Type = type;
        }

        /// <summary>
        /// Id do tipo do produto
        /// </summary>
        /// <example>
        /// 1
        /// </example>
        public int Id { get; set; }

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
            .IsGreaterThan(Id, 0, "CatalogBrand.Id", "Deve-se informar o id")
            .IsNotNullOrEmpty(Type, "CatalogBrand.Type", "Deve-se informar o tipo")
            .HasMaxLengthIfNotNullOrEmpty(Type, 100, "CatalogBrand.Type", "Tamanho 100 Caracteres"));
        }
    }
}