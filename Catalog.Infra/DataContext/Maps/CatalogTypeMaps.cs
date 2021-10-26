using Catalog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace catalog.infra.DataContext.Maps
{
    public class CatalogTypeMaps : IEntityTypeConfiguration<CatalogType>
    {
        public void Configure(EntityTypeBuilder<CatalogType> builder)
        {
            builder.ToTable("Type");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("typeId").HasColumnType("INT");
            builder.Property(x => x.Type).HasColumnName("name").HasColumnType("varchar(100)");
        }
    }
}
