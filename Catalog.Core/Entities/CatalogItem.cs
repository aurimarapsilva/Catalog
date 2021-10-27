using System;

namespace Catalog.Core.Entities
{
    /// <summary>
    /// Classe modelo para produtos
    /// </summary>
    public class CatalogItem : Entity
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Nome</param>
        /// <param name="description">Descrição</param>
        /// <param name="price">Price</param>
        /// <param name="catalogTypeId">Id tipo</param>
        /// <param name="catalogBrandId">Id Marca</param>
        /// <param name="restockThreshold">Limiar de reabastecimento</param>
        /// <param name="maxStockThreshold">Estoque maximo</param>
        public CatalogItem(string name, string description, decimal price, int catalogTypeId, int catalogBrandId, decimal restockThreshold, decimal maxStockThreshold)
        {
            Name = name;
            Description = description;
            Price = price;
            CatalogTypeId = catalogTypeId;
            CatalogBrandId = catalogBrandId;
            AvailableStock = 0;
            RestockThreshold = restockThreshold;
            MaxStockThreshold = maxStockThreshold;
            LastUpdate = DateTime.Now;
        }

        /// <summary>
        /// Nome
        /// </summary>
        /// <example>
        /// Leite condensado
        /// </example>
        public string Name { get; private set; }

        /// <summary>
        /// Descrição
        /// </summary>
        /// <example>
        /// 8ª maravilha do mundo
        /// </example>
        public string Description { get; private set; }

        /// <summary>
        /// preço
        /// </summary>
        /// <example>
        /// 5.49
        /// </example>
        public decimal Price { get; private set; }
        
        /// <summary>
        /// Id do tipo do produto
        /// </summary>
        /// <example>
        /// 1
        /// </example>
        public int CatalogTypeId { get; private set; }

        /// <summary>
        /// Tipo do produto
        /// </summary>
        public CatalogType CatalogType { get; private set; }

        /// <summary>
        /// Id da marca do produto
        /// </summary>
        /// <example>
        /// 1
        /// </example>
        public int CatalogBrandId { get; private set; }

        /// <summary>
        /// Marca do produto
        /// </summary>
        public CatalogBrand CatalogBrand { get; private set; }

        /// <summary>
        /// Quantidade em estoque
        /// </summary>
        /// <example>
        /// 40
        /// </example>
        public decimal AvailableStock { get; private set; }

        /// <summary>
        /// Estoque disponível no qual devemos fazer um novo pedido
        /// </summary>
        /// <example>
        /// 10
        /// </example>
        public decimal RestockThreshold { get; private set; }

        /// <summary>
        /// Número máximo de unidades que podem estar em estoque a qualquer momento (devido a restrições físicas / logísticas nos armazéns)
        /// </summary>
        /// <example>
        /// 70
        /// </example>
        public decimal MaxStockThreshold { get; private set; }

        /// <summary>
        /// Ultima data de Atualização
        /// </summary>
        /// <example>
        /// 2021-05-25T14:41
        /// </example>
        public DateTime LastUpdate { get; private set; }
        
        /// <summary>
        /// Diminui a quantidade de um item específico no estoque e garante que o limiar de reabastecimento não
        /// foi violado. Nesse caso, uma solicitação de reabastecimento é gerado no virificar limiar.
        /// 
        /// Se houver estoque suficiente de um item, o número inteiro retornado no final desta chamada deve ser o mesmo que quantidade Desejada.
        /// Caso não haja estoque suficiente disponível, o método removerá todo o estoque disponível e retornará essa quantidade ao cliente.
        /// Nesse caso, é responsabilidade do cliente determinar se o valor que é devolvido é o mesmo que quantidade Desejada.
        /// É inválido passar um número negativo.
        /// </summary>
        /// <param name="quantityDesired">Quantidade desejada</param>
        /// <returns></returns>
        public decimal RemoveStock(decimal quantityDesired)
        {
            var original = AvailableStock;

            // Valida se possui estoque do produto
            if (AvailableStock == 0)
                throw new NotImplementedException($"Estoque vazio, item de produto {Name} está esgotado");

            // Valida se a quantidade desejada é superior a 0
            if (quantityDesired <= 0)
                throw new ArgumentException($"As unidades de item desejadas devem ser maiores que zero");

            // valida se a quantidade desejada e superior a quantidade do estoque
            if (quantityDesired > AvailableStock)
                throw new ArgumentException("Não possui a quantidade de produtos desejadas");

            // Faz retirada da quantidade desejada
            AvailableStock -= quantityDesired;

            // registra a ultima atualização do produto
            LastUpdate = DateTime.Now;

            return original -  AvailableStock;
        }

        /// <summary>
        /// Aumenta a quantidade de um determinado item no estoque. 
        /// </summary>
        /// <param name="quantity"> quantidade desejada</param>
        /// <returns></returns>
        public decimal AddStock(decimal quantity)
        {
            var original = AvailableStock;

            // a quantidade informada deve ser maior que 0
            if (quantity < 0)
                throw new NotImplementedException("A quantidade informada não pode ser negativa");

            // A quantidade que o cliente está tentando adicionar ao estoque é maior do que a que pode ser
            // acomodada fisicamente no Armazém
            if ((AvailableStock + quantity) > MaxStockThreshold)
                throw new NotImplementedException("A quantidade informada excede o limite maximo do estoque");

            AvailableStock += quantity;
            
            // Registra a ultima atualização no estoque
            LastUpdate = DateTime.Now;

            return AvailableStock - original;
        }

        /// <summary>
        /// Atualiza os dados do item 
        /// </summary>
        /// <param name="name">Nome</param>
        /// <param name="description">Descrição</param>
        /// <param name="price">Price</param>
        /// <param name="catalogTypeId">Id tipo</param>
        /// <param name="catalogBrandId">Id Marca</param>
        /// <param name="restockThreshold">Limiar de reabastecimento</param>
        /// <param name="maxStockThreshold">Estoque maximo</param>
        public void UpdateCatalogItem(string name, string description, decimal price, int catalogTypeId, int catalogBrandId, decimal restockThreshold, decimal maxStockThreshold)
        {
            Name = name;
            Description = description;
            Price = price;
            CatalogTypeId = catalogTypeId;
            CatalogBrandId = catalogBrandId;
            AvailableStock = 0;
            RestockThreshold = restockThreshold;
            MaxStockThreshold = maxStockThreshold;
            LastUpdate = DateTime.Now;
        }
        
    }
}