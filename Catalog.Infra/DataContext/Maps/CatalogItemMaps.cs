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
            builder.Property(x => x.Name).HasColumnName("productId").HasColumnType("VARCHAR(50)").IsRequired();
            builder.Property(x => x.Description).HasColumnName("productId").HasColumnType("VARCHAR(120)").IsRequired();
            builder.Property(x => x.Price).HasColumnName("productId").HasColumnType("DECIMAL").IsRequired();
            builder.Ignore(x => x.PictureFileName);
            builder.Ignore(x => x.PictureUri);
            builder.Property(x => x.CatalogTypeId).HasColumnName("productId").HasColumnType("INT").IsRequired();
            builder.Property(x => x.CatalogBrandId).HasColumnName("productId").HasColumnType("INT").IsRequired();
            builder.Property(x => x.AvailableStock).HasColumnName("productId").HasColumnType("INDECIMALT").IsRequired();
            builder.Property(x => x.RestockThreshold).HasColumnName("productId").HasColumnType("DECIMAL").IsRequired();
            builder.Property(x => x.MaxStockThreshold).HasColumnName("productId").HasColumnType("DECIMAL").IsRequired();
        }
    }
}
