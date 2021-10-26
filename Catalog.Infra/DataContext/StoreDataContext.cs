using catalog.infra.DataContext.Maps;
using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace catalog.infra.DataContext
{
    /// <summary>
    /// Contexto do banco de dados
    /// </summary>
    public partial class StoreDataContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public StoreDataContext(DbContextOptions<StoreDataContext> options) : base(options) { }

        /// <summary>
        /// Constructor
        /// </summary>
        public StoreDataContext() { }

        public DbSet<CatalogBrand> Brand { get; set; }
        public DbSet<CatalogType> Type { get; set; }
        public DbSet<CatalogItem> Item { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;User Id=root;Password=masterkey;Database=catalog", x => x.ServerVersion("10.6.4-mariadb"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CatalogBrandMaps());
            modelBuilder.ApplyConfiguration(new CatalogItemMaps());
            modelBuilder.ApplyConfiguration(new CatalogTypeMaps());
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}