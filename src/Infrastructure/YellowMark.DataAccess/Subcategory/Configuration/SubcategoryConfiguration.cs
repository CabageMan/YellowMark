using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YellowMark.DataAccess;

/// <summary>
/// Subcategory table configuration.
/// </summary>
public class SubcategoryConfiguration : IEntityTypeConfiguration<Domain.Subcategories.Entity.Subcategory>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Domain.Subcategories.Entity.Subcategory> builder)
    {
        builder
            .ToTable("Subcategories")
            .HasKey(s => s.Id);

        builder
            .Property(s => s.CreatedAt)
            .HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));

        builder
            .Property(user => user.UpdatedAt)
            .HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));

        builder
            .Property(s => s.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .HasMany(s => s.Ads)
            .WithOne(ad => ad.Subcategory)
            .HasForeignKey(ad => ad.SubcategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
