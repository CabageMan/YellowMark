using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YellowMark.DataAccess.Ad.Configuration;

/// <summary>
/// Ad entity configuration.
/// </summary>
public class AdConfiguration : IEntityTypeConfiguration<Domain.Ads.Entity.Ad>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Domain.Ads.Entity.Ad> builder)
    {
        builder
            .ToTable("Ads")
            .HasKey(ad => ad.Id);

        builder
            .Property(ad => ad.CreatedAt)
            .HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));

        builder
            .Property(ad => ad.UpdatedAt)
            .HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));

        builder
            .Property(ad => ad.Title)
            .IsRequired()
            .HasMaxLength(255);

        builder
            .Property(ad => ad.Description)
            .IsRequired()
            .HasMaxLength(2048);

        builder
            .Property(ad => ad.Price)
            .IsRequired(false);

        builder
            .Property(ad => ad.CurrencyId)
            .IsRequired(false);

        builder
            .HasMany(ad => ad.Files)
            .WithOne(file => file.Ad)
            .HasForeignKey(file => file.AdId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(ad => ad.Comments)
            .WithOne(comment => comment.Ad)
            .HasForeignKey(comment => comment.AdId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
