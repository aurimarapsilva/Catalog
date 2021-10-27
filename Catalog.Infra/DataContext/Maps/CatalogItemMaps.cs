using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace catalog.infra.DataContext.Maps
{
    public class CatalogItemMaps : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("productId").HasColumnType("INT");
            builder.Property(x => x.Name).HasColumnName("name").HasColumnType("VARCHAR(50)").IsRequired();
            builder.Property(x => x.Description).HasColumnName("description").HasColumnType("VARCHAR(120)");
            builder.Property(x => x.Price).HasColumnName("price").HasColumnType("DECIMAL(15,2)").IsRequired();
            builder.Property(x => x.CatalogTypeId).HasColumnName("catalogTypeId").HasColumnType("INT").IsRequired();
            builder.Property(x => x.CatalogBrandId).HasColumnName("CatalogBrandId").HasColumnType("INT").IsRequired();
            builder.Property(x => x.AvailableStock).HasColumnName("availableStock").HasColumnType("DECIMAL(15,4)").IsRequired();
            builder.Property(x => x.RestockThreshold).HasColumnName("restockThreshold").HasColumnType("DECIMAL(15,4)").IsRequired();
            builder.Property(x => x.MaxStockThreshold).HasColumnName("maxStockThreshold").HasColumnType("DECIMAL(15,4)").IsRequired();
            builder.Property(x => x.LastUpdate).HasColumnName("lastUpdate").HasColumnType("DateTime").IsRequired();
        }
    }
}
