using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infra.DataContext.EntityConfiguration
{
    public class CatalogTypeConfiguration : IEntityTypeConfiguration<CatalogType>
    {
        public void Configure(EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToTable("Type");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired(true).HasColumnType("varchar(8)").HasMaxLength(8);
            builder.Property(x => x.Type).IsRequired(true).HasColumnType("varchar(100)").HasMaxLength(100);
        }
    }
}