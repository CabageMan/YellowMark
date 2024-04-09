using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YellowMark.DataAccess.Category.Configuration;

/// <summary>
/// Categories table configuration.
/// </summary>
public class CategoryConfiguration : IEntityTypeConfiguration<Domain.Categories.Entity.Category>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Domain.Categories.Entity.Category> builder)
    {
        builder
            .ToTable("Categories")
            .HasKey(c => c.Id);

        builder
            .Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .HasMany(c => c.Subcategories)
            .WithOne(s => s.Category)
            .HasForeignKey(s => s.CategoryId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
