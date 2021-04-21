using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infra.DataContext.EntityConfiguration
{
    public class CatalogItemConfiguration : IEntityTypeConfiguration<CatalogItem>
    {
        public void Configure(EntityTypeBuilder<CatalogItem> builder)
        {
            builder.ToTable("Catalog");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired(true).HasColumnType("int(11)").HasMaxLength(11);
            builder.Property(x => x.Name).IsRequired(true).HasColumnType("varchar(50)").HasMaxLength(50);
            builder.Property(x => x.Price).IsRequired(false).HasColumnType("decimal(13,2)");
            builder.Property(x => x.PictureFileName).IsRequired(false).HasColumnType("varchar(160)").HasMaxLength(160);
            builder.Property(x => x.Description).IsRequired(false).HasColumnType("varchar(120)").HasMaxLength(120);
            builder.Property(x => x.AvailableStock).IsRequired(true).HasColumnType("decimal(11,10)");
            builder.Property(x => x.RestockThreshold).IsRequired(true).HasColumnType("decimal(11,10)");
            builder.Property(x => x.MaxStockThreshold).IsRequired(true).HasColumnType("decimal(11,10)");
            builder.Property(x => x.LastUpdate).IsRequired(true).HasColumnType("dateTime");
            builder.Property(x => x.CatalogBrandId).IsRequired(true).HasColumnType("int(11)").HasMaxLength(11);
            builder.HasOne(x => x.CatalogBrand).WithMany().HasForeignKey(x => x.CatalogBrandId);
            builder.Property(x => x.CatalogTypeId).IsRequired(true).HasColumnType("int(11)").HasMaxLength(11);
            builder.HasOne(x => x.CatalogType).WithMany().HasForeignKey(x => x.CatalogTypeId);
            builder.Ignore(x => x.PictureUri);
        }
    }
}