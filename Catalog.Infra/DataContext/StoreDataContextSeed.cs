using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Catalog.Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Retry;

namespace Catalog.Infra.DataContext
{
    public class StoreDataContextSeed
    {
        public async Task SeedAsync(StoreDataContext context, IWebHostEnvironment env, IOptions<CatalogSettings> settings, ILogger<StoreDataContextSeed> logger)
        {
            var policy = CreatePolicy(logger, nameof(StoreDataContextSeed));

            await policy.ExecuteAsync(async () =>
            {
                var useCustomizationData = settings.Value.UseCustomizationData;
                var contentRootPath = env.ContentRootPath;
                var picturePath = env.WebRootPath;

                if (!context.Brand.Any())
                {
                    await context.Brand.AddRangeAsync(useCustomizationData
                        ? GetCatalogBrandsFromFile(contentRootPath, logger)
                        : GetPreconfiguredCatalogBrands());

                    await context.SaveChangesAsync();
                }

                if (!context.Type.Any())
                {
                    await context.Type.AddRangeAsync(useCustomizationData
                        ? GetCatalogTypesFromFile(contentRootPath, logger)
                        : GetPreconfiguredCatalogTypes());

                    await context.SaveChangesAsync();
                }

                if (!context.Item.Any())
                {
                    await context.Item.AddRangeAsync(useCustomizationData
                        ? GetCatalogItemsFromFile(contentRootPath, context, logger)
                        : GetPreconfiguredItems());

                    await context.SaveChangesAsync();

                    GetCatalogItemPictures(contentRootPath, picturePath);
                }
            });
        }

