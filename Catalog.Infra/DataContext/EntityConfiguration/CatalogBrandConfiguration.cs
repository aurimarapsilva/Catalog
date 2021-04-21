using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infra.DataContext.EntityConfiguration
{
    public class CatalogBrandConfiguration : IEntityTypeConfiguration<CatalogBrand>
    {
        public void Configure(EntityTypeBuilder<CatalogBrand> builder)
        {
            builder.ToTable("Brand");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired(true).HasColumnType("int(11)").HasMaxLength(11);
            builder.Property(x => x.Brand).IsRequired(true).HasColumnType("varchar(100)").HasMaxLength(100);
        }
    }
}