using DeStaProduction.Infrastucture.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class DeStaUserConfiguration : IEntityTypeConfiguration<DeStaUser>
{
    public void Configure(EntityTypeBuilder<DeStaUser> builder)
    {
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(50);
    }
}