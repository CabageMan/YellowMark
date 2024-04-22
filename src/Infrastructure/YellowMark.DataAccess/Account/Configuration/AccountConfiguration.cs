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

        // builder
        //     .Property(acc => acc.CreatedAt)
        //     .HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));

        // builder
        //     .Property(acc => acc.UpdatedAt)
        //     .HasConversion(s => s, s => DateTime.SpecifyKind(s, DateTimeKind.Utc));

        // builder
        //     .Property(acc => acc.Email)
        //     .IsRequired()
        //     .HasMaxLength(100);

        // builder
        //     .Property(acc => acc.PhoneNumber)
        //     .IsRequired()
        //     .HasMaxLength(15);

        // builder
        //     .Property(acc => acc.Role)
        //     .IsRequired()
        //     .HasMaxLength(100);

        builder
            .HasOne(acc => acc.UserInfo)
            .WithOne(user => user.Account)
            .HasForeignKey<Domain.UsersInfos.Entity.UserInfo>(user => user.AccountId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
