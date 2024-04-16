using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YellowMark.DataAccess.File.Configuration;

/// <summary>
/// File entity configuration.
/// </summary>
public class FileConfiguration : IEntityTypeConfiguration<Domain.Files.Entity.File>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Domain.Files.Entity.File> builder)
    {
        builder
            .ToTable("Files")
            .HasKey(file => file.Id);

        builder
            .Property(file => file.CreatedAt)
            .HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));

        builder
            .Property(file => file.UpdatedAt)
            .HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));

        builder
            .Property(file => file.Name)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(file => file.ContentType)
            .IsRequired()
            .HasMaxLength(255);
    }
}
