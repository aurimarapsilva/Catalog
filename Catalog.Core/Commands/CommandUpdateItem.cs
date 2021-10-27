using Catalog.Core.Commands.Contracts;
using Flunt.Notifications;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Core.Commands
{
    /// <summary>
    /// Classe responsável por atualizar os dados do produto
    /// </summary>
    public class CommandUpdateItem : Notifiable, ICommand
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public CommandUpdateItem() { }
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <param name="name">Novo nome</param>
        /// <param name="description">Nova descrição</param>
        /// <param name="price">price</param>
        /// <param name="catalogTypeId">Id do novo tipo</param>
        /// <param name="catalogBrandId">Id da nova marca</param>
        /// <param name="restockThreshold">Novo limiar de reabastecimento</param>
        /// <param name="maxStockThreshold">Novo estoque maximo</param>
        public CommandUpdateItem(int id, string name, string description, decimal price, int catalogTypeId, int catalogBrandId, decimal restockThreshold, decimal maxStockThreshold)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            CatalogTypeId = catalogTypeId;
            CatalogBrandId = catalogBrandId;
            RestockThreshold = restockThreshold;
            MaxStockThreshold = maxStockThreshold;
        }

        /// <summary>
        /// Id do produto
        /// </summary>
        /// <example>
        /// 1
        /// </example>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Nome do produto
        /// </summary>
        /// <example>
        /// Leite condensado
        /// </example>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// Descrição do produto
        /// </summary>
        /// <example>
        /// leite condensado é maravilhoso
        /// </example>
        [MaxLength(120)] 
        public string Description { get; set; }

        /// <summary>
        /// Preço do produto
        /// </summary>
        /// <exemple>
        /// 5.49
        /// </exemple>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Id do tipo de produto
        /// </summary>
        /// <example>
        /// 1
        /// </example>
        [Required]
        public int CatalogTypeId { get; set; }

        /// <summary>
        /// Id da marca do produto
        /// </summary>
        /// <example>
        /// 1
        /// </example>
        [Required]
        public int CatalogBrandId { get; set; }

        /// <summary>
        /// Limiar de reabastecimento
        /// </summary>
        /// <example>
        /// 5
        /// </example>
        public decimal RestockThreshold { get; set; }

        /// <summary>
        /// Estoque maximo
        /// </summary>
        /// <example>
        /// 35
        /// </example>
        public decimal MaxStockThreshold { get; set; }

        /// <summary>
        /// Configurando os campos obrigatorios
        /// </summary>
        private void ConfiguringWorkFields()
        {
            //Cria uma notificação caso não atenda os criterios
            AddNotifications(new Contract()
            .Requires()
            .IsNotNullOrEmpty(Name, "CatalogItem.Name", "Este campo é obrigatório")
            .IsGreaterThan(Price, 0, "CatalogItem.Price", "É necessário colocar um preço maior que 0 (zero)")
            .IsGreaterOrEqualsThan(RestockThreshold, 0, "CatalogItem.RestockThreshold", "É necessário colocar um preço maior que 0 (zero)")
            .IsGreaterOrEqualsThan(MaxStockThreshold, 0, "CatalogItem.MaxStockThreshold", "É necessário colocar um preço maior que 0 (zero)")
            .IsGreaterThan(Id, 0, "CatalogItem.Id", "É necessário informar um id")
            .IsGreaterThan(CatalogTypeId, 0, "CatalogType.Id", "É necessário informar um id")
            .IsGreaterThan(CatalogBrandId, 0, "CatalogBrand.Id", "É necessário informar um id"));
        }

        /// <summary>
        /// Configurando o tamanho maximo dos caracteres
        /// </summary>
        private void SettingTheMaximumFieldSize()
        {
            //Cria uma notificação caso não atenda os criterios
            AddNotifications(new Contract()
            .Requires()
            .HasMaxLengthIfNotNullOrEmpty(Name, 50, "CatalogItem.Name", "Tamanho 50 caracteres")
            .HasMaxLengthIfNotNullOrEmpty(Description, 120, "CatalogItem.Description", "Tamanho 120 caracteres"));
        }


        /// <summary>
        /// Valida se foi informado nos criterios desejados 
        /// </summary>
        public void Validate()
        {
            SettingTheMaximumFieldSize();
            ConfiguringWorkFields();
        }
    }
}