using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YellowMark.DataAccess.Currency.Configuration;

/// <summary>
/// Currencies table configuration.
/// </summary>
public class CurrencyConfiguration : IEntityTypeConfiguration<Domain.Currencies.Entity.Currency>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Domain.Currencies.Entity.Currency> builder)
    {
        builder
            .ToTable("Currencies")
            .HasKey(c => c.Id);

        builder
            .Property(c => c.AlphabeticCode)
            .IsRequired()
            .HasMaxLength(10);

        builder
            .Property(c => c.NumericCode)
            .IsRequired();

        builder
            .HasMany(c => c.Ads)
            .WithOne(c => c.Currency)
            .HasForeignKey(c => c.CurrencyId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
    }
}
