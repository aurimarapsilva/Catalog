using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace catalog.infra.DataContext.Maps
{
    public class CatalogBrandMaps : IEntityTypeConfiguration<CatalogBrand>
    {
        public void Configure(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.ToTable("Brand");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("brandId").HasColumnType("INT");
            builder.Property(x => x.Brand).HasColumnName("name").HasColumnType("varchar(100)");
        }
    }
}
