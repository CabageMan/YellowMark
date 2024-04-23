using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace YellowMark.DataAccess.Account.Configuration;

/// <summary>
/// Account entity configuration.
/// </summary>
public class AccountConfiguration : IEntityTypeConfiguration<Domain.Accounts.Entity.Account>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<Domain.Accounts.Entity.Account> builder)
    {
        builder
            .ToTable("Accounts")
            .HasKey(acc => acc.Id);
    }
}
