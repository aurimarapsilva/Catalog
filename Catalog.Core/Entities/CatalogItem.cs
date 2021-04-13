using System;

namespace Catalog.Core.Entities
{
    public class CatalogItem : Entity
    {
        public CatalogItem(string name, string description, decimal price, string catalogTypeId, string catalogBrandId, decimal restockThreshold = 0, decimal maxStockThreshold = 0, string pictureFileName = null, string pictureUri = null)
        {
            Name = name;
            Description = description;
            Price = price;
            PictureFileName = pictureFileName;
            PictureUri = pictureUri;
            CatalogTypeId = catalogTypeId;
            CatalogBrandId = catalogBrandId;
            AvailableStock = 0;
            RestockThreshold = restockThreshold;
            MaxStockThreshold = maxStockThreshold;
            LastUpdate = DateTime.Now;
        }

        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal Price { get; private set; }
        public string PictureFileName { get; private set; }
        public string PictureUri { get; private set; }
        public string CatalogTypeId { get; private set; }
        public string CatalogBrandId { get; private set; }

        // Quantidade em estoque
        public decimal AvailableStock { get; private set; }
        // Estoque disponível no qual devemos fazer um novo pedido
        public decimal RestockThreshold { get; private set; }
        // Número máximo de unidades que podem estar em estoque a qualquer momento (devido a restrições físicas / logísticas nos armazéns)
        public decimal MaxStockThreshold { get; private set; }
        // Ultima data de Atualização
        public DateTime LastUpdate { get; private set; }

        // Diminui a quantidade de um item específico no estoque e garante que o limiar de reabastecimento não
        // foi violado. Nesse caso, uma solicitação de reabastecimento é gerado no virificar limiar.
        //
        // Se houver estoque suficiente de um item, o número inteiro retornado no final desta chamada deve ser o mesmo que quantidade Desejada.
        // Caso não haja estoque suficiente disponível, o método removerá todo o estoque disponível e retornará essa quantidade ao cliente.
        // Nesse caso, é responsabilidade do cliente determinar se o valor que é devolvido é o mesmo que quantidade Desejada.
        // É inválido passar um número negativo.
        public decimal RemoveStock(int quantityDesired)
        {
            if (AvailableStock == 0)
                AddNotification("CatalogItem.AvailableStock", $"Estoque vazio, item de produto {Name} está esgotado");

            if (quantityDesired <= 0)
                AddNotification("CatalogItem.QuantityDesired", "As unidades de item desejadas devem ser maiores que zero");

            var removed = Math.Min(quantityDesired, AvailableStock);

            AvailableStock -= removed;

            LastUpdate = DateTime.Now;

            return removed;
        }

        // Aumenta a quantidade de um determinado item no estoque.
        public decimal AddStock(decimal quantity)
        {
            var original = AvailableStock;

            // A quantidade que o cliente está tentando adicionar ao estoque é maior do que a que pode ser acomodada fisicamente no Armazém
            if ((AvailableStock + quantity) > MaxStockThreshold)
                // Por enquanto, este método apenas adiciona novas unidades até o limite máximo de estoque. Em uma versão expandida deste aplicativo, nós
                // pode incluir o rastreamento das unidades restantes e armazenar informações sobre o estoque excedente em outros lugares.
                AvailableStock += (MaxStockThreshold - AvailableStock);
            else
                AvailableStock += quantity;

            LastUpdate = DateTime.Now;

            return AvailableStock - original;
        }

        // Atualiza os dados do item 
        public void UpdateCatalogItem(string name, string description, decimal price, string catalogTypeId, string catalogBrandId, decimal restockThreshold, decimal maxStockThreshold, string pictureFileName, string pictureUri)
        {
            Name = name;
            Description = description;
            Price = price;
            PictureFileName = pictureFileName;
            PictureUri = pictureUri;
            CatalogTypeId = catalogTypeId;
            CatalogBrandId = catalogBrandId;
            AvailableStock = 0;
            RestockThreshold = restockThreshold;
            MaxStockThreshold = maxStockThreshold;
            LastUpdate = DateTime.Now;
        }
    }
}