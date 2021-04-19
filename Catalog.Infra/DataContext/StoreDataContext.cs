using Catalog.Core.Entities;
using Catalog.Infra.DataContext.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Infra.DataContext
{
    public partial class StoreDataContext : DbContext
    {
        public StoreDataContext() { }
        public StoreDataContext(DbContextOptions options) : base(options: options) { }

        public DbSet<CatalogBrand> Brand { get; set; }
        public DbSet<CatalogItem> Item { get; set; }
        public DbSet<CatalogType> Type { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CatalogBrandConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogItemConfiguration());
            modelBuilder.ApplyConfiguration(new CatalogTypeConfiguration());
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}