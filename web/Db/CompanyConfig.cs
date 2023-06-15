using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace web.Db;
/*
public class CompanyConfig : IEntityTypeConfiguration<Company>
{
    void IEntityTypeConfiguration<Company>.Configure(EntityTypeBuilder<Company> modelBuilder)
    {
        modelBuilder
           .Property(x => x.created)
           .ValueGeneratedOnAdd()
           .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        modelBuilder
            .Property(p => p.rowversion)
           .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");
        modelBuilder
           .Property(x => x.modified)
           .HasDefaultValueSql("CURRENT_TIMESTAMP(6)");

    }
}
*/