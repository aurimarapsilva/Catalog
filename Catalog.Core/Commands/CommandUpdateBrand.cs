using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Core.Commands
{
    /// <summary>
    /// Classe responsável por realizar a altualização da marca
    /// </summary>
    public class CommandUpdateBrand : Notifiable, ICommand
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CommandUpdateBrand() { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Id da marca</param>
        /// <param name="brand">nome a qual a marca sera atualizada</param>
        public CommandUpdateBrand(int id, string brand)
        {
            Id = id;
            Brand = brand;
        }

        /// <summary>
        /// Id da marca
        /// </summary>
        /// <example>
        /// 1
        /// </example>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Novo nome para a marca
        /// </summary>
        /// <example>
        /// Italac
        /// </example>
        [Required]
        [MaxLength(100)]
        public string Brand { get; set; }

        /// <summary>
        /// Valida se foi informado uma marca nos criterios desejados 
        /// </summary>
        public void Validate()
        {
            //Cria uma notificação caso não atenda os criterios
            AddNotifications(new Contract()
            .Requires()
            .IsNotNullOrEmpty(Brand, "CatalogBrand.Brand", "É necessário informar uma marca")
            .IsGreaterThan(Id, 0, "CatalogBrand.Id", "É necessário informar um id")
            .HasMaxLengthIfNotNullOrEmpty(Brand, 100, "CatalogBrand.Brand", "Tamanho 100 caracteres"));
        }
    }
}