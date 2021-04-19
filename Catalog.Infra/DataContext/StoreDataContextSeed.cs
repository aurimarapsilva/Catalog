using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Catalog.Infra.DataContext
{
    public class StoreDataContextSeed
    {
        public async Task SeedAsync(StoreDataContext storeDataContext, IWebHostEnvironment env, IOptions<CatalogSettings> settings, ILogger<StoreDataContextSeed> logger)
        {

        }

    }
}