        private IEnumerable<CatalogBrand> GetCatalogBrandsFromFile(string contentRootPath, ILogger<StoreDataContextSeed> logger)
        {
            string csvFileCatalogBrands = Path.Combine(contentRootPath, "Setup", "CatalogBrands.csv");

            if (!File.Exists(csvFileCatalogBrands))
                return GetPreconfiguredCatalogBrands();

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "catalogbrand" };
                csvheaders = GetHeaders(csvFileCatalogBrands, requiredHeaders);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message);
                return GetPreconfiguredCatalogBrands();
            }

            return File.ReadAllLines(csvFileCatalogBrands)
                                        .Skip(1) // skip header row
                                        .Select(x => CreateCatalogBrand(x))
                                        .Where(x => x != null);
        }

        private CatalogBrand CreateCatalogBrand(string brand)
        {
            brand = brand.Trim('"').Trim();

            if (String.IsNullOrEmpty(brand))
                throw new Exception("catalog Brand Name is empty");

            return new CatalogBrand(brand: brand);
        }

        private IEnumerable<CatalogBrand> GetPreconfiguredCatalogBrands()
        {
            return new List<CatalogBrand>()
            {
                new CatalogBrand(brand: "Azure"),
                new CatalogBrand(brand: ".NET"),
                new CatalogBrand(brand: "Visual Studio"),
                new CatalogBrand(brand: "SQL Server"),
                new CatalogBrand(brand: "Other")
            };
        }

        private IEnumerable<CatalogType> GetCatalogTypesFromFile(string contentRootPath, ILogger<StoreDataContextSeed> logger)
        {
            string csvFileCatalogTypes = Path.Combine(contentRootPath, "Setup", "CatalogTypes.csv");

            if (!File.Exists(csvFileCatalogTypes))
                return GetPreconfiguredCatalogTypes();

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "catalogtype" };
                csvheaders = GetHeaders(csvFileCatalogTypes, requiredHeaders);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "EXCEPTION ERROR: {Message}", ex.Message);
                return GetPreconfiguredCatalogTypes();
            }

            return File.ReadAllLines(csvFileCatalogTypes)
                                        .Skip(1) // skip header row
                                        .Select(x => CreateCatalogType(x))
                                        .Where(x => x != null);
        }

        private CatalogType CreateCatalogType(string type)
        {
            type = type.Trim('"').Trim();

            if (String.IsNullOrEmpty(type))
                throw new Exception("catalog Type Name is empty");

            return new CatalogType(type: type);
        }

        private IEnumerable<CatalogType> GetPreconfiguredCatalogTypes()
        {
            return new List<CatalogType>()
            {
                new CatalogType(type: "T-Shirt"),
                new CatalogType(type: "Mug"),
                new CatalogType(type: "Sheet"),
                new CatalogType(type: "USB Memory Stick")
            };
        }

        private IEnumerable<CatalogItem> GetCatalogItemsFromFile(string contentRootPath, StoreDataContext context, ILogger<StoreDataContextSeed> logger)
        {
            string csvFileCatalogItems = Path.Combine(contentRootPath, "Setup", "CatalogItems.csv");

            if (!File.Exists(csvFileCatalogItems))
                return GetPreconfiguredItems();

            string[] csvheaders;
            try
            {
                string[] requiredHeaders = { "catalogtypename", "catalogbrandname", "description", "name", "price", "picturefilename" };
                string[] optionalheaders = { "availablestock", "restockthreshold", "maxstockthreshold", "onreorder" };
                csvheaders = GetHeaders(csvFileCatalogItems, requiredHeaders, optionalheaders);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro de exceção: {Message}", ex.Message);
                return GetPreconfiguredItems();
            }

            var catalogTypeIdLookup = context.Type.ToDictionary(ct => ct.Type, ct => ct.Id);
            var catalogBrandIdLookup = context.Brand.ToDictionary(ct => ct.Brand, ct => ct.Id);

            return File.ReadAllLines(csvFileCatalogItems)
                        .Skip(1) // pular a linha do cabeçalho
                        .Select(row => Regex.Split(row, ",(?=(?:[^\"]*\"[^\"]*\")*[^\"]*$)"))
                        .Select(column => CreateCatalogItem(column: column, headers: csvheaders, catalogTypeIdLookup: catalogTypeIdLookup, catalogBrandIdLookup: catalogBrandIdLookup))
                        .Where(x => x != null);
        }

        private CatalogItem CreateCatalogItem(string[] column, string[] headers, Dictionary<String, string> catalogTypeIdLookup, Dictionary<string, string> catalogBrandIdLookup)
        {
            var vRestockThreshold = 0m;
            var vMaxStockThreshold = 0m;

            if (column.Count() != headers.Count())
                throw new Exception($"Contagem de coluna'{column.Count()}' não é o mesmo que a contagem de cabeçalho'{headers.Count()}'");

            string catalogTypeName = column[Array.IndexOf(headers, "catalogtypename")].Trim('"').Trim();

            if (!catalogTypeIdLookup.ContainsKey(catalogTypeName))
                throw new Exception($"Tipo={catalogTypeName} não existe em catalogTypes");

            string catalogBrandName = column[Array.IndexOf(headers, "catalogbrandname")].Trim('"').Trim();

            if (!catalogBrandIdLookup.ContainsKey(catalogBrandName))
                throw new Exception($"Marca={catalogBrandName} não existe em catalogBrands");

            string priceString = column[Array.IndexOf(headers, "price")].Trim('"').Trim();

            if (!decimal.TryParse(priceString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out decimal price))
                throw new Exception($"Preço={priceString}não é um numero decimal valido");

            int restockThresholdIndex = Array.IndexOf(headers, "restockthreshold");
            if (restockThresholdIndex != -1)
            {
                string restockThresholdString = column[restockThresholdIndex].Trim('"').Trim();
                if (!String.IsNullOrEmpty(restockThresholdString))
                    if (decimal.TryParse(restockThresholdString, out decimal restockThreshold))
                        vRestockThreshold = restockThreshold;
                    else
                        throw new Exception($"Limiar de reabastecimento= {restockThreshold} não é um numero decimal valido");
            }

            int maxStockThresholdIndex = Array.IndexOf(headers, "maxstockthreshold");
            if (maxStockThresholdIndex != -1)
            {
                string maxStockThresholdString = column[maxStockThresholdIndex].Trim('"').Trim();
                if (!String.IsNullOrEmpty(maxStockThresholdString))
                {
                    if (int.TryParse(maxStockThresholdString, out int maxStockThreshold))
                        vMaxStockThreshold = maxStockThreshold;
                    else
                        throw new Exception($"Limite maximo de estoque= {maxStockThreshold} não é um numero decimal valido");
                }
            }

            var catalogItem = new CatalogItem(
                name: column[Array.IndexOf(headers, "name")].Trim('"').Trim(),
                description: column[Array.IndexOf(headers, "description")].Trim('"').Trim(),
                price: price,
                catalogTypeId: catalogTypeIdLookup[catalogTypeName],
                catalogBrandId: catalogBrandIdLookup[catalogBrandName],
                restockThreshold: vRestockThreshold,
                maxStockThreshold: vMaxStockThreshold
            );

            int availableStockIndex = Array.IndexOf(headers, "availablestock");
            if (availableStockIndex != -1)
            {
                string availableStockString = column[availableStockIndex].Trim('"').Trim();
                if (!String.IsNullOrEmpty(availableStockString))
                {
                    if (decimal.TryParse(availableStockString, out decimal availableStock))
                        catalogItem.AddStock(availableStock);
                    else
                        throw new Exception($"Estique disponivel= {availableStockString} não é um numero decimal valido");
                }
            }

            int onReorderIndex = Array.IndexOf(headers, "onreorder");
            if (onReorderIndex != -1)
            {
                var onReorderString = column[onReorderIndex].Trim('"').Trim();
                if (!String.IsNullOrEmpty(onReorderString))
                {
                    if (bool.TryParse(onReorderString, out bool onReorder))
                        catalogItem.NewOrder(onReorder);
                    else
                        throw new Exception($"Pedido novo={onReorderString} não é uma variavel booleana valida");
                }
            }

            return catalogItem;
        }

        private IEnumerable<CatalogItem> GetPreconfiguredItems()
        {
            return new List<CatalogItem>()
            {
                new CatalogItem (name: ".NET Bot Black Hoodie", description: ".NET Bot Black Hoodie", price: 19.5m,catalogBrandId: "2", catalogTypeId: "2", restockThreshold: 2, maxStockThreshold:100, pictureFileName:"1.png"),
                new CatalogItem (name: ".NET Black & White Mug", description: ".NET Black & White Mug", price: 8.5m,catalogBrandId: "1", catalogTypeId: "2", restockThreshold: 2, maxStockThreshold:100, pictureFileName:"2.png"),
                new CatalogItem (name: "Prism White T-Shirt", description: "Prism White T-Shirt", price: 12m,catalogBrandId: "2", catalogTypeId: "5", restockThreshold: 2, maxStockThreshold:100, pictureFileName:"3.png"),
                new CatalogItem (name: ".NET Foundation T-shirt", description: ".NET Foundation T-shirt", price: 12m,catalogBrandId: "2", catalogTypeId: "2", restockThreshold: 2, maxStockThreshold:100, pictureFileName:"4.png"),
                new CatalogItem (name: "Roslyn Red Sheet", description: "Roslyn Red Sheet", price: 8.5m,catalogBrandId: "3", catalogTypeId: "5", restockThreshold: 2, maxStockThreshold:100, pictureFileName:"5.png"),
                new CatalogItem (name: ".NET Blue Hoodie", description: ".NET Blue Hoodie", price: 12m,catalogBrandId: "2", catalogTypeId: "2", restockThreshold: 2, maxStockThreshold:100, pictureFileName:"6.png"),
                new CatalogItem (name: "Roslyn Red T-Shirt", description: "Roslyn Red T-Shirt", price: 12m,catalogBrandId: "2", catalogTypeId: "5", restockThreshold: 2, maxStockThreshold:100, pictureFileName:"7.png"),
                new CatalogItem (name: "Kudu Purple Hoodie", description: "Kudu Purple Hoodie", price: 8.5m,catalogBrandId: "2", catalogTypeId: "5", restockThreshold: 2, maxStockThreshold:100, pictureFileName:"8.png"),
                new CatalogItem (name: "Cup<T> White Mug", description: "Cup<T> White Mug", price: 12m,catalogBrandId: "1", catalogTypeId: "5", restockThreshold: 2, maxStockThreshold:100, pictureFileName:"9.png"),
                new CatalogItem (name: ".NET Foundation Sheet", description: ".NET Foundation Sheet", price: 12m,catalogBrandId: "3", catalogTypeId: "2", restockThreshold: 2, maxStockThreshold:100, pictureFileName:"10.png"),
                new CatalogItem (name: "Cup<T> Sheet", description: "Cup<T> Sheet", price: 8.5m,catalogBrandId: "3", catalogTypeId: "2", restockThreshold: 2, maxStockThreshold:100, pictureFileName:"11.png"),
                new CatalogItem (name: "Prism White TShirt", description: "Prism White TShirt", price: 12m,catalogBrandId: "2", catalogTypeId: "5", restockThreshold: 2, maxStockThreshold:100, pictureFileName:"12.png")
            };
        }

        private string[] GetHeaders(string csvfile, string[] requiredHeaders, string[] optionalHeaders = null)
        {
            var csvheaders = File.ReadLines(csvfile).First().ToLowerInvariant().Split(',');

            if (csvheaders.Count() < requiredHeaders.Count())
                throw new Exception($"Contagem de cabeçalho necessária'{ requiredHeaders.Count()}' é maior que a contagem de cabeçalhos csv '{csvheaders.Count()}' ");

            if (optionalHeaders != null)
                if (csvheaders.Count() > (requiredHeaders.Count() + optionalHeaders.Count()))
                    throw new Exception($"Contagem de cabeçalho csv'{csvheaders.Count()}'  é maior que o necessario '{requiredHeaders.Count()}' e opcional '{optionalHeaders.Count()}' contagem de cabeçalhos");

            foreach (var requiredHeader in requiredHeaders)
            {
                if (!csvheaders.Contains(requiredHeader))
                    throw new Exception($"não contem cabeçalho obrigatório '{requiredHeader}'");
            }

            return csvheaders;
        }

        private void GetCatalogItemPictures(string contentRootPath, string picturePath)
        {
            if (picturePath != null)
            {
                DirectoryInfo directory = new DirectoryInfo(picturePath);
                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }

                string zipFileCatalogItemPictures = Path.Combine(contentRootPath, "Setup", "CatalogItems.zip");
                ZipFile.ExtractToDirectory(zipFileCatalogItemPictures, picturePath);
            }
        }

        private AsyncRetryPolicy CreatePolicy(ILogger<StoreDataContextSeed> logger, string prefix, int retries = 3)
        {
            return Policy.Handle<SqlException>().
                WaitAndRetryAsync(
                    retryCount: retries,
                    sleepDurationProvider: retry => TimeSpan.FromSeconds(5),
                    onRetry: (exception, timeSpan, retry, ctx) =>
                    {
                        logger.LogWarning(exception, "[{prefix}] Exceção {ExceptionType} com mensagem {Message} detectado na tentativa {retry} de {retries}", prefix, exception.GetType().Name, exception.Message, retry, retries);
                    }
                );
        }

    }
}